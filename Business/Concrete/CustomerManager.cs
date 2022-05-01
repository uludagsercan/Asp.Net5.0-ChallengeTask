using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
   
    [LogAspect(typeof(FileLogger))]
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult("Ekleme işlemi başarılıdır.");
        }
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Delete(string id)
        {
            var businessResult = BusinessRule.Run(CheckIfCustomerExist(id));
            if (businessResult != null)
            {
                return businessResult;
            }
            _customerDal.DeleteById(id);
            return new SuccessResult("Silme işlemi başarılıdır");
        }
        [CacheAspect]
        
        public IDataResult<Customer> Get(string id, string name)
        {
          
   
            var businessResult = BusinessRule.Run(CheckIfCustomerExist(id));
            if (businessResult != null)
            {
                return new ErrorDataResult<Customer>(businessResult.Message);
            }
            var result = _customerDal.Get(x => x.CustomerId == id);
            return new SuccessDataResult<Customer>(result, "Müişteri ait bilgi listelenmiştir");
        }
      
        public IDataResult<ICollection<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            if (result.Count()<=0)
            {
                return new ErrorDataResult<ICollection<Customer>>("Listelenecek müşteri bulunamadı");
            }
            return new SuccessDataResult<ICollection<Customer>>(result, "Müşteriler listelenmiştir");
        }

        [CacheAspect]
        public IDataResult<ICollection<CustomerNameWithOrderDto>> GetAllContainsName(string name)
        {
            var result = _customerDal.GetCustomerContainsNameWithOrder(name);
            if (result.Count()<=0)
            {
                return new ErrorDataResult<ICollection<CustomerNameWithOrderDto>>("Bu isimde müşteri bulunamadı");
            }
            return new SuccessDataResult<ICollection<CustomerNameWithOrderDto>>(result,"Sipariş bilgisi olan müşteriler listelenmiştir.");
        }

        [CacheAspect]
        public IDataResult<ICollection<Customer>> GetCustomerIfOrderIsNotExist()
        {
            var result = _customerDal.GetCustomerIfOrderIsNotExist();
            if (result.Count()<=0)
            {
                return new ErrorDataResult<ICollection<Customer>>("tüm müşterilerin sipariş bilgisi mevcuttur");
            }
            return new SuccessDataResult<ICollection<Customer>>(result, "Sipariş bilgisi olmayan müşteriler listelendi.");
        }
        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            var businessResult = BusinessRule.Run(CheckIfCustomerExist(customer.CustomerId));
            if (businessResult !=null)
            {
                return businessResult;
            }
            _customerDal.Update(customer, customer.CustomerId);
            return new SuccessResult("Güncelleme işlemi başarılıdır");
        }

        private IResult CheckIfCustomerExist(string customerId)
        {
            var result = _customerDal.Get(x => x.CustomerId.Equals(customerId));
            if (result ==null)
            {
                return new ErrorResult("Müşteri bulunamadı");
            }
            return new SuccessResult();
        }
      
       
    }
}
