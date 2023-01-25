using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace Connect4
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderWindow window = new RenderWindow(new VideoMode(780, 780), "Connect4");
            Color background = new Color(Color.Black);
            Font font = new Font("JoeJack.ttf");
            Text gameWonText = new Text("                   Red won\n Click to restart", font);
            gameWonText.FillColor = background;
            gameWonText.CharacterSize = 50;
            gameWonText.Position = new Vector2f(50, 300);
            Text gameWonTextYellow = new Text("                         Yellow won\n Click to restart", font);
            gameWonTextYellow.FillColor = background;
            gameWonTextYellow.CharacterSize = 50;
            gameWonTextYellow.Position = new Vector2f(50, 300);
            Board board = new Board("./boardInfo.txt");
            BackBoard backBoard = new BackBoard(board.GetCurrentPlayer());
            bool isWon = false;
            window.Closed += HandleClose;
            window.MouseButtonPressed += OnClickPressed;
            window.MouseMoved += OnMouseMoved;
            while (window.IsOpen) {
                window.DispatchEvents();
                window.Clear(background);

                //draw booard background and stuff
                backBoard.drawBack(window);
                //draw Pieces based on board
                board.drawPieces(window);
                if (isWon)
                {
                    board.ClearBoardInfo("./boardInfo.txt");
                    if(backBoard.player == 1)
                    window.Draw(gameWonTextYellow);
                    else
                    window.Draw(gameWonText);
                }
                window.Display();
            }
            void restartGame() {
                board = new Board();
                backBoard = new BackBoard(1);
                isWon = false;
            }
            void HandleClose(object Sender, EventArgs e) {
                window.Close();
            }
            void OnClickPressed(object Sender, MouseButtonEventArgs e)
            {
                if (isWon)
                {
                    restartGame();
                    return;
                }
                int col = backBoard.checkMouseClick(e.X, e.Y);
                if(col!=-1 && !board.isColFull(col)) {
                    board.addToBoard(col, backBoard.player);
                    if (board.didPlayerWin()) {
                        isWon = true;
                    }
                    if (backBoard.player == 1)
                    {
                        backBoard.player = 2;
                    }
                    else 
                    {
                        backBoard.player = 1;
                    }
                }
            }
            void OnMouseMoved(object Sender, MouseMoveEventArgs e)
            {
                if(!isWon)
                backBoard.checkMouseCol(e.X, e.Y);
            }

        }
    }
}
