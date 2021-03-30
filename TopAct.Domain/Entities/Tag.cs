namespace TopAct.Domain.Entities
{
    public class Tag : Entity
    {
        public string TagName { get; private set; }

        public Tag(string tagName)
        {
            TagName = tagName;
        }
    }
}
