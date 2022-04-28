﻿using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Business
{
    public class BusinessRule
    {
        public static IResult Run(params IResult[] results)
        {
            foreach (var item in results)
            {
                if (!item.Success)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
