
using Core.CrossCuttingConcerns.Logging;
using Core.Entities.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Core.Entities.Concrete
{
    
    public class Detail
    {
        
        public string? ExceptionMessage { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string MethodName { get; set; }
        public List<LogParameter> LogParameters { get; set; }
        public Detail()
        {
            LogParameters = new List<LogParameter>();
        }
    }
}
