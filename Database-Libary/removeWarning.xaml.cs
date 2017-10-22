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
    /// Interaction logic for removeWarning.xaml
    /// </summary>
    public partial class removeWarning : Window
    {
        MySQL mysql = new MySQL();

        public removeWarning(string NameTobring)
        {
            InitializeComponent();
            name.Content = NameTobring;
            cancel.Focus();
        }

        private void removeGame_Click(object sender, RoutedEventArgs e)
        {
            if (mysql.removeGame(name.Content.ToString()))
            {
                this.Close();
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
