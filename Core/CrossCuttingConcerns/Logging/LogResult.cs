using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrossCuttingConcerns.Logging
{
    public class LogResult:LogDetail
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
