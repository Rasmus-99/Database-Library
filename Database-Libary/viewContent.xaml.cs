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
        bool edit;

        public viewContent(int id, string title, string number, string secondTitle, string collectorsEdition, string genre, string publisher, string developers, string platform)
        {
            InitializeComponent();

            //Textboxes
            ID_txt.Text = id.ToString();
            title_txt.Text = title;
            number_txt.Text = number;
            secondTitle_txt.Text = secondTitle;
            collectorsEdition_txt.Text = collectorsEdition;
            genres_txt.Text = genre;
            publisher_txt.Text = publisher;
            developers_txt.Text = developers;
            platform_txt.Text = platform;

            //Labels
            ID_label.Content = id.ToString();
            title_label.Content = title;
            number_label.Content = number;
            secondTitle_label.Content = secondTitle;
            collectorsEdition_label.Content = collectorsEdition;
            genres_label.Content = genre;
            publisher_label.Content = publisher;
            developers_label.Content = developers;
            platform_label.Content = platform;

            btn_cancel.Visibility = Visibility.Hidden;

            edit = true;
            hideTxt(true);
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (edit == true)
            {
                btn_cancel.Visibility = Visibility.Visible;

                btn_edit.Content = "Save";
                edit = false;

                hideTxt(false);
            }
            else if (edit == false)
            {
                btn_cancel.Visibility = Visibility.Hidden;

                btn_edit.Content = "Edit";
                edit = true;

                hideTxt(true);
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            btn_cancel.Visibility = Visibility.Hidden;

            btn_edit.Content = "Edit";
            edit = true;
            
            hideTxt(true);
        }

        /// <summary>
        /// Hides the TextBoxes and/or the labels
        /// </summary>
        void hideTxt(bool hide)
        {
            if (hide)
            {
                //Hides the TextBoxes and shows the labels
                ID_txt.Visibility = Visibility.Hidden;
                title_txt.Visibility = Visibility.Hidden;
                number_txt.Visibility = Visibility.Hidden;
                secondTitle_txt.Visibility = Visibility.Hidden;
                collectorsEdition_txt.Visibility = Visibility.Hidden;
                genres_txt.Visibility = Visibility.Hidden;
                publisher_txt.Visibility = Visibility.Hidden;
                developers_txt.Visibility = Visibility.Hidden;
                platform_txt.Visibility = Visibility.Hidden;

                ID_label.Visibility = Visibility.Visible;
                title_label.Visibility = Visibility.Visible;
                number_label.Visibility = Visibility.Visible;
                secondTitle_label.Visibility = Visibility.Visible;
                collectorsEdition_label.Visibility = Visibility.Visible;
                genres_label.Visibility = Visibility.Visible;
                publisher_label.Visibility = Visibility.Visible;
                developers_label.Visibility = Visibility.Visible;
                platform_label.Visibility = Visibility.Visible;

                hide = false;
            }
            else if (!hide)
            {
                //Shows the TextBoxes and hides the labels
                ID_txt.Visibility = Visibility.Visible;
                title_txt.Visibility = Visibility.Visible;
                number_txt.Visibility = Visibility.Visible;
                secondTitle_txt.Visibility = Visibility.Visible;
                collectorsEdition_txt.Visibility = Visibility.Visible;
                genres_txt.Visibility = Visibility.Visible;
                publisher_txt.Visibility = Visibility.Visible;
                developers_txt.Visibility = Visibility.Visible;
                platform_txt.Visibility = Visibility.Visible;

                ID_label.Visibility = Visibility.Hidden;
                title_label.Visibility = Visibility.Hidden;
                number_label.Visibility = Visibility.Hidden;
                secondTitle_label.Visibility = Visibility.Hidden;
                collectorsEdition_label.Visibility = Visibility.Hidden;
                genres_label.Visibility = Visibility.Hidden;
                publisher_label.Visibility = Visibility.Hidden;
                developers_label.Visibility = Visibility.Hidden;
                platform_label.Visibility = Visibility.Hidden;

                hide = true;
            }
        }
    }
}
