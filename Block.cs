using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace C_Snap_Snap
{
    public abstract class Block : UserControl
    {
        /*protected static readonly SoundPlayer Snap = new SoundPlayer(Properties.Resources.snap);
        protected static readonly SoundPlayer Unsnap = new SoundPlayer(Properties.Resources.unsnap);*/

        public abstract bool Indent { get; }
        public abstract Size Size { get; }
        public abstract SolidColorBrush BlockColor { get; }
        public abstract double Bottom { get; }
        public Block Next { get; set; }
        public Block Prev { get; set; }
        public string File { get; set; }
        public Point Pos { get; set; }
        public bool IsDefault { get; set; }

        public Block()
        { }

        public Block(string file, Block next, Block prev, Point pos)
        {
            File = file;
            Next = next;
            Prev = prev;
            Pos = pos;
        }

        protected abstract void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e);
        protected abstract void Canvas_PointerEntered(object sender, PointerRoutedEventArgs e);
        protected abstract void Canvas_PointerExited(object sender, PointerRoutedEventArgs e);
        public abstract void Redraw(bool isSelected);
        public abstract void UpdatePos(Point pos);
        public abstract void SnapTo(Block block, int section);
        public abstract void UnSnap();
        public abstract void UnSnapFrom(Block block);
        public abstract Block Clone();
        public override abstract string ToString();

        public int IndentAmount()
        {
            if (this.Prev != null) return (Indent ? 1 : 0) + Prev.IndentAmount();
            else return Indent ? 1 : 0;
        }

        public static int SizeInside(Block inside)
        {
            if (inside == null) return 30;
            double size = inside.Size.Height;
            while (inside.Next != null)
            {
                inside = inside.Next;
                size += inside.Size.Height;
            }
            return (int)size;
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
