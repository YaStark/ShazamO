namespace ShazamO
{
    partial class MainForm
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.transformToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fromwavToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frommp3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileWavTransform = new System.Windows.Forms.OpenFileDialog();
            this.openFileMp3Transform = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.labelStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelProgressMain = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressMain = new System.Windows.Forms.ToolStripProgressBar();
            this.labelProgressSub = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressSub = new System.Windows.Forms.ToolStripProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.buttonSaveLog = new System.Windows.Forms.Button();
            this.buttonClearLog = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.transformToolStripMenuItem,
            this.viewToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(731, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // transformToolStripMenuItem
            // 
            this.transformToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fromwavToolStripMenuItem,
            this.frommp3ToolStripMenuItem});
            this.transformToolStripMenuItem.Name = "transformToolStripMenuItem";
            this.transformToolStripMenuItem.Size = new System.Drawing.Size(96, 20);
            this.transformToolStripMenuItem.Text = "Sign generator";
            // 
            // fromwavToolStripMenuItem
            // 
            this.fromwavToolStripMenuItem.Name = "fromwavToolStripMenuItem";
            this.fromwavToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.fromwavToolStripMenuItem.Text = "From *.wav";
            this.fromwavToolStripMenuItem.Click += new System.EventHandler(this.fromwavToolStripMenuItem_Click);
            // 
            // frommp3ToolStripMenuItem
            // 
            this.frommp3ToolStripMenuItem.Name = "frommp3ToolStripMenuItem";
            this.frommp3ToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.frommp3ToolStripMenuItem.Text = "From *.mp3";
            this.frommp3ToolStripMenuItem.Click += new System.EventHandler(this.frommp3ToolStripMenuItem_Click);
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLogToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // showLogToolStripMenuItem
            // 
            this.showLogToolStripMenuItem.Name = "showLogToolStripMenuItem";
            this.showLogToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.showLogToolStripMenuItem.Text = "Show log";
            this.showLogToolStripMenuItem.Click += new System.EventHandler(this.showLogToolStripMenuItem_Click);
            // 
            // openFileWavTransform
            // 
            this.openFileWavTransform.FileName = "openFileDialog1";
            // 
            // openFileMp3Transform
            // 
            this.openFileMp3Transform.FileName = "openFileDialog1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.labelStatus,
            this.labelProgressMain,
            this.toolStripProgressMain,
            this.labelProgressSub,
            this.toolStripProgressSub});
            this.statusStrip1.Location = new System.Drawing.Point(0, 281);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(731, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // labelStatus
            // 
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(118, 17);
            this.labelStatus.Text = "toolStripStatusLabel1";
            this.labelStatus.Visible = false;
            // 
            // labelProgressMain
            // 
            this.labelProgressMain.Name = "labelProgressMain";
            this.labelProgressMain.Size = new System.Drawing.Size(118, 17);
            this.labelProgressMain.Text = "toolStripStatusLabel1";
            this.labelProgressMain.Visible = false;
            // 
            // toolStripProgressMain
            // 
            this.toolStripProgressMain.Name = "toolStripProgressMain";
            this.toolStripProgressMain.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressMain.Visible = false;
            // 
            // labelProgressSub
            // 
            this.labelProgressSub.Name = "labelProgressSub";
            this.labelProgressSub.Size = new System.Drawing.Size(118, 17);
            this.labelProgressSub.Text = "toolStripStatusLabel2";
            this.labelProgressSub.Visible = false;
            // 
            // toolStripProgressSub
            // 
            this.toolStripProgressSub.Name = "toolStripProgressSub";
            this.toolStripProgressSub.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressSub.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Location = new System.Drawing.Point(399, 47);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(307, 140);
            this.panel1.TabIndex = 3;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.richTextBox1.Size = new System.Drawing.Size(307, 110);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.buttonSaveLog);
            this.panel2.Controls.Add(this.buttonClearLog);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 110);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(307, 30);
            this.panel2.TabIndex = 3;
            // 
            // buttonSaveLog
            // 
            this.buttonSaveLog.Location = new System.Drawing.Point(84, 3);
            this.buttonSaveLog.Name = "buttonSaveLog";
            this.buttonSaveLog.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveLog.TabIndex = 4;
            this.buttonSaveLog.Text = "Save log";
            this.buttonSaveLog.UseVisualStyleBackColor = true;
            // 
            // buttonClearLog
            // 
            this.buttonClearLog.Location = new System.Drawing.Point(3, 3);
            this.buttonClearLog.Name = "buttonClearLog";
            this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
            this.buttonClearLog.TabIndex = 0;
            this.buttonClearLog.Text = "Clear log";
            this.buttonClearLog.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 303);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem transformToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fromwavToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frommp3ToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileWavTransform;
        private System.Windows.Forms.OpenFileDialog openFileMp3Transform;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressMain;
        private System.Windows.Forms.ToolStripStatusLabel labelProgressMain;
        private System.Windows.Forms.ToolStripStatusLabel labelProgressSub;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressSub;
        private System.Windows.Forms.ToolStripStatusLabel labelStatus;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button buttonClearLog;
        private System.Windows.Forms.Button buttonSaveLog;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showLogToolStripMenuItem;
    }
}