using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

 
namespace MainUIGame
{
    public struct room
    {
        public string roomName;
        public int noPlayers;
        public int noSpectator;
        public tokencolor Tcl;
        public int row;
        public int column;

    }
    
    public partial class Lobby : Form
    {
        int x;
        int y;
        int roomcount;
        Button[] newButton = new Button[100];
        ListBox[] newlistbox = new ListBox[100];
        Panel[] newPanel = new Panel[100];
        Label newlabel;
        int j ;
        int ticks;
        int n ;
        string reqNo;
        Button btn;
        room[] availablerooms;


      
        // tcp connection

        public string lb
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Lobby()
        {
            InitializeComponent();
           
            this.WindowState = FormWindowState.Maximized;

            j = 0;
            x = 150;
            y = 150;
           
            roomcount =0;
          
         

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Create

        
            //reqNo = "2";
            //User.getInstance().BW.Write(reqNo);
            ////User.getInstance().BW.Write(lb);

                CreateDialog cdlg = new CreateDialog();
                
                DialogResult dReasult = cdlg.ShowDialog();
                if (dReasult == DialogResult.OK)
                {if(cdlg.op=="8")

                {
                    if (n < 10)
                    {
                        newPanel[n] = new Panel();
                        newPanel[n].Location = new System.Drawing.Point(x, y);
                        newPanel[n].BackColor = Color.Transparent;
                        newPanel[n].Name = "panel2";
                        newPanel[n].Size = new System.Drawing.Size(100, 150);
                        newPanel[n].TabIndex = 5;
                        newPanel[n].BorderStyle = BorderStyle.None;
                        this.Controls.Add(newPanel[n]);
                        x += 235;

                        if (x >= this.Width - 200)
                        { x -= (this.Width - 200); y += 230; }

                        newButton[n] = new Button();
                        newButton[n].Text = "join";
                        newButton[n].BackColor = Color.DarkSlateGray;
                        newButton[n].Location = new System.Drawing.Point(20, 100);
                        newButton[n].Size = new System.Drawing.Size(60, 30);
                        newButton[n].Click += NewButton_Click2;
                        newButton[n].TabIndex = n;
                        newPanel[n].Controls.Add(newButton[n]);

                        newlabel = new Label();
                        newlabel.AutoSize = true;
                        newlabel.Location = new System.Drawing.Point(5, 5);
                        newlabel.Name = "newlabel";
                        newlabel.Size = new System.Drawing.Size(20, 20);
                        newlabel.ForeColor = Color.White;
                        newlabel.TabIndex = 1;
                        newlabel.Text = "Room players";
                        newPanel[n].Controls.Add(newlabel);

                        newlistbox[n] = new ListBox();
                        newlistbox[n].FormattingEnabled = true;
                        newlistbox[n].ItemHeight = 20;
                        newlistbox[n].Location = new System.Drawing.Point(0, 20);
                        newlistbox[n].Name = "lnewlistbox";
                        newlistbox[n].Size = new System.Drawing.Size(120, 100);
                        newlistbox[n].BackColor = Color.DarkBlue;
                        newlistbox[n].TabIndex = n;
                        newPanel[n].Controls.Add(newlistbox[n]);
                        newlistbox[n].Items.Add("1 player");

                  
                        n++;
                        GameBoard gb = new GameBoard(cdlg.RomeName,int.Parse(cdlg.RowNo), int.Parse(cdlg.ColNo));
                        GameBoard.turn= 2;
                        gb.setHostColor(cdlg.TokenCol);
                        gb.Text = User.getInstance().username;
                        gb.Show();
                        this.Close();
                        MessageBox.Show("Please hold for the other player to connect sir ... ");
                    }
                    else if (cdlg.op == "-1")
                    {
                        cdlg.Close();
                        string err = User.getInstance().BR.ReadStringIgnoreNull();
                        MessageBox.Show(err);
                    }
                    
                }


               

            }
            else { MessageBox.Show("sorry can't create more rooms"); }
        }

