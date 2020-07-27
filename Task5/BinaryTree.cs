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

        public void Add()
        {
            throw new NotImplementedException();
        }

        public void Remove(T item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        {
            throw new NotImplementedException();
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
