﻿using System.Drawing;

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

        private int numInside;
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

            Rectangles.Add(new Rectangle(pos.X, pos.Y, 100, 30));
            Rectangles.Add(new Rectangle(pos.X, pos.Y + 30, 10, 30));
            Rectangles.Add(new Rectangle(pos.X, pos.Y + 30 * 2, 100, 10));
        }

        public override void Draw(Graphics g, bool isSelected)
        {
            g.FillRectangles(Brush, Rectangles.ToArray());
            if (isSelected) g.DrawRectangles(Highlight, Rectangles.ToArray());
        }

        public override void UpdatePos(Point pos)
        {
            Pos = pos;
            Rectangles[0] = new Rectangle(pos.X, pos.Y, Rectangles[0].Width, Rectangles[0].Height);
            Rectangles[1] = new Rectangle(pos.X, pos.Y + 30, Rectangles[1].Width, Rectangles[1].Height);
            Rectangles[2] = new Rectangle(pos.X, pos.Y + 30 * 2, Rectangles[2].Width, Rectangles[2].Height);
            Next?.UpdatePos(new Point(Rectangles[Rectangles.Count - 1].Left, Bottom));
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