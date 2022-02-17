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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            
        }

        private void Login_Load(object sender, EventArgs e)
        {
            
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Clearfield_Click(object sender, EventArgs e)
        {
            UsrName.Clear();
           
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string s;
            if (UsrName.Text!="")
            {
                s = UsrName.Text;
            MessageBox.Show("working");
            }
            else
            {
                s = "Guest";
                MessageBox.Show("Please enter a correct password or usernam");     

            }
            Lobby lob = new FormT();
            lob.lb = s;
            this.Hide();
            lob.Show();
        }
    }
}
