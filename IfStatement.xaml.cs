using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using Windows.UI;

namespace C_Snap_Snap
{
    public sealed partial class IfStatement : Block
    {
        public override bool Indent => false;
        public override Size Size
        {
            get
            {
                return new Size(primaryBlockSize.Width, primaryBlockSize.Height + sideBarSize.Height + endBarSize.Height);
            }
        }
        public override SolidColorBrush BlockColor => new(Color.FromArgb(255, 255, 171, 25));
        public override double Bottom
        {
            get
            {
                return primaryBlockSize.Height + sideBarSize.Height + endBarSize.Height;
            }
        }
        public string Condition { get; set; }
        public Block Inside { get; set; }

        private Size primaryBlockSize = new(100, 30);
        private Size sideBarSize = new(10, 33);
        private Size endBarSize = new(100, 10);

        public IfStatement(string file, Point pos) : this(file, null, null, pos, null, "null", false)
        { }

        public IfStatement(string file, Point pos, bool isDefault) : this(file, null, null, pos, null, "null", isDefault)
        { }

        public IfStatement(string file, Block next, Block prev, Point pos, Block inside, string condition, bool isDefault) : base(file, next, prev, pos)
        {
            Next = next;
            Prev = prev;
            Pos = pos;
            Inside = inside;
            Condition = condition;
            IsDefault = isDefault;

            InitializeComponent();
        }

        protected override void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {

        }

        protected override void Canvas_PointerEntered(object sender, PointerRoutedEventArgs e)
        {

        }

        protected override void Canvas_PointerExited(object sender, PointerRoutedEventArgs e)
        {

        }

        public override void Redraw(bool isSelected)
        {

        }

        public override void UpdatePos(Point pos)
        {
            Pos = pos;
            /*Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Rectangles[1] = new Rectangle(pos.X, Rectangles[0].Bottom, Rectangles[1].Width, SizeInside(Next));
            Rectangles[2] = new Rectangle(pos.X, Rectangles[1].Bottom, Rectangles[2].Width, Rectangles[2].Height);
            Next?.UpdatePos(new Point(Rectangles[1].Right, Rectangles[0].Bottom));*/
        }

        public override void SnapTo(Block block, int section)
        {
            //Snap.Play();

            if (this.Next != null) block.GetLast().SnapTo(this.Next, 0);

            this.Next = block;
            block.Prev = this;
        }

        public override void UnSnap()
        {
            if (Prev == null) return;

            //Unsnap.Play();
            Prev.UnSnapFrom(this);
            Prev = null;
        }

        public override void UnSnapFrom(Block block)
        {
            if (Next == null) return;
            if (Next == block) Next = null;
        }

        public override IfStatement Clone()
        {
            return new IfStatement("", Next, Prev, Pos, Inside, Condition, false);
        }

        public override string ToString()
        {
            return $"if ({Condition}) {{\n";
        }
    }
}
