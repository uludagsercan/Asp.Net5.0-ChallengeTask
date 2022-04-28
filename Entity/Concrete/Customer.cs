
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;
using Core.Entities.Abstract;
using System.Text.Json.Serialization;

namespace Entity.Concrete
{
    public class Customer:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public short CustomerAge { get; set; }
        [BsonIgnore]
        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}
