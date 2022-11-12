using System.Collections.Generic;
using System.Drawing;

namespace C_Snap_Snap
{
    internal class Block
    {
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

        public void Draw(Graphics g)
        {
            if (Main.Files.SelectedTab.Name != file) return;
            using (Pen pen = new Pen(color, 2))
            {
                foreach (Rectangle rect in Rectangles)
                {
                    g.DrawRectangle(pen, Pos.X, Pos.Y, rect.Width, rect.Height);
                }
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