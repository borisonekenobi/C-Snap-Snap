using System.Drawing;
using System.Media;

namespace C_Snap_Snap
{
    internal abstract class Block
    {
        protected static readonly Pen Highlight = new Pen(Color.White, 1);
        protected static readonly SoundPlayer Snap = new SoundPlayer(Properties.Resources.snap);
        protected static readonly SoundPlayer Unsnap = new SoundPlayer(Properties.Resources.unsnap);

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
        public abstract void SnapTo(Block block, int section);
        public abstract void UnSnap();
        public abstract void UnSnapFrom(Block block);
        public abstract Block Clone();
        public override abstract string ToString();

        public int IsHover(Point mouse)
        {
            for (int i = 0; i < Rectangles.Length; i++)
            {
                if (Rectangles[i].Contains(mouse)) return i / 2;
            }
            return -1;
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

        public Block GetLast()
        {
            Block next = this;
            while(next.Next != null)
            {
                next = next.Next;
            }
            return next;
        }
    }
}