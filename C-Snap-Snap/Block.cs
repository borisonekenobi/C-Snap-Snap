using System.Collections.Generic;
using System.Drawing;

namespace C_Snap_Snap
{
    internal abstract class Block
    {
        protected static readonly Pen Highlight = new Pen(Color.White, 1);

        protected Block next;
        protected Block prev;
        protected Color color;

        public abstract SolidBrush brush { get; }
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

        public abstract void Draw(Graphics g, bool isSelected);
        public abstract void UpdatePos(Point pos);
        public abstract Block Clone();
        public override abstract string ToString();

        public bool IsHover(Point mouse)
        {
            for (int i = 0; i < Rectangles.Count; i++)
            {
                if (Rectangles[i].Contains(mouse)) return true;
            }
            return false;
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