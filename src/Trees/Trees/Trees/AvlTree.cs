using System;

namespace Trees.Trees
{
    public class AvlTree
    {
        public AvlTreeNode Root { get; set; }

        public AvlTreeNode Insert(AvlTreeNode node, int key)
        {
            /* 1. Perform the normal BST insertion */
            if (node == null)
            {
                return new AvlTreeNode(key);
            }

            if (key < node.Value)
            {
                node.Left = Insert(node.Left, key);
            }
            else if (key > node.Value)
            {
                node.Right = Insert(node.Right, key);
            }
            else // Duplicate keys not allowed  
            {
                return node;
            }

            /* 2. Update height of this ancestor node */
            IncreaseHeight(node);
            return BalanceTree(node, key);
        }

        private static AvlTreeNode BalanceTree(AvlTreeNode node, int value)
        {
            var balance = GetBalance(node);

            // Left left case
            if (balance > 1 && value < node.Left.Value)
            {
                return RightRotate(node);

            }

            // Right right case
            if (balance < -1 && value > node.Right.Value)
            {
                return LeftRotate(node);
            }

            // Left right case
            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Right left  case
            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private static int GetHeight(AvlTreeNode node) => node?.Height ?? 0;

        private static int GetBalance(AvlTreeNode node) =>
            node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        // A utility function to right  
        // rotate subtree rooted with node  
        // See the diagram given above.  
        private static AvlTreeNode RightRotate(AvlTreeNode node)
        {
            var originalLeft = node.Left;
            var originalLeftRight = originalLeft.Right;

            // Perform rotation  
            originalLeft.Right = node;
            node.Left = originalLeftRight;

            // Update heights  
            IncreaseHeight(node);
            IncreaseHeight(originalLeft);

            // Return new root  
            return originalLeft;
        }

        // A utility function to left 
        // rotate subtree rooted with node  
        // See the diagram given above.  
        private static AvlTreeNode LeftRotate(AvlTreeNode node)
        {
            var originalRight = node.Right;
            var originalRightLeft = originalRight.Left;

            // Perform rotation  
            originalRight.Left = node;
            node.Right = originalRightLeft;

            // Update heights  
            IncreaseHeight(node);
            IncreaseHeight(originalRight);

            // Return new root  
            return originalRight;
        }

        private static void IncreaseHeight(AvlTreeNode node)
        {
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }


        /* Given a non-empty binary search tree, return the 
           node with minimum key value found in that tree. 
           Note that the entire tree does not need to be 
           searched. */
        AvlTreeNode minValueNode(AvlTreeNode node)
        {
            var current = node;

            /* loop down to find the leftmost leaf */
            while (current.Left != null)
            {
                current = current.Left;
            }

            return current;
        }

        public AvlTreeNode DeleteNode(AvlTreeNode root, int key)
        {
            // STEP 1: PERFORM STANDARD BST DELETE  
            if (root == null)
                return root;

            // If the key to be deleted is smaller than  
            // the root's key, then it lies in left subtree  
            if (key < root.Value)
                root.Left = DeleteNode(root.Left, key);

            // If the key to be deleted is greater than the  
            // root's key, then it lies in right subtree  
            else if (key > root.Value)
                root.Right = DeleteNode(root.Right, key);

            // if key is same as root's key, then this is the node  
            // to be deleted  
            else
            {

                // node with only one child or no child  
                if (root.Left == null || root.Right == null)
                {
                    AvlTreeNode temp = null;
                    if (temp == root.Left)
                        temp = root.Right;
                    else
                        temp = root.Left;

                    // No child case  
                    if (temp == null)
                    {
                        temp = root;
                        root = null;
                    }
                    else // One child case  
                        root = temp; // Copy the contents of  
                                     // the non-empty child  
                }
                else
                {

                    // node with two children: Get the inorder  
                    // successor (smallest in the right subtree)  
                    var temp = minValueNode(root.Right);

                    // Copy the inorder successor's data to this node  
                    root.Value = temp.Value;

                    // Delete the inorder successor  
                    root.Right = DeleteNode(root.Right, temp.Value);
                }
            }

            // If the tree had only one node then return  
            if (root == null)
                return root;

            // STEP 2: UPDATE HEIGHT OF THE CURRENT NODE  
            root.Height = Math.Max(GetHeight(root.Left), GetHeight(root.Right)) + 1;

            // STEP 3: GET THE BALANCE FACTOR 
            // OF THIS NODE (to check whether  
            // this node became unbalanced)  
            int balance = GetBalance(root);

            // If this node becomes unbalanced,  
            // then there are 4 cases  
            // Left Left Case  
            if (balance > 1 && GetBalance(root.Left) >= 0)
                return RightRotate(root);

            // Left Right Case  
            if (balance > 1 && GetBalance(root.Left) < 0)
            {
                root.Left = LeftRotate(root.Left);
                return RightRotate(root);
            }

            // Right Right Case  
            if (balance < -1 && GetBalance(root.Right) <= 0)
                return LeftRotate(root);

            // Right Left Case  
            if (balance < -1 && GetBalance(root.Right) > 0)
            {
                root.Right = RightRotate(root.Right);
                return LeftRotate(root);
            }

            return root;
        }
    }
}


