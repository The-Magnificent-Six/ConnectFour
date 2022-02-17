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

        private bool checkWinCondition(int x, int y, int TokenColor,int connect = 4)
        {
            int i,j;
            if(rows - x > connect )
            {
                for(i = x; i < x+connect ; i++)
                {
                    if( matrix[i,y] != TokenColor )
                    {
                        break;
                    }

                }
                if( i == x+connect )
                    return true;
                
            }
            if(x > connect)
            {
                for( i = x; i > x-connect; i--)
                {
                    if( matrix[i,y] != TokenColor )
                    {
                        break;
                    }
                }
                if( i == x-connect )
                    return true;
            }

            if( y > connect )
            {
                for( j = y; j > y-connect ; j-- )
                {
                    break;
                }
                //if()
            }


            return false;
        }
        public bool play(int x, int y , int TokenColor)
        {
            matrix[x,y] = TokenColor;
            return checkWinCondition(x, y,TokenColor);
        }
    }
}
