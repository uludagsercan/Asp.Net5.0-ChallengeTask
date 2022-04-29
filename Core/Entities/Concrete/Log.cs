using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Concrete
{
    public class Log
    {
        
        public Detail Detail { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Level { get; set; }
       

    }
}
