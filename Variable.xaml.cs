using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using System.ComponentModel;
using Windows.Foundation;
using Windows.UI;

namespace C_Snap_Snap
{
    public sealed partial class Variable : Block, INotifyPropertyChanged
    {
        public override bool Indent => false;
        public override Size Size => size;
        public override SolidColorBrush BlockColor => new(Color.FromArgb(255, 255, 140, 26));
        public override double Bottom => size.Height;
        public string Type { get; set; }
        public new string Name { get; set; }
        public string Value { get; set; }

        private readonly Visibility borderVisibility = Visibility.Collapsed;
        private Size size = new(100, 30);

        public Variable(string file, Point pos) : this(file, null, null, pos, "null", "null", "null", false)
        { }

        public Variable(string file, Point pos, bool isDefault) : this(file, null, null, pos, "null", "null", "null", isDefault)
        { }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value) : this(file, next, prev, pos, type, name, value, false)
        { }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value, bool isDefault) : base(file, next, prev, pos)
        {
            Next = next;
            Prev = prev;
            Pos = pos;
            Type = type;
            Name = name;
            Value = value;
            IsDefault = isDefault;

            InitializeComponent();
        }

        protected override void Canvas_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            size = new(10, 10);
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
            /*Pos = pos;
            Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Next?.UpdatePos(new Point(Rectangles[0].Left, Bottom));*/
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

        public override Block Clone()
        {
            return new Variable("", Next, Prev, Pos, Type, Name, Value, false);
        }

        public override string ToString()
        {
            return $"{Type} {Name} = {Value};";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertychangedEvent(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
