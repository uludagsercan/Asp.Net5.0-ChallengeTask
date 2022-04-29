using Core.CrossCuttingConcerns.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Detail
    {
        
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
    }
}
