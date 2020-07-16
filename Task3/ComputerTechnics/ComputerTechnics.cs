
namespace Task3
{
    /// <summary>
    /// Represents Computer technics products category
    /// </summary>
    public abstract class ComputerTechnics : ProductType
    {
        public ComputerTechnics(string name, double price) : base(name, "Computure technics", price) { }
    }
}
