﻿using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ILogService
    {
        public Task LogSave();
        public IDataResult<ICollection<Log>> GetAllByDateAndLevel(string date, string level);
    }
}
