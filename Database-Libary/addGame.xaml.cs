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
    /// Interaction logic for addGame.xaml
    /// </summary>
    public partial class addGame : Window
    {
        MySqlConnection con = new MySqlConnection("host=localhost;user=root;database=library;");
        MySqlCommand cmd;
        MySqlDataReader reader;

        public event Action updateListAdd;

        MySQL mysql = new MySQL();
        
        string genres = "";
        string specialEdition_string;

        string[] removeLastCreator = new string[50];
        string[] removeLastGenre = new string[50];
        
        int i = 0;

        public addGame()
        {
            InitializeComponent();

            nameOfGame.Text = "";
            genre.Text = "";
            specialEdition.IsChecked = false;
            
            genreListbox.Items.Add("            Genres");
            genreListbox.Items.Add("------------------------"); //42

            nameOfGame.Focus();

            fillCombo("SELECT * FROM publishers", publisher);
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            if (nameOfGame.Text != "" && (genre.Text != "" || genreListbox.Items.Count > 0))
            {
                if (specialEdition.IsChecked == true)
                {
                    specialEdition_string = "Yes";
                }
                else
                {
                    specialEdition_string = "No";
                }

                if (genre.Text != "")
                {
                    genreListbox.Items.Add(genre.Text);
                    genre.Text = "";
                }

                for (int i = 2; i < genreListbox.Items.Count; i++)
                {
                    genres += genreListbox.Items[i].ToString() + " - ";
                }
                
                genres = genres.Remove(genres.Length - 3);
                
                /*
                if (mysql.addGame(nameOfGame.Text, creators, genres, specialEdition_string))
                {
                    if (updateListAdd != null) updateListAdd();
                    this.Close();
                }
                */
            }
            else if (nameOfGame.Text == "" || genreListbox.Items.Count == 0)
            {
                MessageBox.Show("Not all fields are filled in");
            }
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            nameOfGame.Text = "";
            genre.Text = "";
            specialEdition.IsChecked = false;
            nameOfGame.Focus();
        }

        private void genre_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

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

        void fillCombo(string querry, ComboBox c)
        {
            c.Items.Clear();
            c.SelectedIndex = 0;
            c.Items.Add("Select a publisher");

            con.Close();

            try
            {
                con.Open();

                cmd = new MySqlCommand(querry, con);
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader.GetString(2);
                    c.Items.Add(name);
                }

                con.Close();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            c.SelectionChanged += publisher_SelectionChanged;
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
    }
}
