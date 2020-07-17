
using System;

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
    }
}
