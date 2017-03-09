using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ciaranbird
{
    public class TreeNode
    {
        private static int NODE_COUNTER = 0;


        int Id;
        bool hasParent;
        bool hasChildren;

        public TreeNode()
        {
            hasParent = false;
            hasChildren = false;
            Id = NODE_COUNTER++; 
        }
//Gets the boolean to see if node has a parent or not.
        //public bool setParent(bool parent) { this.hasParent = parent; }
        //public bool getParent(bool parent) { return hasParent; }
        //public bool setChildren(bool children) { this.hasChildren = children;
        //public bool getChidlren() { return hasChildren; }
        //public int getID() { return Id; }

        public static void Main()
        {
            TreeNode parent = new TreeNode();
            List<TreeNode> listOfChildren = new List<TreeNode>();
        }


        //Parent Node
        //Child Node
       
        /*
         * Create a Tree of node of every single different game state
         * Search through each node and return the branches that return a win
         * Pick one of the trees and execute.
         * 
         * Need:
         * Getter and Setter to set/get the nodes previous and next nodes in the search.
         * Getter and Setter to set/get id's to each nodes.
         * Getter and Setter to set/get scoreNumber for each branch.
         * Add and Remove Children Node.
         * Add and Remove Parent Node.
         */
    }
}
