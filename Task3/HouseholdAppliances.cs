
namespace Task3
{
    /// <summary>
    /// Represents Household appliances products category
    /// </summary>
    public abstract class HouseholdAppliances : ProductType
    {
        public HouseholdAppliances(string name, double price) : base(name, "Household appliances", price) { }
    }
}
