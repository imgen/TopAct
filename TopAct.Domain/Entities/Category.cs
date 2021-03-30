namespace TopAct.Domain.Entities
{
    public class Category : Entity
    {
        public string CategoryName { get; private set; }

        public Category(string categoryName)
        {
            CategoryName = categoryName;
        }
    }
}
