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

        List<TreeNode> listOfChildren;
        List<TreeNode> listOfParents;
        int Id;
        bool hasParent;
        bool hasChildren;

        public TreeNode(Board board)
        {
            this.board = board;
            listOfChildren = new List<TreeNode>(); ;
            listOfParents = new List<TreeNode>(); ;
            hasParent = false;
            hasChildren = false;
            Id = NODE_COUNTER++; 
        }

        /// <summary>
        /// Sets the bool of that node to know if it has a parent or not
        /// </summary>
        public bool setParent(bool hasParent) { this.hasParent = hasParent; }
        /// <summary>
        /// Returns the value of parent of that node.
        /// </summary>
        public void getParent() { return hasParent; }
        /// <summary>
        /// Sets the bool of that node to know if that node has children or not (I feel sorry for the node if it does).
        /// </summary>
        public bool setChildren(bool hasChildren) { this.hasChildren = hasChildren; }
        /// <summary>
        /// Returns the bool value of that node to see if it has children
        /// </summary>
        public void getChidlren() { return hasChildren; }
        /// <summary>
        /// Pass in a TreeNode and it will add it to node.
        /// </summary>
        public void addChild(TreeNode node)
        {
            listOfChildren.Add(node);
        }
        /// <summary>
        /// Returns list of Children nodes.
        /// </summary>
        public List<TreeNode> getListOfChildren(int Id)
        {
            return List <TreeNode> listOfChildren;
        }
        /// <summary>
        /// Adds a TreeNode which would be a parent node.
        /// </summary>
        public void addParent(TreeNode node)
        {
            listOfParents.Add(node);
        }
        /// <summary>
        /// Returns the list of Parent nodes.
        /// </summary>
        public List<TreeNode> getListOfParents(int Id)
        {
            return List <TreeNode> listOfParents;
        }
        /// <summary>
        /// Returns the ID of the TreeNode.
        /// </summary>
        public int getID() { return Id; }

        public static void Main()
        {

        }
        /*
         * Create a Tree of node of every single different game state
         * Search through each node and return the branches that return a win
         * Pick one of the trees and execute.
         */
    }
}
