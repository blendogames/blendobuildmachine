namespace blendobuildmachine
{
    partial class Form1
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLocalExeFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.logsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToClipboardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copySelectedToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewChangelogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.past24HoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.past48HoursToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.past7DaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.past30DaysToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allOfThemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.verboseViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.openLocalExeFolderWhenDoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox1.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(13, 29);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(659, 516);
            this.listBox1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(13, 582);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(659, 67);
            this.button1.TabIndex = 1;
            this.button1.Text = "Create build.";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.viewChangelogsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(684, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openLocalExeFolderToolStripMenuItem,
            this.openLocalExeFolderWhenDoneToolStripMenuItem,
            this.toolStripMenuItem3,
            this.optionsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.logsToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openLocalExeFolderToolStripMenuItem
            // 
            this.openLocalExeFolderToolStripMenuItem.Name = "openLocalExeFolderToolStripMenuItem";
            this.openLocalExeFolderToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openLocalExeFolderToolStripMenuItem.Text = "Open exe folder";
            this.openLocalExeFolderToolStripMenuItem.Click += new System.EventHandler(this.openLocalExeFolderToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(216, 6);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(216, 6);
            // 
            // logsToolStripMenuItem
            // 
            this.logsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToClipboardToolStripMenuItem1,
            this.copySelectedToClipboardToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.logsToolStripMenuItem.Name = "logsToolStripMenuItem";
            this.logsToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.logsToolStripMenuItem.Text = "Logs";
            // 
            // copyToClipboardToolStripMenuItem1
            // 
            this.copyToClipboardToolStripMenuItem1.Name = "copyToClipboardToolStripMenuItem1";
            this.copyToClipboardToolStripMenuItem1.Size = new System.Drawing.Size(215, 22);
            this.copyToClipboardToolStripMenuItem1.Text = "Copy all to clipboard";
            this.copyToClipboardToolStripMenuItem1.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem1_Click);
            // 
            // copySelectedToClipboardToolStripMenuItem
            // 
            this.copySelectedToClipboardToolStripMenuItem.Name = "copySelectedToClipboardToolStripMenuItem";
            this.copySelectedToClipboardToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.copySelectedToClipboardToolStripMenuItem.Text = "Copy selected to clipboard";
            this.copySelectedToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copySelectedToClipboardToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(215, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // viewChangelogsToolStripMenuItem
            // 
            this.viewChangelogsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.past24HoursToolStripMenuItem,
            this.past48HoursToolStripMenuItem,
            this.past7DaysToolStripMenuItem,
            this.past30DaysToolStripMenuItem,
            this.allOfThemToolStripMenuItem,
            this.toolStripMenuItem1,
            this.verboseViewToolStripMenuItem});
            this.viewChangelogsToolStripMenuItem.Name = "viewChangelogsToolStripMenuItem";
            this.viewChangelogsToolStripMenuItem.Size = new System.Drawing.Size(108, 20);
            this.viewChangelogsToolStripMenuItem.Text = "View changelogs";
            // 
            // past24HoursToolStripMenuItem
            // 
            this.past24HoursToolStripMenuItem.Name = "past24HoursToolStripMenuItem";
            this.past24HoursToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.past24HoursToolStripMenuItem.Text = "Past 24 hours";
            this.past24HoursToolStripMenuItem.Click += new System.EventHandler(this.past24HoursToolStripMenuItem_Click);
            // 
            // past48HoursToolStripMenuItem
            // 
            this.past48HoursToolStripMenuItem.Name = "past48HoursToolStripMenuItem";
            this.past48HoursToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.past48HoursToolStripMenuItem.Text = "Past 48 hours";
            this.past48HoursToolStripMenuItem.Click += new System.EventHandler(this.past48HoursToolStripMenuItem_Click);
            // 
            // past7DaysToolStripMenuItem
            // 
            this.past7DaysToolStripMenuItem.Name = "past7DaysToolStripMenuItem";
            this.past7DaysToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.past7DaysToolStripMenuItem.Text = "Past 7 days";
            this.past7DaysToolStripMenuItem.Click += new System.EventHandler(this.past7DaysToolStripMenuItem_Click);
            // 
            // past30DaysToolStripMenuItem
            // 
            this.past30DaysToolStripMenuItem.Name = "past30DaysToolStripMenuItem";
            this.past30DaysToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.past30DaysToolStripMenuItem.Text = "Past 30 days";
            this.past30DaysToolStripMenuItem.Click += new System.EventHandler(this.past30DaysToolStripMenuItem_Click);
            // 
            // allOfThemToolStripMenuItem
            // 
            this.allOfThemToolStripMenuItem.Name = "allOfThemToolStripMenuItem";
            this.allOfThemToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.allOfThemToolStripMenuItem.Text = "Since the beginning of time";
            this.allOfThemToolStripMenuItem.Click += new System.EventHandler(this.allOfThemToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(217, 6);
            // 
            // verboseViewToolStripMenuItem
            // 
            this.verboseViewToolStripMenuItem.CheckOnClick = true;
            this.verboseViewToolStripMenuItem.Name = "verboseViewToolStripMenuItem";
            this.verboseViewToolStripMenuItem.Size = new System.Drawing.Size(220, 22);
            this.verboseViewToolStripMenuItem.Text = "Verbose changelog";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(13, 551);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(659, 25);
            this.progressBar1.Step = 1;
            this.progressBar1.TabIndex = 3;
            // 
            // openLocalExeFolderWhenDoneToolStripMenuItem
            // 
            this.openLocalExeFolderWhenDoneToolStripMenuItem.Checked = true;
            this.openLocalExeFolderWhenDoneToolStripMenuItem.CheckOnClick = true;
            this.openLocalExeFolderWhenDoneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.openLocalExeFolderWhenDoneToolStripMenuItem.Name = "openLocalExeFolderWhenDoneToolStripMenuItem";
            this.openLocalExeFolderWhenDoneToolStripMenuItem.Size = new System.Drawing.Size(219, 22);
            this.openLocalExeFolderWhenDoneToolStripMenuItem.Text = "Open exe folder when done";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 661);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blendo build machine";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ToolStripMenuItem viewChangelogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem past24HoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem past48HoursToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem past7DaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem past30DaysToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allOfThemToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verboseViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem logsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem copySelectedToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLocalExeFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openLocalExeFolderWhenDoneToolStripMenuItem;
    }
}

