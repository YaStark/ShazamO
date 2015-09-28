namespace ShazamO
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataSignInfo = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listSearchSigns = new System.Windows.Forms.ListBox();
            this.buttonSearchOpenSigns = new System.Windows.Forms.Button();
            this.labelSearchSignsCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.listSearchMain = new System.Windows.Forms.ListBox();
            this.buttonSearchOpenMain = new System.Windows.Forms.Button();
            this.labelSearchMainCount = new System.Windows.Forms.Label();
            this.textSearchLogList = new System.Windows.Forms.TextBox();
            this.buttonLogOpen = new System.Windows.Forms.Button();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.openFileSign = new System.Windows.Forms.OpenFileDialog();
            this.openFileWavMp3 = new System.Windows.Forms.OpenFileDialog();
            this.openFileLog = new System.Windows.Forms.OpenFileDialog();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.listBoxByFileName = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.button7 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.listBoxMiddleSearch = new System.Windows.Forms.ListBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.очиститьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.очиститьРезультатыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textResult = new System.Windows.Forms.TextBox();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.folderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.convertTargetSelectDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.convertSourceDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.convertTargetSourceFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.convertSourceFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataSignInfo)).BeginInit();
            this.panel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataSignInfo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(245, 421);
            this.panel1.TabIndex = 5;
            // 
            // dataSignInfo
            // 
            this.dataSignInfo.AllowUserToAddRows = false;
            this.dataSignInfo.AllowUserToDeleteRows = false;
            this.dataSignInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataSignInfo.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataSignInfo.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataSignInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataSignInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataSignInfo.ColumnHeadersVisible = false;
            this.dataSignInfo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dataSignInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataSignInfo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataSignInfo.GridColor = System.Drawing.SystemColors.Control;
            this.dataSignInfo.Location = new System.Drawing.Point(0, 302);
            this.dataSignInfo.Name = "dataSignInfo";
            this.dataSignInfo.ReadOnly = true;
            this.dataSignInfo.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataSignInfo.RowHeadersVisible = false;
            this.dataSignInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataSignInfo.Size = new System.Drawing.Size(245, 119);
            this.dataSignInfo.TabIndex = 47;
            // 
            // Column1
            // 
            this.Column1.Frozen = true;
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 5;
            // 
            // Column2
            // 
            this.Column2.Frozen = true;
            this.Column2.HeaderText = "Column2";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 5;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 281);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(245, 21);
            this.label1.TabIndex = 46;
            this.label1.Text = "Информация";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(245, 281);
            this.panel3.TabIndex = 45;
            // 
            // label3
            // 
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(245, 21);
            this.label3.TabIndex = 47;
            this.label3.Text = "Сигнатуры";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.listSearchSigns);
            this.groupBox2.Controls.Add(this.buttonSearchOpenSigns);
            this.groupBox2.Controls.Add(this.labelSearchSignsCount);
            this.groupBox2.Location = new System.Drawing.Point(3, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(236, 122);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ролики";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(6, 95);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 21);
            this.label6.TabIndex = 42;
            this.label6.Text = "Открыто:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listSearchSigns
            // 
            this.listSearchSigns.FormattingEnabled = true;
            this.listSearchSigns.Location = new System.Drawing.Point(6, 19);
            this.listSearchSigns.Name = "listSearchSigns";
            this.listSearchSigns.ScrollAlwaysVisible = true;
            this.listSearchSigns.Size = new System.Drawing.Size(224, 69);
            this.listSearchSigns.TabIndex = 39;
            this.listSearchSigns.SelectedIndexChanged += new System.EventHandler(this.listSearchSigns_SelectedIndexChanged);
            // 
            // buttonSearchOpenSigns
            // 
            this.buttonSearchOpenSigns.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSearchOpenSigns.Location = new System.Drawing.Point(166, 94);
            this.buttonSearchOpenSigns.Name = "buttonSearchOpenSigns";
            this.buttonSearchOpenSigns.Size = new System.Drawing.Size(64, 23);
            this.buttonSearchOpenSigns.TabIndex = 26;
            this.buttonSearchOpenSigns.Text = "Открыть";
            this.buttonSearchOpenSigns.UseVisualStyleBackColor = true;
            this.buttonSearchOpenSigns.Click += new System.EventHandler(this.button2_Click);
            // 
            // labelSearchSignsCount
            // 
            this.labelSearchSignsCount.Location = new System.Drawing.Point(107, 95);
            this.labelSearchSignsCount.Name = "labelSearchSignsCount";
            this.labelSearchSignsCount.Size = new System.Drawing.Size(35, 21);
            this.labelSearchSignsCount.TabIndex = 33;
            this.labelSearchSignsCount.Text = "0";
            this.labelSearchSignsCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.listSearchMain);
            this.groupBox1.Controls.Add(this.buttonSearchOpenMain);
            this.groupBox1.Controls.Add(this.labelSearchMainCount);
            this.groupBox1.Location = new System.Drawing.Point(4, 22);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(236, 125);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Эфиры";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 21);
            this.label2.TabIndex = 41;
            this.label2.Text = "Открыто:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // listSearchMain
            // 
            this.listSearchMain.FormattingEnabled = true;
            this.listSearchMain.Location = new System.Drawing.Point(4, 19);
            this.listSearchMain.Name = "listSearchMain";
            this.listSearchMain.ScrollAlwaysVisible = true;
            this.listSearchMain.Size = new System.Drawing.Size(226, 69);
            this.listSearchMain.TabIndex = 38;
            this.listSearchMain.SelectedIndexChanged += new System.EventHandler(this.listSearchSigns_SelectedIndexChanged);
            // 
            // buttonSearchOpenMain
            // 
            this.buttonSearchOpenMain.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.buttonSearchOpenMain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSearchOpenMain.Location = new System.Drawing.Point(166, 93);
            this.buttonSearchOpenMain.Name = "buttonSearchOpenMain";
            this.buttonSearchOpenMain.Size = new System.Drawing.Size(64, 23);
            this.buttonSearchOpenMain.TabIndex = 27;
            this.buttonSearchOpenMain.Text = "Открыть";
            this.buttonSearchOpenMain.UseVisualStyleBackColor = true;
            this.buttonSearchOpenMain.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // labelSearchMainCount
            // 
            this.labelSearchMainCount.Location = new System.Drawing.Point(107, 94);
            this.labelSearchMainCount.Name = "labelSearchMainCount";
            this.labelSearchMainCount.Size = new System.Drawing.Size(35, 21);
            this.labelSearchMainCount.TabIndex = 32;
            this.labelSearchMainCount.Text = "0";
            this.labelSearchMainCount.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textSearchLogList
            // 
            this.textSearchLogList.Location = new System.Drawing.Point(4, 20);
            this.textSearchLogList.Multiline = true;
            this.textSearchLogList.Name = "textSearchLogList";
            this.textSearchLogList.Size = new System.Drawing.Size(186, 20);
            this.textSearchLogList.TabIndex = 35;
            // 
            // buttonLogOpen
            // 
            this.buttonLogOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonLogOpen.Location = new System.Drawing.Point(196, 19);
            this.buttonLogOpen.Name = "buttonLogOpen";
            this.buttonLogOpen.Size = new System.Drawing.Size(63, 20);
            this.buttonLogOpen.TabIndex = 34;
            this.buttonLogOpen.Text = "Открыть";
            this.buttonLogOpen.UseVisualStyleBackColor = true;
            this.buttonLogOpen.Click += new System.EventHandler(this.buttonLogOpen_Click);
            // 
            // buttonSearch
            // 
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonSearch.Location = new System.Drawing.Point(7, 53);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(141, 28);
            this.buttonSearch.TabIndex = 37;
            this.buttonSearch.Text = "Поиск";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click_1);
            // 
            // buttonCompare
            // 
            this.buttonCompare.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCompare.Location = new System.Drawing.Point(6, 165);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(141, 29);
            this.buttonCompare.TabIndex = 19;
            this.buttonCompare.Text = "Поиск";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.button6_Click);
            // 
            // openFileSign
            // 
            this.openFileSign.Filter = "Signature file|*.sgn";
            this.openFileSign.Multiselect = true;
            // 
            // openFileWavMp3
            // 
            this.openFileWavMp3.Filter = "Wav and MP3 files|*.mp3;*.wav";
            this.openFileWavMp3.Multiselect = true;
            // 
            // openFileLog
            // 
            this.openFileLog.FileName = "openFileDialog2";
            this.openFileLog.Filter = "Log or txt files|*.log;*.txt";
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.Controls.Add(this.tabControl2);
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(245, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(542, 138);
            this.panel2.TabIndex = 10;
            // 
            // tabControl2
            // 
            this.tabControl2.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl2.Controls.Add(this.tabPage3);
            this.tabControl2.Controls.Add(this.tabPage4);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl2.HotTrack = true;
            this.tabControl2.Location = new System.Drawing.Point(0, 25);
            this.tabControl2.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(542, 113);
            this.tabControl2.TabIndex = 39;
            this.tabControl2.SelectedIndexChanged += new System.EventHandler(this.tabControl2_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Controls.Add(this.buttonSearch);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(534, 84);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "По лог-файлу";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.buttonLogOpen);
            this.groupBox3.Controls.Add(this.textSearchLogList);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(528, 44);
            this.groupBox3.TabIndex = 48;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Выберите файл лога";
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox5);
            this.tabPage4.Controls.Add(this.buttonCompare);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(534, 84);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "По имени файла";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.tableLayoutPanel1);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox5.Location = new System.Drawing.Point(3, 3);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(528, 156);
            this.groupBox5.TabIndex = 23;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Введите имя файла";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 137F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(522, 137);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.listBoxByFileName);
            this.panel6.Controls.Add(this.label4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(286, 0);
            this.panel6.Margin = new System.Windows.Forms.Padding(0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(236, 137);
            this.panel6.TabIndex = 2;
            // 
            // listBoxByFileName
            // 
            this.listBoxByFileName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxByFileName.FormattingEnabled = true;
            this.listBoxByFileName.Location = new System.Drawing.Point(0, 20);
            this.listBoxByFileName.Name = "listBoxByFileName";
            this.listBoxByFileName.ScrollAlwaysVisible = true;
            this.listBoxByFileName.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxByFileName.Size = new System.Drawing.Size(236, 117);
            this.listBoxByFileName.TabIndex = 49;
            this.listBoxByFileName.DoubleClick += new System.EventHandler(this.button7_Click);
            // 
            // label4
            // 
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(236, 20);
            this.label4.TabIndex = 48;
            this.label4.Text = "Объекты поиска:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.button7);
            this.panel5.Controls.Add(this.button6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(236, 0);
            this.panel5.Margin = new System.Windows.Forms.Padding(0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(50, 137);
            this.panel5.TabIndex = 1;
            // 
            // button7
            // 
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.Location = new System.Drawing.Point(3, 83);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(44, 28);
            this.button7.TabIndex = 5;
            this.button7.Text = "<=";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.Location = new System.Drawing.Point(3, 49);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(44, 28);
            this.button6.TabIndex = 4;
            this.button6.Text = "=>";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.listBoxMiddleSearch);
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.textBoxSearch);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(236, 137);
            this.panel4.TabIndex = 0;
            // 
            // listBoxMiddleSearch
            // 
            this.listBoxMiddleSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxMiddleSearch.FormattingEnabled = true;
            this.listBoxMiddleSearch.Location = new System.Drawing.Point(0, 30);
            this.listBoxMiddleSearch.Margin = new System.Windows.Forms.Padding(8);
            this.listBoxMiddleSearch.Name = "listBoxMiddleSearch";
            this.listBoxMiddleSearch.ScrollAlwaysVisible = true;
            this.listBoxMiddleSearch.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxMiddleSearch.Size = new System.Drawing.Size(236, 107);
            this.listBoxMiddleSearch.TabIndex = 5;
            this.listBoxMiddleSearch.DoubleClick += new System.EventHandler(this.button6_Click_1);
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 20);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(236, 10);
            this.panel7.TabIndex = 4;
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxSearch.Location = new System.Drawing.Point(0, 0);
            this.textBoxSearch.Margin = new System.Windows.Forms.Padding(8);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(236, 20);
            this.textBoxSearch.TabIndex = 0;
            this.textBoxSearch.TextChanged += new System.EventHandler(this.textBoxSearch_TextChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSeparator3,
            this.toolStripDropDownButton1,
            this.toolStripSeparator4,
            this.toolStripDropDownButton2});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(542, 25);
            this.toolStrip1.TabIndex = 38;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(71, 22);
            this.toolStripButton1.Text = "Настройки";
            this.toolStripButton1.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.очиститьToolStripMenuItem,
            this.очиститьРезультатыToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(55, 22);
            this.toolStripDropDownButton1.Text = "Вывод";
            // 
            // очиститьToolStripMenuItem
            // 
            this.очиститьToolStripMenuItem.Name = "очиститьToolStripMenuItem";
            this.очиститьToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.очиститьToolStripMenuItem.Text = "Очистить лог";
            this.очиститьToolStripMenuItem.Click += new System.EventHandler(this.button3_Click_2);
            // 
            // очиститьРезультатыToolStripMenuItem
            // 
            this.очиститьРезультатыToolStripMenuItem.Name = "очиститьРезультатыToolStripMenuItem";
            this.очиститьРезультатыToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.очиститьРезультатыToolStripMenuItem.Text = "Очистить результаты";
            this.очиститьРезультатыToolStripMenuItem.Click += new System.EventHandler(this.очиститьРезультатыToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(245, 138);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(542, 283);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(534, 257);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Лог";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textLog
            // 
            this.textLog.BackColor = System.Drawing.Color.White;
            this.textLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textLog.Location = new System.Drawing.Point(3, 3);
            this.textLog.Multiline = true;
            this.textLog.Name = "textLog";
            this.textLog.ReadOnly = true;
            this.textLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textLog.Size = new System.Drawing.Size(528, 251);
            this.textLog.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.textResult);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(534, 248);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Результаты";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // textResult
            // 
            this.textResult.BackColor = System.Drawing.Color.White;
            this.textResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textResult.Location = new System.Drawing.Point(3, 3);
            this.textResult.Multiline = true;
            this.textResult.Name = "textResult";
            this.textResult.ReadOnly = true;
            this.textResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textResult.Size = new System.Drawing.Size(528, 242);
            this.textResult.TabIndex = 0;
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertTargetSourceFiles,
            this.convertTargetSelectDirectory,
            this.toolStripSeparator2,
            this.convertSourceDirectory,
            this.convertSourceFiles});
            this.toolStripDropDownButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton2.Image")));
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(109, 22);
            this.toolStripDropDownButton2.Text = "Конвертировать";
            // 
            // convertTargetSelectDirectory
            // 
            this.convertTargetSelectDirectory.Name = "convertTargetSelectDirectory";
            this.convertTargetSelectDirectory.Size = new System.Drawing.Size(309, 22);
            this.convertTargetSelectDirectory.Text = "Выбрать целевую папку...";
            this.convertTargetSelectDirectory.Click += new System.EventHandler(this.convertTargetSelectDirectory_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(306, 6);
            // 
            // convertSourceDirectory
            // 
            this.convertSourceDirectory.Name = "convertSourceDirectory";
            this.convertSourceDirectory.Size = new System.Drawing.Size(309, 22);
            this.convertSourceDirectory.Text = "Конвертировать директорию...";
            this.convertSourceDirectory.Click += new System.EventHandler(this.convertSourceDirectory_Click);
            // 
            // convertTargetSourceFiles
            // 
            this.convertTargetSourceFiles.Name = "convertTargetSourceFiles";
            this.convertTargetSourceFiles.Size = new System.Drawing.Size(309, 22);
            this.convertTargetSourceFiles.Text = "Располагать рядом с исходными файлами";
            this.convertTargetSourceFiles.Click += new System.EventHandler(this.convertTargetSourceFiles_Click);
            // 
            // convertSourceFiles
            // 
            this.convertSourceFiles.Name = "convertSourceFiles";
            this.convertSourceFiles.Size = new System.Drawing.Size(309, 22);
            this.convertSourceFiles.Text = "Конвертировать файлы...";
            this.convertSourceFiles.Click += new System.EventHandler(this.convertSourceFiles_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(787, 421);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataSignInfo)).EndInit();
            this.panel3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.OpenFileDialog openFileSign;
        private System.Windows.Forms.OpenFileDialog openFileWavMp3;
        private System.Windows.Forms.OpenFileDialog openFileLog;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textSearchLogList;
        private System.Windows.Forms.Button buttonLogOpen;
        private System.Windows.Forms.Label labelSearchSignsCount;
        private System.Windows.Forms.Label labelSearchMainCount;
        private System.Windows.Forms.Button buttonSearchOpenMain;
        private System.Windows.Forms.Button buttonSearchOpenSigns;
        private System.Windows.Forms.ListBox listSearchSigns;
        private System.Windows.Forms.ListBox listSearchMain;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox textLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox textResult;
        private System.Windows.Forms.DataGridView dataSignInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ListBox listBoxMiddleSearch;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem очиститьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem очиститьРезультатыToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ListBox listBoxByFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowser;
        private System.Windows.Forms.ToolStripMenuItem convertTargetSelectDirectory;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem convertSourceDirectory;
        private System.Windows.Forms.ToolStripMenuItem convertTargetSourceFiles;
        private System.Windows.Forms.ToolStripMenuItem convertSourceFiles;
    }
}

