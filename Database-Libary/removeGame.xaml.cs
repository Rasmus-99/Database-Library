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

namespace Database_Libary
{
    /// <summary>
    /// Interaction logic for removeGame.xaml
    /// </summary>
    public partial class removeGame : Window
    {
        public event Action updateListRemove;

        MySQL mysql = new MySQL();
       
        public removeGame()
        {
            InitializeComponent();
            nameOfGame.Text = "";
            nameOfGame.Focus();
        }

        private void RemoveGame_Click(object sender, RoutedEventArgs e)
        {
            /*
            if (mysql.checkGame(nameOfGame.Text))
            {
                removeWarning RemoveWarning = new removeWarning(nameOfGame.Text);

                mysql.textToBring = nameOfGame.Text;

                RemoveWarning.Owner = this;
                RemoveWarning.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                RemoveWarning.ShowDialog();

                if (updateListRemove != null) updateListRemove();

                this.Close();
            }
            else
            {
                MessageBox.Show("The game doesn't exist");
                nameOfGame.Text = "";
                nameOfGame.Focus();
            }
            */
        }
    }
}
