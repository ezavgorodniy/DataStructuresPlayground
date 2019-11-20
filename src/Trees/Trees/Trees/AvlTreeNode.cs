namespace Trees.Trees
{
    public class AvlTreeNode
    {
        public AvlTreeNode Left { get; set; }

        public AvlTreeNode Right { get; set; }

        public int Height { get; set; }

        public int Value { get; set; }

        public AvlTreeNode(int value)
        {
            Value = value;
        }
    }
}
