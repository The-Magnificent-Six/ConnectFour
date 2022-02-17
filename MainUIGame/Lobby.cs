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
        dialog dg = new dialog();
        Button btn;
        room[] availablerooms;
        Login log;


      
        // tcp connection

        public string lb
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Lobby()
        {
            InitializeComponent();
            User.getInstance().username = lb;
            this.WindowState = FormWindowState.Maximized;
            log = new Login();

            j = 0;
            x = 150;
            y = 150;
           
            roomcount =0;
          
         

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Create

        
            reqNo = "3";
            User.getInstance().BW.Write(reqNo);
            User.getInstance().BW.Write(lb);

                CreateDialog cdlg = new CreateDialog();
                
                DialogResult dReasult = cdlg.ShowDialog();
                if (dReasult == DialogResult.OK)
                {if(cdlg.op=="8")

                {
                    if (roomcount < 10)
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
                        GameBoard gb = new GameBoard(cdlg.RomeName, cdlg.RowNo, cdlg.ColNo);
                        gb.turn= 2;
                        gb.setHostColor = cdlg.TokenCol;
                        gb.show();

                        this.Close();
                    }
                else if (cdlg.op == "-1")
                    {
                        cdlg.Close();
                        string err = User.getInstance().BR.ReadString();
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
                btn.Text = "Spectate";
                dialog dlg = new dialog();
                string rn =availablerooms[btn.TabIndex].roomName;
                int rw = availablerooms[btn.TabIndex].row;
                int cl= availablerooms[btn.TabIndex].column;
                dlg.Col = availablerooms[btn.TabIndex].Tcl;

                DialogResult joindr = dlg.ShowDialog();
                if (joindr==DialogResult.OK)
                {if(dlg.op=="8")
                    {
                        GameBoard gb = new GameBoard(rn, rw, cl);
                        gb.turn = 1;
                        gb.setChallangerColor = dlg.Col;
                        gb.show();
                        newlistbox[btn.TabIndex].Items.Clear();
                        newlistbox[btn.TabIndex].Items.Add("2 players");
                        this.Close();
                        
                    }


                }

                //joining request to spectate a game
           

            }
            else if(availablerooms[btn.TabIndex].noPlayers == 2)
            {
                joinTospectate();
                reqNo = "4";
                User.getInstance().BW.Write(reqNo);
                User.getInstance().BW.Write(lb);

            }

            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            EXrooms();
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

                    int serverRes = int.Parse(User.getInstance().BR.ReadString());
                    if (serverRes == 5)
                    {
                        string player1, color1, player2, color2;
                        int row, col;
                        player1 = User.getInstance().BR.ReadString();
                        color1 = User.getInstance().BR.ReadString();
                        player2 = User.getInstance().BR.ReadString();
                        color2 = User.getInstance().BR.ReadString();
                        row = int.Parse(User.getInstance().BR.ReadString());
                        col = int.Parse(User.getInstance().BR.ReadString());
                        GameBoard gameSpectate = new GameBoard(availablerooms[btn.TabIndex].roomName, row, col);
                        gameSpectate.turn = 3;
                        gameSpectate.setHostColor = color1;
                        gameSpectate.setChallangerColor = color2;
                        for (int i = 0; i < row; i++)
                        {
                            for (int j = 0; j < col; j++)
                            {
                                gameSpectate.board[i, j] = int.Parse(User.getInstance().BR.ReadString());
                            }
                        }
                        gameSpectate.Show();
                        this.Close();




                    }
                    else if (serverRes == -1)
                    {
                        string e = User.getInstance().BR.ReadString();
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

               int p = User.getInstance().BR.Read();
            if (p == 7)

            {
                n = User.getInstance().BR.Read();
                availablerooms = new room[n];
                for (int i = 0; i < availablerooms.Length; i++)
                {
                    availablerooms[i].roomName = User.getInstance().BR.ReadString();
                    availablerooms[i].noPlayers = User.getInstance().BR.Read();
                    if (availablerooms[i].noPlayers == 1)
                    {
                        availablerooms[i].Tcl = (tokencolor)User.getInstance().BR.Read();
                    }
                    availablerooms[i].noSpectator = User.getInstance().BR.Read();
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
