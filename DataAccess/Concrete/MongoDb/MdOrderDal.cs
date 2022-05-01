using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
namespace DataAccess.Concrete.MongoDb
{
    public class MdOrderDal : MongoDbRepository<Order>, IOrderDal
    {
       

    }
}
