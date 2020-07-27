using System;
using System.Collections;
using System.Collections.Generic;

namespace Tree
{
    public class BinaryTree<T> : ICollection<T> where T : IComparable<T>
    {
        private Node<T> root = null;

        #region methods of interacting with the AVL tree (public methods)

        /// <summary>
        /// Allows to get amount of elements in the
        /// current BinaryTree class instance
        /// </summary>
        public int Count
        {
            get
            {
                int Count(Node<T> node)
                {
                    if(node == null)
                    {
                        return 0;
                    }
                    return 1 + Count(node.Left) + Count(node.Right);
                }

                return Count(root);
            }
        }

        public bool IsReadOnly => false;

        public void Add(T item)
        {
            if(root == null)
            {
                root = new Node<T>(item);
            }
            else
            {
                root = RecursiveInsert(item, root);
            }
        }

        public void AddRange(IEnumerable<T> collection)
        {
            foreach(T item in collection)
            {
                Add(item);
            }
        }

        public bool Remove(T item)
        {
            bool isRemoved = false;

            if (item != null)
            {
                root = RecursiveRemove(item, root, ref isRemoved);
            }

            return isRemoved;
        }

        public void Clear()
        {
            root = null;
        }

        public bool Contains(T item)
        {
            if(item == null)
            {
                return false;
            }

            return RecursiveContains(item, root);
        }

        /// <summary>
        /// Searches the tree nodes and returns the minimum value of the tree node
        /// </summary>
        /// <returns>minimum value of a tree node</returns>
        public T Min()
        {
            if(root == null)
            {
                throw new BinaryTreeException("tree contains no elements");
            }

            return RecursiveMin(root).Item;
        }

        /// <summary>
        /// searches the tree nodes and returns the maximum value of the tree node
        /// </summary>
        /// <returns>maximum value of a tree node</returns>
        public T Max()
        {
            if (root == null)
            {
                throw new BinaryTreeException("tree contains no elements");
            }

            return RecursiveMax(root).Item;
        }
       
        public void CopyTo(T[] array, int arrayIndex)
        {
            int currentIndex = arrayIndex;

            foreach(T item in this)
            {
                array[currentIndex] = item;
                currentIndex++;
            }
        }

        public IEnumerator<T> GetEnumerator() => InOrderTravers(root);

