
namespace ColorMaterial
{
    /// <summary>
    /// Describes the Film material 
    /// and the possibility of it's colorizing
    /// </summary>
    public sealed class Film : ColoratedMaterial
    {
        public Film()
        {
            Color = Color.Transparent;
        }

        public override void Colorize(Color color)
        {
            throw new ColorationException("Can't colorize a film");
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Determines whether two instances of an object are equal
        /// </summary>
        /// <param name="obj">2nd instance for comparsion</param>
        /// <returns>is equals</returns>
        public override bool Equals(object obj)
        {
            if (obj is null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>String representation of a class instance</returns>
        public override string ToString()
        {
            return $"{Color} Film";
        }
    }
}
