using Business.Abstract;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using System.Collections.Generic;


namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult("Ekleme işlemi başarılıdır.");
        }

        public IResult Delete(string id)
        {
            _customerDal.DeleteById(id);
            return new SuccessResult("Silme işlemi başarılıdır");
        }

        public IDataResult<Customer> Get(string id)
        {
            var result = _customerDal.Get(x => x.CustomerId == id);
            return new SuccessDataResult<Customer>(result, "Müişteri ait bilgi listelenmiştir");
        }

        public IDataResult<ICollection<Customer>> GetAll()
        {
            var result = _customerDal.GetAll();
            return new SuccessDataResult<ICollection<Customer>>(result, "Müşteriler listelenmiştir");
        }

        public IDataResult<ICollection<CustomerNameWithOrderDto>> GetAllContainsName(string name)
        {
            var result = _customerDal.GetCustomerContainsNameWithOrder(name);
            return new SuccessDataResult<ICollection<CustomerNameWithOrderDto>>(result,"Sipariş bilgisi olan müşteriler listelenmiştir.");
        }

        public IDataResult<ICollection<Customer>> GetCustomerIfOrderIsNotExist()
        {
            var result = _customerDal.GetCustomerIfOrderIsNotExist();
            return new SuccessDataResult<ICollection<Customer>>(result, "Sipariş bilgisi olmayan müşteriler listelendi.");
        }

        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer, customer.CustomerId);
            return new SuccessResult("Güncelleme işlemi başarılıdır");
        }
    }
}
