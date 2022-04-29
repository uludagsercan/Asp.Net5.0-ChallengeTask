using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{

    public class Log: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string LogId { get; set; }
        public Detail Detail { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Level { get; set; }
       

    }
}
