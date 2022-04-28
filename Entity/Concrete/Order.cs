using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Bson;
using Core.Entities.Abstract;

namespace Entity.Concrete
{
    public class Order:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string OrderId { get; set; }
        public string OrderName { get; set; }
        public double OrderPrice { get; set; }
        public string? CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Customer? Customer { get; set; }
    }
}
