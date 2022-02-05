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
        public string lb
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            x = 31;
             y = 100;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (x == 600)
            //{ x = 31; y += 320; }

            Panel newPanel = new Panel();
                newPanel.Location = new System.Drawing.Point(x, y);
                newPanel.BackColor = Color.Beige;
                newPanel.Name = "panel2";
                newPanel.Size = new System.Drawing.Size(200, 200);
                newPanel.TabIndex = 5;
                this.Controls.Add(newPanel);
            x += 235;

            


            Button newButton = new Button();
            newButton.Text = "join";
            newButton.BackColor = Color.White;
            newButton.Location = new System.Drawing.Point(80, 150);
            newButton.Click += NewButton_Click;
            newPanel.Controls.Add(newButton);

            ListBox newlistbox = new ListBox();
            newlistbox.FormattingEnabled = true;
            newlistbox.ItemHeight = 20;
            newlistbox.Location = new System.Drawing.Point(40, 20);
            newlistbox.Name = "lnewlistbox";
            newlistbox.Size = new System.Drawing.Size(120, 84);
            newlistbox.TabIndex = 4;
            newPanel.Controls.Add(newlistbox);



        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int i = 0; i<4;i++)
            {
                Panel newPanel = new Panel();
                newPanel.Location = new System.Drawing.Point(x, y);
                newPanel.BackColor = Color.Beige;
                newPanel.Name = "panel2";
                newPanel.Size = new System.Drawing.Size(200, 200);
                newPanel.TabIndex = 5;
                this.Controls.Add(newPanel);
                x += 235;




                Button newButton = new Button();
                newButton.Text = "join";
                newButton.BackColor = Color.White;
                newButton.Location = new System.Drawing.Point(80, 150);
                newButton.Click += NewButton_Click;
                newPanel.Controls.Add(newButton);
                Label newlabel = new Label();
                newlabel.AutoSize = true;
                newlabel.Location = new System.Drawing.Point(5, 5);
                newlabel.Name = "newlabel";
                newlabel.Size = new System.Drawing.Size(20, 20);
                newlabel.TabIndex = 1;
                newlabel.Text = "Room players";
                newPanel.Controls.Add(newlabel);

                ListBox newlistbox = new ListBox();
                newlistbox.FormattingEnabled = true;
                newlistbox.ItemHeight = 20;
                newlistbox.Location = new System.Drawing.Point(40, 20);
                newlistbox.Name = "lnewlistbox";
                newlistbox.Size = new System.Drawing.Size(120, 84);
                newlistbox.TabIndex = 4;

                newPanel.Controls.Add(newlistbox);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}
