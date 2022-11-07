using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Snap_Snap
{
    public partial class Main : Form
    {
        private bool initDone = false;
        private int prevIndex = 0;
        private bool resettingLanguage = false;
        private string path;
        private readonly Color primary = Color.FromArgb(141, 35, 15);
        private readonly Color secondary = Color.FromArgb(30, 67, 76);
        private readonly Color accent = Color.FromArgb(155, 79, 15);
        private readonly Color accentSecondary = Color.FromArgb(201, 158, 16);

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            splitContainer2.Panel2.BackColor = secondary;
            splitContainer2.Panel1.BackColor = accent;
            splitContainer1.Panel1.BackColor = secondary;
            blockCloser.BackColor = secondary;
            language.SelectedIndex = 0;

            for (int i = 0; i < blocks.TabCount; i++)
            {
                blocks.SelectedIndex = i;
                blocks.SelectedTab.BackColor = secondary;
            }
            blocks.SelectedIndex = 0;

            initDone = true;
        }

        private void splitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer2.SplitterDistance <= 50)
            {
                splitContainer2.Panel1Collapsed = true;
                blockCloser.Text = ">";
            }
            else
            {
                splitContainer2.Panel1Collapsed = false;
                blockCloser.Text = "<";
            }
        }

        private void blockCloser_Click(object sender, EventArgs e)
        {
            if (splitContainer2.Panel1Collapsed)
            {
                splitContainer2.Panel1Collapsed = false;
                blockCloser.Text = "<";
            }
            else
            {
                splitContainer2.Panel1Collapsed = true;
                blockCloser.Text = ">";
            }
        }

        private void language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initDone) return;
            if (resettingLanguage)
            {
                resettingLanguage = false;
                return;
            }

            if (files.Visible)
            {
                /*var saveChanges = MessageBox.Show("Would you like to save any changes?", "Warning!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (saveChanges == DialogResult.Cancel)
                {
                    resettingLanguage = true;
                    language.SelectedIndex = prevIndex;
                    return;
                }*/
            }

            if (language.SelectedIndex == 0)
            {
                files.Visible = false;
                export.Enabled = false;
                prevIndex = 0;
            }
            else
            {
                files.Visible = true;
                AddFile(sender, e);
                export.Enabled = true;
                prevIndex = language.SelectedIndex;
            }
        }

        private void AddFile(object sender, EventArgs e)
        {
            if (!files.Visible) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                //DefaultExt = FileExt(language.SelectedText),
                Filter = FileExt(language.SelectedItem.ToString()) + "All files (*.*)|*.*"
            };
            //sfd.InitialDirectory = path;
            var isFileSelected = sfd.ShowDialog();
            if (isFileSelected == DialogResult.Cancel)
            {
                language.SelectedIndex = prevIndex;
                return;
            }

            var file = new FileInfo(sfd.FileName);

            TabPage newFile = new TabPage
            {
                Name = sfd.FileName,
                Text = file.Name,
                BackColor = primary,
                ForeColor = primary
            };
            files.TabPages.Add(newFile);
            File.WriteAllText(sfd.FileName, "your data here...");
        }

        private string FileExt(string language)
        {
            switch (language)
            {
                case "C": return "C (*.c;*.i)|*.c;*.i|";
                case "C#": return "C# (*.cs;*.csx;*.cake)|*.cs;*.csx;*.cake|";
                case "C++": return "C++ (*.cpp;*.cc;*.cxx;*.c++;*.hpp;*.hh;*.h++;*.h;*.ii)|*.cpp;*.cc;*.cxx;*.c++;*.hpp;*.hh;*.h++;*.h;*.ii|";
                //TODO: add more languages
            }
            return language;
        }
    }
}