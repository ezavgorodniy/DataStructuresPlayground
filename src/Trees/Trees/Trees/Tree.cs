using System;

namespace Trees.Trees
{
    public class Tree<T>
        where T : IComparable
    {
        public TreeNode<T> Root { get; set; }
    }
}
