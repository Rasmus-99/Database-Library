using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.ComponentModel;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;

namespace Database_Libary
{
    class customMethods
    {
        /// <summary>
        /// Sorts the datagrid data selected order
        /// </summary>
        public void sortList(DataGrid dg, string sort1, string sort2, string sort3)
        {
            ICollectionView view = CollectionViewSource.GetDefaultView(dg.ItemsSource);

            view.SortDescriptions.Clear();

            SortDescription sd = new SortDescription(sort1, ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);

            sd = new SortDescription(sort2, ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);

            sd = new SortDescription(sort3, ListSortDirection.Ascending);
            view.SortDescriptions.Add(sd);
        }

        /// <summary>
        /// Fills a combobox with data from the selected table
        /// </summary>
        public void fillCombo(ComboBox c, string startText, string sql, int getID)
        {
            c.Items.Clear();
            c.SelectedIndex = 0;

            c.Items.Add(startText);

            MySQL.con.Close();

            try
            {
                MySQL.con.Open();

                MySQL.cmd = new MySqlCommand(sql, MySQL.con);
                MySQL.rdr = MySQL.cmd.ExecuteReader();

                while (MySQL.rdr.Read())
                {
                    string name = MySQL.rdr.GetString(getID);
                    c.Items.Add(name);
                }

                MySQL.con.Close();
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

        /// <summary>
        /// Moves the selected items from one ListBox to antoher
        /// </summary>
        public void moveItem(ListBox lstFrom, ListBox lstTo)
        {
            try
            {
                while (lstFrom.SelectedItems.Count > 0)
                {
                    string item = (string)lstFrom.SelectedItems[0];
                    lstTo.Items.Add(item);
                    lstFrom.Items.Remove(item);
                }

                lstTo.Items.SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        /// <summary>
        /// Moves all the items from one ListBox to another
        /// </summary>
        public void moveItems(ListBox lsFrom, ListBox lsTo)
        {
            for (int i = 0; i < lsFrom.Items.Count; i++)
            {
                lsTo.Items.Add(lsFrom.Items[i]);
            }

            lsFrom.Items.Clear();
        }

        /// <summary>
        /// Checks a string before sending it to another window
        /// </summary>
        public bool checkString(string number, string secondTitle)
        {
            if (number == "" && secondTitle == "")
            {
                MySQL.result_string = MySQL.nameOfGame;
            }
            else if (number != "" && secondTitle == "")
            {
                MySQL.result_string = MySQL.nameOfGame + " " + number;
            }
            else if (number == "" && secondTitle != "")
            {
                MySQL.result_string = MySQL.nameOfGame + " - " + secondTitle;
            }
            else if (number != "" && secondTitle != "")
            {
                MySQL.result_string = MySQL.nameOfGame + " " + number + " - " + secondTitle;
            }

            return MySQL.result_bool = true;
        }
    }
}
