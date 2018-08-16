using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vehicle
{
    class TreeNode
    {
        
        public string lb { get; set; }                                  // status of a node is shown but not used in algorithms
        public string Dec_AtribType { get; set; }                       // Attribute type this node uses as rule
        public List<TreeBranch> Branches { get; set; }                  // Branches coming out of this node
        public bool isLeaf { get; set; }
        public double StaticGain { get; set; }                          // Gain of the attribute type on this node

        public TreeNode(string lb)
        {
            this.lb = lb;
            this.Dec_AtribType = null;
            Branches = new List<TreeBranch>();

        }

    }
}
