using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using Entity.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        Task<IResult> Add(Customer customer);

        IResult Update(Customer customer);
        IResult Delete(string id);
        IDataResult<Customer> Get(string id,string name);
        IDataResult<ICollection<Customer>> GetAll();
        IDataResult<ICollection<CustomerNameWithOrderDto>> GetAllContainsName(string name);
        IDataResult<ICollection<Customer>> GetCustomerIfOrderIsNotExist();
    }
}
