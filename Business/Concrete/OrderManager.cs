using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly ICustomerService _customerService;

        public OrderManager(IOrderDal orderDal, ICustomerService customerService)
        {
            _orderDal = orderDal;
            _customerService = customerService;
        }

        public IResult Add(Order order)
        {
            if (order.CustomerId != null)
            {
                var result = _customerService.Get(order.CustomerId);
                order.Customer = result.Data;
            }
            
            _orderDal.Add(order);
            return new SuccessResult("Ekleme işlemi başarılıdır.");
        }

        public IResult Delete(string id)
        {
           _orderDal.DeleteById(id);
            return new SuccessResult("Silme işlemi başarılır");
        }

        public IDataResult<Order> Get(string id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<ICollection<Order>> GetAll()
        {
            var result = _orderDal.GetAll();
            return new SuccessDataResult<ICollection<Order>>(result,"Listeleme işlemi başarılıdır");
        }

        public IResult Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
