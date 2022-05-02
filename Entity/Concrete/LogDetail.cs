using Core.Entities.Abstract;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LogDetail:IEntity
    {
        
#nullable enable
        [BsonIgnoreIfNull]
        public string? ExceptionMessage { get; set; }
        #nullable disable
        public bool Success { get; set; }      
        public string Message { get; set; }
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
    }
}
