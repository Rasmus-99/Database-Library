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
        MySqlConnection con = new MySqlConnection("host=localhost;user=root;database=library;");
        MySqlDataAdapter adapter;

        MySQL mysql = new MySQL();

        string sql;
        
        public MainWindow()
        {
            InitializeComponent();

            showGames();

            //Sorts the DataGrid
            listOfGames.Items.SortDescriptions.Add(new SortDescription("NameOfGame", ListSortDirection.Ascending));

            addGame Add = new addGame();
            Add.Show();
            this.Close();
        }

        public void showGames()
        {
            sql = "SELECT * FROM game";
            //sql = "SELECT b.Title, a.Game_Title, b.Publishers, c.Developer FROM games AS a INNER JOIN games AS b ON a.ID = b.ID";

            adapter = new MySqlDataAdapter(sql, con);
            
            DataSet ds = new DataSet();
            adapter.Fill(ds);

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

        /*
        private void removeGame_Click(object sender, RoutedEventArgs e)
        {
            //removeGame Remove = new removeGame();

            //Remove.updateListRemove += updateList;

            //Remove.Owner = this;
            //Remove.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //Remove.Show();
        }
        */

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRow = (DataRowView)listOfGames.SelectedItems[0];
            string selectedRow = dataRow["NameOfGame"].ToString();

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
    }
}
