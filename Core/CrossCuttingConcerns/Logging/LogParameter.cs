﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
 
    public class LogParameter
    {
        public string Name { get; set; }
        public Object Value { get; set; }
        public string Type { get; set; }
    }
}
