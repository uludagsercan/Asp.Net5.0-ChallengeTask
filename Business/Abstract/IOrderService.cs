﻿using Core.Utilities.Results.Abstract;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        
        IResult Add(Order order);
        IResult Update(Order order);
        IResult Delete(string id);
        IDataResult<ICollection<Order>> GetAll();
        IDataResult<Order> Get(string id);
    }
}
