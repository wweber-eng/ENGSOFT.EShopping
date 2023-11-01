using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Catalog.Core.Entities
{
    public class ProductType : BaseEntity
    {
        [BsonElement("Type")]
        public string? Type { get; set; }

    }
}
