using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Board
    {
        int[,] matrix;
        //int rows, columns;

        public Board(int rows, int columns)
        {
            matrix = new int[rows, columns];
        }
        private bool checkWinCondition(int x, int y)
        {
            return false;
        }
        public bool play(int x, int y , int TokenColor)
        {
            matrix[x,y] = TokenColor;
            return !checkWinCondition(x, y);
        }
    }
}
