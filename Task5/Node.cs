using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tree
{
    /// <summary>
    /// Represents tree node
    /// </summary>
    /// <typeparam name="T">type of the variable stored in the node</typeparam>
    [Serializable]
    public class Node<T>
    {
        public T Item { get; set; }

        public int Height { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public Node(T item)
        {
            Item = item;
        }

        public Node() { }   //parameterless constructor for xml serialization

        /// <summary>
        /// Recursive method for getting string representation of current node 
        /// </summary>
        /// <param name="sb">StringBuilder class instance for collecting result</param>
        /// <param name="node">the current recursively viewed node</param>
        /// <param name="level">current recursively viewed node level</param>
        private static void GetTreeString(StringBuilder sb, Node<T> node, int level = 0)
        {
            if (node != null)
            {
                GetTreeString(sb, node.Right, level + 1);

                foreach(int i in Enumerable.Range(0, level))
                {
                    sb.Append("     ");
                }
                sb.Append(node.Item.ToString() + '\n');
                GetTreeString(sb, node.Left, level + 1);
            }
        }

        /// <summary>
        /// Method for getting string representation of current node (tree)
        /// </summary>
        /// <returns>string of the tree node rotates to the left 90 degrees</returns>
        public override string ToString()
        {
            if(Item == null)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            GetTreeString(sb, this);
            return sb.ToString();
        }

        /// <summary>
        /// Determines whether two instances of an Node class are equal
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>is equals</returns>
        public override bool Equals(object obj)
        {
            if(obj != null && obj is Node<T>)
            {
                bool ItemsComparer<V>(V a, V b)
                {
                    if(a == null && b == null)
                    {
                        return true;
                    }

                    if(a == null || b == null)
                    {
                        return false;
                    }

                    return a.Equals(b);
                }

                Node<T> anotherNode = obj as Node<T>;

                return ItemsComparer(Item, anotherNode.Item) &
                       ItemsComparer(Height, anotherNode.Height) &
                       ItemsComparer(Left, anotherNode.Left) &
                       ItemsComparer(Right, anotherNode.Right);
            }
            return false;
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            int hashCode = Height.GetHashCode();

            if(Item != null)
            {
                hashCode ^= Item.GetHashCode();
            }

            if(Left != null)
            {
                hashCode ^= Left.GetHashCode();
            }

            if(Right != null)
            {
                hashCode ^= Right.GetHashCode();
            }

            return hashCode;
        }

        /// <summary>
        /// Supports a simple iteration over a tree nodes.
        /// </summary>
        /// <returns>IEnumerator</returns>
        public IEnumerator<T> GetEnumerator()
        {
            if (Left != null)
            {
                foreach (var v in Left)
                {
                    yield return v;
                }
            }

            yield return Item;

            if (Right != null)
            {
                foreach (var v in Right)
                {
                    yield return v;
                }
            }
        }
    }
}
