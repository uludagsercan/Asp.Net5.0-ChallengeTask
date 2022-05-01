using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entity.Concrete;
using Entity.Dtos;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.Concrete.MongoDb
{
    public class MdCustomerDal : MongoDbRepository<Customer>, ICustomerDal
    {
        public ICollection<CustomerNameWithOrderDto> GetCustomerContainsNameWithOrder(string name)
        {
            using (var context = new MongoDbContext())
            {
                var result = (from c in context.GetCollection<Customer>().AsQueryable().ToList()
                              join o in context.GetCollection<Order>().AsQueryable().ToList()
                              on c.CustomerId equals o.CustomerId
                              where c.CustomerName.Contains(name)
                              select new CustomerNameWithOrderDto
                              {
                                  CustomerAge=c.CustomerAge,
                                  CustomerId=c.CustomerId,
                                  CustomerName=c.CustomerName,
                                  OrderId=o.OrderId
                              }).ToList();
                return result;
            }
        }

        public ICollection<Customer> GetCustomerIfOrderIsNotExist()
        {
            using (var context= new MongoDbContext())
            {
                var result = (from c in context.GetCollection<Customer>().AsQueryable().ToList()
                              join o in context.GetCollection<Order>().AsQueryable().ToList()
                              on c.CustomerId equals o.CustomerId into gj
                              from co in gj.DefaultIfEmpty()
                              where co == null
                              select new Customer
                              {
                                  CustomerAge = c.CustomerAge,
                                  CustomerId = c.CustomerId,
                                  CustomerName = c.CustomerName

                              }).ToList();
                return result;
            }
        }
    }
}
