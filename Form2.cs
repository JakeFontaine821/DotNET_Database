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
    public partial class Form2 : Form
    {
        IConfigurationSection databaseConfig;

        public Form2(IConfigurationSection _databaseConfig)
        {
            this.databaseConfig = _databaseConfig;

            InitializeComponent();
            fill_listbox();
        }

        void fill_listbox()
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                string query = "select * from database.edata;";
                //MessageBox.Show(query);

                MySqlCommand cmdDataBase = new MySqlCommand(query, myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string sName = myReader.GetString("name");
                    listBox1.Items.Add(sName);
                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                string gender = "";
                if (radioButton1.Enabled == true) gender = "Male";
                if (radioButton2.Enabled == true) gender = "Female";

                string query = "insert into database.edata (eid, name, surname, age, gender, password) values("
                    + this.txtEID.Text + ", '" + this.txtFirstName.Text + "', '" + this.txtLastName.Text + "', "
                    + this.txtAge.Text + ", '" + gender + "', '" + this.txtPassword.Text + "');";
                MessageBox.Show(query);
                MySqlCommand cmdDataBase = new MySqlCommand(query, myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("Saved");
                while (myReader.Read())
                {

                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                string query = "select * from database.edata where name = '" + listBox1.Text + "';";
                MessageBox.Show(query);

                MySqlCommand cmdDataBase = new MySqlCommand(query, myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = cmdDataBase.ExecuteReader();

                while (myReader.Read())
                {
                    string sEid = myReader.GetInt32("eid").ToString();
                    string sName = myReader.GetString("name");
                    string sSurname = myReader.GetString("surname");
                    string sAge = myReader.GetString("age").ToString();
                    string sPassword = myReader.GetString("password");
                    string sGender = myReader.GetString("gender");
                    txtEID.Text = sEid;
                    txtFirstName.Text = sName;
                    txtLastName.Text = sSurname;
                    txtAge.Text = sAge;
                    txtPassword.Text = sPassword;
                    if (sGender == "Male")
                        radioButton1.Checked = true;
                    else
                        radioButton2.Checked = true;
                }

                myConn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                string sGender = "";
                if (radioButton1.Enabled == true) sGender = "Male";
                if (radioButton2.Enabled == true) sGender = "Female";

                string query = "update database.edata set eid=" + txtEID.Text
                    + ", name='" + this.txtFirstName.Text + "', surname='" + this.txtLastName.Text
                    + "', age=" + this.txtAge.Text + ", gender='" + sGender
                    + "', password='" + this.txtPassword.Text + "' where eid=" + this.txtEID.Text + ";";

                MessageBox.Show(query);
                MySqlCommand cmdDataBase = new MySqlCommand(query, myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("Updated");
                while(myReader.Read())
                {

                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string myConnection = databaseConfig["MyDatabaseConnection"];
                MySqlConnection myConn = new MySqlConnection(myConnection);

                string query = "delete from database.edata where eid=" + this.txtEID.Text + ";";

                MessageBox.Show(query);
                MySqlCommand cmdDataBase = new MySqlCommand(query, myConn);

                MySqlDataReader myReader;
                myConn.Open();
                myReader = cmdDataBase.ExecuteReader();
                MessageBox.Show("Deleted");
                while(myReader.Read())
                {

                }

                myConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
