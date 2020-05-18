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

            DoSanityCheck(false);
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
    }
}
