using System.Drawing;
using System.Xml.Linq;

namespace C_Snap_Snap
{
    internal class IfStatement : Block
    {
        private static readonly Size size = new Size(100, 30);

        private Block inside;
        private int numInside;

        public IfStatement(string file, Point pos) : this(file, null, null, pos, null, false)
        {
        }
        
        public IfStatement(string file, Block next, Block prev, Point pos, Block inside, bool isDefault) : base(file, next, prev, pos)
        {
            this.next = next;
            this.prev = prev;
            this.Pos = pos;
            this.inside = inside;
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
    }
}
