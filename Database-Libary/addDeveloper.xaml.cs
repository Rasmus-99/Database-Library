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
        MySQL mysql = new MySQL();
        customMethods methods = new customMethods();

        int i = 0;

        string[] removeLastDeveloper = new string[100];

        public addDeveloper()
        {
            InitializeComponent();
            textAdd.Clear();

            methods.fillCombo(dropdown, "Select a publisher", "SELECT * FROM publishers", 2);
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            lsBox.Items.Add(textAdd.Text);

            removeLastDeveloper[i] = textAdd.Text;
            i++;

            textAdd.Text = "";
            textAdd.Focus();
        }

        private void btnAddList_Click(object sender, RoutedEventArgs e)
        {
            if (textAdd.Text != "")
            {
                lsBox.Items.Add(textAdd.Text);
                textAdd.Text = "";
            }

            if (dropdown.SelectedIndex != 0)
            {
                mysql.insertDeveloper(lsBox, textAdd, dropdown);
            }
            else
            {
                MessageBox.Show("Not all fields are filled in");
            }
        }

        private void btnRemoveList_Click(object sender, RoutedEventArgs e)
        {
            if (lsBox.Items.Count > 0)
            {
                i--;
                lsBox.Items.Remove(removeLastDeveloper[i]);
            }
            else
            {
                MessageBox.Show("Error 404 \nDeveloper not found", "Error 404", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}