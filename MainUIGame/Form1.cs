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
        ListBox newlistbox;


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
            roomcount = 4;



        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (roomcount < 10)
            {
                int j = 1;
                Panel newPanel = new Panel();
                newPanel.Location = new System.Drawing.Point(x, y);
                newPanel.BackColor = Color.Transparent;
                newPanel.Name = "panel2";
                newPanel.Size = new System.Drawing.Size(100, 150);
                newPanel.TabIndex = 5;
                newPanel.BorderStyle = BorderStyle.None;
                this.Controls.Add(newPanel);
                x += 235;

                if (x >= this.Width - 200)
                { x -= (this.Width - 200); y += 230; }


                Button newButton = new Button();
                newButton.Text = "join";
                newButton.BackColor = Color.DarkSlateGray;
                newButton.Location = new System.Drawing.Point(20, 100);
                newButton.Size = new System.Drawing.Size(60, 30);
                newButton.Click += NewButton_Click2;
                newPanel.Controls.Add(newButton);

                Label newlabel = new Label();
                newlabel.AutoSize = true;
                newlabel.Location = new System.Drawing.Point(5, 5);
                newlabel.Name = "newlabel";
                newlabel.Size = new System.Drawing.Size(20, 20);
                newlabel.ForeColor = Color.White;
                newlabel.TabIndex = 1;
                newlabel.Text = "Room players";
                newPanel.Controls.Add(newlabel);

                
                newlistbox = new ListBox();
                newlistbox.FormattingEnabled = true;
                newlistbox.ItemHeight = 20;
                newlistbox.Location = new System.Drawing.Point(0, 20);
                newlistbox.Name = "lnewlistbox";
                newlistbox.Size = new System.Drawing.Size(120, 100);
                newlistbox.BackColor = Color.DarkBlue;
                newlistbox.TabIndex = 4;


                newPanel.Controls.Add(newlistbox);

                roomcount++;
                //MessageBox.Show(roomcount.ToString());

            }
            else { MessageBox.Show("sorry can't create more rooms"); }
        }

        private void NewButton_Click2(object sender, EventArgs e)
        {
           this.newlistbox.Items.Add("player1");
            MessageBox.Show(this.ToString());

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            int j = 1;
            for (int i = 0; i < 4; i++)
            {
                Panel newPanel = new Panel();
                newPanel.Location = new System.Drawing.Point(x, y);
                newPanel.BackColor = Color.Transparent;
                newPanel.Name = "panel2";
                newPanel.Size = new System.Drawing.Size(100, 150);
                newPanel.TabIndex = 5;
                this.Controls.Add(newPanel);
                x += 235;
                if (x >= this.Width - 200)
                { x -= (this.Width - 200); y += 230; }




                Button newButton = new Button();
                newButton.Text = "join";
                newButton.BackColor = Color.DarkSlateGray;
                newButton.Location = new System.Drawing.Point(20, 100);
                newButton.Size = new System.Drawing.Size(60, 30);
                newButton.Click += NewButton_Click1; ;
                newPanel.Controls.Add(newButton);

                Label newlabel = new Label();
                newlabel.AutoSize = true;
                newlabel.Location = new System.Drawing.Point(5, 5);
                newlabel.Name = "newlabel";
                newlabel.Size = new System.Drawing.Size(50, 50);
                newlabel.TabIndex = 1;
                newlabel.Text = "Room players";
                newlabel.ForeColor = Color.White;

                newPanel.Controls.Add(newlabel);

                newlistbox = new ListBox();
                newlistbox.FormattingEnabled = true;
                newlistbox.ItemHeight = 20;
                newlistbox.Location = new System.Drawing.Point(0, 20);
                newlistbox.Name = "lnewlistbox";
                newlistbox.Size = new System.Drawing.Size(120, 100);
                newlistbox.TabIndex = 4;
                newlistbox.BackColor = Color.DarkBlue;

                newPanel.Controls.Add(newlistbox);
            }
        }

        private void NewButton_Click1(object sender, EventArgs e)
        {
            this.newlistbox.Items.Add("player1");
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }


    }
}
