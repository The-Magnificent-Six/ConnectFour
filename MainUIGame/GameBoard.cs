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
        public int[,] board;
        int x;
        int y;
        //3)
        public static int turn; // on login define if Host or Challanger
        //4)

        //
        int player;
        //5)


        public static int playerTurn;

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
        //     public static winORlose winandlose;



        public GameBoard()
        {
            InitializeComponent();
            this.boardcolumns = new Rectangle[columns];
            this.board = new int[rows, columns];//x,y
            HostBrush = new SolidBrush(HostColor);
            HostColor = Color.FromName(User.getInstance().userColor.ToString());
            ChallangerBrush = new SolidBrush(ChallangerColor);
            currntGameboard = this;

            mainlobby = this;



            currntGameboard = this;
        }
        //  public setcolor (tokencolor)

        public GameBoard(string RoomName, int rows, int cols) : this()
        {
            columns = cols;
            this.rows = rows;
            this.board = new int[rows, columns];//x,y
            this.boardcolumns = new Rectangle[columns];
            HostBrush = new SolidBrush(HostColor);
            //HostBrush = new SolidBrush(Color.FromName(tokColor1.ToString));

        }


        public void repaintBord()
        {
            Graphics g = this.CreateGraphics();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (board[i, j] == 1)
                    {
                        g.FillEllipse(HostBrush, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    }
                    else if (board[i, j] == 2)
                    {
                        g.FillEllipse(ChallangerBrush, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    }
                    else if (board[i, j] == 0)
                    {
                        g.FillEllipse(Brushes.White, 32 + 48 * j, 32 + 48 * i, 32, 32);
                    }

                }
            }

        }

        public void setHostColor(tokencolor HostPlayerColor)
        {
            HostColor = Color.FromName(HostPlayerColor.ToString());
            HostBrush = new SolidBrush(HostColor);
        }
        public void setChallangeColor(tokencolor challangePlayer)
        {
            ChallangerColor = Color.FromName(challangePlayer.ToString());
            ChallangerBrush = new SolidBrush(ChallangerColor);
        }


        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.Blue, 24, 24, columns * 48, rows * 48);

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
            int columnIndex = this.columNumber(e.Location);
            //validate to add
            if (columnIndex != -1)
            {
                int rowindex = this.EmptyRow(columnIndex);
                if (rowindex != -1)
                {
                    this.board[rowindex, columnIndex] = turn;  /// server 


                    if (playerTurn == turn) //cuurnt player 
                    {


                        this.SendServerRequest();


                    }
                    else if (playerTurn == 3)
                    {

                        MessageBox.Show(" you are spectating the Game \n  you can't play");

                    }
                    else
                    {
                        ;
                        MessageBox.Show(" That is not your turn please \n wait for the Other player Move ");

                    }


                   


                    //private void GameBoard_FormClosing(object sender, FormClosingEventArgs e)
                    //{
                    //    Lobby.mainlobby.Show();
                    //}
                }
            }

        }

        int columNumber(Point mouse)
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
        int EmptyRow(int col)
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
            string op = User.getInstance().BR.ReadStringIgnoreNull();
            if (op == "6")
            {

                if (User.getInstance().ns.CanRead)
                {
                    string z = User.getInstance().BR.ReadStringIgnoreNull();
                    string w = User.getInstance().BR.ReadStringIgnoreNull();
                    string c = User.getInstance().BR.ReadStringIgnoreNull();
                    if (x.ToString() != z && y.ToString() != w && c != cl.ToString())
                    {
                        MessageBox.Show("error");


                    }
                    else
                    {

                        repaintBord();
                    }


                }

            }

        }
    }

    public class Player
    {
        public string Name { get; }
        public Color PlayerColor { get; set; }

        public Player(string name)
        {
            Name = name;

        }

    }
}
           
  
