using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using StudyTester.ServiceReference1;

namespace StudyTester
{
    class QuestionsList : ListBox
    {
        int categoryId = -1;

        BackgroundWorker
            loader = new BackgroundWorker(),
            adder = new BackgroundWorker(),
            deleter = new BackgroundWorker();

        public CallbackAfterRPC functionToCall = null;

        public QuestionsList()
            : base()
        {
            loader.DoWork += loader_DoWork;
            loader.RunWorkerCompleted += loader_RunWorkerCompleted;

            adder.DoWork += adder_DoWork;
            adder.RunWorkerCompleted += adder_RunWorkerCompleted;

            deleter.DoWork += deleter_DoWork;
            deleter.RunWorkerCompleted += adder_RunWorkerCompleted;
        }

        public int SelectedQuestion
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

        void deleter_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            int qId = (int)arg[0];

            arg[0] = true;
            arg[1] = "Success";

            using (TestServiceClient client = new TestServiceClient())
            using (TestManagementClient manage = new TestManagementClient())
            {
                try
                {
                    foreach (var a in client.getQuestionAnswers(qId))
                    {
                        manage.removeAnswerFromQuestion(a.Key, qId);
                    }

                    manage.deleteQuestion(qId);
                }
                catch (Exception error)
                {
                    arg[0] = false;
                    arg[1] = error.Message;
                }
            }

            e.Result = arg;
        }

        void adder_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;

            if ((bool)result[0])
            {
                InitForCategory(categoryId);
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

        void adder_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            string question = (string)arg[0];
            string answer = (string)arg[1];
            int categoryId = (int)arg[2];

            arg[0] = true;
            arg[1] = "Success";

            using (TestManagementClient manager = new TestManagementClient())
            {
                try
                {
                    int answerId = manager.createAnswer(answer);
                    manager.createQuestion(question, categoryId, answerId);
                }
                catch (Exception error)
                {
                    arg[0] = false;
                    arg[1] = error.Message;
                }
            }

            e.Result = arg;
        }

        public void AddQuestion(string QBody, string ABody, CallbackAfterRPC completionRoutine)
        {
            functionToCall = completionRoutine;
            object[] arg = { QBody, ABody, categoryId };
            adder.RunWorkerAsync(arg);
        }

        public void RemoveSelectedQuestion(CallbackAfterRPC completionRoutine)
        {
            functionToCall = completionRoutine;
            if (SelectedQuestion != -1)
            {
                object[] arg = { SelectedQuestion, null };
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

        void loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            object[] result = (object[])e.Result;
            ListBoxItem item;

            if ((bool)result[0])
            {
                Dictionary<int, string> results = (Dictionary<int, string>)result[1];

                Items.Clear();

                foreach (var pair in results)
                {
                    item = new ListBoxItem();
                    item.MinWidth = pair.Key;
                    item.Content = pair.Value;
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
            object[] result = { true, null };

            using (TestServiceClient client = new TestServiceClient())
            {
                try
                {
                    result[1] = client.getQuestions((int)e.Argument);
                }
                catch (Exception error)
                {
                    result[0] = false;
                    result[1] = error.Message;
                }
            }

            e.Result = result;
        }

        public void InitForCategory(int NewcategoryId)
        {
            categoryId = NewcategoryId;
            IsEnabled = false;
            Items.Clear();
            Items.Add("Loading...");
            loader.RunWorkerAsync(categoryId);
        }
    }
}
