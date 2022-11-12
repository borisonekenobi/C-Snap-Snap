﻿using System.Drawing;

namespace C_Snap_Snap
{
    internal class Variable : Block
    {
        private readonly Size size = new Size(100, 30);

        public string type { get; set; }
        public string name { get; set; }
        public string value { get; set; }

        public Variable(string file, Point pos) : this(file, null, null, pos, null, null, null, false)
        {
        }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value) : this(file, next, prev, pos, type, name, value, false)
        {
        }

        public Variable(string file, Block next, Block prev, Point pos, string type, string name, string value, bool isDefault) : base(file, next, prev, pos)
        {
            this.next = next;
            this.prev = prev;
            this.Pos = pos;
            this.type = type;
            this.name = name;
            this.value = value;
            this.IsDefault = isDefault;

            color = Color.Orange;

            Rectangles.Add(new Rectangle(pos.X, pos.Y, size.Width, size.Height));
        }
    }
}