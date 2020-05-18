using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;

namespace blendobuildmachine
{
    public partial class Form1 : Form
    {
        BackgroundWorker backgroundWorker;
        DateTime start;

        string exeFolder;

        public Form1()
        {
            InitializeComponent();

            this.FormClosed += MyClosedHandler;

            AddLog("Welcome to Blendo build machine.");
            AddLog(string.Empty);

            SanityCheck();

            exeFolder = string.Empty;

            openLocalExeFolderWhenDoneToolStripMenuItem.Checked = Properties.Settings.Default.openexefolder;
        }

        //DOWNLOAD THE LATEST CODE AND DATA FROM SOURCE CONTROL.
        private void OnDownloadDoWork(object sender, DoWorkEventArgs e)
        {
            //Check out and update.
            DirectoryInfo localfolder = new DirectoryInfo(Properties.Settings.Default.localFolderpath);

            if (!File.Exists(Properties.Settings.Default.svnExecutable))
            {
                AddLogInvoke(string.Format("ERROR: failed to find svn executable {0}", Properties.Settings.Default.svnExecutable));
                e.Result = false;
                return;
            }

            if (!Directory.Exists(Properties.Settings.Default.localFolderpath))
            {
                AddLogInvoke(string.Format("ERROR: failed to find game folder {0}", Properties.Settings.Default.localFolderpath));
                e.Result = false;
                return;
            }

            string[] arguments = new string[2]
            {
                //string.Format("checkout {0} {1} --username {2} --password {3}", url, localfolder, username, password),
                //string.Format("update {0} --username {1} --password {2}", localfolder, username, password)
                string.Format("checkout {0} {1} ", Properties.Settings.Default.repositoryUrl, localfolder),
                string.Format("update {0} ", localfolder)
            };

            for (int i = 0; i < arguments.Length; i++)
            {
                AddLogInvoke(i <= 0 ? "SVN: checking out..." : "SVN: updating data...");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = Properties.Settings.Default.svnExecutable;
                startInfo.Arguments = arguments[i];
                startInfo.UseShellExecute = false;
                startInfo.RedirectStandardOutput = true;
                startInfo.CreateNoWindow = true;

                try
                {
                    FileInfo svnFile = new FileInfo(Properties.Settings.Default.svnExecutable);
                    AddLogInvoke(string.Format("{0} {1}", svnFile.Name, arguments[i])); //Show the command.
                }
                catch
                {
                }

                try
                {
                    Process proc = new Process();
                    proc.StartInfo = startInfo;
                    proc.Start();

                    while (!proc.StandardOutput.EndOfStream)
                    {
                        string line = proc.StandardOutput.ReadLine();

                        AddLogInvoke("    " + line);
                    }
                }
                catch (Exception err)
                {
                    AddLogInvoke(i <= 0 ? "ERROR: SVN checkout failed." : "ERROR: SVN update failed.");
                    AddLogInvoke(err.Message);
                    e.Result = false;
                    return;
                }

                if (i <= 0)
                {
                    MethodInvoker mi = delegate() { progressBar1.Value = 50; };
                    this.Invoke(mi);
                }
            }

            e.Result = true;
        }

        //FINISHED WITH CODE/DATA DOWNLOAD. PROCEED TO COMPILING...
        private void OnDownloadCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddLog(string.Format("ERROR: {0}", e.Error));
                return;
            }

            if (e.Result is bool)
            {
                bool returnvalue = (bool)e.Result;

                if (!returnvalue)
                {
                    //Error.
                    SetButtonsEnabled(true);
                    listBox1.BackColor = Color.Pink;
                    progressBar1.Value = 0;
                    AddLog("ERROR: SVN failed.");
                    return;
                }
            }

