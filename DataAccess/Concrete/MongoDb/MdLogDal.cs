using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.MongoDb
{
    public class MdLogDal : MongoDbRepository<Log>, ILogDal
    {
      
    }
}
