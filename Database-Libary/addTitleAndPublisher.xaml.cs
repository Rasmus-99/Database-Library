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
    /// Interaction logic for addTitleAndPublisher.xaml
    /// </summary>
    public partial class addTitleAndPublisher : Window
    {
        MySQL mysql = new MySQL();

        public addTitleAndPublisher()
        {
            InitializeComponent();

            title.Text = "";
            publisher.Text = "";
            title.Focus();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (mysql.insertTitlePublisher(title.Text, publisher.Text))
            {
                this.Close();
            }
        }
    }
}
