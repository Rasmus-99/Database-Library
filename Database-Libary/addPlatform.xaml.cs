using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for addPlatform.xaml
    /// </summary>
    public partial class addPlatform : Window
    {
        MySQL mysql = new MySQL();

        public addPlatform()
        {
            InitializeComponent();
            platform.Text = "";
            platform.Focus();

            mysql.resetAI();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
