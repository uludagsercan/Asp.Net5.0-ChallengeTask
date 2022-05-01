
using DataAccess.Abstract;
using Entity.Concrete;

namespace DataAccess.Concrete.MongoDb
{
    public class MdLogItemDal : MongoDbRepository<LogItem>, ILogItemDal
    {
      
    }
}
