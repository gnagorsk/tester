using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Window
    {
        public Manage()
        {
            InitializeComponent();
        }

        private void CategoryTree_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedCat.Content = Categories.SelectedCategoryId;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddButton.IsEnabled = false;
            Categories.AddSubCategory("TestHardCodedName", EnableButtons);
        }

        public void EnableButtons()
        {
            AddButton.IsEnabled = true;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Categories.DeleteSelectedCategory(EnableButtons);
        }
    }
}
