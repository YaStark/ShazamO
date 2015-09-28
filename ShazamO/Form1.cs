using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShazamO.ParseToJSON;


namespace ShazamO
{
    public partial class Form1 : Form
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        #region Options

        public float NSigm { get { return Options.Get.Comparing_NSigm; } }
        
        public int BuildingRang { get { return Options.Get.Signature_SamplesPerSecond; } }

        public int ConvertRate { get { return Options.Get.Converter_PreferredSampleRate; } }

        public string ConvertFromPath {
            get
            {
                return Options.Get.ConvertSourceDirectory;
            }
            set
            {
                Options.Get.ConvertSourceDirectory = value;
                Options.Save();
            }
        }

        public string ConvertToPath
        {
            get
            {
                return Options.Get.ConvertTargetDirectory;
            }
            set
            {
                Options.Get.ConvertTargetDirectory = value;
                Options.Save();
            }
        }

        public bool ConvertSaveNear
        {
            get { return Options.Get.ConvertSaveNearWithSources; }
            set
            {
                Options.Get.ConvertSaveNearWithSources = value; 
                Options.Save();
                convertTargetSourceFiles.Checked = value;
                convertTargetSelectDirectory.Checked = !value;
            }
        }

        #endregion

        Signature[] mainSigns = null;
        Dictionary<string, Signature> dictSigns = new Dictionary<string, Signature>();
        LogElement[] logElements = null;
        object logLock = new object();

        OptionsForm optForm = new OptionsForm();

        public Form1()
        {
            InitializeComponent();
            try { Options.Load(); }
            catch
            {
                MessageBox.Show("Ошибка при загрузке файла конфигурации settings.ini. " +
                        Environment.NewLine + "Будут применены настройки по умолчанию");
                Options.Get = new Options(Options.Default);
            }
            LoadOptions();
        }

        public void LoadOptions()
        {
            InfluenceArea.BorderDeviation = Options.Get.InfluenceArea_BorderDeviation;
            ParserToJSON.RequiredFields = Options.Get.LogFile_RequiredFields;
            LogElement.IgnoredTags = Options.Get.LogElement_IgnoredTags;
            ConvertSaveNear = ConvertSaveNear;
        }

        #region Logging

        void Log(string Text)
        {
            string now = "[" + DateTime.Now.ToShortTimeString() + "]: ";
            Text = now + Text.Replace(Environment.NewLine, Environment.NewLine + now) + Environment.NewLine;

            Action act = new Action(() =>
            {
                textLog.AppendText(Text);
            });
            if (textLog.InvokeRequired) textLog.Invoke(act);
            else act();
            Console.WriteLine(Text);
        }

        void Result(string Text)
        {
            Text = Environment.NewLine + Text;
            Action act = new Action(() => textResult.AppendText(Text));
            if (textResult.InvokeRequired) textResult.Invoke(act);
            else act();
            Console.WriteLine(Text);
        }

        void LogInline(string Text)
        {
            string now = "[" + DateTime.Now.ToShortTimeString() + "]: ";
            Action act = new Action(() =>
                {
                    int last = textLog.Text.LastIndexOf(Environment.NewLine) + Environment.NewLine.Length;
                    if (last >= 0 && last < textLog.Text.Length)
                    {
                        textLog.Text = textLog.Text.Remove(last);
                        textLog.AppendText(now + Text);
                    }
                    else textLog.AppendText(now + Text);
                });
            if (textLog.InvokeRequired) textLog.Invoke(act);
            else act();
            Console.Write("\r" + now + Text + "\t\t\t");
        }

        #endregion

        void AddSignItem(Signature sign, int n)
        {
            Action act = new Action(() =>
            {
                labelSearchSignsCount.Text = n.ToString();
                listSearchSigns.Items.Add(sign);
            });
            if (labelSearchSignsCount.InvokeRequired) labelSearchSignsCount.Invoke(act);
            else act();
        }

        void AddMainItem(Signature sign, int n)
        {
            Action act = new Action(() =>
            {
                labelSearchMainCount.Text = n.ToString();
                listSearchMain.Items.Add(sign);
            });
            if (labelSearchMainCount.InvokeRequired) labelSearchMainCount.Invoke(act);
            else act();
        }

