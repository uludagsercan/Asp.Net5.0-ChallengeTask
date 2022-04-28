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
        public ICollection<Order> GetAllWithOrder()
        {
            using (var context = new MongoDbContext())
            {
                var result = (from c in context.GetCollection<Customer>().AsQueryable().ToList()
                              join o in context.GetCollection<Order>().AsQueryable().ToList()
                              on c equals o.Customer
                              select new Order
                              {
                                  OrderId = o.OrderId,
                                  Customer=o.Customer,
                                  OrderName=o.OrderName,
                                  CreatedDate=o.CreatedDate,
                                  OrderPrice=o.OrderPrice
                              }).ToList();
                return result;
            }
        }
    }
}
