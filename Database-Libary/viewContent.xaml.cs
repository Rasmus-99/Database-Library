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
    /// Interaction logic for viewContent.xaml
    /// </summary>
    public partial class viewContent : Window
    {
        public viewContent(int id, string title, string number, string secondTitle, string collectorsEdition, string genre, string publisher, string developer, string platform)
        {
            InitializeComponent();

            ID_txt.Text = id.ToString();
            title_txt.Text = title;
            number_txt.Text = number;
            secondTitle_txt.Text = secondTitle;
            collectorsEdition_txt.Text = collectorsEdition;
            genres_txt.Text = genre;
            publisher_txt.Text = publisher;
            developers_txt.Text = developer;
            platform_txt.Text = platform;
        }
    }
}
