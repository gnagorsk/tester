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

namespace StudyTester
{
    /// <summary>
    /// Interaction logic for InputDialig.xaml
    /// </summary>
    public partial class InputDialog : Window
    {
        public InputDialog(string title, string variable, string exampleText = null)
        {
            InitializeComponent();

            Title = title;
            MainLabel.Content = variable;
            if (exampleText != null)
            {
                Input.Text = exampleText;
            }
        }

        public string TypedText
        {
            get
            {
                return Input.Text;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Input.Text != null)
            {
                DialogResult = true;
            }
            Hide();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void Input_GotFocus_1(object sender, RoutedEventArgs e)
        {
            Input.SelectAll();
        }
    }
}
