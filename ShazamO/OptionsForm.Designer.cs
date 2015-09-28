namespace ShazamO
{
    partial class OptionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comConverter_PreferredSampleRate = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numSignature_SamplesPerSecond = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textLogElement_IgnoredTags = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.buttonLogElement_IgnoredTags_remove = new System.Windows.Forms.Button();
            this.buttonLogElement_IgnoredTags_add = new System.Windows.Forms.Button();
            this.listLogElement_IgnoredTags = new System.Windows.Forms.ListBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.numInfluenceArea_BorderDeviation = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numComparing_NSigm = new System.Windows.Forms.NumericUpDown();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonDefault = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSignature_SamplesPerSecond)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInfluenceArea_BorderDeviation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComparing_NSigm)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comConverter_PreferredSampleRate);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.numSignature_SamplesPerSecond);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(18, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(297, 75);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Генератор сигнатур";
            // 
            // comConverter_PreferredSampleRate
            // 
            this.comConverter_PreferredSampleRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comConverter_PreferredSampleRate.FormattingEnabled = true;
            this.comConverter_PreferredSampleRate.Items.AddRange(new object[] {
            "Without resampling",
            "8000",
            "11025",
            "16000",
            "22050",
            "24000"});
            this.comConverter_PreferredSampleRate.Location = new System.Drawing.Point(170, 19);
            this.comConverter_PreferredSampleRate.Name = "comConverter_PreferredSampleRate";
            this.comConverter_PreferredSampleRate.Size = new System.Drawing.Size(120, 21);
            this.comConverter_PreferredSampleRate.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(158, 25);
            this.label3.TabIndex = 3;
            this.label3.Text = "Частота построителя, Гц";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numSignature_SamplesPerSecond
            // 
            this.numSignature_SamplesPerSecond.Location = new System.Drawing.Point(170, 45);
            this.numSignature_SamplesPerSecond.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numSignature_SamplesPerSecond.Name = "numSignature_SamplesPerSecond";
            this.numSignature_SamplesPerSecond.Size = new System.Drawing.Size(120, 20);
            this.numSignature_SamplesPerSecond.TabIndex = 2;
            this.numSignature_SamplesPerSecond.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Частота входных данных, Гц";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textLogElement_IgnoredTags);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.buttonLogElement_IgnoredTags_remove);
            this.groupBox2.Controls.Add(this.buttonLogElement_IgnoredTags_add);
            this.groupBox2.Controls.Add(this.listLogElement_IgnoredTags);
            this.groupBox2.Location = new System.Drawing.Point(321, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(190, 159);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Файл лога";
            // 
            // textLogElement_IgnoredTags
            // 
            this.textLogElement_IgnoredTags.Location = new System.Drawing.Point(6, 105);
            this.textLogElement_IgnoredTags.Name = "textLogElement_IgnoredTags";
            this.textLogElement_IgnoredTags.Size = new System.Drawing.Size(177, 20);
            this.textLogElement_IgnoredTags.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(180, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Пропущенные элементы";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonLogElement_IgnoredTags_remove
            // 
            this.buttonLogElement_IgnoredTags_remove.Location = new System.Drawing.Point(125, 130);
            this.buttonLogElement_IgnoredTags_remove.Name = "buttonLogElement_IgnoredTags_remove";
            this.buttonLogElement_IgnoredTags_remove.Size = new System.Drawing.Size(58, 22);
            this.buttonLogElement_IgnoredTags_remove.TabIndex = 6;
            this.buttonLogElement_IgnoredTags_remove.Text = "Удалить";
            this.buttonLogElement_IgnoredTags_remove.UseVisualStyleBackColor = true;
            this.buttonLogElement_IgnoredTags_remove.Click += new System.EventHandler(this.buttonLogElement_IgnoredTags_remove_Click);
            // 
            // buttonLogElement_IgnoredTags_add
            // 
            this.buttonLogElement_IgnoredTags_add.Location = new System.Drawing.Point(49, 131);
            this.buttonLogElement_IgnoredTags_add.Name = "buttonLogElement_IgnoredTags_add";
            this.buttonLogElement_IgnoredTags_add.Size = new System.Drawing.Size(70, 21);
            this.buttonLogElement_IgnoredTags_add.TabIndex = 5;
            this.buttonLogElement_IgnoredTags_add.Text = "Добавить";
            this.buttonLogElement_IgnoredTags_add.UseVisualStyleBackColor = true;
            this.buttonLogElement_IgnoredTags_add.Click += new System.EventHandler(this.buttonLogElement_IgnoredTags_add_Click);
            // 
            // listLogElement_IgnoredTags
            // 
            this.listLogElement_IgnoredTags.FormattingEnabled = true;
            this.listLogElement_IgnoredTags.Location = new System.Drawing.Point(6, 41);
            this.listLogElement_IgnoredTags.Name = "listLogElement_IgnoredTags";
            this.listLogElement_IgnoredTags.ScrollAlwaysVisible = true;
            this.listLogElement_IgnoredTags.Size = new System.Drawing.Size(177, 56);
            this.listLogElement_IgnoredTags.TabIndex = 4;
            this.listLogElement_IgnoredTags.SelectedIndexChanged += new System.EventHandler(this.listLogElement_IgnoredTags_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.numInfluenceArea_BorderDeviation);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.numComparing_NSigm);
            this.groupBox3.Location = new System.Drawing.Point(16, 97);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(299, 78);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Сравнение";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(8, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(203, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Допустимое наложение, сэмпл";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numInfluenceArea_BorderDeviation
            // 
            this.numInfluenceArea_BorderDeviation.Location = new System.Drawing.Point(217, 46);
            this.numInfluenceArea_BorderDeviation.Name = "numInfluenceArea_BorderDeviation";
            this.numInfluenceArea_BorderDeviation.Size = new System.Drawing.Size(75, 20);
            this.numInfluenceArea_BorderDeviation.TabIndex = 6;
            this.numInfluenceArea_BorderDeviation.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(205, 25);
            this.label4.TabIndex = 5;
            this.label4.Text = "Сигнализирующее отклонение, сигм";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // numComparing_NSigm
            // 
            this.numComparing_NSigm.DecimalPlaces = 2;
            this.numComparing_NSigm.Location = new System.Drawing.Point(217, 20);
            this.numComparing_NSigm.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numComparing_NSigm.Name = "numComparing_NSigm";
            this.numComparing_NSigm.Size = new System.Drawing.Size(75, 20);
            this.numComparing_NSigm.TabIndex = 4;
            this.numComparing_NSigm.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(407, 248);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(104, 25);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(297, 248);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(104, 25);
            this.buttonOK.TabIndex = 6;
            this.buttonOK.Text = "ОК";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.labelInfo);
            this.groupBox4.Location = new System.Drawing.Point(16, 181);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(494, 61);
            this.groupBox4.TabIndex = 7;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Информация";
            // 
            // labelInfo
            // 
            this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelInfo.Location = new System.Drawing.Point(3, 16);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Padding = new System.Windows.Forms.Padding(5);
            this.labelInfo.Size = new System.Drawing.Size(488, 42);
            this.labelInfo.TabIndex = 0;
            // 
            // buttonDefault
            // 
            this.buttonDefault.Location = new System.Drawing.Point(16, 248);
            this.buttonDefault.Name = "buttonDefault";
            this.buttonDefault.Size = new System.Drawing.Size(104, 25);
            this.buttonDefault.TabIndex = 8;
            this.buttonDefault.Text = "По умолчанию";
            this.buttonDefault.UseVisualStyleBackColor = true;
            this.buttonDefault.Click += new System.EventHandler(this.buttonDefault_Click);
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(517, 285);
            this.Controls.Add(this.buttonDefault);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "OptionsForm";
            this.Text = "Настройки";
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSignature_SamplesPerSecond)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numInfluenceArea_BorderDeviation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numComparing_NSigm)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numSignature_SamplesPerSecond;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comConverter_PreferredSampleRate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textLogElement_IgnoredTags;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button buttonLogElement_IgnoredTags_remove;
        private System.Windows.Forms.Button buttonLogElement_IgnoredTags_add;
        private System.Windows.Forms.ListBox listLogElement_IgnoredTags;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numComparing_NSigm;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numInfluenceArea_BorderDeviation;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonDefault;
    }
}