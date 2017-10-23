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
        MySqlCommand cmd;
        MySqlCommand check;
        MySqlDataReader rdr;

        string sql;

        public addDeveloper()
        {
            InitializeComponent();
            textAdd.Clear();

            fillCombo("SELECT * FROM publishers", dropdown);
        }

        private void AddDeveloper_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sql = "SELECT * FROM developers WHERE Developer = '" + textAdd.Text + "'";
                check = new MySqlCommand(sql, MySQL.con);
                MySQL.con.Open();
                rdr = check.ExecuteReader();

                if (!rdr.Read())
                {
                    MySQL.con.Close();

                    sql = "INSERT INTO developers (Publisher_ID, Developer) VALUES('" + dropdown.SelectedIndex + "','" + textAdd.Text + "')";
                    cmd = new MySqlCommand(sql, MySQL.con);

                    MySQL.con.Open();
                    cmd.ExecuteNonQuery();
                    MySQL.con.Close();

                    MessageBox.Show("Successfully added " + textAdd.Text + " to the list!");
                }
                else
                {
                    MySQL.con.Close();
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

        void fillCombo(string querry, ComboBox c)
        {
            c.Items.Clear();
            c.SelectedIndex = 0;
            c.Items.Add("Select a publisher");

            MySQL.con.Close();

            try
            {
                MySQL.con.Open();

                cmd = new MySqlCommand(querry, MySQL.con);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string name = rdr.GetString(2);
                    c.Items.Add(name);
                }

                MySQL.con.Close();
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
