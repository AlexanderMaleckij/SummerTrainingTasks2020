using System;

namespace Serialization.Serializators
{
    /// <summary>
    /// wraps serializable data and class state
    /// </summary>
    /// <typeparam name="T">Type of serialized data</typeparam>
    [Serializable]
    public class VersionWrapper<T>
    {
        /// <summary>
        /// serializable data
        /// </summary>
        public T Content { get; set; }

        /// <summary>
        /// hash of the serializable data
        /// </summary>
        public int ActualHashOfT { get; set; }

        /// <summary>
        /// current hash of the serializable class
        /// </summary>
        private int ExpectedHashOfT
        {
            get => typeof(T).GetHashCode(); 
        }

        /// <summary>
        /// shows whether the deserialized data 
        /// matches the current state of the class
        /// </summary>
        public bool IsClassChanged
        {
            get => !(ActualHashOfT == ExpectedHashOfT);
        }

        public VersionWrapper(T content)
        {
            Content = content;
            ActualHashOfT = ExpectedHashOfT;
        }

        public VersionWrapper() { } //parameterless constructor for serialization
    }
}
