using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SFML.Graphics;
using System.Threading.Tasks;

namespace Connect4
{
    class Board
    {
        private int width = 100;
        private int padding = 10;
        public int[,] boardInfo = new int[6,7];//6 randuri 7 coloane
        public List<Piece> pieces = new List<Piece>();
        public Board() { 
        }
        public Board(string filePath)
        {
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    for (int i = 0; i < 6; i++)
                    {
                        string[] values = reader.ReadLine().Split(' ');
                        for (int j = 0; j < 7; j++)
                        {
                            boardInfo[i, j] = int.Parse(values[j]);
                            if (boardInfo[i, j] != 0)
                            {
                                pieces.Add(new Piece(boardInfo[i, j], calcX(j), calcY(i)));
                            }
                        }
                    }
                }
            }
            catch(Exception e) {
                Console.WriteLine("Can't read from file. Board will be initial");
            }
        }
        public void addToBoard(int j, int player) {
            //add piece to information
            int line = 0;
            for (int i = 5; i >=0; i--) {
                if (this.boardInfo[i,j] == 0) {
                    this.boardInfo[i,j] = player;
                    line = i;
                    break;
                }
            }
            //add Piece graphics
            this.pieces.Add(new Piece(player, calcX(j), calcY(line)));
            this.SaveBoardInfo("./boardInfo.txt");
        }
        public void SaveBoardInfo(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        writer.Write(boardInfo[i, j] + " ");
                    }
                    writer.WriteLine();
                }
            }
        }
        public bool didPlayerWin() {
            int[] dirsX = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dirsY = new int[8] { 1, 0, -1, 1, -1, 1, 0, -1 };
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    for (int k = 0; k < 8; k++)
                        if(boardInfo[i,j] !=0 && checkDirection(i, j, dirsX[k], dirsY[k]))return true;
                }
            }
            return false;
        }
        private bool checkDirection(int x, int y, int dirX, int dirY) {
            if (x + 3 * dirX >= 6 || y + 3 * dirY >= 7 || x + 3 * dirX < 0 || y + 3 * dirY < 0) return false;
            return boardInfo[x, y] == boardInfo[x + dirX, y + dirY] && boardInfo[x, y] == boardInfo[x + 2 * dirX, y + 2 * dirY] && boardInfo[x, y] == boardInfo[x + 3 * dirX, y + 3 * dirY];
        }
        public bool isColFull(int col) {
            return this.boardInfo[0, col] != 0 ? true : false;
        }
        private int calcX(int col) {
            return col * this.width + col * this.padding + this.padding;
        }
        private int calcY(int line)
        {
            //cols need an extra width at the top
            return line * this.width + line * this.padding + this.width + 2*this.padding;
        }
        public int[,] getBoard() {
            return this.boardInfo;
        }
        public int GetCurrentPlayer()
        {
            int piecesCount = 0;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (boardInfo[i, j] != 0)
                    {
                        piecesCount++;
                    }
                }
            }
            if (piecesCount % 2 == 0)
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
        public void ClearBoardInfo(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        writer.Write("0 ");
                    }
                    writer.WriteLine();
                }
            }
        }
        public void drawPieces(RenderWindow window) {
            foreach (Piece piece in this.pieces) {
                window.Draw(piece.shape);
            }
        }

    }
}
