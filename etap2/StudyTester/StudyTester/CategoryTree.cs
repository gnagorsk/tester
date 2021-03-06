﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using StudyTester.ServiceReference1;

namespace StudyTester
{
    public delegate void CallbackAfterRPC();

    class CategoryTree : TreeView
    {
        BackgroundWorker addCat = new BackgroundWorker();
        BackgroundWorker delCat = new BackgroundWorker();

        CallbackAfterRPC functionToCall = null;

        CategoryTreeItem selectedParent = null;

        public CategoryTree()
            : base()
        {
            CategoryTreeItem root = new CategoryTreeItem();
            Items.Add(root);

            addCat.DoWork += addCat_DoWork;
            addCat.RunWorkerCompleted += addCat_RunWorkerCompleted;

            delCat.DoWork += delCat_DoWork;
            delCat.RunWorkerCompleted += delCat_RunWorkerCompleted;
        }

        void delCat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            IsEnabled = true;

            selectedParent.Refresh();

            if (functionToCall != null)
            {
                functionToCall();
                functionToCall = null;
            }
        }

        void delCat_DoWork(object sender, DoWorkEventArgs e)
        {
            int id = (int)e.Argument;

            using (TestServiceClient client = new TestServiceClient())
            using (TestManagementClient manage = new TestManagementClient())
            {
                try
                {
                    if (id > 0)
                    {
                        deleteCategory(client, manage, id);// manage.deleteCategory(id);
                    }
                    e.Result = "Success";
                }
                catch (Exception error)
                {
                    e.Result = error.Message;
                }
            }
        }

        void deleteCategory(TestServiceClient client, TestManagementClient manage, int id)
        {
            Dictionary<int, string> subCategories = client.getSubcategories(id);

            foreach (var pair in subCategories)
            {
                deleteCategory(client, manage, pair.Key);
            }

            Dictionary<int, string> questions = client.getQuestions(id);

            foreach (var pair in questions)
            {
                deleteQuestion(client, manage, pair.Key);
            }

            manage.deleteCategory(id);
        }

        void deleteQuestion(TestServiceClient client, TestManagementClient manage, int id)
        {
            Dictionary<int, string> answers = client.getQuestionAnswers(id);

            foreach (var pair in answers)
            {
                manage.removeAnswerFromQuestion(pair.Key, id);
            }

            manage.deleteQuestion(id);
        }

        void addCat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (SelectedItem is CategoryTreeItem)
            {
                CategoryTreeItem item = (CategoryTreeItem)SelectedItem;
                if (!item.IsExpanded)
                {
                    item.ExpandSubtree();
                }
                else
                {
                    item.Refresh();
                }
            }
            else
            {
                ((CategoryTreeItem)Items[0]).Refresh();
            }

            IsEnabled = true;

            if (functionToCall != null)
            {
                functionToCall();
                functionToCall = null;
            }
        }

        void addCat_DoWork(object sender, DoWorkEventArgs e)
        {
            object[] arg = (object[])e.Argument;
            int id = (int)arg[0];
            string name = (string)arg[1];
            
            using (TestManagementClient manage = new TestManagementClient())
            {
                try
                {
                    if (id > 0)
                    {
                        manage.createCategory(name, id);
                    }
                    else
                    {
                        manage.createCategory(name, 0);
                    }
                    arg[0] = true;
                }
                catch (Exception error)
                {
                    arg[0] = false;
                    arg[1] = error.Message;
                }
            }

            e.Result = arg;
        }

        public int SelectedCategoryId
        {
            get
            {
                if (SelectedItem is CategoryTreeItem)
                {
                    CategoryTreeItem item = (CategoryTreeItem)SelectedItem;
                    return item.Id;
                }
                else
                {
                    return -1;
                }
            }
        }

        public void DeleteSelectedCategory(CallbackAfterRPC function)
        {
            if (SelectedCategoryId != -1)
            {
                functionToCall = function;

                if (((TreeViewItem)SelectedItem).Parent is CategoryTreeItem)
                {
                    selectedParent = (CategoryTreeItem)((TreeViewItem)SelectedItem).Parent;
                }
                delCat.RunWorkerAsync(SelectedCategoryId);
                this.IsEnabled = false;
            }
        }

        public void AddSubCategory(string catName, CallbackAfterRPC function)
        {
            object[] arg = { SelectedCategoryId, catName };
            functionToCall = function;
            addCat.RunWorkerAsync(arg);
            this.IsEnabled = false;
        }
    }
}
