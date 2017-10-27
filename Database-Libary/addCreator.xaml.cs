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
        MySQL mysql = new MySQL();
        
        public static string developer = "";

        public addCreator(int id)
        {
            InitializeComponent();

            fillList(listboxLeft, id);
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

        void fillList(ListBox lsbox, int id)
        {
            MySQL.sql = "SELECT Developer FROM developers WHERE Publisher_ID = '" + id + "'";
            MySQL.cmd = new MySqlCommand(MySQL.sql, MySQL.con);
            MySQL.con.Open();
            MySQL.rdr = MySQL.cmd.ExecuteReader();

            while (MySQL.rdr.Read())
            {
                string names = MySQL.rdr.GetString(0);
                listboxLeft.Items.Add(names);
            }

            MySQL.con.Close();
            lsbox.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
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

                lstTo.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            developer = "";

            for (int i = 0; i < listboxRight.Items.Count; i++)
            {
                developer += listboxRight.Items[i].ToString() + " - ";
            }

            developer = developer.Remove(developer.Length - 3);

            this.Close();
        }
    }
}
