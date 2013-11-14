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
using StudyTester.ServiceReference1;

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
            Categories.AddSubCategory(categoryName.Text, EnableButtons);
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

        private void QAddButton_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new NewQuestion();
            if (dialog.ShowDialog() == true)
            {
                using (TestManagementClient manage = new TestManagementClient())
                {
                    int answer = manage.createAnswer(dialog.answerText.Text);
                    manage.createQuestion(dialog.questionText.Text, Categories.SelectedCategoryId, answer);
                }
                Questions.InitForCategory(Categories.SelectedCategoryId);
            }
        }

        private void QRemoveButton_Click_1(object sender, RoutedEventArgs e)
        {
            int qId = Questions.SelectedQuestion;
            if (qId == -1) return;
            
            TestManagementClient manage = new TestManagementClient();
            TestServiceClient client = new TestServiceClient();
            
            foreach(var a in client.getQuestionAnswers(qId)) 
            {
                manage.removeAnswerFromQuestion(a.Key, qId);
            }
            
            manage.deleteQuestion(qId);

            Questions.InitForCategory(Categories.SelectedCategoryId);
        }

        private void AAddButton_Click_1(object sender, RoutedEventArgs e)
        {
            int qId = Questions.SelectedQuestion;
            if (qId == -1) return;

            TestManagementClient manage = new TestManagementClient();
            int aId = manage.createAnswer(answerText.Text);

            manage.addAnswerToQuestion(aId, qId);
            AnswersList.InitForQuestion(Questions.SelectedQuestion);
        }

        private void ARemoveButton_Click_1(object sender, RoutedEventArgs e)
        {
            int qId = Questions.SelectedQuestion;
            if (qId == -1) return;

            int aId = AnswersList.SelectedAnswer;
            if (aId == -1) return;

            TestManagementClient manage = new TestManagementClient();
            manage.removeAnswerFromQuestion(aId, qId);
            AnswersList.InitForQuestion(Questions.SelectedQuestion);
        }

        
    }
}
