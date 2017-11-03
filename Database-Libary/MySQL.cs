using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;

namespace Database_Libary
{
    public class MySQL
    {
        //public static MySqlConnection con = new MySqlConnection("host=localhost;user=root;database=library;");
        public static MySqlConnection con = new MySqlConnection("host=10.11.4.150;user=rasmus;password=ubuntu;database=Rasmus_library;");
        public static MySqlCommand check;
        public static MySqlCommand cmd;
        public static MySqlDataReader rdr;
        public static MySqlDataAdapter adapter;

        public static string sql;
        public static string sqlID;
        public static string nameOfGame;

        public static string result_string = "";

        public static bool result_bool = false;

        public bool insertgames(int title_ID, string number, string secondTitle, string collectorsEdition, string genre, int publisher_ID, string developers, int platform_ID)
        {
            result_bool = false;

            try
            {
                sql = "SELECT * FROM games WHERE Number = '" + number + "' AND SecondTitle = '" + secondTitle + "' AND Platform_ID = '" + platform_ID + "'";
                check = new MySqlCommand(sql, con);
                con.Open();
                rdr = check.ExecuteReader();

                if (!rdr.Read())
                {
                    con.Close();

                    sql = "INSERT INTO games (Title_ID, Number, SecondTitle, CollectorsEdition, Genre, Publisher_ID, Developers, platform_ID) VALUES('" + title_ID + "','" + number + "','" + secondTitle + "','" + collectorsEdition + "','" + genre + "','" + publisher_ID + "','" + developers + "','" + platform_ID + "')";

                    cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    resetAI();

                    MessageBox.Show("Successfully added " + result_string + " to the list!");

                    nameOfGame = "";
                    number = "";
                    secondTitle = "";
                    collectorsEdition = "";
                    genre = "";
                    publisher_ID = 0;
                    developers = "";
                    platform_ID = 0;
                    
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

                    resetAI();

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

        public bool removeGame(int id)
        {
            result_bool = false;
            
            try
            {
                //This is the command
                sql = "DELETE FROM games WHERE ID ='" + id + "'";

                //This handles the connection and the query
                cmd = new MySqlCommand(sql, con);

                //Opens the connection to the SQL database
                con.Open();

                //Executes the query and saves the data to the database
                cmd.ExecuteNonQuery();

                //Closes the connection to the database
                con.Close();

                resetAI();

                MessageBox.Show("Successfully removed " + result_string + " from the list!");
                
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

        /// <summary>
        /// Fills the ListBox with data from the desired database table
        /// </summary>
        public void fillList(ListBox lsbox, int id, int getID)
        {
            sql = "SELECT Developer FROM developers WHERE Publisher_ID = '" + id + "'";
            cmd = new MySqlCommand(sql, con);
            con.Open();
            rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                string names = rdr.GetString(getID);
                lsbox.Items.Add(names);
            }

            con.Close();
            lsbox.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
        }

        /// <summary>
        /// Resets the Auto Increment ID of every table
        /// </summary>
        public void resetAI()
        {
            try
            {
                MySQL.con.Close();

                string[] sql = { "developers", "games", "platform", "publishers" };

                for (int i = 0; i < sql.Length; i++)
                {
                    MySQL.sql = "ALTER TABLE " + sql[i] + " auto_increment = 1; ";

                    MySQL.cmd = new MySqlCommand(MySQL.sql, MySQL.con);

                    MySQL.con.Open();
                    MySQL.cmd.ExecuteNonQuery();
                    MySQL.con.Close();
                }
            }
            catch (Exception exc)
            {
                if (MySQL.con.State == ConnectionState.Open)
                {
                    MySQL.con.Close();
                }

                MessageBox.Show(exc.Message);
            }
        }
    }
}
