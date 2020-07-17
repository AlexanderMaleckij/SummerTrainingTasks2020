
namespace ColorMaterial
{
    /// <summary>
    /// Describes the paper material and 
    /// the possibility of it's colorizing
    /// </summary>
    public sealed class Paper : ColoratedMaterial
    {
        public bool IsCanPaint { get; set; }

        public Paper()
        {
            IsCanPaint = true;
        }

        public override void Colorize(Color color)
        {
            if(IsCanPaint)
            {
                Color = color;
                IsCanPaint = false;
            }
            else
            {
                throw new ColorationException("The paper has already been painted");
            }
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// String representation of a class instance
        /// </summary>
        /// <returns>String representation of a class instance</returns>
        public override string ToString()
        {
            return "Paper";
        }

        /// <summary>
        /// Serves as a default hash function
        /// </summary>
        /// <returns>instance hash code</returns>
        public override int GetHashCode()
        {
            return IsCanPaint.GetHashCode();
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
                Paper paper = (Paper)obj;

                if(paper.IsCanPaint == IsCanPaint)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
