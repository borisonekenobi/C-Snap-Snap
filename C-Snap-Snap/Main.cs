using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace C_Snap_Snap
{
    public partial class Main : Form
    {
        private readonly Color primary = Color.FromArgb(141, 35, 15);
        private readonly Color secondary = Color.FromArgb(30, 67, 76);
        private readonly Color accent = Color.FromArgb(155, 79, 15);
        private readonly Color accentSecondary = Color.FromArgb(201, 158, 16);
        public static TabControl Files { get; set; } = new TabControl();

        private bool initDone = false;
        private int prevIndex = 0;
        private bool resettingLanguage = false;
        private List<string> filePaths = new List<string>();
        private readonly List<Block> blocks = new List<Block>();

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Files.Size = new Size(splitContainer2.Panel2.Width, splitContainer2.Panel2.Height);
            Files.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            Files.Visible = false;
            splitContainer2.Panel2.Controls.Add(Files);

            splitContainer2.Panel2.BackColor = secondary;
            splitContainer2.Panel1.BackColor = accent;
            splitContainer1.Panel1.BackColor = secondary;
            BlockCloser.BackColor = secondary;
            Language.SelectedIndex = 0;

            for (int i = 0; i < Blocks.TabCount; i++)
            {
                Blocks.SelectedIndex = i;
                Blocks.SelectedTab.BackColor = secondary;
            }
            Blocks.SelectedIndex = 0;

            initDone = true;
        }

        private void SplitContainer2_SplitterMoved(object sender, SplitterEventArgs e)
        {
            if (splitContainer2.SplitterDistance <= 50)
            {
                splitContainer2.Panel1Collapsed = true;
                BlockCloser.Text = ">";
            }
            else
            {
                splitContainer2.Panel1Collapsed = false;
                BlockCloser.Text = "<";
            }
        }

        private void BlockCloser_Click(object sender, EventArgs e)
        {
            if (splitContainer2.Panel1Collapsed)
            {
                splitContainer2.Panel1Collapsed = false;
                BlockCloser.Text = "<";
            }
            else
            {
                splitContainer2.Panel1Collapsed = true;
                BlockCloser.Text = ">";
            }
        }

        private void Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!initDone) return;
            if (resettingLanguage)
            {
                resettingLanguage = false;
                return;
            }

            if (Files.SelectedIndex != -1)
            {
                var saveChanges = MessageBox.Show("Any unsaved work will be deleted! Would you like to save changes?", "Warning!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if (saveChanges == DialogResult.Cancel)
                {
                    resettingLanguage = true;
                    Language.SelectedIndex = prevIndex;
                    return;
                }
            }

            if (Language.SelectedIndex == 0)
            {
                Files.Visible = false;
                Export.Enabled = false;
                prevIndex = 0;
            }
            else
            {
                Files.Visible = true;
                AddFile(sender, e);
                Export.Enabled = true;
                prevIndex = Language.SelectedIndex;
            }
        }

        private void AddFile(object sender, EventArgs e)
        {
            if (!Files.Visible) return;

            SaveFileDialog sfd = new SaveFileDialog
            {
                RestoreDirectory = true,
                Filter = "C-Snap-Snap file (*.snap)|*.snap|All files (*.*)|*.*"
            };
            var isFileSelected = sfd.ShowDialog();
            if (isFileSelected == DialogResult.Cancel)
            {
                Language.SelectedIndex = prevIndex;
                return;
            }

            var file = new FileInfo(sfd.FileName);
            filePaths.Add(file.FullName);

            TabPage newFile = new TabPage
            {
                Name = sfd.FileName,
                Text = file.Name,
                BackColor = primary,
                ForeColor = primary
            };
            newFile.MouseClick += new MouseEventHandler(Files_MouseClick);
            newFile.MouseDoubleClick += new MouseEventHandler(Files_MouseClick);
            newFile.Paint += new PaintEventHandler(NewFiles_Paint);
            Files.TabPages.Add(newFile);
            Files.SelectedIndex = Files.TabCount - 1;

            File.WriteAllText(sfd.FileName, "TESTING C++ FILE");
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

        private void NewFiles_Paint(object sender, PaintEventArgs e)
        {
            foreach (var block in blocks)
            {
                block.Draw(e);
            }
        }

        private void Files_MouseClick(object sender, MouseEventArgs e)
        {
            Point pos = MousePosition;
            blocks.Add(new Variable(filePaths[Files.SelectedIndex], null, null, new Position(pos), "int", "x", "5"));
            Files.SelectedTab.Invalidate();
        }
    }
}