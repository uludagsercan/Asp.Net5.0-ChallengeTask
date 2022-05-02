using Core.DataAccess.MongoDb;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IUserDal:IRepositoryBase<User>
    {
        ICollection<OperationClaim> GetClaims(User user);
    }
}
