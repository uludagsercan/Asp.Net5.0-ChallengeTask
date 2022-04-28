using Core.DataAccess.MongoDb;
using Entity.Concrete;
using Entity.Dtos;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface ICustomerDal:IRepositoryBase<Customer>
    {
        public ICollection<CustomerNameWithOrderDto> GetCustomerContainsNameWithOrder(string name);

        public ICollection<Customer> GetCustomerIfOrderIsNotExist();
    }
}
