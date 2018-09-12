namespace Contact.Domain
{
    using MongoDB.Bson.Serialization.Attributes;

    public interface IViewModel
    {
        [BsonId]
        string Id { get; set; }
    }
}