        /// <summary>
        /// Select signs
        /// </summary>
        private async void button2_Click(object sender, EventArgs e)
        {
            if (openFileSign.ShowDialog() != DialogResult.OK) return;
            
            int n = openFileSign.FileNames.Length;
            List<Signature> _signs = new List<Signature>();
            Log("Sample signs: " + n);
            listSearchSigns.Items.Clear();

            string[] fileNames = openFileSign.FileNames;
            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        _signs.Add(Signature.LoadFromFile(fileNames[i]));
                        LogInline(String.Format("[{0}/{1}]", i + 1, n));
                        AddSignItem(_signs[_signs.Count - 1], _signs.Count);
                    }
                    catch (Exception ex)
                    {
                        LogInline(String.Format("[{0}/{1}] Error while reading {2}: {3}{4}",
                                                i + 1, n, fileNames[i], ex.Message,
                                                Environment.NewLine));
                    }
                }
            });

            if (_signs.Count < 1)
            {
                Log(String.Format("Loaded 0 signs, {0} errors", n));
                return;
            }
            dictSigns = _signs.Distinct(new Signature.ComparerByName())
                                .ToDictionary(item => item.ToString());
            LogInline(Environment.NewLine);
            Log(String.Format("Loaded {0} unique signs, {1} errors, {2} repeating", 
                            dictSigns.Count, n - _signs.Count, _signs.Count - dictSigns.Count));
        }

        /// <summary>
        /// Select main sign
        /// </summary>
        private async void button3_Click_1(object sender, EventArgs e)
        {
            if (openFileSign.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            
            int n = openFileSign.FileNames.Length;
            if (n == 0) return;
            List<Signature> _signs = new List<Signature>();
            Log("Main signs: " + n); 
            listSearchMain.Items.Clear();
            string[] fileNames = openFileSign.FileNames;

            await Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        _signs.Add(Signature.LoadFromFile(fileNames[i]));
                        LogInline(String.Format("[{0}/{1}]", i + 1, n));
                        AddMainItem(_signs[_signs.Count - 1], _signs.Count);
                    }
                    catch (Exception ex)
                    {
                        LogInline(String.Format("[{0}/{1}] Error while reading {2}: {3}{4}",
                                                i + 1, n, fileNames[i], ex.Message,
                                                Environment.NewLine));
                    }
                }
            });

            if (_signs.Count < 1)
            {
                Log(String.Format("Loaded 0 signs, {0} errors", n));
                return;
            }

            LogInline(Environment.NewLine);
            mainSigns = _signs.ToArray();
            Log(String.Format("Loaded {0} signs, {1} errors.", _signs.Count, n - _signs.Count));
        }

        /// <summary>
        /// Set new sign info on dataSignInfo
        /// </summary>
        private void ShowSignInfo(Signature sign)
        {
            dataSignInfo.Rows.Clear();
            dataSignInfo.Rows.Add("Файл", sign.ToString());
            dataSignInfo.Rows.Add("Сигнатур в секунду", sign.FreqRate);
            dataSignInfo.Rows.Add("Частота источника", sign.BaseFileInfo.SampleRate);
            dataSignInfo.Rows.Add("Оконная функция", sign.WindowFunction.ToString());
            dataSignInfo.Rows.Add("Длительность", sign.ToTimeSpan().ToString());
        }
        
        private async Task ConvertFilesToSigns(string[] FileNames)
        {
            Signature.Parallel = true;
            WindowFunction WindowFunction = WindowFunction.HannWindow;

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int error = 0, i = 0, n = FileNames.Length;
            Log("Convert " + n.ToString() + " files; " + Environment.NewLine);

            var onSono = new Signature.OnSonoBuildHandler((desc, progress) =>
            {
                if (progress > 0) LogInline(String.Format("{0}: {1:0.0}%", desc, progress));
                else LogInline(desc);
                return true;
            });

            if (ConvertToPath == null)
            {
                if (MessageBox.Show("Располагать результат рядом с исходными файлами?", "",
                    MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    convertTargetSelectDirectory_Click(null, null);
                }
                else convertTargetSourceFiles_Click(null, null);
            }
            bool dir = convertTargetSelectDirectory.Checked;

            foreach (string FileName in FileNames)
            {
                i++;
                try
                {
                    Signature sign = null;
                    string ext = FileParser.GetExtension(FileName);
                    if (ext == ".mp3")
                        sign = await Signature.GenerateFromMp3(
                                FileName, BuildingRang, WindowFunction, onSono, ConvertRate);
                    else if (ext == ".wav") sign = await Signature.GenerateFromWav(
                            FileName, BuildingRang, WindowFunction, onSono, ConvertRate);
                    else throw new Exception("Wrong file format: " + ext);

                    if (sign != null)
                    {
                        if (dir) sign.Save(ConvertToPath + "\\" + sign.ToString() + ".sgn");
                        else sign.Save(sign.BaseFilePath + ".sgn");

                        LogInline(String.Format("[{0}/{1}]: Transform \"{2}.sgn\", {3} lines {4}",
                                             i, n, FileName, sign.Data.Length, Environment.NewLine));
                    }
                }
                catch (Exception ex)
                {
                    LogInline(String.Format("[{0}/{1}]: Error transforming \"{2}\":{3} {4}",
                                         i, n, FileName, ex.Message, Environment.NewLine));
                    error++;
                }
            }
            sw.Stop();
            Log(String.Format("Elapsed: {0}. Success {1}, errors {2}.", sw.Elapsed, i - error, error));
        }

        /// <summary>
        /// Compare
        /// </summary>
        private async void button6_Click(object sender, EventArgs e)
        {
            if (!CheckInputData()) return;

                // Load signs list
            Signature[] _signs = null;
            if (listBoxByFileName.Items.Count == 0) _signs = dictSigns.Values.ToArray();
            else
            {
                List<Signature> __signs = new List<Signature>();
                foreach (var item in listBoxByFileName.Items) __signs.Add(item as Signature);
                _signs = __signs.ToArray();
            }

            for (int i = 0; i < mainSigns.Length; i++)
            {
                Congruence[] cong = await CompareParallel(mainSigns[i], _signs);
                
                InfluenceArea area = new InfluenceArea(mainSigns[i].Data.Length);
                Result("File \"" + mainSigns[i].ToString() + "\"" + Environment.NewLine);
                
                if(cong != null) foreach (var item in cong)
                {
                    area.AddInfluence(item.SignLink, item.Begin, item.Length, item.SignSigma);
                }
                foreach (var item in area)
                {
                    Result(String.Format("{0} :: {1} - {2}",
                                    TimeSpan.FromSeconds(item.Key.Begin / mainSigns[i].FreqRate),
                                    TimeSpan.FromSeconds(item.Key.End / mainSigns[i].FreqRate),
                                    item.Value.ToString()));
                }
            }
        }

        /// <summary>
        /// Comparing 
        /// </summary>
        async Task<Congruence[]> CompareParallel(Signature mainSign, Signature[] signs)
        {
            if (mainSigns == null || signs == null || signs.Length == 0) return null;
            IEnumerable<Congruence> ret = null;
            object retLock = new object();

            Log(String.Format("Parallel compare. Delta is Sigma * {0:0.00}", NSigm));
            Recognition.Initialize();

            Stopwatch sw = new Stopwatch();
            sw.Start();
            int k = 1, n = signs.Length;
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(0, signs.Length, delegate(int i)
                {
                    if(signs[i].FreqRate != mainSign.FreqRate)
                    {
                        Log(String.Format("Error: \"{0}\" is uncomparable with \"{1}\"", 
                            signs[i].ToString(), mainSign.ToString()));
                        return;
                    }
                    else if (signs[i].BaseFileInfo.SampleRate != mainSign.BaseFileInfo.SampleRate)
                    {
                        Log("Warning: basic rates is not equal:");
                        Log(String.Format("{0} [{1} Hz] : {2} [{3} Hz]",
                            mainSign.ToString(), mainSign.BaseFileInfo.SampleRate, 
                            signs[i].ToString(), signs[i].BaseFileInfo.SampleRate));
                    }
                    
                        // Сравнение
                    Congruence[] cong = mainSign.CompareTo(signs[i], NSigm);
                    
                    #region Вывод
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("[{0}/{1}]:\t{2} {3}", k++, n, signs[i].ToString(), Environment.NewLine);                    
                    if (cong == null)
                    {
                        sb.Append("\t\t[NULL]");
                        lock (logLock) Log(sb.ToString());
                        return;
                    }
                    for (int j = 0; j < cong.Length - 1; j++)
                    {
                        sb.AppendFormat("\t\t{0:T}; sigms: {1:0.00} {2}",
                                    mainSign.ToTimeSpan(cong[j].Coord), cong[j].SignSigma, Environment.NewLine);
                    }
                    sb.AppendFormat("\t\t{0:T}; sigms: {1:0.00}",
                                mainSign.ToTimeSpan(cong[cong.Length - 1].Coord), cong[cong.Length - 1].SignSigma);
                    lock (logLock) Log(sb.ToString());
                    #endregion

                    lock (retLock)
                    {
                        if (ret == null) ret = cong.AsEnumerable();
                        else ret = ret.Concat(cong);
                    }
                });
            });
            sw.Stop();
            Log("Elapsed: " + sw.Elapsed + Environment.NewLine + "Compare complete.");
            return ret == null ? null : ret.ToArray();
        }

        /// <summary>
        /// Check mainSign and dictSigns if exist, else log error info
        /// </summary>
        private bool CheckInputData()
        {
            if (mainSigns == null || mainSigns.Length == 0)
            {
                Log("Please select correct main sign.");
                return false;
            }
            else if (dictSigns.Count == 0)
            {
                Log("Please select correct signs.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Load log file
        /// </summary>
        private void buttonLogOpen_Click(object sender, EventArgs e)
        {
            if (openFileLog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string json = ParserToJSON.Parse(openFileLog.FileName);
                    logElements = LogElement.DeserializeJSON(json);
                }
                catch (Exception ex)
                {
                    Log("Cannot load log file " + openFileLog.FileName + ": " + ex.Message);
                    return;
                }

                Log("Load log file: " + openFileLog.FileName);
                textSearchLogList.Text = FileParser.GetFileName(openFileLog.FileName);
                Log(String.Format("Loaded {0} elements (Unique {1})",
                            logElements.Length, logElements.Distinct(new LogElement.Comparer()).Count()));
            }
        }

        private bool TrySignNameAsDateTime(string Name, out DateTime TimeSpan)
        {
            string fileNameAsTime = Name.Replace("-", ":");
            return DateTime.TryParse(fileNameAsTime, out TimeSpan);
        }

        /// <summary>
        /// Search
        /// </summary>
        private async void buttonSearch_Click_1(object sender, EventArgs e)
        {
            if (!CheckInputData()) return;
            if (logElements == null)
            {
                Log("Please select correct log file.");
                return;
            }
            
            foreach (var mainSign in mainSigns)
            {
                    // Время начала берется из названия файла, а длительность - длительность файла
                DateTime timeBegin;
                if (!TrySignNameAsDateTime(mainSign.ToString(), out timeBegin))
                {
                    Log("Error: Cannot recognize time stamp in filename " + mainSign.ToString());
                    continue;
                }
                TimeInterval mainInterval = new TimeInterval(timeBegin, mainSign.ToTimeSpan());
                Log(String.Format("Search on {0} ({1}):", mainSign.ToString(), mainSign.ToTimeSpan()));

                    // Перебираем все записи из этого интервала                    
                List<LogElement> _elems = logElements.Where((logElement) =>
                    {
                        if (mainInterval.Within(new TimeInterval(logElement.Begin, logElement.Length)))
                        {
                            Log(logElement.ToString());
                            return true;
                        }
                        return false;
                    }).ToList();
                Log(String.Format("{0} elements on time interval {1}", _elems.Count, mainInterval.ToString()));
                if (_elems.Count < 1) continue;

                    // Сопоставляем каждой записи сигнатуру
                List<Signature> _signs = new List<Signature>();
                List<LogElement> _notFoundElems = new List<LogElement>();
                foreach (var elem in _elems)
                {
                    try { _signs.Add(dictSigns[elem.ElemName]); }
                    catch (KeyNotFoundException) { _notFoundElems.Add(elem); }
                }
                    // Выводим информацию об этом
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Got {0}/{1} files.", _signs.Count, _elems.Count);
                if (_notFoundElems.Count > 0)
                {
                    sb.Append(" Not found: " + Environment.NewLine);
                    foreach (var elem in _notFoundElems) sb.AppendLine(elem.ToString());
                }
                else sb.AppendLine();
                Log(sb.ToString());

                    // Сравниваем
                Congruence[] _congs = await CompareParallel(mainSign, _signs.Distinct().ToArray());

                    // Обрабатываем результаты сравнения
                float[] dtime = new float[_elems.Count];
                for (int i = 0; i < _elems.Count; i++) dtime[i] = float.NaN;
                if (_congs != null)
                {
                    for (int i = 0; i < _elems.Count; i++)
                    {
                        foreach (var cong in _congs)
                        {
                            if (cong.SignLink.ToString() == _elems[i].ElemName)
                            {
                                TimeSpan _timeBegin = timeBegin.Add(mainSign.ToTimeSpan(cong.Begin)).TimeOfDay;
                                float delta = (float)_elems[i].Begin.TimeOfDay.Subtract(_timeBegin).TotalSeconds;
                                if (float.IsNaN(dtime[i]) || (Math.Abs(delta) < Math.Abs(dtime[i]))) 
                                    dtime[i] = delta;
                            }
                        }
                    }
                }

                    // Выводим результаты
                sb = new StringBuilder();
                sb.AppendLine("File " + mainSign.ToString() + ":");
                for (int i = 0; i < _elems.Count; i++)
                {
                    sb.AppendFormat("[{0:T}]: ", _elems[i].Begin);
                    if (float.IsNaN(dtime[i])) sb.Append("Failed; ");
                    else sb.AppendFormat("Success (dev. {0}); ", TimeSpan.FromSeconds(Math.Abs(dtime[i])));
                    sb.AppendLine(_elems[i].ElemName);
                }
                Result(sb.ToString());
                Log(sb.ToString());
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            optForm.Initialize();
            if (optForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Options.Save();
                LoadOptions();
            }
        }

        private void listSearchSigns_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { ShowSignInfo((sender as ListBox).SelectedItem as Signature); }
            catch { }
        }

        /// <summary>
        /// Clear log
        /// </summary>
        private void button3_Click_2(object sender, EventArgs e)
        {
            textLog.Text = "";
        }

        /// <summary>
        /// Resize tabControl2
        /// </summary>
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int borderHeight = 29;
            int clientHeight = tabControl2.SelectedTab.GetPreferredSize(new Size()).Height;
            tabControl2.Height = clientHeight + borderHeight;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string cmp = textBoxSearch.Text;
            listBoxMiddleSearch.Items.Clear();
            if (cmp != null)
            {
                Signature[] _signs = dictSigns.Where(item => item.Key.Contains(cmp))
                                              .Select(item => item.Value)
                                              .ToArray();
                listBoxMiddleSearch.Items.AddRange(_signs);
            }
        }

        /// <summary>
        /// Add signs to search list
        /// </summary>
        private void button6_Click_1(object sender, EventArgs e)
        {
            foreach (var itemObject in listBoxMiddleSearch.SelectedItems)
            {
                if (!listBoxByFileName.Items.Contains(itemObject))
                {
                    listBoxByFileName.Items.Add(itemObject);
                }
            }
        }

        /// <summary>
        /// Remove signs from search list
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            List<Object> _objs = new List<Object>();
            List<Object> _selected = new List<object>();
            foreach (var item in listBoxByFileName.SelectedItems) _selected.Add(item);
            foreach (var item in _selected)
            {
                listBoxByFileName.Items.Remove(item);
            }
        }

        /// <summary>
        /// Clear results
        /// </summary>
        private void очиститьРезультатыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textResult.Text = "";
        }

        #region Converting views

        /// <summary>
        /// Set converting target as near source files
        /// </summary>
        private void convertTargetSourceFiles_Click(object sender, EventArgs e)
        {
            ConvertSaveNear = true;
            Log("Set converting target location as near source files");
        }

        /// <summary>
        /// Select converting' target directory
        /// </summary>
        private void convertTargetSelectDirectory_Click(object sender, EventArgs e)
        {
            if(ConvertToPath != null) folderBrowser.SelectedPath = ConvertToPath;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                ConvertToPath = folderBrowser.SelectedPath;
                ConvertSaveNear = false;
                Log("Set converting target location to '" + ConvertToPath + "'");
            }
            else convertTargetSourceFiles_Click(null, null);
        }

        /// <summary>
        /// Convert from directory
        /// </summary>
        private async void convertSourceDirectory_Click(object sender, EventArgs e)
        {
            if (ConvertFromPath != null) folderBrowser.SelectedPath = ConvertFromPath;
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                ConvertFromPath = folderBrowser.SelectedPath;
                Log("Begin search " + ConvertFromPath + " directory.");
                var fileNames = await Task.Factory.StartNew<IEnumerable<string>>(() =>
                    GetFileNames(new DirectoryInfo(ConvertFromPath)));

                Log("Selected " + fileNames.Count().ToString() + " file(s)");

                await ConvertFilesToSigns(fileNames.ToArray());
            }
        }

        /// <summary>
        /// Recurrent search files by search pattern
        /// </summary>
        public IEnumerable<string> GetFileNames(DirectoryInfo CurDir)
        {
            var fileNames = CurDir.EnumerateFiles("*.mp3").Select(o => o.FullName)
                    .Concat(CurDir.EnumerateFiles("*.wav").Select(o => o.FullName)).ToArray();

            foreach (var dir in CurDir.EnumerateDirectories())
                fileNames = fileNames.Concat(GetFileNames(dir)).ToArray();
            return fileNames;
        }

        /// <summary>
        /// Convert from files list
        /// </summary>
        private async void convertSourceFiles_Click(object sender, EventArgs e)
        {
            if (openFileWavMp3.ShowDialog() == DialogResult.OK)
            {
                await ConvertFilesToSigns(openFileWavMp3.FileNames);
            }
        }
        #endregion

    }
}
