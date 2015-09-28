using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShazamO
{
    public partial class MainForm : Form
    {
        public int ResampleFreqRate { get; set; }

        public MainForm()
        {
            InitializeComponent();
        }

        void StatusSet(string Text)
        {
            Action act = new Action(() => labelStatus.Text = Text);
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        #region Progress bars

        void ProgressMainIni(string Status, int Max)
        {
            Action act = new Action(() =>
            {
                toolStripProgressMain.Visible = true;
                toolStripProgressMain.Value = 0;
                toolStripProgressMain.Maximum = Max;
                labelProgressMain.Text = Status;
            });
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        void ProgressMainSet(int Progress)
        {
            Action act = new Action(() =>
            {
                toolStripProgressMain.Value = Progress;
            });
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        void ProgressMainHide()
        {
            Action act = new Action(() => 
            {
                toolStripProgressMain.Visible = false;
                toolStripProgressMain.Value = 0;
                labelProgressMain.Text = "";
            });
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        void ProgressSubIni(string Status, int Max)
        {
            Action act = new Action(() =>
            {
                toolStripProgressSub.Visible = true;
                toolStripProgressSub.Value = 0;
                toolStripProgressSub.Maximum = Max;
                labelProgressSub.Text = Status;
            });
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        void ProgressSubSet(int Progress)
        {
            Action act = new Action(() =>
            {
                toolStripProgressSub.Value = Progress;
            });
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        void ProgressSubHide()
        {
            Action act = new Action(() =>
            {
                toolStripProgressSub.Visible = false;
                toolStripProgressSub.Value = 0;
                labelProgressSub.Text = "";
            });
            if (statusStrip1.InvokeRequired) statusStrip1.Invoke(act);
            else act();
        }

        #endregion

        private void fromwavToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void frommp3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Parallel Mp3 to sign
            if (openFileMp3Transform.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Signature.Parallel = true;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                int error = 0, i = 0, n = openFileMp3Transform.FileNames.Length;
                Log("Convert " + n.ToString() + " files; ");
                Log("");

                foreach (string FileName in openFileMp3Transform.FileNames)
                {
                    i++;
                    try
                    {
                        var onSono = new Signature.OnSonoBuildHandler((desc, progress) =>
                        {
                            if (progress > 0) LogInline(String.Format("{0}: {1:0.0}%", desc, progress));
                            else LogInline(desc);
                            return true;
                        });
                        Signature sign = await Signature.GenerateFromMp3(FileName,
                            BuildingRung, WindowFunction.HannWindow, onSono, ResampleFreqRate);
                        sign.Save(FileName + ".sgn");
                        LogInline(String.Format("[{0}/{1}]: Transform \"{2}.sgn\", {3} lines {4}",
                                             i, n, FileName, sign.Data.Length, Environment.NewLine));
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
        }

        private void showLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showLogToolStripMenuItem.Checked = !showLogToolStripMenuItem.Checked;
            panel1.Visible = showLogToolStripMenuItem.Checked;
        }
    }
}
