using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class TreeBranch
    {
        public string lb { get; set; }
        public bool treeDrawn { get; set; }
        public TreeNode ConnNode { get; set; }              // Node this branch is connected to
        
        public TreeBranch(string lb)
        {
            treeDrawn = false;
            this.lb = lb;
        }

    }
}
