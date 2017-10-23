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
        MySqlConnection con = new MySqlConnection("host=localhost;user=root;database=library;");
        MySqlCommand check;
        MySqlCommand cmd;
        MySqlDataReader rdr;

        string sql;

        bool result = false;

        public static string test = "This is a test";

        public string textToBring { get; set; }

        public bool addGame(string name, string creator, string genre, string special)
        {
            result = false;

            try
            {
                sql = "SELECT * FROM games WHERE NameOfGame = '" + name + "'";
                check = new MySqlCommand(sql, con);
                con.Open();
                rdr = check.ExecuteReader();

                if (!rdr.Read())
                {
                    con.Close();

                    sql = "INSERT INTO games (NameOfGame,Creators,Genre,Special_Edition) VALUES('" + name + "','" + creator + "','" + genre + "','" + special + "')";
                    cmd = new MySqlCommand(sql, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("Successfully added " + name + " to the list!");

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

        public bool removeGame(string name)
        {
            result = false;

            try
            {
                // this is the command
                sql = "DELETE FROM games WHERE NameOfGame = '" + name + "'";

                //This handles the connection and the query
                cmd = new MySqlCommand(sql, con);

                //Opens the connection to the SQL database
                con.Open();

                //Executes the query and saves the data to the database
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfully removed " + name + " from the list!");

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

            sql = "SELECT * FROM games WHERE NameOFGame = '" + name + "'";
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
