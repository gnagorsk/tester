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
    /// Interaction logic for NewQuestion.xaml
    /// </summary>
    public partial class NewQuestion : Window
    {
        public NewQuestion()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Hide();
        }
    }
}
