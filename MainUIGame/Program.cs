using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainUIGame
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
           // Application.Run(new Lobby());
            Application.Run(new Login());
            //Application.Run(new dialog());
=======
            Application.Run(new Login());
>>>>>>> 1a7d1075044eda4077d613e97a80a095c5b40255
        }
    }
}
