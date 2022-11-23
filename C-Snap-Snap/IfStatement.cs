using System.Drawing;

namespace C_Snap_Snap
{
    internal class IfStatement : Block
    {
        private static readonly Size size = new Size(100, 70);
        public override SolidBrush Brush
        {
            get
            {
                return new SolidBrush(Color.FromArgb(255, 171, 25));
            }
        }
        public override bool Indent
        {
            get { return true; }
        }

        private int numInside;
        private string condition;

        public Block Inside { get; set; }

        public IfStatement(string file, Point pos) : this(file, null, null, pos, null, "null", false)
        { }

        public IfStatement(string file, Point pos, bool isDefault) : this(file, null, null, pos, null, "null", isDefault)
        { }
        
        public IfStatement(string file, Block next, Block prev, Point pos, Block inside, string condition ,bool isDefault) : base(file, next, prev, pos)
        {
            this.Next = next;
            this.Prev = prev;
            this.Pos = pos;
            this.Inside = inside;
            this.condition = condition;
            this.IsDefault = isDefault;

            Rectangles.Add(new Rectangle(pos.X, pos.Y, 100, 30));
            Rectangles.Add(new Rectangle(pos.X, pos.Y + 30, 10, 30));
            Rectangles.Add(new Rectangle(pos.X, pos.Y + 30 * 2, 100, 10));
        }

        public override void Draw(Graphics g, bool isSelected)
        {
            g.FillRectangles(Brush, Rectangles.ToArray());
            if (isSelected) g.DrawRectangles(Highlight, Rectangles.ToArray());
        }

        public override void UpdatePos(Point pos)
        {
            Pos = pos;
            Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Rectangles[1] = new Rectangle(pos.X, pos.Y + 30, Rectangles[1].Width, Rectangles[1].Height);
            Rectangles[2] = new Rectangle(pos.X, pos.Y + 30 * 2, Rectangles[2].Width, Rectangles[2].Height);
            Inside?.UpdatePos(new Point(Rectangles[1].Right, Rectangles[0].Bottom));
            Next?.UpdatePos(new Point(Rectangles[Rectangles.Count - 1].Left, Bottom));
        }

        public override Block Clone()
        {
            return new IfStatement(Main.Files.SelectedTab.Name, Next, Prev, Pos, Inside, condition, false);
        }

        public override string ToString()
        {
            return string.Format("if ({0}) {{\n", condition);
        }
    }
}
