using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;
using System.Threading.Tasks;
using SFML.System;

namespace Connect4
{
    class BackBoard
    {
        private int width = 100;
        private int padding = 10;
        public bool flagDrawPiece = false;
        public int colToDraw = new int();
        public int player = 1;
        public CircleShape circle = new CircleShape(100/2);
        public List<CircleShape> pieces = new List<CircleShape>();
        public RectangleShape boardBack = new RectangleShape(new Vector2f(780, 670));
        public List<RectCol> rects = new List<RectCol>();
        public BackBoard(int player) {
            this.player = player;
            //backdrop
            this.boardBack.FillColor = new Color(21, 141, 207);
            this.boardBack.Position = new Vector2f(0, 110);
            this.circle.FillColor = Color.White;
            //init shapes to draw 
            for (int i = 0; i < 6; i++) {
                for (int j = 0; j < 7; j++)
                {
                    this.circle.Position = new Vector2f(calcX(j), calcY(i));
                    this.pieces.Add(new CircleShape(this.circle));
                }
            }
            //add rects to mouse over and click on
            for (int i = 0; i < 7; i++) {
                this.rects.Add(new RectCol(calcX(i),i));
            }
            
        }
        private int calcX(int col)
        {
            return col * this.width + col * this.padding + this.padding;
        }
        private int calcY(int line)
        {
            //cols need an extra width at the top
            return line * this.width + line * this.padding + this.width + 2*this.padding;
        }
        public void drawBack(RenderWindow window)
        {
            window.Draw(boardBack);
            foreach (CircleShape piece in this.pieces)
            {
                window.Draw(piece);
                if (flagDrawPiece) {
                    this.circle.FillColor = player == 1 ? new Color(194, 33, 54) : new Color(241, 250, 72);
                    this.circle.Position = new Vector2f(calcX(this.colToDraw), 10);
                    window.Draw(this.circle);
                }
            }
        }
        public void checkMouseCol(int x, int y) {
            foreach (RectCol rect in this.rects) {
                if (rect.box.Contains(x, y)) {
                    this.flagDrawPiece = true;
                    this.colToDraw = rect.col;
                    return;
                }
            }
            this.flagDrawPiece = false;
        }
        public int checkMouseClick(int x, int y)
        {
            foreach (RectCol rect in this.rects)
            {
                if (rect.box.Contains(x, y))
                {
                    return rect.col;
                }
            }
            return -1;
        }
    }
}
