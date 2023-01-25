using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace Connect4
{
    class Piece : CircleShape
    {
        public CircleShape shape = new CircleShape(50);
        public int player = new int();

        public Piece(int player, int posX, int posY)
        {
            Color pieceColor = player == 1 ? new Color(194, 33, 54) : new Color(241, 250, 72);
            this.shape.FillColor = pieceColor;
            this.player = player;
            shape.Position = new Vector2f(posX,posY);
     
        }
    }
}
