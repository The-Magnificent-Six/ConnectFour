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
        public int rows, columns, noMoves = 0;
        public bool checkDrawCondition { get => (noMoves == rows * columns); }

        public Board(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns; 
            matrix = new int[rows, columns];
        }

        private bool checkWinCondition(int x, int y, int TokenColor,int connect = 4)
        {
            int i,j,count = -1;
            
            //horizontal
            for(i = x ; i < rows; i ++)
            {
                if(matrix[i,y] == TokenColor)
                    count++;
                else
                    break;
            }
            for(i = x ; i >= 0 ; i--)
            {
                if(matrix[i,y] == TokenColor)
                    count++;
                else
                    break;
            }
            if (count >= connect)
                return true;
            
            //vertical
            count = -1;
            for(j=y;j<columns;j++)
            {
                if(matrix[x,j] == TokenColor)
                    count++;
                else
                    break;
            }
            for(j=y;j>=0;j--)
            {
                if(matrix[x,j] == TokenColor)
                    count++;
                else
                    break;
            }
            if (count >= connect)
                return true;
            
            //positive slope
            count = -1;
            for(i=x,j=y;i<rows&&j<columns;i++,j++)
            {
                if(matrix[i,j] == TokenColor)
                    count++;
                else
                    break;
            }
            for(i=x,j=y;i>=0&&j>=0;i--,j--)
            {
                if(matrix[i,j] == TokenColor)
                    count++;
                else
                    break;
            }
            if (count >= connect)
                return true;
            
            //negative slope
            count = -1;
            for(i=x,j=y;i<rows&&j>=0;i++,j--)
            {
                if(matrix[i,j] == TokenColor)
                    count++;
                else
                    break;
            }
            for(i=x,j=y;i>=0&&j<columns;i--,j++)
            {
                if(matrix[i,j] == TokenColor)
                    count++;
                else
                    break;
            }
            if (count >= connect)
                return true;
                
            return false;
        }

        public bool play(int x, int y , int TokenColor)
        {
            noMoves++;
            matrix[x,y] = TokenColor;
            return checkWinCondition(x, y, TokenColor) || checkDrawCondition ;
        }
        public void reset()
        {
            noMoves = 0;
            matrix = new int[rows, columns];
        }
    }
}
