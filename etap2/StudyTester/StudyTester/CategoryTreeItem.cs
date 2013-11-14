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
    class CategoryTreeItem : TreeViewItem
    {
        int CategoryId;
        bool ItemsLoaded = false;

        BackgroundWorker loader = new BackgroundWorker();

        public int Id
        {
            get
            {
                return CategoryId;
            }
        }

        public void Refresh()
        {
            if (!loader.IsBusy)
            {
                Items.Add("Loading...");
                loader.RunWorkerAsync((object)CategoryId);
            }
        }

        public CategoryTreeItem(int id=-1, string header="Availible categories:")
            : base()
        {
            CategoryId = id;
            Header = header;

            Items.Add("Loading...");

            loader.DoWork += loader_DoWork;
            loader.RunWorkerCompleted += loader_RunWorkerCompleted;

            Expanded += CategoryTreeItem_Expanded;
        }

        void loader_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Items.Clear();

            foreach (var pair in (Dictionary<int, String>)e.Result)
	        {
	            Items.Add(new CategoryTreeItem(pair.Key, pair.Value));
	        }
        }

        void loader_DoWork(object sender, DoWorkEventArgs e)
        {
            int catId = (int)e.Argument;

            Dictionary<int, String> resultCategories = null;

            using (TestServiceClient remote = new TestServiceClient())
            {
                try
                {
                    if (catId == -1)
                    {
                        resultCategories = remote.getCategories();
                    }
                    else
                    {
                        resultCategories = remote.getSubcategories(catId);
                    }
                }
                catch (Exception error)
                {
                    System.Windows.Forms.MessageBox.Show(error.Message);
                }
            }

            e.Result = resultCategories;
        }

        void CategoryTreeItem_Expanded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (!ItemsLoaded)
            {
                if (!loader.IsBusy)
                {
                    loader.RunWorkerAsync((object)CategoryId);
                    ItemsLoaded = true;
                }
            }
        }
    
    }
}
