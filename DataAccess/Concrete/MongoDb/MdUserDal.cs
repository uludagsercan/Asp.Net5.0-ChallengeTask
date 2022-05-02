using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.MongoDb
{
    public class MdUserDal : MongoDbRepository<User>, IUserDal
    {
        public ICollection<OperationClaim> GetClaims(User user)
        {
            using (var context = new MongoDbContext())
            {
                var result = from operationClaim in context.GetCollection<OperationClaim>().AsQueryable().ToList()
                             join userOperationClaim in context.GetCollection<UserOperationClaim>().AsQueryable().ToList()
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId.Equals(user.Id)
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }
    }
}
