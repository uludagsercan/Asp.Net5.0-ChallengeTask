using Entity.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
   
           
        }

        private bool IsValidHex(string id)
        {
            int res = 0;
            if (Int32.TryParse(id,System.Globalization.NumberStyles.HexNumber,System.Globalization.CultureInfo.InvariantCulture,out res))
            {
                return true;
            }
            return false;
        }
    }
}
