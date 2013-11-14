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

        public CallbackAfterRPC functionToCall = null;

        BackgroundWorker
            loader = new BackgroundWorker(),
            adder = new BackgroundWorker(),
            deleter = new BackgroundWorker(),
            marker = new BackgroundWorker();

        public AnswersBox()
            : base()
        {
            loader.DoWork += loader_DoWork;
            loader.RunWorkerCompleted += loader_RunWorkerCompleted;

            adder.DoWork += adder_DoWork;
            adder.RunWorkerCompleted += marker_RunWorkerCompleted;

            deleter.DoWork += deleter_DoWork;
            deleter.RunWorkerCompleted += marker_RunWorkerCompleted;

            marker.DoWork += marker_DoWork;
            marker.RunWorkerCompleted += marker_RunWorkerCompleted;
        }

        void marker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;

            if ((bool)result[0])
            {
                InitForQuestion(questionId);
            }
            else
            {
                System.Windows.MessageBox.Show((string)result[1]);
                if (functionToCall != null)
                {
                    functionToCall();
                    functionToCall = null;
                }
            }
        }

        void marker_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            int questionId = (int)arg[0];
            int answerId = (int)arg[1];

            arg[0] = true;
            arg[1] = "Success";

            using (TestManagementClient manager = new TestManagementClient())
            {
                try
                {
                    //manager.markAsCorrect(questionId, answerId);
                }
                catch (Exception error)
                {
                    arg[0] = false;
                    arg[1] = error.Message;
                }
            }

            e.Result = arg;
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

        public void MarkSelectedAsRight(CallbackAfterRPC completionRoutine)
        {
            functionToCall = completionRoutine;
            if (SelectedAnswer != -1)
            {
                object[] arg = { questionId, SelectedAnswer };
                marker.RunWorkerAsync(arg);
            }
            else
            {
                if (functionToCall != null)
                {
                    functionToCall();
                    functionToCall = null;
                }
            }
        }

        public void RemoveSelected(CallbackAfterRPC completionRoutine)
        {
            functionToCall = completionRoutine;
            if (SelectedAnswer != -1)
            {
                object[] arg = { questionId, SelectedAnswer };
                deleter.RunWorkerAsync(arg);
            }
            else
            {
                if (functionToCall != null)
                {
                    functionToCall();
                    functionToCall = null;
                }
            }
        }

        void deleter_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            int questionId = (int)arg[0];
            int answerId = (int)arg[1];

            arg[0] = true;
            arg[1] = "Success";

            using (TestManagementClient manager = new TestManagementClient())
            {
                try
                {
                    manager.removeAnswerFromQuestion(answerId, questionId);
                }
                catch (Exception error)
                {
                    arg[0] = false;
                    arg[1] = error.Message;
                }
            }

            e.Result = arg;
        }

        public void AddAnswer(string body, CallbackAfterRPC completionRoutine)
        {
            functionToCall = completionRoutine;
            object[] arg = { questionId, body };
            adder.RunWorkerAsync(arg);
        }

        void adder_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            int questionId = (int)arg[0];
            string answerBody = (string)arg[1];

            arg[0] = true;
            arg[1] = "Success";

            using (TestManagementClient manager = new TestManagementClient())
            {
                try
                {
                    int answerId = manager.createAnswer(answerBody);
                    manager.addAnswerToQuestion(answerId, questionId);
                }
                catch (Exception error)
                {
                    arg[0] = false;
                    arg[1] = error.Message;
                }
            }

            e.Result = arg;
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

            if (functionToCall != null)
            {
                functionToCall();
                functionToCall = null;
            }

            IsEnabled = true;
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
            IsEnabled = false;
            Items.Clear();
            Items.Add("Loading...");
            loader.RunWorkerAsync(questionId);
        }
    }
}
