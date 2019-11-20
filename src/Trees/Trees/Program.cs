using System;
using Trees.Trees;

namespace Trees
{
    class Program
    {
        static void Main(string[] args)
        {
            var avlTree = new AvlTree();

            avlTree.Root = avlTree.Insert(avlTree.Root, 9);
            avlTree.Root = avlTree.Insert(avlTree.Root, 5);
            avlTree.Root = avlTree.Insert(avlTree.Root, 10);
            avlTree.Root = avlTree.Insert(avlTree.Root, 0);
            avlTree.Root = avlTree.Insert(avlTree.Root, 6);
            avlTree.Root = avlTree.Insert(avlTree.Root, 11);
            avlTree.Root = avlTree.Insert(avlTree.Root, -1);
            avlTree.Root = avlTree.Insert(avlTree.Root, 1);
            avlTree.Root = avlTree.Insert(avlTree.Root, 2);

            CheckResult(avlTree.Root);

            avlTree.Root = avlTree.DeleteNode(avlTree.Root, 10);

            CheckResultAfterDeletion(avlTree.Root);
        }

        private static void CheckResult(AvlTreeNode root)
        {
            AssertTrue(() => root.Value == 9);


            AssertTrue(() => root.Left.Value == 1);
            AssertTrue(() => root.Left.Left.Value == 0);
            AssertTrue(() => root.Left.Left.Left.Value == -1);
            AssertTrue(() => root.Left.Left.Left.Right == null);
            AssertTrue(() => root.Left.Left.Left.Left == null);
            AssertTrue(() => root.Left.Right.Value == 5);

            AssertTrue(() => root.Left.Right.Left.Value == 2);
            AssertTrue(() => root.Left.Right.Left.Left == null);
            AssertTrue(() => root.Left.Right.Left.Right == null);

            AssertTrue(() => root.Left.Right.Right.Value == 6);
            AssertTrue(() => root.Left.Right.Right.Left == null);
            AssertTrue(() => root.Left.Right.Right.Right == null);

            AssertTrue(() => root.Right.Value == 10);
            AssertTrue(() => root.Right.Left == null);


            AssertTrue(() => root.Right.Right.Value == 11);
            AssertTrue(() => root.Right.Right.Right == null);
            AssertTrue(() => root.Right.Right.Left == null);
        }


        private static void CheckResultAfterDeletion(AvlTreeNode root)
        {
            AssertTrue(() => root.Value == 1);


            AssertTrue(() => root.Left.Value == 0);
            AssertTrue(() => root.Left.Right == null);

            AssertTrue(() => root.Left.Left.Value == -1);
            AssertTrue(() => root.Left.Left.Right == null);
            AssertTrue(() => root.Left.Left.Left == null);

            AssertTrue(() => root.Right.Value == 9);
            AssertTrue(() => root.Right.Left.Value == 5);
            AssertTrue(() => root.Right.Right.Value == 11);
            AssertTrue(() => root.Right.Right.Left == null);
            AssertTrue(() => root.Right.Right.Right == null);


            AssertTrue(() => root.Right.Left.Right.Value == 6);
            AssertTrue(() => root.Right.Left.Right.Left == null);
            AssertTrue(() => root.Right.Left.Right.Right == null);

            AssertTrue(() => root.Right.Left.Left.Value == 2);
            AssertTrue(() => root.Right.Left.Left.Left == null);
            AssertTrue(() => root.Right.Left.Left.Right == null);
        }

        private static void AssertTrue(Func<bool> func)
        {
            if (!func())
            {
                Console.WriteLine("ERROR");
            }
        }
    }
}
