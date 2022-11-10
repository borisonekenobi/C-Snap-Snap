using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C_Snap_Snap
{
    internal class Block
    {
        protected string file;
        protected Block next;
        protected Block prev;
        protected Position pos;

        public Block(string file, Block next, Block prev, Position pos)
        {
            this.file = file;
            this.next = next;
            this.prev = prev;
            this.pos = pos;
        }

        virtual
        public void Draw(PaintEventArgs e)
        {
        }
    }
}