using StudyTester.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace StudyTester
{
    class AnswersBox : ListBox
    {
        int questionId = -1;

        int correctAnswer = 0;

        BackgroundWorker
            loader = new BackgroundWorker(),
            adder = new BackgroundWorker(),
            deleter = new BackgroundWorker();

        public AnswersBox()
            : base()
        {
            loader.DoWork += loader_DoWork;
            loader.RunWorkerCompleted += loader_RunWorkerCompleted;

            adder.DoWork += adder_DoWork;
            adder.RunWorkerCompleted += adder_RunWorkerCompleted;

            deleter.DoWork += deleter_DoWork;
            deleter.RunWorkerCompleted += deleter_RunWorkerCompleted;
        }

        public int SelectedAnswer
        {
            get
            {
                if (SelectedItem is ListBoxItem)
                {
                    ListBoxItem item = (ListBoxItem)SelectedItem;
                    return (int)item.MinWidth;
                }
                else
                {
                    return -1;
                }
            }
        }

        void deleter_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void deleter_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        void adder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void adder_DoWork(object sender, DoWorkEventArgs e)
        {
            throw new NotImplementedException();
        }

        void loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;
            ListBoxItem item;

            correctAnswer = (int)result[2];

            if ((bool)result[0])
            {
                Dictionary<int, string> results = (Dictionary<int, string>)result[1];

                Items.Clear();

                foreach (var pair in results)
                {
                    item = new ListBoxItem();
                    item.MinWidth = pair.Key; // id masked in minwidth
                    item.Content = pair.Value;
                    if (pair.Key == correctAnswer)
                    {
                        item.Background = Brushes.LightGreen;
                    }
                    Items.Add(item);
                }
            }
            else
            {
                System.Windows.MessageBox.Show((string)result[1]);
            }
        }

        void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] result = { true, null, 0};

            using (TestServiceClient client = new TestServiceClient())
            {
                try
                {
                    result[1] = client.getQuestionAnswers((int)e.Argument);

                    foreach(var answer in (Dictionary<int, string>) result[1])
                    {
                        if (client.validateAnswer((int)e.Argument, answer.Key))
                        {
                            result[2] = answer.Key;
                        }
                    }
                }
                catch (Exception error)
                {
                    result[0] = false;
                    result[1] = error.Message;
                }
            }

            e.Result = result;
        }

        public void InitForQuestion(int newQuestionID)
        {
            questionId = newQuestionID;
            Items.Clear();
            Items.Add("Loading...");
            loader.RunWorkerAsync(questionId);
        }
    }
}
