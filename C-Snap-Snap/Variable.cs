using System.Drawing;

namespace C_Snap_Snap
{
    internal class Variable : Block
    {
        private static readonly Size size = new Size(100, 30);
        public override SolidBrush Brush {
            get { return new SolidBrush(Color.FromArgb(255, 140, 26)); } 
        }
        public override bool Indent
        {
            get { return false; }
        }
        public override int Mimi
        {
            get { return size.Height; }
        }
        public override Rectangle[] Rectangles { get; set; }

        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Variable(string file, Point pos) : this(file, null, null, pos, "null", "null", "null", false)
        { }

        public Variable(string file, Point pos, bool isDefault) : this(file, null, null, pos, "null", "null", "null", isDefault)
        { }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value) : this(file, next, prev, pos, type, name, value, false)
        { }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value, bool isDefault) : base(file, next, prev, pos)
        {
            this.Next = next;
            this.Prev = prev;
            this.Pos = pos;
            this.Type = type;
            this.Name = name;
            this.Value = value;
            this.IsDefault = isDefault;

            Rectangles = new Rectangle[1];
            Rectangles[0] = new Rectangle(pos.X, pos.Y, size.Width, size.Height);
        }

        public override void Draw(Graphics g, bool isSelected)
        {
            g.FillRectangles(Brush, Rectangles);
            if (isSelected) g.DrawRectangles(Highlight, Rectangles);
        }

        public override void UpdatePos(Point pos)
        {
            Pos = pos;
            Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Next?.UpdatePos(new Point(Rectangles[0].Left, Bottom));
        }

        public override void SnapTo(Block block, int section)
        {
            Snap.Play();

            if (this.Next != null) block.GetLast().SnapTo(this.Next, 0);

            this.Next = block;
            block.Prev = this;
        }

        public override void UnSnap()
        {
            if (Prev == null) return;

            Unsnap.Play();
            Prev.UnSnapFrom(this);
            Prev = null;
        }

        public override void UnSnapFrom(Block block)
        {
            if (Next == null) return;
            if (Next == block) Next = null;
        }

        public override Block Clone()
        {
            return new Variable(Main.Files.SelectedTab.Name, Next, Prev, Pos, Type, Name, Value, false);
        }

        public override string ToString()
        {
            return string.Format("{0} {1} = {2};\n", Type, Name, Value);
        }
    }
}