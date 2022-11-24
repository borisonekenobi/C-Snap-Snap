using System.Drawing;

namespace C_Snap_Snap
{
    internal class Function : Block
    {
        private static readonly Size size = new Size(10, 30);
        public override SolidBrush Brush
        {
            get
            {
                return new SolidBrush(Color.FromArgb(255, 77, 106));
            }
        }
        public override bool Indent
        {
            get { return true; }
        }
        public override int Mimi
        {
            get
            {
                if (Next == null) return 30;
                Block i = Next;
                int size = i.Mimi;
                while (i.Next != null)
                {
                    i = i.Next;
                    size = i.Mimi;
                }
                return size;
            }
        }
        public override Rectangle[] Rectangles { get; set; }

        private string returnType;
        private string parameters;

        public string Name { get; set; }

        public Function(string file, Point pos) : this(file, null, null, pos, "null", "null", "null", false)
        { }

        public Function(string file, Point pos, bool isDefault) : this(file, null, null, pos, "null", "null", "null", isDefault)
        { }

        public Function(string file, Block next, Block prev, Point pos, string returnType, string name, string parameters, bool isDefault) : base(file, next, prev, pos)
        {
            this.Next = next;
            this.Prev = prev;
            this.Pos = pos;
            this.returnType = returnType;
            this.Name = name;
            this.parameters = parameters;
            this.IsDefault = isDefault;

            Rectangles = new Rectangle[3];
            Rectangles[0] = new Rectangle(pos.X, pos.Y, 100, 30);
            Rectangles[1] = new Rectangle(pos.X, pos.Y + 30, 10, 33);
            Rectangles[2] = new Rectangle(pos.X, pos.Y + 30 * 2, 100, 10);
        }

        public override void Draw(Graphics g, bool isSelected)
        {
            g.FillRectangles(Brush, Rectangles);
            if (isSelected) g.DrawRectangles(Highlight, Rectangles);
        }

        public override void UpdatePos(Point pos)
        {
            Pos = pos;
            Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Rectangles[1] = new Rectangle(pos.X, Rectangles[0].Bottom, Rectangles[1].Width, SizeInside(Next));
            Rectangles[2] = new Rectangle(pos.X, Rectangles[1].Bottom, Rectangles[2].Width, Rectangles[2].Height);
            Next?.UpdatePos(new Point(Rectangles[1].Right, Rectangles[0].Bottom));
        }

        public override Block Clone()
        {
            return new Function(Main.Files.SelectedTab.Name, Next, Prev, Pos, returnType, Name, parameters, false);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}({2}) {{\n", returnType, Name, parameters);
        }
    }
}
