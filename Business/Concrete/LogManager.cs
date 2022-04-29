using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LogManager : ILogService
    {
        private readonly ILogDal _logDal;

        public LogManager(ILogDal logDal)
        {
            _logDal = logDal;
        }

        public IDataResult<ICollection<Log>> GetAllByDateAndLevel(string date, string level)
        {
            var result = _logDal.GetAll(x=> x.Level.Equals(level) && x.Date.Equals(level));
            return new SuccessDataResult<ICollection<Log>>(result, "Tarihe ve Log leveline göre loglar listelenmiştir.");
        }

        public Task LogSave()
        {        
            return Task.Run(() =>
            {
                using (var stream = new StreamReader(new FileStream("C:/Log/log.json", FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite)))
                {
                    var logs = new List<Log>();
                    
                    string line;
                    while ((line = stream.ReadLine()) != null)
                    {
                        logs.Add(JsonConvert.DeserializeObject<Log>(line));
                    }
                    if (logs.Count()>0)
                    {
                        foreach (var logItem in logs)
                        {
                            _logDal.Add(logItem);                          
                        }
                    }
                    stream.Close();
                    File.WriteAllText("C:/Log/log.json",string.Empty);                 
                }
            });
        }
    }
}