            //Continue to next step...
            progressBar1.Value = 80;
            AddLog(string.Empty);
            AddLog("---- Starting step 2 of 2: compiling game code ----");
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += OnCompileDoWork;
            backgroundWorker.RunWorkerCompleted += OnCompileCompleted;
            backgroundWorker.RunWorkerAsync();
        }




        //COMPILE THE CODE AND GENERATE A BUILD.
        private void OnCompileDoWork(object sender, DoWorkEventArgs e)
        {
            if (!File.Exists(Properties.Settings.Default.compilerExecutable))
            {
                AddLogInvoke(string.Format("ERROR: failed to find compiler executable {0}", Properties.Settings.Default.compilerExecutable));
                e.Result = false;
                return;
            }

            if (!File.Exists(Properties.Settings.Default.solutionfile))
            {
                AddLogInvoke(string.Format("ERROR: failed to find solution file {0}", Properties.Settings.Default.solutionfile));
                e.Result = false;
                return;
            }

            bool buildSuccess = true;

            List<string> executables = new List<string>();

            //Set environment variables.
            string[] envVars = Properties.Settings.Default.environmentVars.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (envVars.Length > 0)
            {
                for (int i = 0; i < envVars.Length; i++)
                {
                    envVars[i] = envVars[i].Trim();

                    int indexOfFirstSpace = envVars[i].IndexOf(' ', 0);

                    if (indexOfFirstSpace < 0)
                    {
                        AddLogInvoke(string.Format("Bad environment variable setting: {0}", envVars[i]));
                        continue;
                    }

                    string key = envVars[i].Substring(0, indexOfFirstSpace);
                    string value = envVars[i].Substring(indexOfFirstSpace + 1, envVars[i].Length - indexOfFirstSpace - 1);

                    Environment.SetEnvironmentVariable(key, value);
                    AddLogInvoke(string.Format("Setting {0}={1}", key, value));
                }
            }

            //Start the compile.
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Properties.Settings.Default.compilerExecutable;
            startInfo.Arguments = Properties.Settings.Default.solutionfile;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;
            Process proc = new Process();

            try
            {
                FileInfo compilerFile = new FileInfo(Properties.Settings.Default.compilerExecutable);
                AddLogInvoke(string.Format("{0} {1}", compilerFile.Name, Properties.Settings.Default.solutionfile)); //Show the command.
            }
            catch
            {
            }

            try
            {
                proc.StartInfo = startInfo;
                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();
                    AddLogInvoke("    " + line);

                    if (line.StartsWith("build failed.", true, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        buildSuccess = false;
                    }

                    if (line.EndsWith("error(s)", true, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        //determine how many errors are in this build.

                        string[] chunks = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        int numberOfErrors = int.Parse(chunks[0]);

                        if (numberOfErrors > 0)
                        {
                            //Errors found!
                            buildSuccess = false;
                        }
                    }

                    if (line.Contains(".exe"))
                    {
                        executables.Add(line);
                    }
                }
            }
            catch (Exception err)
            {
                AddLogInvoke("ERROR: failed to compile.");
                AddLogInvoke(err.Message);
            }

            if (executables.Count > 0)
            {
                AddLogInvoke(string.Empty);

                for (int i = 0; i < executables.Count; i++)
                {
                    int arrowIndex = executables[i].IndexOf("->");

                    if (arrowIndex >= 0 && executables[i].EndsWith(".exe", StringComparison.InvariantCultureIgnoreCase))
                    {
                        executables[i] = executables[i].Remove(0, arrowIndex + 2);
                        executables[i] = executables[i].Trim();

                        if (File.Exists(executables[i]))
                        {
                            FileInfo exeFile = new FileInfo(executables[i]);

                            if (exeFile != null)
                            {
                                AddLogInvoke(string.Format("Output exe: {0}", exeFile.FullName));
                                exeFolder = exeFile.DirectoryName;
                            }
                        }
                    }
                }
            }

            //return build status.
            e.Result = buildSuccess;
        }

        //COMPILE IS DONE. EVERYTHING IS NOW DONE!
        private void OnCompileCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progressBar1.Value = 100;
            SetButtonsEnabled(true);

            if (e.Error != null)
            {
                AddLog(string.Format("ERROR: {0}", e.Error));
                return;
            }

            if (e.Result is bool)
            {
                bool returnvalue = (bool)e.Result;

                if (!returnvalue)
                {
                    //Error.
                    listBox1.BackColor = Color.Pink;
                    progressBar1.Value = 0;
                    AddLog("ERROR: code compile failed.");
                    return;
                }
            }

            TimeSpan delta = DateTime.Now.Subtract(start);
            AddLog(string.Empty);

            string timeStr = string.Empty;
            if (delta.TotalSeconds >= 60)
                timeStr = (string.Format("{0} minutes", Math.Round(delta.TotalMinutes, 1)));
            else
                timeStr = (string.Format("{0} seconds", Math.Round(delta.TotalSeconds, 1)));

            AddLog(string.Format("-- BUILD COMPLETE! -- Total deployment time: {0}.", timeStr));
            AddLog(string.Empty);
            AddLog(string.Empty);

            listBox1.BackColor = Color.GreenYellow;

            if (openLocalExeFolderWhenDoneToolStripMenuItem.Checked)
            {
                //Auto open exe folder.
                AddLog(string.Format("Opening local exe folder: \"{0}\"", exeFolder));
                AddLog(string.Empty);
                openLocalExeFolderToolStripMenuItem_Click(null, null);
            }
        }

        //Click the big "Compile Build" button.
        private void button1_Click(object sender, EventArgs e)
        {
            //Click the big button.
            AddLog(string.Empty);
            bool sanity = SanityCheck();

            if (!sanity)
            {
                AddLog(string.Empty);
                AddLog("---- Errors found. Build cancelled ----");
                AddLog(string.Empty);
                return;
            }

            progressBar1.Value = 0;
            listBox1.BackColor = Color.White;

            start = DateTime.Now;
            SetButtonsEnabled(false);

            AddLog(string.Empty);
            AddLog("Starting build.");
            AddLog(string.Empty);
            AddLog("---- Starting step 1 of 2: downloading latest game data ----");

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += OnDownloadDoWork;
            backgroundWorker.RunWorkerCompleted += OnDownloadCompleted;
            backgroundWorker.RunWorkerAsync();

            progressBar1.Value = 25;
        }       

        private bool SanityCheck()
        {
            //Sanity check all the options.
            bool success = true;
            if (!Directory.Exists(Properties.Settings.Default.localFolderpath))
            {
                AddLog("ERROR: failed to find game code folder. Please go to File > Options");
                success = false;
            }

            if (!File.Exists(Properties.Settings.Default.solutionfile))
            {
                AddLog("ERROR: failed to find code solution file. Please go to File > Options");
                success = false;
            }

            if (!File.Exists(Properties.Settings.Default.svnExecutable))
            {
                AddLog("ERROR: failed to find svn executable. Please go to File > Options");
                success = false;
            }

            if (!File.Exists(Properties.Settings.Default.compilerExecutable))
            {
                AddLog("ERROR: failed to find code compiler executable. Please go to File > Options");
                success = false;
            }

            if (!success)
                listBox1.BackColor = Color.Pink;

            return success;
        }

        protected void MyClosedHandler(object sender, EventArgs e)
        {
            Properties.Settings.Default.openexefolder = openLocalExeFolderWhenDoneToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            optionsmenu newOptionsmenu = new optionsmenu();
            newOptionsmenu.ShowDialog();


            AddLog("Options updated.");
            listBox1.BackColor = Color.White;
            SanityCheck();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AddLog(string text)
        {
            listBox1.Items.Add(text);

            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;

            this.Update();
            this.Refresh();
        }

        

        

        private void SetButtonsEnabled(bool value)
        {
            button1.Enabled = value;
            fileToolStripMenuItem.Enabled = value;
            viewChangelogsToolStripMenuItem.Enabled = value;

            button1.Text = (value ? "Create build." : "Please wait...");            
        }


        
        


        private void AddLogInvoke(string text)
        {
            MethodInvoker mi = delegate () { AddLog(text); };
            this.Invoke(mi);
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }


        private void OnViewlogDoWork(object sender, DoWorkEventArgs e)
        {
            TimeSpan timespan;

            if (e.Argument is TimeSpan)
            {
                timespan = (TimeSpan)e.Argument;
            }
            else
            {
                e.Result = false;
                return;
            }


            DateTime nowdate = DateTime.Now;
            DateTime startDate = nowdate.Subtract(timespan);

            string enddateStr = string.Format("{0}-{1}-{2}", nowdate.Year, nowdate.Month, nowdate.Day);
            string startdateSTr = string.Format("{0}-{1}-{2}", startDate.Year, startDate.Month, startDate.Day);

            string arguments = string.Format("log -r {{{0}}}:{{{1}}} {2}",  startdateSTr, enddateStr, Properties.Settings.Default.localFolderpath);

            string spanStr = string.Empty;

            if (timespan.TotalHours < 0)
            {
                spanStr = "all of them";
                arguments = string.Format("log {0}", Properties.Settings.Default.localFolderpath);
            }
            else if (timespan.TotalHours <= 48)
            {
                spanStr = string.Format("past {0} hours", timespan.TotalHours);
            }
            else
            {
                spanStr = string.Format("past {0} days", timespan.TotalDays);
            }

            if (verboseViewToolStripMenuItem.Checked)
            {
                arguments += " -v";
            }

            AddLogInvoke(string.Format("-- Retrieving changelogs ({0}) --", spanStr));

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = Properties.Settings.Default.svnExecutable;
            startInfo.Arguments = arguments;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.CreateNoWindow = true;

            try
            {
                Process proc = new Process();
                proc.StartInfo = startInfo;
                proc.Start();

                while (!proc.StandardOutput.EndOfStream)
                {
                    string line = proc.StandardOutput.ReadLine();

                    if (!verboseViewToolStripMenuItem.Checked)
                    {
                        //simple changelog view.

                        if (string.IsNullOrWhiteSpace(line))
                            continue;
                        else if (line.StartsWith("----------------------------------------"))
                            line = string.Empty;
                        else if (line.StartsWith("r", StringComparison.InvariantCultureIgnoreCase) && line.IndexOf('|') >= 0)
                            continue;
                    }

                    AddLogInvoke("    " + line);
                }
            }
            catch (Exception err)
            {
                AddLogInvoke("ERROR: SVN retrieve changelog failed.");
                AddLogInvoke(err.Message);
                e.Result = false;
                return;
            }

            e.Result = true;
        }

        private void OnViewlogCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                AddLog(string.Format("ERROR: {0}", e.Error));
                return;
            }

            if (e.Result is bool)
            {
                bool returnvalue = (bool)e.Result;

                if (!returnvalue)
                {
                    //Error.
                    listBox1.BackColor = Color.Pink;
                    progressBar1.Value = 0;
                    AddLog("ERROR: view changelog failed.");
                    SetButtonsEnabled(true);
                    return;
                }
            }
            
            progressBar1.Value = 100;            

            TimeSpan delta = DateTime.Now.Subtract(start);
            AddLog(string.Empty);

            string timeStr = string.Empty;
            if (delta.TotalSeconds >= 60)
                timeStr = (string.Format("{0} minutes", Math.Round(delta.TotalMinutes, 1)));
            else
                timeStr = (string.Format("{0} seconds", Math.Round(delta.TotalSeconds, 1)));

            AddLog("-- End of changelogs --");
            AddLog(string.Format("Changelog retrieval time: {0}.", timeStr));
            AddLog(string.Empty);
            AddLog(string.Empty);

            SetButtonsEnabled(true);
        }

        private void ViewChangelogs(int hours)
        {
            start = DateTime.Now;
            SetButtonsEnabled(false);
            listBox1.BackColor = Color.White;
            progressBar1.Value = 0;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += OnViewlogDoWork;
            backgroundWorker.RunWorkerCompleted += OnViewlogCompleted;
            backgroundWorker.RunWorkerAsync(new TimeSpan(hours, 0,0));

            progressBar1.Value = 70;
        }

        private void past24HoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewChangelogs(24);
        }

        private void past48HoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewChangelogs(48);
        }

        private void past7DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewChangelogs(168);
        }

        private void allOfThemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewChangelogs(-1);
        }

        private void past30DaysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewChangelogs(720);
        }

        private void clearLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Blendo build machine\nby Brendon Chung\nMay 2020\n\nUse this program to create the latest build.\n\nHere's what it does:\n1. Grabs latest data & code from source control.\n2. Compiles a new build for you.\n\nThis program allows you to stay up-to-date with the absolute latest developments and features. Whenever you want a new build, just click that big 'Create build' button.", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void copySelectedPartOfLogToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void copyToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Copy contents to clipboard.

            string output = "";

            foreach (object item in listBox1.Items)
                output += item.ToString() + "\r\n";

            if (string.IsNullOrWhiteSpace(output))
            {
                AddLog(string.Empty);
                AddLog("No log found.");
                return;
            }

            Clipboard.SetText(output);

            AddLog(string.Empty);
            AddLog("Copied entire log to clipboard.");
        }

        private void copySelectedToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string output = "";

            foreach (object item in listBox1.SelectedItems)
            {
                output += item.ToString() + "\r\n";
            }

            if (string.IsNullOrWhiteSpace(output))
            {
                AddLog(string.Empty);
                AddLog("No selected log found.");
                return;
            }

            Clipboard.SetText(output);

            AddLog(string.Empty);
            AddLog("Copied selected log to clipboard.");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            progressBar1.Value = 0;
            listBox1.BackColor = Color.White;
        }

        private void openLocalFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.localFolderpath))
            {
                AddLog("No local folder path set.");
                return;
            }

            if (!Directory.Exists(Properties.Settings.Default.localFolderpath))
            {
                AddLog(string.Format("Couldn't find folder: {0}", Properties.Settings.Default.localFolderpath));
                return;
            }

            try
            {
                Process.Start(Properties.Settings.Default.localFolderpath);

            }
            catch (Exception err)
            {
                AddLog(err.Message);
                return;
            }

            AddLog("Opened local folder.");
        }

        private void openLocalExeFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(exeFolder))
            {
                AddLog("No exe folder found.");
                return;
            }

            if (!Directory.Exists(exeFolder))
            {
                AddLog(string.Format("Failed to open folder: {0}", exeFolder));
                return;
            }

            try
            {
                Process.Start(exeFolder);

            }
            catch (Exception err)
            {
                AddLog(err.Message);
                return;
            }

            AddLog("Opened local exe folder.");
        }
    }
}
