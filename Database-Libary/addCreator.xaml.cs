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
using System.ComponentModel;

namespace Database_Libary
{
    /// <summary>
    /// Interaction logic for addCreator.xaml
    /// </summary>
    public partial class addCreator : Window
    {
        MySqlConnection con = new MySqlConnection("host=localhost;user=root;database=library;");
        MySqlCommand cmd;
        MySqlDataReader reader;

        string sql;

        public addCreator(int id)
        {
            InitializeComponent();

            fillList(id);
        }

        private void moveToRight_Click(object sender, RoutedEventArgs e)
        {
            moveItems(listboxLeft, listboxRight);
        }

        private void moveToLeft_Click(object sender, RoutedEventArgs e)
        {
            moveItems(listboxRight, listboxLeft);
        }

        private void moveAllToRight_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < listboxLeft.Items.Count; i++)
            {
                listboxRight.Items.Add(listboxLeft.Items);
            }

            listboxLeft.Items.Clear();
        }

        private void moveAllToLeft_Click(object sender, RoutedEventArgs e)
        {

        }

        void fillList(int id)
        {
            sql = "SELECT Developer FROM developers WHERE Publisher_ID = '" + id + "'";
            cmd = new MySqlCommand(sql, con);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string names = reader.GetString(0);
                listboxLeft.Items.Add(names);
            }
        }

        void moveItems(ListBox lstFrom, ListBox lstTo)
        {
            try
            {
                while (lstFrom.SelectedItems.Count > 0)
                {
                    string item = (string)lstFrom.SelectedItems[0];
                    lstTo.Items.Add(item);
                    lstFrom.Items.Remove(item);
                }

                lstTo.Items.SortDescriptions.Add(new System.ComponentModel.SortDescription("", ListSortDirection.Ascending));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
    }
}