        private void NewButton_Click2(object sender, EventArgs e)
        {
            //Join 


            btn = (Button)sender;
            //j++;
            if (availablerooms[btn.TabIndex].noPlayers==1)
            { 
                
                dialog dlg = new dialog();
                string rn =availablerooms[btn.TabIndex].roomName;
                int rw = availablerooms[btn.TabIndex].row;
                int cl= availablerooms[btn.TabIndex].column;
                dlg.Col = availablerooms[btn.TabIndex].Tcl;

                DialogResult joindr = dlg.ShowDialog();
                User.getInstance().BW.Write(User.getInstance().username);
                User.getInstance().BW.Write(rn);
                
                string op = User.getInstance().BR.ReadStringIgnoreNull();

                if (joindr==DialogResult.OK)
                {if(op=="8")
                    {
                        GameBoard gb = new GameBoard(rn, rw, cl);
                        GameBoard.turn = 1;
                        gb.setChallangeColor(dlg.Col) ;
                        gb.Text = User.getInstance().username;
                        gb.Show();
                        newlistbox[btn.TabIndex].Items.Clear();
                        newlistbox[btn.TabIndex].Items.Add("2 players");
                        this.Close();
                        

                    }


                }

                //joining request to spectate a game
           

            }
            else if(availablerooms[btn.TabIndex].noPlayers == 2)
            {
                btn.Text = "Spectate";
                joinTospectate();
                reqNo = "4";
                User.getInstance().BW.Write(reqNo);
                User.getInstance().BW.Write(lb);

            }

            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            EXrooms();
            User.getInstance().username = lb;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ticks++;

         
            if (ticks == 30)
            {
              
                timer1.Stop();

      
                for (int i = 0; i < n; i++)
                {
                    newPanel[i].Dispose();
                    //roomcount--;

                }

                x = 150;
                y = 150;

           



                Form1_Load(null, EventArgs.Empty);

               

                }

            }
        private void joinTospectate()
        {
            string reqNoSpectate = "4";

            User.getInstance().BW.Write(reqNoSpectate);

            User.getInstance().BW.Write(availablerooms[btn.TabIndex].roomName);
            while (true)
            {
                if (User.getInstance().ns.CanRead)
                {

                    int serverRes = int.Parse(User.getInstance().BR.ReadStringIgnoreNull());
                    if (serverRes == 5)
                    {
                        string player1, color1, player2, color2;
                        int row, col;
                        player1 = User.getInstance().BR.ReadStringIgnoreNull();
                        color1 = User.getInstance().BR.ReadStringIgnoreNull();
                        player2 = User.getInstance().BR.ReadStringIgnoreNull();
                        color2 = User.getInstance().BR.ReadStringIgnoreNull();
                        row = int.Parse(User.getInstance().BR.ReadStringIgnoreNull());
                        col = int.Parse(User.getInstance().BR.ReadStringIgnoreNull());
                        GameBoard gameSpectate = new GameBoard(availablerooms[btn.TabIndex].roomName, row, col);
                        GameBoard.turn =3 ;
                       // gameSpectate.setHostColor ();
                        //gameSpectate.setChallangeColor (color2);
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                gameSpectate.board[i, j] = (tokencolor)int.Parse(User.getInstance().BR.ReadStringIgnoreNull());
                            }
                        }
                        gameSpectate.Show();
                        this.Close();
                        
                    }
                    else if (serverRes == -1)
                    {
                        string e = User.getInstance().BR.ReadStringIgnoreNull();
                        MessageBox.Show(e);
                    }
                    break;
                }
            }
        }
        private void EXrooms()
            {
            //call servr for rooms count 
            
            reqNo = "1";
            User.getInstance().BW.Write(reqNo);
            while (!User.getInstance().ns.CanRead) { }

               string p = User.getInstance().BR.ReadStringIgnoreNull();
            if (p == "7")

            {
                n = int.Parse (User.getInstance().BR.ReadStringIgnoreNull());
                availablerooms = new room[n];
                for (int i = 0; i < availablerooms.Length; i++)
                {
                    availablerooms[i].roomName = User.getInstance().BR.ReadStringIgnoreNull();
                    availablerooms[i].row = int.Parse(User.getInstance().BR.ReadStringIgnoreNull());
                    availablerooms[i].column = int.Parse(User.getInstance().BR.ReadStringIgnoreNull());
                    availablerooms[i].noPlayers = int.Parse (User.getInstance().BR.ReadStringIgnoreNull());
                    if (availablerooms[i].noPlayers == 1)
                    {
                        availablerooms[i].Tcl = (tokencolor) int.Parse (User.getInstance().BR.ReadStringIgnoreNull());
                    }
                    availablerooms[i].noSpectator = int.Parse(User.getInstance().BR.ReadStringIgnoreNull());

                }
                 
            }
            else
            {
                throw new Exception("enta 3abit ya server") ;
            }
            


            ticks = 0;
            timer1.Start();
           
            for (int i = 0; i < n; i++)
            {
                newPanel[i] = new Panel();
                newPanel[i].Location = new System.Drawing.Point(x, y);
                newPanel[i].BackColor = Color.Transparent;
                newPanel[i].Name = "panel2";
          
                newPanel[i].Size = new System.Drawing.Size(100, 150);
                newPanel[i].TabIndex = i;
                this.Controls.Add(newPanel[i]);
                x += 235;
                if (x >= this.Width - 200)
                { x -= (this.Width - 200); y += 230; };

                newButton[i] = new Button();
                newButton[i].Text = "join";
                newButton[i].BackColor = Color.DarkSlateGray;
                newButton[i].Location = new System.Drawing.Point(20, 100);
                newButton[i].Size = new System.Drawing.Size(60, 30);
                newButton[i].Click += NewButton_Click2;
                newButton[i].TabIndex = i;
                newPanel[i].Controls.Add(newButton[i]);

                newlabel = new Label();
                newlabel.AutoSize = true;
                newlabel.Location = new System.Drawing.Point(5, 5);
                newlabel.Name = "newlabel";
                newlabel.Size = new System.Drawing.Size(50, 50);
                newlabel.TabIndex = i;
                newlabel.Text = "Room players";
      
                newlabel.ForeColor = Color.White;

                newPanel[i].Controls.Add(newlabel);

                newlistbox[i] = new ListBox();
                newlistbox[i].FormattingEnabled = true;
                newlistbox[i].ItemHeight = 20;
                newlistbox[i].Location = new System.Drawing.Point(0, 20);
                newlistbox[i].Name = "lnewlistbox";
                newlistbox[i].Size = new System.Drawing.Size(120, 100);
                newlistbox[i].BackColor = Color.DarkBlue;
                newlistbox[i].TabIndex = i;
               
                newPanel[i].Controls.Add(newlistbox[i]);




                roomcount++;



            }
         

        }

       
    }
}
