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
    public partial class Form1 : Form
    {
        int x;
        int y;
        int roomcount;
        Button[] newButton = new Button[100];
        ListBox[] newlistbox = new ListBox[100];
        Panel[] newPanel = new Panel[100];
        int j = 0;
        int ticks;
        int n = 5;

        Label newlabel;

        // ListBox newlistbox;


        public string lb
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            x = 150;
            y = 150;
            //int z;
            roomcount =0;
            //timer
            //timer1.Start();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (roomcount < 10)
            {
                
                newPanel[j] = new Panel();
                newPanel[j].Location = new System.Drawing.Point(x, y);
                newPanel[j].BackColor = Color.Transparent;
                newPanel[j].Name = "panel2";
                newPanel[j].Size = new System.Drawing.Size(100, 150);
                newPanel[j].TabIndex = 5;
                newPanel[j].BorderStyle = BorderStyle.None;
                this.Controls.Add(newPanel[j]);
                x += 235;

                if (x >= this.Width - 200)
                { x -= (this.Width - 200); y += 230; }

                
                newButton[j] = new Button();
                newButton[j].Text = "join";
                newButton[j].BackColor = Color.DarkSlateGray;
                newButton[j].Location = new System.Drawing.Point(20, 100);
                newButton[j].Size = new System.Drawing.Size(60, 30);
                newButton[j].Click += NewButton_Click2;
                newButton[j].TabIndex = j;
                newPanel[j].Controls.Add(newButton[j]);

                newlabel = new Label();
                newlabel.AutoSize = true;
                newlabel.Location = new System.Drawing.Point(5, 5);
                newlabel.Name = "newlabel";
                newlabel.Size = new System.Drawing.Size(20, 20);
                newlabel.ForeColor = Color.White;
                newlabel.TabIndex = 1;
                newlabel.Text = "Room players";
                newPanel[j].Controls.Add(newlabel);

                
                newlistbox[j] = new ListBox();
                newlistbox[j].FormattingEnabled = true;
                newlistbox[j].ItemHeight = 20;
                newlistbox[j].Location = new System.Drawing.Point(0, 20);
                newlistbox[j].Name = "lnewlistbox";
                newlistbox[j].Size = new System.Drawing.Size(120, 100);
                newlistbox[j].BackColor = Color.DarkBlue;
                newlistbox[j].TabIndex = j;


                newPanel[j].Controls.Add(newlistbox[j]);

                roomcount++;
                j++;
                //MessageBox.Show(roomcount.ToString());

            }
            else { MessageBox.Show("sorry can't create more rooms"); }
        }

        private void NewButton_Click2(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

           newlistbox[btn.TabIndex].Items.Add("player1");
            

        }



        private void Form1_Load(object sender, EventArgs e)
        {
           
            EXrooms();

        }


        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ticks++;

            if (ticks == 5)
            { 
                timer1.Stop();
                for (int i = 0; i < 5; i++)
                {
                    newPanel[i].Dispose();

                }

                //EXrooms();
                Form1_Load(null, EventArgs.Empty);
            }
            
        }
        private void EXrooms()
        {
            

            ticks = 0;
            timer1.Start();
            
            
            for (int i = 0; i < n; i++)
            {
                
                newPanel[j] = new Panel();
                newPanel[j].Location = new System.Drawing.Point(x, y);
                newPanel[j].BackColor = Color.Transparent;
                newPanel[j].Name = "panel2";
                newPanel[j].Size = new System.Drawing.Size(100, 150);
                newPanel[j].TabIndex = 5;
                this.Controls.Add(newPanel[j]);
                x += 235;
                if (x >= this.Width - 200)
                { x -= (this.Width - 200); y += 230; }




                newButton[j] = new Button();
                newButton[j].Text = "join";
                newButton[j].BackColor = Color.DarkSlateGray;
                newButton[j].Location = new System.Drawing.Point(20, 100);
                newButton[j].Size = new System.Drawing.Size(60, 30);
                newButton[j].Click += NewButton_Click2;
                newButton[j].TabIndex = j;
                newPanel[j].Controls.Add(newButton[j]);

                newlabel = new Label();
                newlabel.AutoSize = true;
                newlabel.Location = new System.Drawing.Point(5, 5);
                newlabel.Name = "newlabel";
                newlabel.Size = new System.Drawing.Size(50, 50);
                newlabel.TabIndex = 1;
                newlabel.Text = "Room players";
                newlabel.ForeColor = Color.White;

                newPanel[j].Controls.Add(newlabel);

                newlistbox[j] = new ListBox();
                newlistbox[j].FormattingEnabled = true;
                newlistbox[j].ItemHeight = 20;
                newlistbox[j].Location = new System.Drawing.Point(0, 20);
                newlistbox[j].Name = "lnewlistbox";
                newlistbox[j].Size = new System.Drawing.Size(120, 100);
                newlistbox[j].BackColor = Color.DarkBlue;
                newlistbox[j].TabIndex = j;


                newPanel[j].Controls.Add(newlistbox[j]);

                roomcount++;
                j++;


            }
            x = 150;
            y = 150;
            if (n > 1)
            { n--; }

        }
    }
}
