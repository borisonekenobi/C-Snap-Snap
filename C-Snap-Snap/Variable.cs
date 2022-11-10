using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Snap_Snap
{
    internal class Variable : Block
    {
        private readonly Size size = new Size(100, 30);
        private readonly Color color = Color.Orange;

        private string Type { get; set; }
        private string Name { get; set; }
        private string Value { get; set; }

        public Variable(string file, Block next, Block prev, Position pos, string type, string name, string value) : this(file, next, prev, pos, type, name, value, false)
        {
        }

        public Variable(string file, Block next, Block prev, Position pos, string type, string name, string value, bool isDefault) : base(file, next, prev, pos)
        {
            this.next = next;
            this.prev = prev;
            this.pos = pos;
            Type = type;
            Name = name;
            Value = value;
            this.isDefault = isDefault;
        }

        override
        public void Draw(PaintEventArgs e)
        {
            if (Main.Files.SelectedTab.Name != file) return;
            using (Pen pen = new Pen(color, 2))
            {
                e.Graphics.DrawRectangle(pen, new Rectangle(pos.X - 200, pos.Y - 100, size.Width, size.Height));
            }
        }
    }
}