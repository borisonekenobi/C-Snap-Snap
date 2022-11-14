using System.Collections.Generic;
using System.Drawing;

namespace C_Snap_Snap
{
    internal class Block
    {
        private readonly Pen DrawPen = new Pen(Color.Orange, 3);
        private readonly Pen Highlight = new Pen(Color.White, 1);

        protected Block next;
        protected Block prev;
        protected Color color;

        public string File { get; set; }
        public Point Pos { get; set; }
        public bool IsDefault { get; set; }
        public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();


        public Block(string file, Block next, Block prev, Point pos)
        {
            this.File = file;
            this.next = next;
            this.prev = prev;
            this.Pos = pos;
        }

        public void Draw(Graphics g, bool isSelected)
        {
            g.DrawRectangles(DrawPen, Rectangles.ToArray());
            if (isSelected) g.DrawRectangles(Highlight, Rectangles.ToArray());
        }

        public bool IsHover(Point mouse)
        {
            foreach (Rectangle rect in Rectangles)
            {
                if (rect.Left <= mouse.X && rect.Right >= mouse.X && rect.Top <= mouse.Y && rect.Bottom >= mouse.Y) return true;
            }
            return false;
        }

        public void UpdatePos(Point pos)
        {
            Pos = pos;
            for (int i = 0; i < Rectangles.Count; i++)
            {
                Rectangles[i] = new Rectangle(pos.X, pos.Y, Rectangles[i].Width, Rectangles[i].Height);
            }
        }
    }
}