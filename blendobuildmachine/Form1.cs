﻿using System;
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
        string exeFilepath;

        public Form1()
        {
            InitializeComponent();

            this.FormClosing += MyClosingHandler;

            AddLog("Welcome to Blendo build machine.");
            AddLog(string.Empty);

            SanityCheck();

            exeFolder = Properties.Settings.Default.lastexefolder;
            exeFilepath = string.Empty;

            this.Load += new EventHandler(Form1_Load);            
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {
            //We do this here to avoid the "invoke or begininvoke cannot be called on a control until the window handle has been created" bug.
            if (Properties.Settings.Default.runonstart)
            {
                button1_Click(null, null);
            }
        }

        //DOWNLOAD THE LATEST CODE AND DATA FROM SOURCE CONTROL.
        private void OnDownloadDoWork(object sender, DoWorkEventArgs e)
        {
            //Check out and update.
            DirectoryInfo localfolder = new DirectoryInfo(Properties.Settings.Default.localFolderpath);

            if (!File.Exists(Properties.Settings.Default.svnExecutable))
            {
                AddLogInvoke(string.Format("Error: failed to find svn executable {0}", Properties.Settings.Default.svnExecutable));
                e.Result = false;
                return;
            }

            if (!Directory.Exists(Properties.Settings.Default.localFolderpath))
            {
                AddLogInvoke(string.Format("Error: failed to find game folder {0}", Properties.Settings.Default.localFolderpath));
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
                AddLogInvoke(i <= 0 ? "SVN: checking out..." : "SVN: updating data..."); //Print what stage we're in.

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

                int outputlineCount = 0;

                try
                {
                    Process proc = new Process();
                    proc.StartInfo = startInfo;
                    proc.Start();

                    //Output the SVN results.
                    while (!proc.StandardOutput.EndOfStream)
                    {
                        if (backgroundWorker.CancellationPending)
                        {
                            //Cancel.
                            e.Result = false;
                            return;
                        }

                        string line = proc.StandardOutput.ReadLine();
                        AddLogInvoke("    " + line);
                        outputlineCount++;
                    }
                }
                catch (Exception err)
                {
                    AddLogInvoke(i <= 0 ? "Error: SVN checkout failed." : "Error: SVN update failed.");
                    AddLogInvoke(err.Message);
                    e.Result = false;
                    return;
                }

                if (outputlineCount <= 0)
                {
                    //If there are zero output lines, then something has gone wrong.
                    AddLogInvoke("Error: received no response from SVN.");
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
                AddLog(string.Format("Error: {0}", e.Error));
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
                    AddLog("Error: SVN failed.");
                    return;
                }
            }

            //Continue to next step...
            StartCompile();
        }

        private void StartCompile()
        {
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
                AddLogInvoke(string.Format("Error: failed to find compiler executable {0}", Properties.Settings.Default.compilerExecutable));
                e.Result = false;
                return;
            }

            if (!File.Exists(Properties.Settings.Default.solutionfile))
            {
                AddLogInvoke(string.Format("Error: failed to find solution file {0}", Properties.Settings.Default.solutionfile));
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
            startInfo.Arguments = string.Format("{0} {1}", Properties.Settings.Default.solutionfile, Properties.Settings.Default.compilercommandline);
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
                    if (backgroundWorker.CancellationPending)
                    {
                        //Cancel.
                        e.Result = false;
                        return;
                    }

                    string line = proc.StandardOutput.ReadLine();

                    //Filter out warnings and notes.
                    if (!Properties.Settings.Default.verbose
                        && (line.Contains("): warning ") || line.Contains("): note: ")                        
                        || line.Contains(" : warning LNK")
                        ))
                    {
                        continue;
                    }
                    

                    //Add log output.
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
                AddLogInvoke("Error: failed to compile.");
                AddLogInvoke(err.Message);
                buildSuccess = false;
            }

            if (Properties.Settings.Default.playbeep)
            {
                Console.Beep(800, 500);
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
                                exeFilepath = exeFile.FullName;

                                if (!string.IsNullOrWhiteSpace(exeFolder))
                                {
                                    if (Directory.Exists(exeFolder))
                                    {
                                        Properties.Settings.Default.lastexefolder = exeFolder;
                                    }
                                }
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
                AddLog(string.Format("Error: {0}", e.Error));
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
                    AddLog("Error: code compile failed.");
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

            if (Properties.Settings.Default.openexefolder)
            {
                //When compile complete, auto open the exe folder.
                if (string.IsNullOrWhiteSpace(exeFolder))
                {
                    AddLog("Couldn't find exe folder.");
                }
                else
                {
                    AddLog(string.Format("Opening local exe folder: \"{0}\"", exeFolder));
                    AddLog(string.Empty);
                    openLocalExeFolderToolStripMenuItem_Click(null, null);
                }
            }

            if (Properties.Settings.Default.runexewhendone)
            {
                if (string.IsNullOrWhiteSpace(exeFilepath) || !File.Exists(exeFilepath))
                {
                    AddLog(string.Format("Couldn't find exe file: {0}", exeFilepath));
                }
                else
                {
                    AddLog(string.Format("Running exe: {0}", exeFilepath));

                    try
                    {
                        var startInfo = new ProcessStartInfo();
                        startInfo.WorkingDirectory = exeFolder;
                        startInfo.FileName = exeFilepath;

                        if (!string.IsNullOrEmpty(Properties.Settings.Default.arguments))
                        {
                            startInfo.Arguments = Properties.Settings.Default.arguments;
                        }

                        Process.Start(startInfo);
                    }
                    catch (Exception err)
                    {
                        AddLog(string.Format("Failed to run exe. {0}", err.Message));
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.runfilewhendone))
            {
                //RUN SPECIFIC FILE WHEN DONE.
                string fileToRun = Properties.Settings.Default.runfilewhendone;

                if (!File.Exists(fileToRun))
                {
                    AddLog(string.Format("Cannot find file: {0}", fileToRun));
                }
                else
                {
                    try
                    {
                        string workingFolder = Path.GetDirectoryName(fileToRun);

                        var startInfo = new ProcessStartInfo();
                        startInfo.FileName = fileToRun;
                        startInfo.WorkingDirectory = workingFolder;
                        Process.Start(startInfo);
                        AddLog(string.Format("Running: {0}", fileToRun));
                    }
                    catch (Exception err)
                    {
                        AddLog(string.Format("Failed to run file: {0}", err.Message));
                    }
                }
            }

            if (Properties.Settings.Default.exitwhendone)
            {
                //Exit when done is ON. So, exit here.
                Application.Exit();
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

            progressBar1.Value = 25;

            AddLog("---- Starting step 1 of 2: downloading latest game data ----");

            if (Properties.Settings.Default.useSVN)
            {
                backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += OnDownloadDoWork;
                backgroundWorker.RunWorkerCompleted += OnDownloadCompleted;
                backgroundWorker.RunWorkerAsync();
            }
            else
            {
                AddLog(string.Empty);
                AddLog(">>>>>>>> SKIPPING DOWNLOAD STEP <<<<<<<<");
                AddLog("Note: to re-enable downloading latest data, click 'Use SVN source control' in the Options menu.");
                AddLog(string.Empty);

                StartCompile();
            }
        }       

        private bool SanityCheck()
        {
            //Sanity check all the options.
            bool success = true;
            if (!Directory.Exists(Properties.Settings.Default.localFolderpath))
            {
                AddLog("Error: failed to find game code folder. Please go to File > Options");
                success = false;
            }

            if (!File.Exists(Properties.Settings.Default.solutionfile))
            {
                AddLog("Error: failed to find code solution file. Please go to File > Options");
                success = false;
            }

            if (!File.Exists(Properties.Settings.Default.svnExecutable))
            {
                AddLog("Error: failed to find svn executable. Please go to File > Options");
                success = false;
            }

            if (!File.Exists(Properties.Settings.Default.compilerExecutable))
            {
                AddLog("Error: failed to find code compiler executable. Please go to File > Options");
                success = false;
            }

            if (!success)
                listBox1.BackColor = Color.Pink;

            return success;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                if (backgroundWorker != null)
                {
                    if (backgroundWorker.IsBusy)
                    {
                        backgroundWorker.WorkerSupportsCancellation = true;
                        backgroundWorker.CancelAsync();
                    }
                }

                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        

        protected void MyClosingHandler(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                //If build is happening, intercept the close command and cancel the build process.

                bool exit = true;

                if (backgroundWorker != null)
                {
                    if (backgroundWorker.IsBusy)
                    {
                        backgroundWorker.WorkerSupportsCancellation = true;
                        backgroundWorker.CancelAsync();
                        e.Cancel = true;
                        exit = false;
                    }
                }

                if (exit)
                {
                    Properties.Settings.Default.Save();
                    base.OnClosing(e);
                    return;
                }
            }
            else
            {
                Properties.Settings.Default.Save();
                base.OnClosing(e);
            }
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

       

        

        

        private void SetButtonsEnabled(bool value)
        {
            button1.Enabled = value;
            fileToolStripMenuItem.Enabled = value;
            viewChangelogsToolStripMenuItem.Enabled = value;

            button1.Text = (value ? "Create build." : "Please wait...");            
        }





        private void AddLog(string text)
        {
            listBox1.Items.Add(text);

            int nItems = (int)(listBox1.Height / listBox1.ItemHeight);
            listBox1.TopIndex = listBox1.Items.Count - nItems;

            this.Update();
            this.Refresh();
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
                    if (backgroundWorker.CancellationPending)
                    {
                        //Cancel.
                        e.Result = false;
                        return;
                    }

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
                AddLogInvoke("Error: SVN retrieve changelog failed.");
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
                AddLog(string.Format("Error: {0}", e.Error));
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
                    AddLog("Error: view changelog failed.");
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
                listBox1.BackColor = Color.Pink;
                AddLog(err.Message);
                return;
            }

            AddLog(string.Format("Opened local exe folder {0}", exeFolder));
        }
    }
}
