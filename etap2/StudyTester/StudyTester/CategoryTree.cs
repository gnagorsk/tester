using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StudyTester
{
    class CategoryTree : TreeView
    {
        public CategoryTree()
            : base()
        {
            CategoryTreeItem root = new CategoryTreeItem();
            Items.Add(root);
        }
    }
}
