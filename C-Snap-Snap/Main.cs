using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace C_Snap_Snap
{
    public partial class Main : Form
    {
        private readonly Point MouseConstant = new Point(177, 70);
        private readonly Color primary = Color.FromArgb(141, 35, 15);
        private readonly Color secondary = Color.FromArgb(30, 67, 76);
        private readonly Color accent = Color.FromArgb(155, 79, 15);
        private readonly Color accentSecondary = Color.FromArgb(201, 158, 16);

        public static TabControl Files { get; set; } = new TabControl();

        private Point MousePos;
        private bool initDone = false;
        private int prevIndex = 0;
        private bool resettingLanguage = false;
        private readonly List<string> filePaths = new List<string>();
        private readonly List<Block> blocks = new List<Block>();
        private Block selectedBlock;
        private Point distanceFromMouse = new Point(0, 0);
        private int counter = 1;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Files.Size = new Size(splitContainer2.Panel2.Width, splitContainer2.Panel2.Height);
            Files.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //Files.SetStyle(ControlStyles.DoubleBuffered, true);
            //Files.DoubleBuffered = true;
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

            blocks.Add(new Variable(Blocks.TabPages[0].Name, new Point(0, 0), true)); // need to adjust position
            blocks.Add(new IfStatement(Blocks.TabPages[1].Name, new Point(0, 0), true));
            //TODO: IfElseStatement - TabPages[1]
            //TODO: IfElifElseStatement - TabPages[1]
            //TODO: ForLoop - TabPages[2]
            //TODO: WhileLoop - TabPages[2]
            //TODO: DoWhileLoop - TabPages[2]
            //TODO: MathExpressions - TabPages[3]
            Blocks.SelectedTab.Invalidate();

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

            foreach (TabPage currentFile in Files.TabPages)
            {
                if (currentFile.Name == sfd.FileName)
                {
                    Files.SelectedTab = currentFile;
                    return;
                }
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
            newFile.MouseDown += new MouseEventHandler(Files_MouseDown);
            newFile.MouseUp += new MouseEventHandler(Files_MouseUp);
            newFile.Paint += new PaintEventHandler(NewFiles_Paint);
            newFile.MouseMove += new MouseEventHandler(NewFile_MouseMove);
            //newFile.DoubleBuffered = true;
            Files.TabPages.Add(newFile);
            Files.SelectedIndex = Files.TabCount - 1;

            blocks.Add(new Function(Files.SelectedTab.Name, null, null, new Point(30, 30), "int", "main", null, false));

            File.WriteAllText(sfd.FileName, "TESTING SNAP FILE");
        }

        private string FileExt()
        {
            switch (Language.Text)
            {
                case "C": return ".c";
                case "C#": return ".cs";
                case "C++": return ".cpp";
                    //TODO: add more languages
            }
            return Language.Text;
        }

        private void NewFiles_Paint(object sender, PaintEventArgs e)
        {
            foreach (var block in blocks)
            {
                if (block.File == Files.SelectedTab.Name || block.File == Blocks.SelectedTab.Name) block.Draw(e.Graphics, block == selectedBlock);
            }
            /*using (Pen pen = new Pen(Color.Blue, 3))
            {
                int size = 3;
                e.Graphics.DrawEllipse(pen, new Rectangle(MousePos.X - size, MousePos.Y - size, 2 * size, 2 * size));
                if (selectedBlock != null) e.Graphics.DrawEllipse(pen, new Rectangle(selectedBlock.Pos.X - size, selectedBlock.Pos.Y - size, 2 * size, 2 * size));
            }*/
        }

        private void NewFile_MouseMove(object sender, MouseEventArgs e)
        {
            MousePos = PointToClient(new Point(Cursor.Position.X - MouseConstant.X, Cursor.Position.Y - MouseConstant.Y)); // need to change MouseConstant to change dynamically with screen
            if (selectedBlock == null) return;
            selectedBlock.Pos = new Point(e.X + distanceFromMouse.X, e.Y + distanceFromMouse.Y);
            UpdatePos();
            Files.SelectedTab.Invalidate();
        }

        private void Files_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (blocks.Count == 0) return;
                foreach (var block in blocks)
                {
                    if (block.IsHover(MousePos))
                    {
                        if (block.File == Files.SelectedTab.Name || block.File == Blocks.SelectedTab.Name) selectedBlock = block;
                    }
                }

                if (selectedBlock != null)
                {
                    if (selectedBlock.IsDefault)
                    {
                        selectedBlock = selectedBlock.Clone();
                        blocks.Add(selectedBlock);
                    }
                    selectedBlock.UnSnap();
                    distanceFromMouse = new Point(selectedBlock.Pos.X - MousePos.X, selectedBlock.Pos.Y - MousePos.Y);
                }
            }
        }

        private void Files_MouseUp(object sender, MouseEventArgs e)
        {
            if (selectedBlock == null) return;
            foreach (var block in blocks)
            {
                if (selectedBlock == block) continue;
                if (block.IsHover(MousePos))
                {
                    selectedBlock.SnapTo(block);
                    Files.SelectedTab.Invalidate();
                    break;
                }
            }
            selectedBlock = null;
        }

        private void UpdatePos()
        {
            selectedBlock.UpdatePos(new Point(MousePos.X + distanceFromMouse.X, MousePos.Y + distanceFromMouse.Y));
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            var saveChanges = MessageBox.Show("Any unsaved work will be deleted! Would you like to save changes?", "Warning!", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (saveChanges == DialogResult.Cancel) e.Cancel = true;
            else if (saveChanges == DialogResult.Yes)
            {
                // save changes to .snap file
            }
        }

        // temporary method because blocks are drawn on main drawing area
        private void Blocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            Files.SelectedTab?.Invalidate();
        }

        private void Export_Click(object sender, EventArgs e)
        {
            foreach (TabPage file in Files.TabPages)
            {
                string output = "#include <iostream>\nusing namespace std;\n";
                
                foreach(Block b in blocks)
                {
                    if (b is Function) output += ExportFunction(b as Function);
                }

                int index = file.Name.LastIndexOf(".");
                File.WriteAllText(file.Name.Substring(0, index) + FileExt(), output);
            }
        }

        private string ExportBlock(Block b)
        {
            if (b is Variable) return ExportVariable(b as Variable);
            else if (b is IfStatement) return ExportIfStatement(b as IfStatement);
            else if (b is Function) return ExportFunction(b as Function);
            // will add more
            else return b.ToString();
        }

        private string ExportVariable(Variable block)
        {
            return new string('\t', counter) + block.ToString();
        }

        private string ExportIfStatement(IfStatement block)
        {
            string output = new string('\t', counter) + block.ToString();

            if (block.Inside == null) return output += new string('\t', counter) + "}\n";
            counter++;
            Block next = block.Inside;
            output += ExportBlock(next);
            while (next.Next != null)
            {
                next = next.Next;
                output += ExportBlock(next);
            }
            counter--;
            output += new string('\t', counter) + "}\n";

            return output;
        }

        private string ExportFunction(Function block)
        {
            string output = block.ToString();
            Block next = block;

            while (next.Next != null)
            {
                next = next.Next;
                output += ExportBlock(next);
            }

            return output + "}";
        }
    }
}