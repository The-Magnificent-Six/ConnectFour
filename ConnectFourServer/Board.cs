using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectFourServer
{
    class Board
    {
        public int[,] matrix;
        public int rows, columns;

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns; 
            matrix = new int[rows, columns];
        }

        private bool checkWinCondition(int x, int y, int TokenColor)
        {
            return false;
        }
        public bool play(int x, int y , int TokenColor)
        {
            matrix[x,y] = TokenColor;
            return checkWinCondition(x, y,TokenColor);
        }
    }
}
