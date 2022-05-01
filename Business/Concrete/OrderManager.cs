using Business.Abstract;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using System.Collections.Generic;
using System.Linq;


namespace Business.Concrete
{
    [LogAspect(typeof(FileLogger))]
    public class OrderManager : IOrderService
    {
        private readonly IOrderDal _orderDal;
        private readonly ICustomerService _customerService;

        public OrderManager(IOrderDal orderDal, ICustomerService customerService)
        {
            _orderDal = orderDal;
            _customerService = customerService;
        }
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Add(Order order)
        {

            var customerResult = _customerService.Get(order.CustomerId,"sss");
            if (!customerResult.Success)
            {
                return customerResult;
            }
            order.Customer = customerResult.Data;
            _orderDal.Add(order);
            return new SuccessResult("Ekleme işlemi başarılıdır.");
        }
        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Delete(string id)
        {
            var businessResult = BusinessRule.Run(CheckIfOrderExist(id));
            if (businessResult!=null)
            {
                return businessResult;
            }
            _orderDal.DeleteById(id);
            return new SuccessResult("Silme işlemi başarılır");
        }
        [CacheAspect]
        public IDataResult<Order> Get(string id)
        {
            var businessResult = BusinessRule.Run(CheckIfOrderExist(id));
            if (businessResult!=null)
            {
                return new ErrorDataResult<Order>(businessResult.Message);
            }
            var orderResult = _orderDal.Get(x=> x.OrderId==id);
            return new SuccessDataResult<Order>(orderResult, "Sipariş bilgisi listelendi");
        }
        [CacheAspect]
        public IDataResult<ICollection<Order>> GetAll()
        {
            var result = _orderDal.GetAll();
            if (result.Count()<=0)
            {
                return new ErrorDataResult<ICollection<Order>>("Sipariş bilgisi bulunamadı");
            }
            return new SuccessDataResult<ICollection<Order>>(result, "Listeleme işlemi başarılıdır");
        }

        [CacheRemoveAspect("IOrderService.Get")]
        public IResult Update(Order order)
        {
            var businessResult = BusinessRule.Run(CheckIfOrderExist(order.OrderId),CheckIfCustomerExistAndChange(order.OrderId, order.CustomerId));
            if (businessResult!=null)
            {
                return businessResult;
            }
            _orderDal.Update(order,order.OrderId);
            return new SuccessResult("Güncelleme işlemi başarılıdır");
        }

        private IResult CheckIfOrderExist(string id)
        {
            var result = _orderDal.Get(x => x.CustomerId.Equals(id));
            if (result == null)
            {
                return new ErrorResult("Sipariş bilgisi bulunamadı");
            }
            return new SuccessResult();
        }
        private IResult CheckIfCustomerExistAndChange(string orderId,string customerId)
        {
            
            var orderResult = _orderDal.Get(x=> x.OrderId.Equals(orderId));
            if (!orderResult.CustomerId.Equals(customerId))
            {
                return new ErrorResult("Müşteri bilgisi değiştirilemez");
            }
            return new SuccessResult();
        }
    }
}
