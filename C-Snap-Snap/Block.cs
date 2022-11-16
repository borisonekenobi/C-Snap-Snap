using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms.VisualStyles;

namespace C_Snap_Snap
{
    internal class Block
    {
        private static readonly Pen DrawPen = new Pen(Color.Orange, 3);
        private static readonly Pen Highlight = new Pen(Color.White, 1);

        protected Block next;
        protected Block prev;
        protected Color color;

        public string File { get; set; }
        public Point Pos { get; set; }
        public int Bottom
        {
            get 
            {
                return Rectangles[Rectangles.Count - 1].Bottom;
            }
        }
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
            for (int i = 0; i < Rectangles.Count; i++)
            {
                if (Rectangles[i].Contains(mouse)) return true;
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
            if (next != null)
            {
                next.UpdatePos(new Point(Rectangles[Rectangles.Count - 1].Left, Bottom));
            }
        }

        public void SnapTo(Block block)
        {
            if (block == null) return;
            if (block.next == null)
            {
                this.prev = block;
                this.UpdatePos(new Point(block.Pos.X, block.Bottom));
                block.next = this;
            }
            else
            {
                this.prev = block;
                this.UpdatePos(new Point(block.Pos.X, block.Bottom));
                this.next = block.next;

                block.next.prev = this;
                this.next.UpdatePos(new Point(Pos.X, this.Bottom));

                block.next = this;
            }
        }

        public void UnSnap()
        {
            if (this.prev == null) return;
            this.prev.next = null;
            this.prev = null;
        }
    }
}