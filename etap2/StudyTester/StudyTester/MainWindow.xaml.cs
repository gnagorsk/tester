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
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudyTester.ServiceReference1;

namespace StudyTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button clicked = (Button)sender;
            clicked.IsEnabled = false;
            clicked.Content = "Loading...";

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.RunWorkerAsync(clicked);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;
            Button clicked = (Button)result[2];
            // Show new page with options / add or browse / relating our tests. If we succeed of course.

            if ((bool)result[0] == false)
            {
                MessageBox.Show((string)result[1]);
                
                clicked.Content = "Start!";
                clicked.IsEnabled = true;
            }
            else
            {
                // It works, init the rest! and go to the next page...
                clicked.Content = "Connected!";
                clicked.IsEnabled = true;
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] result = { false, "Unknown error.", e.Argument };

            using (TestServiceClient ServiceConnection = new TestServiceClient())
            {
                try
                {
                    ServiceConnection.getCategories();
                    result[0] = true;
                }
                catch (Exception error)
                {
                    result[1] = error.Message;
                }
            }

            e.Result = result;
        }
    }
}
