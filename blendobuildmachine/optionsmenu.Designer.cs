namespace blendobuildmachine
{
    partial class optionsmenu
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
            this.textBox_localpath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonOptionsOk = new System.Windows.Forms.Button();
            this.textBox_repositoryurl = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_svnexecutable = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_compilerexecutable = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_solutionfile = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_environmentVars = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_localpath
            // 
            this.textBox_localpath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_localpath.Location = new System.Drawing.Point(6, 42);
            this.textBox_localpath.Name = "textBox_localpath";
            this.textBox_localpath.Size = new System.Drawing.Size(491, 20);
            this.textBox_localpath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Local folder that game is in:";
            // 
            // buttonOptionsOk
            // 
            this.buttonOptionsOk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOptionsOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOptionsOk.Location = new System.Drawing.Point(12, 522);
            this.buttonOptionsOk.Name = "buttonOptionsOk";
            this.buttonOptionsOk.Size = new System.Drawing.Size(507, 59);
            this.buttonOptionsOk.TabIndex = 100;
            this.buttonOptionsOk.Text = "Ok";
            this.buttonOptionsOk.UseVisualStyleBackColor = true;
            this.buttonOptionsOk.Click += new System.EventHandler(this.buttonOptionsOk_Click);
            // 
            // textBox_repositoryurl
            // 
            this.textBox_repositoryurl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_repositoryurl.Location = new System.Drawing.Point(6, 98);
            this.textBox_repositoryurl.Name = "textBox_repositoryurl";
            this.textBox_repositoryurl.Size = new System.Drawing.Size(491, 20);
            this.textBox_repositoryurl.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Repository URL:";
            // 
            // textBox_svnexecutable
            // 
            this.textBox_svnexecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_svnexecutable.Location = new System.Drawing.Point(6, 159);
            this.textBox_svnexecutable.Name = "textBox_svnexecutable";
            this.textBox_svnexecutable.Size = new System.Drawing.Size(491, 20);
            this.textBox_svnexecutable.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 143);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(185, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Filepath to SVN executable (svn.exe):";
            // 
            // textBox_compilerexecutable
            // 
            this.textBox_compilerexecutable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_compilerexecutable.Location = new System.Drawing.Point(9, 106);
            this.textBox_compilerexecutable.Name = "textBox_compilerexecutable";
            this.textBox_compilerexecutable.Size = new System.Drawing.Size(487, 20);
            this.textBox_compilerexecutable.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(247, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Filepath to code compiler executable (msbuild.exe):";
            // 
            // textBox_solutionfile
            // 
            this.textBox_solutionfile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_solutionfile.Location = new System.Drawing.Point(9, 45);
            this.textBox_solutionfile.Name = "textBox_solutionfile";
            this.textBox_solutionfile.Size = new System.Drawing.Size(487, 20);
            this.textBox_solutionfile.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(195, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Filepath to game code solution file (.sln):";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.textBox_localpath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_repositoryurl);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_svnexecutable);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(507, 201);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SVN";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.textBox_environmentVars);
            this.groupBox2.Controls.Add(this.textBox_solutionfile);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.textBox_compilerexecutable);
            this.groupBox2.Location = new System.Drawing.Point(13, 244);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(506, 253);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compiling";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 151);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(280, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Environment variables (separate key and value by space):";
            // 
            // textBox_environmentVars
            // 
            this.textBox_environmentVars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_environmentVars.Location = new System.Drawing.Point(9, 170);
            this.textBox_environmentVars.Multiline = true;
            this.textBox_environmentVars.Name = "textBox_environmentVars";
            this.textBox_environmentVars.Size = new System.Drawing.Size(487, 77);
            this.textBox_environmentVars.TabIndex = 5;
            // 
            // optionsmenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(531, 593);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonOptionsOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "optionsmenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_localpath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonOptionsOk;
        private System.Windows.Forms.TextBox textBox_repositoryurl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_svnexecutable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_compilerexecutable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_solutionfile;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox textBox_environmentVars;
        private System.Windows.Forms.Label label6;
    }
}