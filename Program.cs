﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cafffe_Sytem
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
            //Application.Run(new A.M.A.Reports_Page());
            //Application.Run(new Cafffe_Sytem.D.M.M.Clients());
            Application.Run(new Cafffe_Sytem.D.M.M.Offers());
        }
    }
}
