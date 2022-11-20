using System;
using System.Drawing;
using System.Xml.Linq;

namespace C_Snap_Snap
{
    internal class IfStatement : Block
    {
        private static readonly Size size = new Size(100, 30);

        private Block inside;
        private int numInside;
        private string condition;

        public IfStatement(string file, Point pos) : this(file, null, null, pos, null, "null", false)
        { }

        public IfStatement(string file, Point pos, bool isDefault) : this(file, null, null, pos, null, "null", isDefault)
        { }
        
        public IfStatement(string file, Block next, Block prev, Point pos, Block inside, string condition ,bool isDefault) : base(file, next, prev, pos)
        {
            this.next = next;
            this.prev = prev;
            this.Pos = pos;
            this.inside = inside;
            this.condition = condition;
            this.IsDefault = isDefault;

            //color = Color.Orange;

            Rectangles.Add(new Rectangle(pos.X, pos.Y, size.Width, size.Height));
            Rectangles.Add(new Rectangle(pos.X, pos.Y + size.Height, 10, 30));
            Rectangles.Add(new Rectangle(pos.X, pos.Y + size.Height * 2, 100, 10));
        }

        public override void Draw(Graphics g, bool isSelected)
        {
            g.DrawRectangles(DrawPen, Rectangles.ToArray());
            if (isSelected) g.DrawRectangles(Highlight, Rectangles.ToArray());
        }

        public override void UpdatePos(Point pos)
        {
            Pos = pos;
            Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Rectangles[1] = new Rectangle(pos.X, pos.Y + size.Height, 10, 30);
            Rectangles[2] = new Rectangle(pos.X, pos.Y + size.Height * 2, 100, 10);
            if (next != null)
            {
                next.UpdatePos(new Point(Rectangles[Rectangles.Count - 1].Left, Bottom));
            }
        }

        public override Block Clone()
        {
            return new IfStatement(Main.Files.SelectedTab.Name, next, prev, Pos, inside, condition, false);
        }

        public override string ToString()
        {
            return string.Format("if ({0}) {{\n", condition);
        }
    }
}
