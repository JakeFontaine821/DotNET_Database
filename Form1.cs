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
    public partial class frmLogin : Form
    {
        public IConfiguration Configuration { get; }

        Form2 f2;
        Form3 f3;

        IConfigurationSection databaseConfig;

        public frmLogin(IConfigurationSection _databaseConfig)
        {
            InitializeComponent();

            this.databaseConfig = _databaseConfig;

            f2 = new Login.Form2(_databaseConfig);
            f3 = new Login.Form3(this, _databaseConfig);            
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                MySqlCommand SelectCommand = new MySqlCommand("select * from database.edata where name = '" +
                    this.txtUsername.Text + "';", myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = SelectCommand.ExecuteReader();

                MessageBox.Show("Connected");
                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("NOT Connected");
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                string query = "select * from database.edata where name = '"
                    + this.txtUsername.Text + "' AND password = '" + this.txtPassword.Text + "';";
                MessageBox.Show(query);
                MySqlCommand SelectCommand = new MySqlCommand(query, myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = SelectCommand.ExecuteReader();
                int count = 0;
                while(myReader.Read())
                {
                    count += 1;
                }

                if(count == 1)
                {
                    MessageBox.Show("username found");
                    this.Hide();                    
                    f2.ShowDialog();
                }
                else if(count > 1)
                {
                    MessageBox.Show("duplicated username found");
                }
                else
                {
                    MessageBox.Show("not found");
                }
                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            this.Hide();
            f3.ShowDialog();            
        }
    }
}
