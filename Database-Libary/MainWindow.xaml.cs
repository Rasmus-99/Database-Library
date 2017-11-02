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

            sortList(listOfGames);

            listOfGames.Focus();
        }

        public void showGames()
        {
            MySQL.sql = "SELECT g.ID, p.Title, g.Number, g.SecondTitle, g.CollectorsEdition, g.Genre, p.Publisher, g.Developers, pl.Platform FROM games AS g INNER JOIN publishers AS p ON g.Title_ID = p.ID INNER JOIN platform AS pl ON Platform_ID = pl.ID";

            MySQL.adapter = new MySqlDataAdapter(MySQL.sql, MySQL.con);

            DataSet ds = new DataSet();
            MySQL.adapter.Fill(ds);

            listOfGames.ItemsSource = ds.Tables[0].DefaultView;
        }

        void sortList(DataGrid dg)
        {
            //Sorts the DataGrid in multiple columns

            ICollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource);

            view.SortDescriptions.Clear();

            SortDescription sd = new SortDescription("Title", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);

            sd = new SortDescription("Number", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);

            sd = new SortDescription("Platform", ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            addGame Add = new addGame();

            Add.updateListAdd += updateList;

            Add.Owner = this;
            Add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Add.ShowDialog();

            sortList(listOfGames);
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            removeGame(listOfGames);
        }
        
        public void updateList()
        {
            listOfGames.ItemsSource = null;
            listOfGames.Items.Clear();
            showGames();
        }

        private void listOfGames_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return;

            if (listOfGames.Focusable)
            {
                removeGame(listOfGames);
            }

            e.Handled = true;
            sortList(listOfGames);
        }

        void removeGame(DataGrid dg)
        {
            try
            {
                DataRowView dataRow = (DataRowView)dg.SelectedItems[0];

                int id = (int)dataRow["ID"];
                string title = dataRow["Title"].ToString();
                string number = dataRow["Number"].ToString();
                string secondTitle = dataRow["SecondTitle"].ToString();
                string platform = dataRow["Platform"].ToString();

                MySQL.sqlID = "SELECT ID from games WHERE ID = '" + id + "'";
                MySQL.cmd = new MySqlCommand(MySQL.sqlID, MySQL.con);
                MySQL.con.Open();
                MySQL.rdr = MySQL.cmd.ExecuteReader();

                string test_string = "";

                if (MySQL.rdr.Read())
                {
                    test_string = MySQL.rdr.GetString(0);
                }

                MySQL.con.Close();

                removeWarning RemoveWarning = new removeWarning(id, title, number, secondTitle, platform);

                RemoveWarning.Owner = this;
                RemoveWarning.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                RemoveWarning.ShowDialog();

                updateList();
            }
            catch (Exception)
            {
                MessageBox.Show("Error!\nNo game selected", "", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listOfGames_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DependencyObject src = VisualTreeHelper.GetParent((DependencyObject)e.OriginalSource);

            // Checks if the user double clicked on a row in the datagrid [ContentPresenter]
            if (src.GetType() == typeof(ContentPresenter))
            {
                DataRowView dataRow = (DataRowView)listOfGames.SelectedItems[0];

                //Gets the data from the datagrid columns and puts it into variables
                int id = (int)dataRow["ID"];
                string title = dataRow["Title"].ToString();
                string number = dataRow["Number"].ToString();
                string secondTitle = dataRow["SecondTitle"].ToString();
                string collectorsEdition = dataRow["CollectorsEdition"].ToString();
                string genre = dataRow["Genre"].ToString();
                string publisher = dataRow["Publisher"].ToString();
                string developer = dataRow["Developers"].ToString();
                string platform = dataRow["Platform"].ToString();

                MessageBox.Show("ID: " + id + "\nTitle: " + title + "\nNumber: " + number + "\nSecond Title: " + secondTitle + "\nCollector's Edition: " + collectorsEdition +
                                "\nGenre(s): " + genre + "\nPublisher: " + publisher + "\nDeveloper(s): " + developer + "\nPlatform: " + platform);
            }
        }
    }
}
