using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using WindowsFormsApp4;

namespace MainUIGame
{
    public partial class GameBoard : Form
    {


        private GameBoard mainlobby;

        // component
        //1)


        private Rectangle[] boardcolumns;
        //2)
        public tokencolor[,] board;
        int x;
        int y;
        //3)
        public static int turn; // on login define if Host or Challanger
        //4)

        //
        int player;
        //5)

        //SolidBrush player1;
        //SolidBrush player2;

        // Server Connecting members
        public int rows;//6
        public int columns; //7
        public BinaryWriter BW;
        public Color HostColor;
        public Color ChallangerColor;
        static Brush HostBrush;
        static Brush ChallangerBrush;
        public static GameBoard currntGameboard;


        public GameBoard()
        {
             InitializeComponent();
            HostColor = Color.FromName(User.getInstance().userColor.ToString());
            mainlobby = this;

            //1)
            //2) 6 rows  by 7 colum 
            // width , Heigth
            //3)

            //4)
            // winner = this.winnerplayer(this.turn);
            //5) player*color
            //player1 = (SolidBrush)Brushes.Red;
            //player2 = (SolidBrush)Brushes.DarkGreen;

            currntGameboard = this;
        }
        //  public setcolor (tokencolor)

        public GameBoard(string RoomName, int rows, int cols) : this()
        {
            columns = cols;
            this.rows = rows;
            this.board = new tokencolor[rows, columns];//x,y
            this.boardcolumns = new Rectangle[columns];
            //HostBrush = new SolidBrush(HostColor);
            //HostBrush = new SolidBrush(Color.FromName(tokColor1.ToString));
            Task.Factory.StartNew((board) => { ((GameBoard)board).ListenForServerMove(); }, this);//thread board from thid

        }
        //public GameBoard(string RoomName, int rows, int cols ,int tokenColor2) : this(RoomName,rows,cols)
        //{
        //    ChallangerBrush = new SolidBrush(ChallangerColor);

        //}

