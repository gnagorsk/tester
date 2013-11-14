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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StudyTester
{
    /// <summary>
    /// Interaction logic for Manage.xaml
    /// </summary>
    public partial class Manage : Window
    {
        private Grid[] Screens = null;

        private int CurrentDisplayPosition = 0;

        public Manage()
        {
            InitializeComponent();
            Grid[] Windows = { CatGrid, QuestGrid, AnsGrid };
            Screens = Windows;
        }

        private void MoveRight()
        {
            if (CurrentDisplayPosition < 2)
            {
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

                CurrentDisplayPosition++;
            }
        }

        private void MoveLeft()
        {
            if (CurrentDisplayPosition > 0)
            {
                foreach (var grid in Screens)
                {
                    ThicknessAnimation ta = new ThicknessAnimation();
                    ta.DecelerationRatio = 0.9;

                    //your first place
                    ta.From = grid.Margin;
                    //this move your grid 1000 over from left side
                    //you can use -1000 to move to left side
                    Thickness current = grid.Margin;
                    current.Left += 500;
                    ta.To = current;
                    //time the animation playes
                    ta.Duration = new Duration(TimeSpan.FromMilliseconds(400));
                    //dont need to use story board but if you want pause,stop etc use story board
                    grid.BeginAnimation(Grid.MarginProperty, ta);
                }

                CurrentDisplayPosition--;
            }
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

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void QuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            if (Categories.SelectedCategoryId != -1)
            {
                MoveRight();
                Questions.InitForCategory(Categories.SelectedCategoryId);
            }
        }

        private void AnswersButton_Click(object sender, RoutedEventArgs e)
        {
            if (Questions.SelectedItem != null)
            {
                MoveRight();
                AnswersList.InitForQuestion(Questions.SelectedQuestion);
            }
        }

        private void BackToCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            MoveLeft();
            // 500 <- 0
        }

        private void BackToQuestionsButton_Click(object sender, RoutedEventArgs e)
        {
            MoveLeft();
            // 1000 <- 500
        }

        private void MarkerButton_Click(object sender, RoutedEventArgs e)
        {
            if (AnswersList.SelectedAnswer != -1)
            {
                AnswersList.MarkSelectedAsRight();
            }
        }

        private void QAddButton_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void QRemoveButton_Click_1(object sender, RoutedEventArgs e)
        {
            
        }

        private void AAddButton_Click_1(object sender, RoutedEventArgs e)
        {
            
            AnswersList.AddAnswer(Microsoft.VisualBasic.Interaction.InputBox("Please provide the answer:", "Answer input", "Your answer."));
        }

        private void ARemoveButton_Click_1(object sender, RoutedEventArgs e)
        {
            AnswersList.RemoveSelected();
        }

        
    }
}
