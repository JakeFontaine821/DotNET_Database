using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace Login
{
    public partial class Form3 : Form
    {
        frmLogin f1;

        IConfigurationSection databaseConfig;

        public Form3(frmLogin f1, IConfigurationSection _databaseConfig)
        {
            InitializeComponent();
            this.f1 = f1;

            this.databaseConfig = _databaseConfig;
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            if (this.txtAdminPassword.Text == "1")
            {
                if (this.txtNewUsername.Text != "" && this.txtNewPassword.Text != "")
                {
                    MessageBox.Show("Account Created");

                    try
                    {
                        string myConnection = databaseConfig["MyDatabaseConnection"];
                        MySqlConnection myConn = new MySqlConnection(myConnection);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    this.Hide();
                    f1.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please Enter New Username or New Password");
                }
            }
            else
            {
                MessageBox.Show("Admin Password Incorrect");
            }
        }
    }
}
