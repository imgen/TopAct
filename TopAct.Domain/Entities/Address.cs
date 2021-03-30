namespace TopAct.Domain.Entities
{
    public class Address : Entity
    {
        public string AddressName { get; private set; }

        public Address(string addressName)
        {
            AddressName = addressName;
        }
    }
}
