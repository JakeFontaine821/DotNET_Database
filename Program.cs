﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Configuration;

namespace Login
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        [STAThread]
        static void Main()
        {
            IConfigurationRoot config = new ConfigurationBuilder().AddJsonFile("config.json").Build();
            // Access the ConnectionStrings section
            IConfigurationSection connectionStringsSection = config.GetSection("ConnectionStrings");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new frmLogin(connectionStringsSection));
        }
    }
}
