using Core.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LogItem:IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LogId { get; set; }
        public LogDetail Detail { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Level { get; set; }
    }
}
