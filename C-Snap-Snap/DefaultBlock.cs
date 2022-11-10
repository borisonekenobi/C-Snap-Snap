using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Snap_Snap
{
    internal class DefaultBlock : Block
    {
        public bool Visible = true;

        public DefaultBlock(string file, Block next, Block prev, Position pos) : base(file, next, prev, pos)
        {
            this.next = next;
            this.prev = prev;
            this.pos = pos;
        }

        override
        public void Draw(PaintEventArgs e)
        {

        }
    }
}