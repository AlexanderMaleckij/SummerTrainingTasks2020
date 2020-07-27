using System;
using System.Collections.Generic;
using System.Text;

namespace Tree
{
    public class BinaryTree<T> where T : IComparable<T>
    {
        private Node<T> root = null;

        #region methods of interacting with the tree (public methods)

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

        public void Remove(T item)
        {
            if(item != null)
            {
                root = RecursiveRemove(item, root);
            }
        }

        public bool Contains(T item)
        {
            if(item == null)
            {
                return false;
            }

            return RecursiveContains(item, root);
        }

        public T Min()
        {
            if(root == null)
            {
                throw new BinaryTreeException("tree contains no elements");
            }

            return RecursiveMin(root).Item;
        }

        public T Max()
        {
            if (root == null)
            {
                throw new BinaryTreeException("tree contains no elements");
            }

            return RecursiveMax(root).Item;
        }

        #endregion

        #region recursive methods

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

        private static Node<T> RecursiveMin(Node<T> node)
        {
            if(node.Left != null)
            {
                return RecursiveMin(node.Left);
            }

            return node;
        }

        private static Node<T> RecursiveMax(Node<T> node)
        {
            if (node.Right != null)
            {
                return RecursiveMin(node.Right);
            }

            return node;
        }

        private static Node<T> RemoveMin(Node<T> node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            node.Left = RemoveMin(node.Left);
            return node;
        }

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

            return node;
        }

        private static Node<T> RecursiveRemove(T item, Node<T> node)
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

                        return min;

                    }
                case 1: //item > current node
                    {
                        node.Right = RecursiveRemove(item, node.Right);
                        break;
                    }
                case -1: //item < current node
                    {
                        node.Left = RecursiveRemove(item, node.Left);
                        break;
                    }
            }
            return node;
        }

        #endregion

        public override bool Equals(object obj)
        {
            if(obj != null && obj is BinaryTree<T>)
            {
                return root.Equals((obj as BinaryTree<T>).root);
            }

            return false;
        }

        public override string ToString()
        {
            if(root == null)
            {
                return string.Empty;
            }

            return root.ToString();
        }

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
