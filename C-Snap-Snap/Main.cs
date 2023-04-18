using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace C_Snap_Snap
{
    public partial class Main : Form
    {
        private readonly Point MouseConstant = new Point(177, 100);

        private readonly Color formBackground = Color.FromArgb(51, 51, 51);
        private readonly Color menuStripText = Color.FromArgb(204, 204, 204);
        private readonly Color menuStripItemText = Color.FromArgb(204, 204, 204);
        private readonly Color menuStripItemSelectedText = Color.FromArgb(255, 255, 255);
        private readonly Color mainBackground = Color.FromArgb(30, 30, 30);
        private readonly Color sidePanelBackground = Color.FromArgb(37, 37, 38);
        private readonly Color filesBackground = Color.FromArgb(45, 45, 45);

        public static TabControls.MainPanel Files { get; set; } = new TabControls.MainPanel();

        private Point MousePos;
        private bool initDone = false;
        private bool openingFile = false;
        private int prevIndex = -1;
        private bool resettingLanguage = false;
        private readonly List<string> filePaths = new List<string>();
        private readonly List<Block> blocks = new List<Block>();
        private Block selectedBlock;
        private Point distanceFromMouse = new Point(0, 0);
        private int counter = 1;

        public Main()
        {
            InitializeComponent();
            MenuStrip.Renderer = new ToolStripProfessionalRenderer(new MenuStripColorTable());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = formBackground;

            MenuStrip.ForeColor = menuStripText;
            foreach (ToolStripMenuItem tab in MenuStrip.Items)
            {
                foreach (ToolStripItem button in tab.DropDownItems)
                {
                    button.ForeColor = menuStripItemText;
                }
            }

            Files.Size = new Size(splitContainer2.Panel2.Width, splitContainer2.Panel2.Height);
            Files.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            //Files.SetStyle(ControlStyles.DoubleBuffered, true);
            //Files.DoubleBuffered = true;
            Files.Visible = false;

            splitContainer2.Panel2.Controls.Add(Files);

            splitContainer2.Panel2.BackColor = mainBackground;
            splitContainer2.Panel1.BackColor = sidePanelBackground;
            splitContainer1.Panel1.BackColor = sidePanelBackground;
            BlockCloser.BackColor = mainBackground;
            Language.SelectedIndex = 0;

            for (int i = 0; i < Blocks.TabCount; i++)
            {
                Blocks.SelectedIndex = i;
                Blocks.SelectedTab.BackColor = sidePanelBackground;
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
            if (openingFile) return;

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
                BackColor = mainBackground,
                ForeColor = mainBackground
            };
            newFile.MouseDown += new MouseEventHandler(Files_MouseDown);
            newFile.MouseUp += new MouseEventHandler(Files_MouseUp);
            newFile.Paint += new PaintEventHandler(NewFiles_Paint);
            newFile.MouseMove += new MouseEventHandler(NewFile_MouseMove);
            //newFile.DoubleBuffered = true;
            Files.TabPages.Add(newFile);
            Files.SelectedIndex = Files.TabCount - 1;

            blocks.Add(new Function(Files.SelectedTab.Name, null, null, new Point(30, 30), "int", "main", null, false));

            File.WriteAllText(sfd.FileName, "");
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
                    if (block.IsHover(MousePos) >= 0)
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
                int section = block.IsHover(MousePos);
                if (section >= 0)
                {
                    block.SnapTo(selectedBlock, section);
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
            TabPage file = Files.SelectedTab;
            string output = "#include <iostream>\nusing namespace std;\n";

            foreach (Block b in blocks)
            {
                if (b is Function) output += ExportFunction(b as Function);
            }

            int index = file.Name.LastIndexOf(".");
            File.WriteAllText(file.Name.Substring(0, index) + FileExt(), output);
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

        private static Point GetPoint(string str)
        {
            string[] strs = str.Substring(1, str.Length - 2).Split(',');
            return new Point(int.Parse(strs[0]), int.Parse(strs[1]));
        }

        private void AddBlocksFromFile(OpenFileDialog ofd)
        {
            StreamReader reader = File.OpenText(ofd.FileName);
            List<Block> tempBlocks = new List<Block>();
            string line;
            string type = null;
            string[] components = null;
            int i = 0;
            while ((line = reader.ReadLine()) != null)
            {
                if (components != null)
                {
                    if (i == components.Length)
                    {
                        switch (type)
                        {
                            case "Variable": tempBlocks.Add(new Variable(ofd.FileName, null, null, GetPoint(components[3]), components[4], components[5], components[6], false)); break;
                            case "IfStatement": tempBlocks.Add(new IfStatement(ofd.FileName, null, null, GetPoint(components[3]), null, components[5], false)); break;
                            case "Function": tempBlocks.Add(new Function(ofd.FileName, null, null, GetPoint(components[3]), components[4], components[5], components[6] == "null" ? "" : components[6], false)); break;
                        }
                        components = null;
                        i = 0;
                        continue;
                    }
                    components[i] = line.Split(':')[1];
                    i++;
                }
                else if (line.StartsWith("Language:"))
                {
                    Language.SelectedItem = line.Split(':')[1];
                    if (Language.SelectedIndex == 0)
                    {
                        MessageBox.Show("Unsupported file language!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else if (line.StartsWith("\tVariable:"))
                {
                    type = "Variable";
                    components = new string[7];
                }
                else if (line.StartsWith("\tIfStatement"))
                {
                    type = "IfStatement";
                    components = new string[6];
                }
                else if (line.StartsWith("\tFunction"))
                {
                    type = "Function";
                    components = new string[7];
                }
            }

            blocks.AddRange(tempBlocks);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openingFile = true;
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Snap Files (*.snap)|*.snap|All Files (*.*)|*.*"
            };
            DialogResult isFileSelected = ofd.ShowDialog();
            if (isFileSelected == DialogResult.Cancel)
            {
                return;
            }
            foreach (TabPage currentFile in Files.TabPages)
            {
                if (currentFile.Name == ofd.FileName)
                {
                    Files.SelectedTab = currentFile;
                    return;
                }
            }

            AddBlocksFromFile(ofd);

            Files.Visible = true;

            FileInfo file = new FileInfo(ofd.FileName);
            filePaths.Add(file.FullName);

            TabPage newFile = new TabPage
            {
                Name = ofd.FileName,
                Text = file.Name,
                BackColor = formBackground,
                ForeColor = formBackground
            };
            newFile.MouseDown += new MouseEventHandler(Files_MouseDown);
            newFile.MouseUp += new MouseEventHandler(Files_MouseUp);
            newFile.Paint += new PaintEventHandler(NewFiles_Paint);
            newFile.MouseMove += new MouseEventHandler(NewFile_MouseMove);
            Files.TabPages.Add(newFile);
            Files.SelectedIndex = Files.TabCount - 1;
            openingFile = false;
            splitContainer2.Panel2.BackColor = filesBackground;
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export_Click(sender, e);
        }

        private void exportAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage current = Files.SelectedTab;
            foreach (TabPage file in Files.TabPages)
            {
                Files.SelectedTab = file;
                Export_Click(sender, e);
            }
            Files.SelectedTab = current;
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddFile(sender, e);
        }
    }

    public class MenuStripColorTable : ProfessionalColorTable
    {
        public override Color ToolStripDropDownBackground
        {
            get
            {
                return Color.FromArgb(30, 30, 30);
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return Color.FromArgb(30, 30, 30);
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return Color.FromArgb(30, 30, 30);
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return Color.FromArgb(30, 30, 30);
            }
        }

        public override Color MenuBorder
        {
            get
            {
                return Color.FromArgb(69, 69, 69);
            }
        }

        public override Color MenuItemBorder
        {
            get
            {
                return Color.FromArgb(30, 30, 30);
            }
        }

        public override Color MenuItemSelected
        {
            get
            {
                return Color.FromArgb(4, 57, 94);
            }
        }

        public override Color MenuStripGradientBegin
        {
            get
            {
                return Color.FromArgb(60, 60, 60);
            }
        }

        public override Color MenuStripGradientEnd
        {
            get
            {
                return Color.FromArgb(60, 60, 60);
            }
        }

        public override Color MenuItemSelectedGradientBegin
        {
            get
            {
                return Color.FromArgb(69, 70, 70);
            }
        }

        public override Color MenuItemSelectedGradientEnd
        {
            get
            {
                return Color.FromArgb(69, 70, 70);
            }
        }

        public override Color MenuItemPressedGradientBegin
        {
            get
            {
                return Color.FromArgb(69, 70, 70);
            }
        }

        public override Color MenuItemPressedGradientEnd
        {
            get
            {
                return Color.FromArgb(69, 70, 70);
            }
        }

        public override Color SeparatorDark
        {
            get
            {
                return Color.FromArgb(69, 69, 69);
            }
        }
    }
}