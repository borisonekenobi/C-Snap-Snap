using System.Collections.Generic;
using System.Drawing;

namespace C_Snap_Snap
{
    internal abstract class Block
    {
        protected static readonly Pen Highlight = new Pen(Color.White, 1);

        public abstract SolidBrush Brush { get; }
        public abstract bool Indent { get; }
        public abstract int Mimi { get; }
        public abstract Rectangle[] Rectangles { get; set; }
        public Block Next { get; set; }
        public Block Prev { get; set; }
        public string File { get; set; }
        public Point Pos { get; set; }
        public int Bottom
        {
            get 
            {
                return Rectangles[Rectangles.Length - 1].Bottom;
            }
        }
        public bool IsDefault { get; set; }

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

        public int IsHover(Point mouse)
        {
            for (int i = 0; i < Rectangles.Count; i++)
            {
                if (Rectangles[i].Contains(mouse)) return i / 2;
            }
            return -1;
        }

        public void SnapTo(Block block, int section)
        {
            if (block == null) return;
            if (block is Function)
            {
                SnapTo(block as Function);
            }
            else if (block is IfStatement)
            {
                SnapTo(block as IfStatement, section);
            }
            else if (block is Variable)
            {
                SnapTo(block as Variable);
            }

            if (this is IfStatement)
            {
                ((IfStatement)this).Inside?.SnapTo(this, 0);
                Next?.SnapTo(this, -1);
            }
            else if (this is Function)
            {
                Next?.SnapTo(this, -1);
            }
            else Next?.SnapTo(this, -1);
        }

        private void SnapTo(Function block)
        {
            this.Prev = block;
            block.Next = this;
            this.UpdatePos(new Point(block.Pos.X + 10, block.Pos.Y + 30));

            this.Next?.SnapTo(this, -1);
        }

        private void SnapTo(IfStatement block, int section)
        {
            this.Prev = block;
            switch (section)
            {
                case 0: block.Inside = this; break;
                case 1: block.Next = this; break;
                //default: block.Next = this; break;
            }
            this.UpdatePos(new Point(block.Pos.X + 10, block.Pos.Y + 30));

            this.Next?.SnapTo(this, section);
        }

        private void SnapTo(Variable block)
        {
            this.Prev = block;
            block.Next = this;
            this.UpdatePos(new Point(block.Pos.X, block.Pos.Y + 30));

            this.Next?.SnapTo(this, -1);
        }

        public void UnSnap()
        {
            if (Prev == null) return;
            if (Prev is IfStatement)
            {
                if (((IfStatement) Prev).Inside == this)
                {
                    ((IfStatement)Prev).Inside = null;
                    Prev = null;
                }
                else
                {
                    Prev.Next = null;
                    Prev = null;
                }
            }
            else
            {
                Prev.Next = null;
                Prev = null;
            }
        }

        public int IndentAmount()
        {
            if (this.Prev != null) return (Indent ? 1 : 0) + Prev.IndentAmount();
            else return Indent ? 1 : 0;
        }

        public int SizeInside(Block inside)
        {
            if (inside == null) return 30;
            int size = inside.Mimi;
            while (inside.Next != null)
            {
                inside = inside.Next;
                size += inside.Mimi;
            }
            return size;
        }
    }
}