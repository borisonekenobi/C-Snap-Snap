using System.Drawing;

namespace C_Snap_Snap
{
    internal class Variable : Block
    {
        private static readonly Size size = new Size(100, 30);

        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Variable(string file, Point pos) : this(file, null, null, pos, null, null, null, false)
        { }

        public Variable(string file, Point pos, bool isDefault) : this(file, null, null, pos, null, null, null, isDefault)
        { }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value) : this(file, next, prev, pos, type, name, value, false)
        { }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value, bool isDefault) : base(file, next, prev, pos)
        {
            this.next = next;
            this.prev = prev;
            this.Pos = pos;
            this.Type = type;
            this.Name = name;
            this.Value = value;
            this.IsDefault = isDefault;

            color = Color.Orange;

            Rectangles.Add(new Rectangle(pos.X, pos.Y, size.Width, size.Height));
        }

        public override void Draw(Graphics g, bool isSelected)
        {
            g.DrawRectangles(DrawPen, Rectangles.ToArray());
            if (isSelected) g.DrawRectangles(Highlight, Rectangles.ToArray());
        }

        public override void UpdatePos(Point pos)
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

        public override Block Clone()
        {
            return new Variable(Main.Files.SelectedTab.Name, next, prev, Pos, Type, Name, Value, false);
        }
    }
}