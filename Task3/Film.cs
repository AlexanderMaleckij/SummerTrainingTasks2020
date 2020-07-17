
namespace ColorMaterial
{
    /// <summary>
    /// Describes the Film material 
    /// and the possibility of it's colorizing
    /// </summary>
    public class Film : ColoratedMaterial
    {
        public override void Colorize(Color color)
        {
            throw new ColorationException("Can't colorize a film");
        }
    }
}
