using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;

namespace Database_Libary
{
    /// <summary>
    /// Interaction logic for addDeveloper.xaml
    /// </summary>
    public partial class addDeveloper : Window
    {
        customMethods methods = new customMethods();

        public addDeveloper()
        {
            InitializeComponent();
            textAdd.Clear();

            methods.fillCombo(dropdown, "Select a publisher", "SELECT * FROM publishers", 2);
        }

        private void AddDeveloper_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySQL.sql = "SELECT * FROM developers WHERE Developer = '" + textAdd.Text + "'";
                MySQL.check = new MySqlCommand(MySQL.sql, MySQL.con);
                MySQL.con.Open();
                MySQL.rdr = MySQL.check.ExecuteReader();

                if (!MySQL.rdr.Read())
                {
                    MySQL.con.Close();

                    MySQL.sql = "INSERT INTO developers (Publisher_ID, Developer) VALUES('" + dropdown.SelectedIndex + "','" + textAdd.Text + "')";
                    MySQL.cmd = new MySqlCommand(MySQL.sql, MySQL.con);

                    MySQL.con.Open();
                    MySQL.cmd.ExecuteNonQuery();
                    MySQL.con.Close();

                    MessageBox.Show("Successfully added " + textAdd.Text + " to the list!");

                    this.Close();
                }
                else
                {
                    MySQL.con.Close();
                    textAdd.Clear();
                    textAdd.Focus();
                    MessageBox.Show("Developer already exists");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

                if (MySQL.con.State == ConnectionState.Open)
                {
                    MySQL.con.Close();
                }
            }
        }
    }
}
