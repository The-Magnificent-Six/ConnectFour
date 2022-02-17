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
        //Login log;


        // ListBox newlistbox;
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
            //log = new Login();
            
            j = 0;
            x = 150;
            y = 150;
            //int z;
            //n = 2;
            roomcount =0;
            //timer
            //timer1.Start();
            //players = new string[100][];
            //players[0] = new string[2];
            //players[1] = new string[2];
            //players[2] = new string[2];
            //players[3] = new string[2];

        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Create

            //Form2 f2=new Form2();
            //f2.showDialog();

            reqNo = "3";
            User.getInstance().BW.Write(reqNo);
            User.getInstance().BW.Write(lb);

            if (roomcount <10)
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

                roomcount++;
                n++;


                CreateDialog cdlg = new CreateDialog();
                cdlg.ShowDialog();
                DialogResult dReasult = cdlg.DialogResult;
                if(dReasult == DialogResult.OK)
                {
                    
                }


                //MessageBox.Show("Number of rooms in create "+roomcount.ToString());

            }
            else { MessageBox.Show("sorry can't create more rooms"); }
        }

        private void NewButton_Click2(object sender, EventArgs e)
        {
            //Join 

            reqNo = "3";
            User.getInstance().BW.Write(reqNo);
            User.getInstance().BW.Write(lb);

            btn = (Button)sender;
            j++;
            if (j == 1)
            { 
                btn.Text = "Spectate";
                dialog dlg = new dialog();
                dlg.Col = availablerooms[btn.TabIndex].Tcl;
                dlg.ShowDialog();
                DialogResult jdr = dlg.DialogResult;
                if(jdr==DialogResult.OK)
                {
                    string p = User.getInstance().BR.ReadString();

                }

                //joining request to spectate a game
                j = 0;

            }
            if(btn.Text == "Spectate")
            {
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

            //MessageBox.Show(ticks.ToString());
            if (ticks == 30)
            {
                //n--;
                //MessageBox.Show(ticks.ToString());
                timer1.Stop();

                //MessageBox.Show("no of rooms in timer " + roomcount.ToString());
                for (int i = 0; i < n; i++)
                {
                    newPanel[i].Dispose();
                    roomcount--;

                }

                x = 150;
                y = 150;

                //if (n > 0)
                //{
                //    n--;

                //}
                //else
                //{
                //    MessageBox.Show("No More Rooms");
                //}



                Form1_Load(null, EventArgs.Empty);

                //    //EXrooms();
                //    //MessageBox.Show("Number of inside tick fun " + n.ToString());
                //    //MessageBox.Show(ticks.ToString());

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
            //MessageBox.Show("Number of rooms inside Exrooms fun "+n.ToString());
            for (int i = 0; i < n; i++)
            {
                newPanel[i] = new Panel();
                newPanel[i].Location = new System.Drawing.Point(x, y);
                newPanel[i].BackColor = Color.Transparent;
                newPanel[i].Name = "panel2";
                //newPanel[i].Name = availablerooms[i].roomName;
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
                //newlabel.Text= availablerooms[i].roomName;
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
                //newlistbox[i].items.Add(alavilablerooms[i].noPlayers+" players");
                //newlistbox[i].items.Add(alavilablerooms[i].noSpectator+" Spactators");
                newPanel[i].Controls.Add(newlistbox[i]);




                roomcount++;



            }
           // MessageBox.Show("number of rooms on load "+roomcount.ToString());

        }

       
    }
}
