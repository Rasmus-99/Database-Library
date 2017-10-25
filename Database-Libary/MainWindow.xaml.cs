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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using MySql.Data.MySqlClient;
using System.ComponentModel;

namespace Database_Libary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MySQL mysql = new MySQL();
        
        public MainWindow()
        {
            InitializeComponent();

            showGames();

            //Sorts the DataGrid
            listOfGames.Items.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
        }

        public void showGames()
        {
            MySQL.sql = "SELECT p.Title, g.Number, g.SecondTitle, g.CollectorsEdition, g.Genre, p.Publishers, g.Developers FROM games AS g INNER JOIN publishers AS p ON g.Title_ID = p.ID";

            MySQL.adapter = new MySqlDataAdapter(MySQL.sql, MySQL.con);

            DataSet ds = new DataSet();
            MySQL.adapter.Fill(ds);

            listOfGames.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void addGame_Click(object sender, RoutedEventArgs e)
        {            
            addGame Add = new addGame();

            Add.updateListAdd += updateList;

            Add.Owner = this;
            Add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Add.Show();
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)listOfGames.SelectedItems[0];
            string selectedRow = dataRow["SecondTitle"].ToString();

            removeWarning RemoveWarning = new removeWarning(selectedRow);

            RemoveWarning.Owner = this;
            RemoveWarning.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            RemoveWarning.ShowDialog();

            updateList();
        }
        
        public void updateList()
        {
            listOfGames.ItemsSource = null;
            listOfGames.Items.Clear();
            showGames();
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("This is a test");
        }
    }
}
