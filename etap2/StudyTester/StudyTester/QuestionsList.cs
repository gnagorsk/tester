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

        public QuestionsList()
            : base()
        {
            loader.DoWork += loader_DoWork;
            loader.RunWorkerCompleted += loader_RunWorkerCompleted;

            adder.DoWork += adder_DoWork;
            adder.RunWorkerCompleted += adder_RunWorkerCompleted;

            deleter.DoWork += deleter_DoWork;
            deleter.RunWorkerCompleted += deleter_RunWorkerCompleted;
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
            Items.Clear();
            Items.Add("Loading...");
            loader.RunWorkerAsync(categoryId);
        }
    }
}
