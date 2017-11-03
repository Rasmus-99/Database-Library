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
        customMethods methods = new customMethods();

        public static string developer = "";

        public addCreator(int id)
        {
            InitializeComponent();

            mysql.fillList(listboxLeft, id, 0);
        }

        private void moveToRight_Click(object sender, RoutedEventArgs e)
        {
            methods.moveItem(listboxLeft, listboxRight);
        }

        private void moveToLeft_Click(object sender, RoutedEventArgs e)
        {
            methods.moveItem(listboxRight, listboxLeft);
        }

        private void moveAllToRight_Click(object sender, RoutedEventArgs e)
        {
            methods.moveItems(listboxLeft, listboxRight);
        }

        private void moveAllToLeft_Click(object sender, RoutedEventArgs e)
        {
            methods.moveItems(listboxRight, listboxLeft);
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
