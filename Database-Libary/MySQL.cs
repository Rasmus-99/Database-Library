using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace Database_Libary
{
    public class MySQL
    {
        public static MySqlConnection con = new MySqlConnection("host=localhost;user=root;database=library;");
        public static MySqlCommand check;
        public static MySqlCommand cmd;
        public static MySqlDataReader rdr;
        public static MySqlDataAdapter adapter;

        public static string sql;
        public static string nameOfGame;

        bool result = false;

        public string textToBring { get; set; }

        public bool insertgames(int title_ID, string number, string secondTitle, string collectorsEdition, string genre, int publisher_ID, string developers)
        {
            result = false;
            string text = "";

            try
            {
                sql = "SELECT * FROM games WHERE Title_ID = '" + title_ID + "'";
                check = new MySqlCommand(sql, con);
                con.Open();
                rdr= check.ExecuteReader();

                if (!rdr.Read())
                {
                    con.Close();

                    sql = "INSERT INTO games (Title_ID, Number, SecondTitle, CollectorsEdition, Genre, Publisher_ID, Developers) VALUES('" + title_ID + "','" + number + "','" + secondTitle + "','" + collectorsEdition + "','" + genre + "','" + publisher_ID + "','" + developers + "')";

                    cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    if (secondTitle == "" && number == "")
                    {
                        text = nameOfGame;
                    }
                    else if (secondTitle != "" && number == "")
                    {
                        text = nameOfGame + " " + secondTitle;
                    }
                    else if (secondTitle == "" && number != "")
                    {
                        text = nameOfGame + " " + number;
                    }
                    else if (secondTitle != "" && number != "")
                    {
                        text = nameOfGame + " " + number + " " + secondTitle;
                    }

                    MessageBox.Show("Successfully added " + text + " to the list!");

                    nameOfGame = "";
                    number = "";
                    secondTitle = "";
                    collectorsEdition = "";
                    genre = "";
                    publisher_ID = 0;
                    developers = "";
                    
                    result = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Game already exists");

                    result = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }

        public bool insertTitlePublisher(string title, string publisher)
        {
            result = false;

            try
            {
                sql = "SELECT * FROM games WHERE Title = '" + title + "'";

                check = new MySqlCommand(sql, con);
                con.Open();
                rdr = check.ExecuteReader();

                if (!rdr.Read())
                {
                    con.Close();

                    sql = "INSERT INTO games (Title, Publishers) VALUES('" + title + "','" + publisher + "')";

                    cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Successfully added " + title + " to the list!");

                    result = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Title and publisher already exists");

                    result = false;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);

                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return result;
        }

        public bool removeGame(string title_ID)
        {
            result = false;

            try
            {
                //This is the command
                //sql = "DELETE FROM games WHERE Title_ID = '" + title_ID + "'";
                sql = "DELETE FROM games WHERE SecondTitle = '" + title_ID + "'";

                //This handles the connection and the query
                cmd = new MySqlCommand(sql, con);

                //Opens the connection to the SQL database
                con.Open();

                //Executes the query and saves the data to the database
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully removed " + title_ID + " from the list!");

                //Closes the connection to the database
                con.Close();

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(ex.Message);
            }

            return result;
        }

        public bool checkGame(string name)
        {
            result = false;

            //sql = "SELECT * FROM games WHERE NameOFGame = '" + name + "'";
            cmd = new MySqlCommand(sql, con);
            con.Open();
            rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                result = true;
            }

            con.Close();

            return result;
        }
    }
}
