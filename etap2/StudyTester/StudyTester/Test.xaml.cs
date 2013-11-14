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
using StudyTester.ServiceReference1;

namespace StudyTester
{
    /// <summary>
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        private void fillSubcategories(TreeViewItem parent, int id)
        {
            using (TestServiceClient ServiceConnection = new TestServiceClient())
            {
                Dictionary<int, String> categories = ServiceConnection.getSubcategories(id);
                foreach (var v in categories)
                {
                    TreeViewItem c = new TreeViewItem();
                    c.Header = v.Value;
                    c.Tag = v.Key;
                    fillSubcategories(c, v.Key);
                    parent.Items.Add(c);
                }
            }
        }

        private void fillCategories()
        {
            using (TestServiceClient ServiceConnection = new TestServiceClient())
            {
                Dictionary<int, String> categories = ServiceConnection.getCategories();
                foreach (var v in categories) {
                    TreeViewItem c = new TreeViewItem();
                    c.Header = v.Value;
                    c.Tag = v.Key;
                    fillSubcategories(c, v.Key);
                    CategoryTree.Items.Add(c);
                }
            }

        }

        public Test()
        {
            InitializeComponent();
            fillCategories();
        }

        private class TestAnswer
        {
            public TestAnswer(String text, int id)
            {
                this.text = text;
                this.id = id;
            }
            public String text { get; set; }
            public int id { get; set; }

            public override string ToString()
            {
                return text;
            }
        }

        private class TestQuestion
        {
            public TestQuestion(String text, int id)
            {
                this.text = text;
                this.id = id;
                shown = false;
                skipped = false;
                answers = new List<TestAnswer>();
            }
            public String text { get; set; }
            public int id { get; set; }
            public bool shown { get; set; }
            public bool skipped { get; set; }
            public List<TestAnswer> answers { get; set; }
        }

        private List<TestQuestion> questions = new List<TestQuestion>();

        private int answered_questions { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CategoryTab.IsSelected = false;
            CategoryTab.IsEnabled = false;
            QuestionsTab.IsEnabled = true;
            QuestionsTab.IsSelected = true;

            answered_questions = 0;

            TreeViewItem selectedItem = ((TreeViewItem)CategoryTree.SelectedItem);
            using (TestServiceClient ServiceConnection = new TestServiceClient())
            {
                Dictionary<int, String> squestions = ServiceConnection.getQuestions((int)selectedItem.Tag);
                foreach (var vq in squestions)
                {
                    TestQuestion q = new TestQuestion(vq.Value, vq.Key);
                    Dictionary<int, String> answers = ServiceConnection.getQuestionAnswers(vq.Key);
                    foreach (var va in answers)
                    {
                        q.answers.Add(new TestAnswer(va.Value, va.Key));
                    }
                    questions.Add(q);
                }
            }

            QuestionsLeft.Content = "Questions left: " + answered_questions + "/" + questions.Count;

            if (questions.Count == 0)
            {
                CheckAnswer.Content = "Score";
            }

            showNext();
        }

        TestQuestion current;

        public void showNext()
        {
            Correctness.Content = "";
            current = checkNext();

            if (current != null)
            {
                current.shown = true;
                QuestionText.Content = current.text;
                AnswerList.ItemsSource = current.answers;
            }
            
        }

        private TestQuestion checkNext()
        {
            TestQuestion next = null;
            foreach (TestQuestion q in questions)
            {
                if (q.shown == false && q.skipped == false)
                {
                    next = q;
                    break;
                }
            }

            if (next == null)
            {
                foreach (TestQuestion q in questions)
                {
                    if (q.shown == false && q.skipped == true)
                    {
                        next = q;
                        break;
                    }
                }
            }

            return next;
        }

        private void CheckAnswer_Click(object sender, RoutedEventArgs e)
        {
            if (CheckAnswer.Content.Equals("Next"))
            {
                CheckAnswer.Content = "Check";
                SkipQuestion.IsEnabled = true;
                showNext();
            }
            else if (CheckAnswer.Content.Equals("Score"))
            {
                QuestionsTab.IsEnabled = false;
                QuestionsTab.IsSelected = false;
                txtScore.Content = answered_questions + "/" + questions.Count;
                qrScore.Text = answered_questions + "/" + questions.Count;
                ScoreTab.IsEnabled = true;
                ScoreTab.IsSelected = true;
            }
            else
            {
                SkipQuestion.IsEnabled = false;
                TestAnswer answer = (TestAnswer)AnswerList.SelectedItem;
                using (TestServiceClient ServiceConnection = new TestServiceClient())
                {
                    bool correct = ServiceConnection.validateAnswer(current.id, answer.id);
                    if (correct)
                    {
                        Correctness.Content = "Correct!";
                        QuestionsLeft.Content = "Questions left: " + ++answered_questions + "/" + questions.Count;
                    }
                    else
                    {
                        Correctness.Content = "Wrong!";
                    }
                    if (checkNext() != null)
                    {
                        CheckAnswer.Content = "Next";
                    }
                    else
                    {
                        AnswerList.ItemsSource = null;
                        QuestionText.Content = "Congratulations, you have answered all the questions.";
                        CheckAnswer.Content = "Score";
                        SkipQuestion.IsEnabled = false;
                    }
                }
            }
        }

        private void SkipQuestion_Click(object sender, RoutedEventArgs e)
        {
            current.skipped = true;
            showNext();
        }
    }
}
