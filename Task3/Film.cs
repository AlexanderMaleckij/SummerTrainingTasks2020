
using System;

namespace ColorMaterial
{
    /// <summary>
    /// Describes the Film material 
    /// and the possibility of it's colorizing
    /// </summary>
    public sealed class Film : ColoratedMaterial
    {
        public override void Colorize(Color color)
        {
            throw new ColorationException("Can't colorize a film");
        }

        public override object Clone()
        {
            return MemberwiseClone();
        }
    }
}
