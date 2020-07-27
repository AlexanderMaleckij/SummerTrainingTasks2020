using System;

namespace Tree
{
    public class BinaryTreeException : Exception
    {
        public BinaryTreeException()
        {
        }

        public BinaryTreeException(string message)
            : base(message)
        {
        }

        public BinaryTreeException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
