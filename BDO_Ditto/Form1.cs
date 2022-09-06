using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace BDO_Ditto
{
    public partial class MainForm : Form
    {
        private readonly BdoAppearanceSwapper _appearanceSwapper = new BdoAppearanceSwapper();
        private readonly List<string> _sectionsToCopy = new List<string>();

        public MainForm()
        {
            InitializeComponent();

            Text = string.Format("Ditto (v{0})", Application.ProductVersion);
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        private string GetBDOCustomizationFolder()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "Black Desert",
                "Customization"
            );
            return path;
        }

        private string OpenCustomizationTemplateBrowser(string title)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            string baseDir = GetBDOCustomizationFolder();
            if (Directory.Exists(baseDir))
            {
                openDialog.InitialDirectory = GetBDOCustomizationFolder();
            }

            openDialog.Title = title;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                return openDialog.FileName;
            }
            return string.Empty;
        }

        private void Btt_SourceBrowse_Click(object sender, EventArgs e)
        {
            string path = OpenCustomizationTemplateBrowser("Source Template File");

            if (path != "")
            {
                Tb_SourcePath.Text = path;

                if (_appearanceSwapper.LoadSource(path))
                {
                    Gb_Source.Text = string.Format("Source Template ({0})", _appearanceSwapper.GetSourceClassStr());
                }
            }

            Btt_CopySections.Enabled = _appearanceSwapper.IsSourceAndTragetAppearanceLoaded();
        }

        private void Btt_TargetBrowse_Click(object sender, EventArgs e)
        {
            string path = OpenCustomizationTemplateBrowser("Target Template File");

            if (path != "")
            {
                Tb_TargetPath.Text = path;

                if (_appearanceSwapper.LoadTarget(path))
                {
                    Gb_Target.Text = string.Format("Target Template ({0})", _appearanceSwapper.GetTargetClassStr());
                }
            }

            Btt_CopySections.Enabled = _appearanceSwapper.IsSourceAndTragetAppearanceLoaded();
        }

        // Open the help web page
        private void MainForm_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            Process.Start("http://goomichan.github.io/BDO-Ditto/Index.html");
        }

        private void Btt_CopySections_Click(object sender, EventArgs e)
        {
            List<string> sectionsToCopy = new List<string>(_sectionsToCopy);
            PrintSectionsToCopy();
            _appearanceSwapper.CopySectionsToTarget(sectionsToCopy);
        }

        // Global handler for selecting what sections to copy
        private void AppearanceSectionsCheckedHandler(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(CheckBox))
            {
                CheckBox cb = (CheckBox)sender;
                if (cb.Name.Contains("Cb_") || cb.Name.Length > 3)
                {
                    string sectionName = cb.Name.Substring(3);
                    //Debug.WriteLine(string.Format("Checkbox {0} set to {1}. section name: {2}", cb.Name, cb.Checked, sectionName));

                    if (cb.Checked == false && _sectionsToCopy.Contains(sectionName))
                    {
                        _sectionsToCopy.Remove(sectionName);
                        Debug.WriteLine(string.Format("Removed section {0} from copy list", sectionName));
                    }
                    else if (cb.Checked && !_sectionsToCopy.Contains(sectionName))
                    {
                        _sectionsToCopy.Add(sectionName);
                        Debug.WriteLine(string.Format("Added section {0} to copy list", sectionName));
                    }
                }
                else
                {
                    Debug.Fail(string.Format("Checkbox {0} was not prefixed with Cb_ but had the appearance handler assigned D:", cb.Name));
                }
            }
        }

        [Conditional("DEBUG")]
        private void PrintSectionsToCopy()
        {
            Debug.WriteLine("Copying {0} sections: ", _sectionsToCopy.Count);
            Debug.WriteLine("-----------------------------------------------------------");
            Debug.WriteLine("{0,-20}", "Section Name");
            Debug.WriteLine("-----------------------------------------------------------");
            foreach (string section in _sectionsToCopy)
            {
                Debug.WriteLine("{0,-20} {1,-10} {2,-10}", section);
            }
            Debug.WriteLine("-----------------------------------------------------------");
        }
    }
}
