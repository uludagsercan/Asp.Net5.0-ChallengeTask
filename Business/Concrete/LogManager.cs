using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Abstract;
using Entity.Concrete;
using MongoDB.Bson.Serialization;
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
        private readonly ILogItemDal _logDal;

        private List<LogParameter> LogParameters { get; set; }

        public LogManager(ILogItemDal logDal)
        {
            _logDal = logDal;
            LogParameters = new List<LogParameter>();
        }

        public IDataResult<ICollection<LogItem>> GetAllByDateAndLevel(string date, string level)
        {
            var result = _logDal.GetAll(x => x.Level.Equals(level) && x.Date.Equals(date));
            return new SuccessDataResult<ICollection<LogItem>>(result, "Tarihe ve Log leveline göre loglar listelenmiştir.");
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
                    if (logs.Count() > 0)
                    {
                        foreach (var logItem in logs)
                        {
                            LogParameters = new List<LogParameter>();
                            foreach (var item in logItem.Detail.LogParameters)
                            {

                                LogParameters.Add(new LogParameter
                                {
                                    Name = item.Name,
                                    Type = item.Type,
                                    Value = item.Value.ToString()
                                });

                            }
                            var detail = new LogDetail();
                            detail.LogParameters = LogParameters;
                            detail.ExceptionMessage = logItem.Detail.ExceptionMessage;
                            detail.Success = logItem.Detail.Success;
                            detail.MethodName = logItem.Detail.MethodName;
                            detail.Message = logItem.Detail.Message;
                            _logDal.Add(new LogItem
                            {
                                Date = logItem.Date,
                                Detail = detail,
                                Level = logItem.Level,
                                Time = logItem.Time
                            });
                        }
                    }
                    stream.Close();
                   
                 
                }
                using (var writer = new StreamWriter(new FileStream("C:/Log/log.json",FileMode.Truncate,FileAccess.Write,FileShare.ReadWrite)))
                {
                   
                    writer.Close();
                }
                
            });
        }
    }
}
