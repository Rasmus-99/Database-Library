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
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;

namespace Database_Libary
{
    /// <summary>
    /// Interaction logic for addGame.xaml
    /// </summary>
    public partial class addGame : Window
    {
        MySQL mysql = new MySQL();

        public event Action updateListAdd;
        
        string genres = "";
        string CollectorsEdition_string;

        string[] removeLastGenre = new string[50];
        
        int i = 0;

        public addGame()
        {
            InitializeComponent();

            Clear();
            
            genreListbox.Items.Add("            Genres");
            genreListbox.Items.Add("------------------------");

            fillCombo("SELECT * FROM publishers", title, 1);
            fillCombo("SELECT * FROM publishers", publisher, 2);
            fillCombo("SELECT * FROM platform", platform, 1);
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            bool isNotValid = false;

            if (CollectorsEdition.IsChecked == true)
            {
                if (string.IsNullOrWhiteSpace(CollectorsName.Text))
                {
                    isNotValid = true;
                }
            }

            if (title.SelectedIndex != 0 && publisher.SelectedIndex != 0 && !isNotValid && (genre.Text != "" || genreListbox.Items.Count > 0) && platform.SelectedIndex != 0)
            {
                if (CollectorsName.Text != "")
                {
                    CollectorsEdition_string = CollectorsName.Text;
                }
                else
                {
                    CollectorsEdition_string = CollectorsName.Text;
                }

                if (genre.Text != "")
                {
                    genreListbox.Items.Add(genre.Text);
                    genre.Text = "";
                }

                genres = "";

                for (int i = 2; i < genreListbox.Items.Count; i++)
                {
                    genres += genreListbox.Items[i].ToString() + " - ";
                }
                
                genres = genres.Remove(genres.Length - 3);
                
                MySQL.nameOfGame = title.Text;

                if (mysql.checkString(number.Text, secondTitle.Text))
                {
                    if (mysql.insertgames(title.SelectedIndex, number.Text, secondTitle.Text, CollectorsEdition_string, genres, publisher.SelectedIndex, addCreator.developer, platform.SelectedIndex))
                    {
                        if (updateListAdd != null) updateListAdd();
                        this.Close();
                    }
                }
            }
            else
            {
                isNotValid = true;
                MessageBox.Show("Not all fields are filled in");
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        void Clear()
        {
            title.SelectedIndex = 0;
            publisher.SelectedIndex = 0;
            platform.SelectedIndex = 0;
            secondTitle.Text = "";
            number.Text = "";
            genre.Text = "";
            CollectorsEdition.IsChecked = false;
            title.Focus();
        }

        private void genre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;

            if (genre.Focusable)
            {
                addGenreToList();
            }

            e.Handled = true;
        }

        private void addGenre_Click(object sender, RoutedEventArgs e)
        {
            addGenreToList();
        }

        private void removeGenre_Click(object sender, RoutedEventArgs e)
        {
            if (genreListbox.Items.Count > 0)
            {
                i--;
                genreListbox.Items.Remove(removeLastGenre[i]);
            }
            else
            {
                MessageBox.Show("Error 404 \nGenre not found", "Error 404", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void addGenreToList()
        {
            if (genre.Text != "")
            {
                genreListbox.Items.Add(genre.Text);
                removeLastGenre[i] = genre.Text;
                
                i++;

                genre.Text = "";
                genre.Focus();
            }
        }

        void fillCombo(string querry, ComboBox c, int getString)
        {
            c.Items.Clear();
            c.SelectedIndex = 0;

            if (c == title)
            {
                c.Items.Add("Select a title");
            }
            else if (c == publisher)
            {
                c.Items.Add("Select a publisher");
            }
            else if (c == platform)
            {
                c.Items.Add("Select a platform");
            }

            MySQL.con.Close();

            try
            {
                MySQL.con.Open();

                MySQL.cmd = new MySqlCommand(querry, MySQL.con);
                MySQL.rdr = MySQL.cmd.ExecuteReader();

                while (MySQL.rdr.Read())
                {
                    string name = MySQL.rdr.GetString(getString);
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

            if (c == publisher)
            {
                c.SelectionChanged += publisher_SelectionChanged;
            }
        }

        private void publisher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string index = publisher.SelectedIndex.ToString();

            int id = int.Parse(index);
                        
            addCreator Add = new addCreator(id);

            Add.Owner = this;
            Add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Add.Show();
        }

        private void addTitle_Click(object sender, RoutedEventArgs e)
        {
            addTitleAndPublisher Add = new addTitleAndPublisher();

            Add.Owner = this;
            Add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Add.ShowDialog();
        }

        private void addDeveloper_Click(object sender, RoutedEventArgs e)
        {
            addDeveloper Add = new addDeveloper();

            Add.Owner = this;
            Add.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Add.ShowDialog();
        }

        private void CollectorsEdition_Checked(object sender, RoutedEventArgs e)
        {
            CollectorsName.Visibility = Visibility.Visible;
        }

        private void CollectorsEdition_Unchecked(object sender, RoutedEventArgs e)
        {
            CollectorsName.Visibility = Visibility.Hidden;
        }
    }
}
