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

        public static string result_string = "";

        bool result_bool = false;

        public string textToBring { get; set; }

        public bool insertgames(int title_ID, string number, string secondTitle, string collectorsEdition, string genre, int publisher_ID, string developers)
        {
            result_bool = false;

            try
            {
                sql = "SELECT * FROM games WHERE Number = '" + number + "' AND SecondTitle = '" + secondTitle + "'";
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

                    MessageBox.Show("Successfully added " + result_string + " to the list!");

                    nameOfGame = "";
                    number = "";
                    secondTitle = "";
                    collectorsEdition = "";
                    genre = "";
                    publisher_ID = 0;
                    developers = "";

                    result_bool = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Game already exists");

                    result_bool = false;
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

            return result_bool;
        }

        public bool insertTitlePublisher(string title, string publisher)
        {
            result_bool = false;

            try
            {
                sql = "SELECT * FROM publishers WHERE Title = '" + title + "' AND Publisher = '" + publisher + "'";

                check = new MySqlCommand(sql, con);
                con.Open();
                rdr = check.ExecuteReader();

                if (!rdr.Read())
                {
                    con.Close();

                    sql = "INSERT INTO publishers (Title, Publisher) VALUES('" + title + "','" + publisher + "')";

                    cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Successfully added " + title + " to the list!");

                    result_bool = true;
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Title and publisher already exists");

                    result_bool = false;
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

            return result_bool;
        }

        public bool removeGame(string number, string secondTitle)
        {
            result_bool = false;

            try
            {
                //This is the command
                sql = "DELETE FROM games WHERE Number = '" + number + "' AND SecondTitle = '" + secondTitle + "'";

                //This handles the connection and the query
                cmd = new MySqlCommand(sql, con);

                //Opens the connection to the SQL database
                con.Open();

                //Executes the query and saves the data to the database
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully removed " + result_string + " from the list!");

                //Closes the connection to the database
                con.Close();

                result_bool = true;
            }
            catch (Exception ex)
            {
                result_bool = false;
                MessageBox.Show(ex.Message);
            }

            return result_bool;
        }

        public bool checkGame(string name)
        {
            result_bool = false;

            //sql = "SELECT * FROM games WHERE NameOFGame = '" + name + "'";
            cmd = new MySqlCommand(sql, con);
            con.Open();
            rdr = cmd.ExecuteReader();

            if (rdr.Read())
            {
                result_bool = true;
            }

            con.Close();

            return result_bool;
        }

        public bool checkString(string number, string secondTitle)
        {
            if (number == "" && secondTitle == "")
            {
                result_string = nameOfGame;
            }
            else if (number != "" && secondTitle == "")
            {
                result_string = nameOfGame + " " + number;
            }
            else if (number == "" && secondTitle != "")
            {
                result_string = nameOfGame + " - " + secondTitle;
            }
            else if (number != "" && secondTitle != "")
            {
                result_string = nameOfGame + " " + number + " - " + secondTitle;
            }

            return result_bool = true;
        }
    }
}
