using Core.Entities.Abstract;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Concrete
{
    public class LogParameter:IEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
