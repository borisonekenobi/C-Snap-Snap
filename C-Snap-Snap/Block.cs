using System.Collections.Generic;
using System.Drawing;

namespace C_Snap_Snap
{
    internal class Block
    {
        private readonly Pen DrawPen = new Pen(Color.Orange, 2);
        private readonly Pen Highlight = new Pen(Color.White, 1);

        protected string file;
        protected Block next;
        protected Block prev;
        protected Color color;

        public Point Pos { get; set; }
        public bool IsDefault { get; set; }
        public List<Rectangle> Rectangles { get; set; } = new List<Rectangle>();


        public Block(string file, Block next, Block prev, Point pos)
        {
            this.file = file;
            this.next = next;
            this.prev = prev;
            this.Pos = pos;
        }

        public void Draw(Graphics g, bool isSelected)
        {
            if (Main.Files.SelectedTab.Name != file) return;
            foreach (Rectangle rect in Rectangles)
            {
                if (isSelected)
                {
                    g.DrawRectangle(Highlight, rect.Left - 2, rect.Top - 2, rect.Width + 2, rect.Height + 2);
                }
                g.DrawRectangle(DrawPen, rect);
            }
        }

        public bool IsHover(Point mouse)
        {
            foreach (Rectangle rect in Rectangles)
            {
                if (rect.Left <= mouse.X && rect.Right >= mouse.X && rect.Top <= mouse.Y && rect.Bottom >= mouse.Y) return true;
            }
            return false;
        }
    }
}