﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results.Abstract
{
    public interface IDataResult<TEntity>:IResult
    {
        public TEntity Data { get; }
    }
}
