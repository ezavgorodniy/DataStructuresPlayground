using System;

namespace Trees.Trees
{
    public class TreeNode<T>
        where T : IComparable
    {
        public TreeNode<T> Left { get; set; }

        public TreeNode<T> Right { get; set; }

        public T Value { get; set; }

        public TreeNode(T value)
        {
            Value = value;
        }
    }
}
