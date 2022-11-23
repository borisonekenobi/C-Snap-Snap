using System.Collections.Generic;
using System.Drawing;

namespace C_Snap_Snap
{
    internal abstract class Block
    {
        protected static readonly Pen Highlight = new Pen(Color.White, 1);

        public abstract SolidBrush Brush { get; }
        public abstract bool Indent { get; }
        public Block Next { get; set; }
        public Block Prev { get; set; }
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
            this.Next = next;
            this.Prev = prev;
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
            if (block is Function)
            {
                SnapTo(block as Function);
            }
            else if (block is IfStatement)
            {
                SnapTo(block as IfStatement, 0);
            }
            else if (block is Variable)
            {
                SnapTo(block as Variable);
            }

            if (this is IfStatement)
            {
                ((IfStatement) this).Inside?.SnapTo(this);
                Next?.SnapTo(this);
            }
            else if (this is Function) Next?.SnapTo(this);
            else Next?.SnapTo(this);
        }

        private void SnapTo(Function block)
        {
            this.Prev = block;
            block.Next = this;
            this.UpdatePos(new Point(block.Pos.X + 10, block.Pos.Y + 30));

            this.Next?.SnapTo(this);
        }

        private void SnapTo(IfStatement block, int section)
        {
            this.Prev = block;
            /*switch (section)
            {
                case 0: block.Next = this; break;
                default: block.Inside = this; break;
            }*/
            block.Inside = this;
            this.UpdatePos(new Point(block.Pos.X + 10, block.Pos.Y + 30));

            this.Next?.SnapTo(this);
        }

        private void SnapTo(Variable block)
        {
            this.Prev = block;
            block.Next = this;
            this.UpdatePos(new Point(block.Pos.X, block.Pos.Y + 30));

            this.Next?.SnapTo(this);
        }

        public void UnSnap()
        {
            if (this.Prev == null) return;
            this.Prev.Next = null;
            this.Prev = null;
        }

        public int IndentAmount()
        {
            if (this.Prev != null) return (Indent ? 1 : 0) + Prev.IndentAmount();
            else return Indent ? 1 : 0;
        }
    }
}