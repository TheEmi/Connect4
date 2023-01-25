using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace Connect4
{
    class RectCol
    {
        public IntRect box = new IntRect(new Vector2i(),new Vector2i(100, 780));
        public int col = new int();
        public RectCol(int position, int col) {
            this.col = col;
            this.box.Left = position;
        }

    }
}