        public void repaintBord()
        {
            Graphics g = this.CreateGraphics();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    g.FillEllipse(new SolidBrush(Color.FromName( board[i,j].ToString() )), 32 + 48 * j, 32 + 48 * i, 32, 32);
                    //if (board[i, j] == 1)
                    //{
                    //}
                    //else if (board[i, j] == 2)
                    //{
                    //    g.FillEllipse(ChallangerBrush, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    //}

                }
            }


        }

        public void setHostColor(tokencolor HostPlayerColor)
        {
            HostColor = Color.FromName(HostPlayerColor.ToString());
            HostBrush = new SolidBrush(HostColor);
        }
        public void setChallangeColor(tokencolor challangePlayer )
        {
            ChallangerColor = Color.FromName(challangePlayer.ToString());
            ChallangerBrush = new SolidBrush(ChallangerColor);
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Silver, 24, 24, columns * 48, rows * 48);

            for (int i = 0; i < rows; i++)//x
            {
                for (int j = 0; j < columns; j++)//y
                {
                    if (i == 0)
                    {
                        this.boardcolumns[j] = new Rectangle(32 + 48 * j, 24, 32, rows * 48);
                    }
                    e.Graphics.FillEllipse(Brushes.White, (32 + 48 * j), (32 + 48 * i), 32, 32);

                }
            }
        }

        //Event of Mouseclick
        
        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
            if (turn==1)
            { 
                int columnIndex = this.columNumber(e.Location);
                //validate to add
                if (columnIndex != -1)
                {
                    int rowindex = this.EmptyRow(columnIndex);
                   
                    if (rowindex != -1)
                    {
                        x = rowindex;
                        y = columnIndex;
                        SendServerRequest();
                        
                        //this.board[rowindex, columnIndex] = turn;  // server
                        //repaintBord();
                        //  user.SendServerRequest(Single.SendMove, columnIndex.ToString(), rowindex.ToString());

                        //***************Winner***********
                        //int winner = this.winnerplayer(turn);

                        //if (winner != -1)//There is a winning player
                        //{

                        //    if (winner == 1)
                        //    {
                        //        player = (int)User.getInstance().userColor;
                        //    }
                        //    else
                        //    { player = (int)User.getInstance().userColor; }
                        //    //MessageBox.Show(player + "player has win");
                        //}

                        // change 1=>2 && 2=>1
                        //if (turn == 1)
                        //{
                        //    turn = 2;
                        //    ListenForServerMove();
                        //    turn = 1;


                        //}
                        //else
                        //{
                        //    turn = 1;
                        //}

                    }
                }
            }
        }
        // Winner conditions:

        public void ListenForServerMove()
        {
            User u = User.getInstance();
            commOp Op;
            int x_;
            int y_;
            tokencolor tokcol_;
            while (true)
            {
                if (u.ns.CanRead)
                {
                    string OpString = u.BR.ReadStringIgnoreNull();
                   
                    Op = (commOp)int.Parse(OpString);

                   
                    if (Op==commOp.playerMoveReq)
                    {
                        x_ =int.Parse(u.BR.ReadStringIgnoreNull());

                        y_ =int.Parse(u.BR.ReadStringIgnoreNull());

                        tokcol_ = (tokencolor)int.Parse(u.BR.ReadStringIgnoreNull());

                        board[x_, y_] = tokcol_;

                        repaintBord();

                    }
                    else if (Op == commOp.winLoss)
                    {
                        MessageBox.Show(u.BR.ReadStringIgnoreNull());
                        break;
                    }
                    else
                    {
                        MessageBox.Show("Server w7sh");
                    }

                    if (turn == 1)
                        turn = 2;
                    else if(turn==2)
                        turn = 1;
                }
            }
        }
        //private int winnerplayer(int Checkplayer)
        //{
        //    //1)Vertical
        //    for (int row = 0; row < this.board.GetLength(0) - 3; row++)
        //    {
        //        for (int colum = 0; colum < this.board.GetLength(1); colum++)
        //        {
        //            //check if the winner get 4 point vertically 
        //            if (this.AllNumber(Checkplayer, this.board[row, colum], this.board[row + 1, colum], this.board[row + 2, colum], this.board[row + 3, colum]))
        //            {
        //                //if True
        //                return Checkplayer;
        //            }
        //        }
        //    }
        //    //2)Horizontal
        //    for (int row = 0; row < this.board.GetLength(0); row++)
        //    {
        //        for (int colum = 0; colum < this.board.GetLength(1) - 3; colum++)
        //        {
        //            //check if the winner get 4 point Horizontal 
        //            if (this.AllNumber(Checkplayer, this.board[row, colum], this.board[row, colum + 1], this.board[row, colum + 2], this.board[row, colum + 3]))
        //            {
        //                //if True
        //                return Checkplayer;
        //            }
        //        }
        //    }
        //    //3)top-left diagonal(\)
        //    for (int row = 0; row < this.board.GetLength(0) - 3; row++)
        //    {
        //        for (int colum = 0; colum < this.board.GetLength(1) - 3; colum++)
        //        {
        //            //check if the winner get 4 point Horizontal 
        //            if (this.AllNumber(Checkplayer, this.board[row, colum], this.board[row + 1, colum + 1], this.board[row + 2, colum + 2], this.board[row + 3, colum + 3]))
        //            {
        //                //if True
        //                return Checkplayer;
        //            }
        //        }
        //    }
        //    //4)top-right diagonal(/)
        //    for (int row = 0; row < this.board.GetLength(0) - 3; row++)
        //    {
        //        for (int colum = 3; colum < this.board.GetLength(1); colum++)
        //        {
        //            //check if the winner get 4 point Horizontal 
        //            if (this.AllNumber(Checkplayer, this.board[row, colum], this.board[row + 1, colum - 1], this.board[row + 2, colum - 2], this.board[row + 3, colum - 3]))
        //            {
        //                //if True
        //                return Checkplayer;
        //            }
        //        }
        //    }

        //    return -1;
        //}
        //function to check all number is checked 
        //private bool AllNumber(int tocheck, params int[] numbers)
        //{
        //    foreach (int num in numbers)
        //    {
        //        if (num != tocheck) //check if the player get 4 point 
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        //function 

        private int columNumber(Point mouse)
        {
            for (int i = 0; i < this.boardcolumns.Length; i++)
            {
                //check Mouse location X ,Y
                if ((mouse.X >= this.boardcolumns[i].X) && (mouse.Y >= this.boardcolumns[i].Y))
                {
                    if ((mouse.X <= this.boardcolumns[i].X + this.boardcolumns[i].Width) && (mouse.Y <= this.boardcolumns[i].Y + this.boardcolumns[i].Height))
                    {
                        return i;
                    }
                }

            }
            return -1;
        }
        // ***************fill empty row*****************
        private int EmptyRow(int col)
        {
            // check if valid to add or no
            for (int i = rows - 1; i >= 0; i--) //(start add from lowest one to highest one)
            {
                if (this.board[i, col] == 0)
                {
                    return i;
                }
            }

            return -1;

        }
        public void SendServerRequest()
        {


            User.getInstance().BW.Write("6");
            User.getInstance().BW.Write(x.ToString());
            User.getInstance().BW.Write(y.ToString());
            int cl = (int)User.getInstance().userColor;
            User.getInstance().BW.Write(cl.ToString());
            //string op   =  User.getInstance().BR.ReadStringIgnoreNull();
            //if (op == "6")
            //{


            //    if (User.getInstance().ns.CanRead)
            //    {
            //        string z = User.getInstance().BR.ReadStringIgnoreNull();
            //        string w = User.getInstance().BR.ReadStringIgnoreNull();
            //        string c = User.getInstance().BR.ReadStringIgnoreNull();
            //        if (x.ToString() != z && y.ToString() != w &&  c != cl.ToString())
            //        {
            //            MessageBox.Show("error");

                            
            //        }
            //        else
            //        {
                  
            //            repaintBord();
            //        }
                    

            //    }

            //}

        }

        public class Player
        {
            public string Name { get; }
            public Color PlayerColor { get; set; }

            public Player(string name)
            {
                Name = name;

            }


            //private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
            //{
            //    Lobby.mainlobby.Show();
            //}
        }
    }
}
