using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Dtos
{
    public class CustomerNameWithOrderDto:IDto
    {
        public string CustomerId { get; set; }
        public string OrderId { get; set; }
        public string CustomerName { get; set; }
        public short CustomerAge { get; set; }

    }
}
