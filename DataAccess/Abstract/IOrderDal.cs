using Core.DataAccess.MongoDb;
using Entity.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IOrderDal:IRepositoryBase<Order>
    {
        public ICollection<Order> GetAllWithOrder();
    }
}