        IEnumerator<T> InOrderTravers(Node<T> node)
        {
            if (node != null)
            {
                InOrderTravers(node.Left);
                yield return node.Item;
                InOrderTravers(node.Right);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region recursive methods

        /// <summary>
        /// Searches for a node with the specified value in the tree
        /// </summary>
        /// <param name="item">desired value</param>
        /// <param name="node">root node for searching</param>
        /// <returns>is find specified item in the subtree</returns>
        private static bool RecursiveContains(T item, Node<T> node)
        {
            if(node == null)
            {
                return false;
            }

            switch(item.CompareTo(node.Item))
            {
                case 0:     //item = current node
                    {
                        return true;
                    }
                case 1:     //item > current node
                    {
                        return RecursiveContains(item, node.Right);
                    }
                case -1:    //item < current node
                    {
                        return RecursiveContains(item, node.Left);
                    }
            }

            return false;
        }

        /// <summary>
        /// Searches the tree nodes and returns the minimum value of the tree node
        /// </summary>
        /// <param name="node">root node for searching</param>
        /// <returns>minimum value of a tree node</returns>
        private static Node<T> RecursiveMin(Node<T> node)
        {
            if(node.Left != null)
            {
                return RecursiveMin(node.Left);
            }

            return node;
        }

        /// <summary>
        /// Searches the tree nodes and returns the maximum value of the tree node
        /// </summary>
        /// <param name="node">root node for searching</param>
        /// <returns>maximum value of a tree node</returns>
        private static Node<T> RecursiveMax(Node<T> node)
        {
            if (node.Right != null)
            {
                return RecursiveMin(node.Right);
            }

            return node;
        }

        /// <summary>
        /// Remove node with mininal value in the tree in which the root is node variable
        /// </summary>
        /// <param name="node">root node for searching min and removing</param>
        /// <returns>new tree root</returns>
        private static Node<T> RemoveMin(Node<T> node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = RemoveMin(node.Left);
            return BalanceNode(node);
        }

        /// <summary>
        /// Adds a node to the tree
        /// </summary>
        /// <param name="item">value to add</param>
        /// <param name="node">the root of the tree in which to insert</param>
        /// <returns>new tree root</returns>
        private static Node<T> RecursiveInsert(T item, Node<T> node)
        {
            if(node == null)
            {
                return new Node<T>(item);
            }

            switch (item.CompareTo(node.Item))
            {
                case 0:     //item = current node
                    {
                        throw new BinaryTreeException("Value with same key also exists in the tree");
                    }
                case 1:     //item > current node
                    {
                        node.Right = RecursiveInsert(item, node.Right);
                        break;
                    }
                case -1:    //item < current node
                    {
                        node.Left = RecursiveInsert(item, node.Left);
                        break;
                    }
            }

            return BalanceNode(node);
        }

        /// <summary>
        /// Remove a node from the tree
        /// </summary>
        /// <param name="item">value to remove</param>
        /// <param name="node">the root of the tree in which to remove</param>
        /// <returns>new tree root</returns>
        private static Node<T> RecursiveRemove(T item, Node<T> node, ref bool isRemoved)
        {
            if (node == null)
            {
                return null;
            }

            switch (item.CompareTo(node.Item))
            {
                case 0: //item = current node
                    {
                        Node<T> leftSubTree = node.Left;
                        Node<T> rightSubTree = node.Right;
                        node = null;
                        if (leftSubTree == null)
                        {
                            return rightSubTree;
                        }
                        if (rightSubTree == null)
                        {
                            return leftSubTree;
                        }

                        Node<T> min = RecursiveMin(rightSubTree);
                        min.Right = RemoveMin(rightSubTree);
                        min.Left = leftSubTree;
                        isRemoved = true;
                        return BalanceNode(min);

                    }
                case 1: //item > current node
                    {
                        node.Right = RecursiveRemove(item, node.Right, ref isRemoved);
                        break;
                    }
                case -1: //item < current node
                    {
                        node.Left = RecursiveRemove(item, node.Left, ref isRemoved);
                        break;
                    }
            }
            return BalanceNode(node);
        }

        #endregion

        #region tree balance methods

        /// <summary>
        /// Returns the height of a tree node
        /// </summary>
        /// <param name="node">tree node</param>
        /// <returns>height of a tree node</returns>
        private static int NodeHeight(Node<T> node)
        {
            if(node == null)
            {
                return -1;
            }
            else
            {
                return node.Height;
            }
        }

        /// <summary>
        /// Calculates the difference in heights of the left and right subtrees
        /// </summary>
        /// <param name="node">tree node</param>
        /// <returns>difference in heights of the left and right subtrees</returns>
        private static int Delta(Node<T> node) => NodeHeight(node.Right) - NodeHeight(node.Left);

        /// <summary>
        /// Balances one level (root) of the tree
        /// </summary>
        /// <param name="node">tree root</param>
        /// <returns>new tree root</returns>
        private static Node<T> BalanceNode(Node<T> node)
        {
            int leftHeight = NodeHeight(node.Left);
            int rightHeight = NodeHeight(node.Right);

            if(leftHeight > rightHeight)
            {
                node.Height = leftHeight + 1;
            }
            else
            {
                node.Height = rightHeight + 1;
            }

            if(Delta(node) == 2)
            {
                if(Delta(node.Right) < 0)
                {
                    node.Right = SmallRightRotate(node.Right);
                }

                return SmallLeftRotate(node);
            }
            if(Delta(node) == -2)
            {
                if(Delta(node.Left) > 0)
                {
                    node.Left = SmallLeftRotate(node.Left);
                }

                return SmallRightRotate(node);
            }
            return node;
        }

        /// <summary>
        /// Performs a small left rotation of the tree around the specified node
        /// </summary>
        /// <param name="node">tree node</param>
        /// <returns>new tree root</returns>
        private static Node<T> SmallLeftRotate(Node<T> node)
        {
            Node<T> tmp = node.Right;
            node.Right = tmp.Left;
            tmp.Left = node;
            node.Height -= 2;
            return tmp;
        }

        /// <summary>
        /// Performs a small right rotation of the tree around the specified node
        /// </summary>
        /// <param name="node">tree node</param>
        /// <returns>new tree root</returns>
        private static Node<T> SmallRightRotate(Node<T> node)
        {
            Node<T> tmp = node.Left;
            node.Left = tmp.Right;
            tmp.Right = node;
            node.Height -= 2;
            return tmp;
        }

        #endregion

        /// <summary>
        /// Check equality of two BinaryTree class instances
        /// </summary>
        /// <param name="obj">second tree for comparison</param>
        /// <returns>is two BinaryTree class instances equals</returns>
        public override bool Equals(object obj)
        {
            if(obj != null && obj is BinaryTree<T>)
            {
                return root.Equals((obj as BinaryTree<T>).root);
            }

            return false;
        }

        /// <summary>
        /// Get string representation of BinaryTree class instance
        /// </summary>
        /// <returns>string of the tree rotated to the left 90 degrees</returns>
        public override string ToString()
        {
            if(root == null)
            {
                return string.Empty;
            }

            return root.ToString();
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            if(root == null)
            {
                return 0;
            }

            return root.GetHashCode();
        }
    }
}
