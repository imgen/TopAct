namespace TopAct.Domain.Entities
{
    public class CustomField : Entity
    {
        public CustomField(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; private set; }
        public string Value { get; private set; }
    }
}
