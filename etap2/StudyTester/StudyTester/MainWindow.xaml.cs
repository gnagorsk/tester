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
using System.Windows.Media.Animation;

namespace StudyTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DoubleAnimationUsingKeyFrames
                    popButtonX = new DoubleAnimationUsingKeyFrames(),
                    popButtonY = new DoubleAnimationUsingKeyFrames();

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
            if (worker.IsBusy)
            {
                MessageBox.Show("LOL!");
            }
            worker.RunWorkerAsync(clicked);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;
            Button clicked = (Button)result[2];

            if ((bool)result[0] == false)
            {
                MessageBox.Show((string)result[1]);
                
                clicked.Content = "Start!";
                clicked.IsEnabled = true;
            }
            else
            {
                clicked.Content = "Connected!";
                clicked.Click -= Button_Click_1;
                clicked.IsEnabled = true;

                int DurationMilis = 300;

                popButtonX.KeyFrames.Add(new LinearDoubleKeyFrame(StartButton.Width, KeyTime.FromPercent(0)));
                popButtonX.KeyFrames.Add(new LinearDoubleKeyFrame(StartButton.Width + 20, KeyTime.FromPercent(0.5)));
                popButtonX.KeyFrames.Add(new LinearDoubleKeyFrame(StartButton.Width, KeyTime.FromPercent(1)));

                popButtonY.KeyFrames.Add(new LinearDoubleKeyFrame(StartButton.Height, KeyTime.FromPercent(0)));
                popButtonY.KeyFrames.Add(new LinearDoubleKeyFrame(StartButton.Height + 20, KeyTime.FromPercent(0.5)));
                popButtonY.KeyFrames.Add(new LinearDoubleKeyFrame(StartButton.Height, KeyTime.FromPercent(1)));

                popButtonX.Completed += popButtonX_Completed;

                popButtonY.Duration = popButtonX.Duration = new Duration(TimeSpan.FromMilliseconds(DurationMilis));

                StartButton.BeginAnimation(Button.WidthProperty, popButtonX);
                StartButton.BeginAnimation(Button.HeightProperty, popButtonY);
            }
        }

        private void popButtonX_Completed(object sender, EventArgs e)
        {
            Grid[] Screens = { WelcomeScreen, ActionSelect };

            foreach (var grid in Screens)
            {
                ThicknessAnimation ta = new ThicknessAnimation();
                ta.DecelerationRatio = 0.9;

                //your first place
                ta.From = grid.Margin;
                //this move your grid 1000 over from left side
                //you can use -1000 to move to left side
                Thickness current = grid.Margin;
                current.Left -= 500;
                ta.To = current;
                //time the animation playes
                ta.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                //dont need to use story board but if you want pause,stop etc use story board
                grid.BeginAnimation(Grid.MarginProperty, ta);
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

        private void ManageButton_Click_1(object sender, RoutedEventArgs e)
        {
            Manage ManageWindow = new Manage();
            this.Hide();
            ManageWindow.ShowDialog();
            this.Show();
        }

        private void TestButton_Click_1(object sender, RoutedEventArgs e)
        {
            Test TestWindow = new Test();
            this.Hide();
            TestWindow.ShowDialog();
            this.Show();
        }

        private void ExitButton_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
