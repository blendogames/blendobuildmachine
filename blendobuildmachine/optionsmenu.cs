using System;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Drawing;

namespace blendobuildmachine
{
    public partial class optionsmenu : Form
    {
        public optionsmenu()
        {
            InitializeComponent();

            textBox_compilerexecutable.Text = Properties.Settings.Default.compilerExecutable;
            textBox_localpath.Text = Properties.Settings.Default.localFolderpath;
            textBox_repositoryurl.Text = Properties.Settings.Default.repositoryUrl;
            textBox_svnexecutable.Text = Properties.Settings.Default.svnExecutable;
            textBox_solutionfile.Text = Properties.Settings.Default.solutionfile;

            string varsList = Properties.Settings.Default.environmentVars.Replace(",", Environment.NewLine);
            textBox_environmentVars.Text = varsList;

            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(OnClickCloseButton);

            textBox_localpath.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_svnexecutable.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_solutionfile.TextChanged += new EventHandler(textBox_TextChanged);
            textBox_compilerexecutable.TextChanged += new EventHandler(textBox_TextChanged);

            checkBox_buildonstart.Checked = Properties.Settings.Default.runonstart;
            checkBox_openfolderwhendone.Checked = Properties.Settings.Default.openexefolder;
            checkBox_runexewhendone.Checked = Properties.Settings.Default.runexewhendone;
            checkBox_verbose.Checked = Properties.Settings.Default.verbose;
            checkBox_beep.Checked = Properties.Settings.Default.playbeep;
            checkBox_exitwhendone.Checked = Properties.Settings.Default.exitwhendone;
            textBox_runfilewhendone.Text = Properties.Settings.Default.runfilewhendone;
            textBox_arguments.Text = Properties.Settings.Default.arguments;

            DoSanityCheck(false);
        }

        private void textBox_TextChanged(Object sender, EventArgs e)
        {
            ((TextBox)sender).BackColor = Color.White;
        }


        private void buttonOptionsOk_Click(object sender, EventArgs e)
        {
            if (DoSanityCheck(true))
            {
                this.Close();
            }
        }

        private void OnClickCloseButton(object sender, FormClosingEventArgs e)
        {
            DoSanityCheck(true);
        }

        private bool DoSanityCheck(bool doPopups)
        {
            //Save the settings.
            Properties.Settings.Default.compilerExecutable = textBox_compilerexecutable.Text;
            Properties.Settings.Default.localFolderpath = textBox_localpath.Text;
            Properties.Settings.Default.repositoryUrl = textBox_repositoryurl.Text;
            Properties.Settings.Default.svnExecutable = textBox_svnexecutable.Text;
            Properties.Settings.Default.solutionfile = textBox_solutionfile.Text;

            string varsList = textBox_environmentVars.Text.Replace(Environment.NewLine, ",");
            Properties.Settings.Default.environmentVars = varsList;

            Properties.Settings.Default.runonstart = checkBox_buildonstart.Checked;
            Properties.Settings.Default.openexefolder = checkBox_openfolderwhendone.Checked;
            Properties.Settings.Default.runexewhendone = checkBox_runexewhendone.Checked;
            Properties.Settings.Default.verbose = checkBox_verbose.Checked;
            Properties.Settings.Default.playbeep = checkBox_beep.Checked;
            Properties.Settings.Default.exitwhendone = checkBox_exitwhendone.Checked;
            Properties.Settings.Default.runfilewhendone = textBox_runfilewhendone.Text;
            Properties.Settings.Default.arguments = textBox_arguments.Text;

            Properties.Settings.Default.Save();

            bool success = true;
            string errorMessage = string.Empty;

            //Sanity check.
            if (!Directory.Exists(Properties.Settings.Default.localFolderpath))
            {
                textBox_localpath.BackColor = Color.Pink;
                errorMessage += "- Failed to find local game folder.\n\n";                
                success = false;
            }
            else
            {
                textBox_localpath.BackColor = Color.White;
            }

            if (!File.Exists(Properties.Settings.Default.solutionfile))
            {
                textBox_solutionfile.BackColor = Color.Pink;
                errorMessage += "- Failed to find code solution file.\n\n";                
                success = false;                
            }
            else
            {
                textBox_solutionfile.BackColor = Color.White;
            }

            if (!File.Exists(Properties.Settings.Default.svnExecutable))
            {
                textBox_svnexecutable.BackColor = Color.Pink;
                errorMessage += "- Failed to find svn executable.\n\n";                
                success = false;                
            }
            else
            {
                textBox_svnexecutable.BackColor = Color.White;
            }

            if (!File.Exists(Properties.Settings.Default.compilerExecutable))
            {
                textBox_compilerexecutable.BackColor = Color.Pink;
                errorMessage += "- Failed to find code compiler executable.\n\n";                
                success = false;                
            }
            else
            {
                textBox_compilerexecutable.BackColor = Color.White;
            }

            if (doPopups && !success)
            {
                MessageBox.Show(string.Format("Errors found:\n\n{0}", errorMessage), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return success;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Attempt to auto-find svn.exe

            if (File.Exists(textBox_svnexecutable.Text))
            {
                MessageBox.Show(string.Format("Field already has valid value:\n\n{0}", textBox_svnexecutable.Text), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Search for common places.
            string[] paths = new string[]
            {                
                "C:\\Program Files\\TortoiseSVN\\bin\\svn.exe",
                "C:\\Program Files\\SlikSvn\\bin\\svn.exe",
                "C:\\Program Files\\CollabNet\\Subversion Client\\svn.exe",
            };

            for (int i = 0; i < paths.Length; i++)
            {
                if (File.Exists(paths[i]))
                {
                    textBox_svnexecutable.Text = paths[i];
                    textBox_svnexecutable.BackColor = Color.GreenYellow;
                    return;
                }
            }

            MessageBox.Show("Unable to auto-find svn.exe\n\nPlease search your computer for 'svn.exe'");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Attempt to auto-find msbuild.exe

            if (File.Exists(textBox_compilerexecutable.Text))
            {
                MessageBox.Show(string.Format("Field already has valid value:\n\n{0}", textBox_compilerexecutable.Text), string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //Search for common places.
            string[] paths = new string[]
            {                
                "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\msbuild.exe",
                "C:\\Program Files (x86)\\MSBuild\\14.0\\Bin\\msbuild.exe",
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\Community\\MSBuild\\15.0\\Bin\\msbuild.exe",
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\BuildTools\\MSBuild\\15.0\\Bin\\msbuild.exe",
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\2017\\BuildTools\\MSBuild\\15.0\\Bin\\amd64\\msbuild.exe",
            };

            for (int i = 0; i < paths.Length; i++)
            {
                if (File.Exists(paths[i]))
                {
                    textBox_compilerexecutable.Text = paths[i];
                    textBox_compilerexecutable.BackColor = Color.GreenYellow;
                    return;
                }
            }

            MessageBox.Show("Unable to auto-find msbuild.exe\n\nPlease search your computer for 'msbuild.exe'");
        }
    }
}
