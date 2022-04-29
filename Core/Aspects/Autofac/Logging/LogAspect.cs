using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Logging
{
    public class LogAspect : MethodInterception
    {
        private LoggerServiceBase _loggerServiceBase;
        private string _value="";
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new System.Exception(AspectMessages.WrongLoggerType);
            }

            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

        protected override void OnBefore(IInvocation invocation)
        {
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                foreach (var item in invocation.Arguments[i].GetType().GetProperties())
                {
                    _value += item.GetValue(invocation.Arguments[i]) != null ?
                        item.Name + " : " + item.GetValue(invocation.Arguments[i])
                        : item.Name + " : null";
                }
            }
            _loggerServiceBase.Info(GetLogDetail(invocation));
           
            
        }
        protected override void OnSuccess(IInvocation invocation)
        {
            var successResult = (bool)invocation.ReturnValue.GetType().GetProperty("Success").GetValue(invocation.ReturnValue, null);
            if (!successResult)
            {
                _loggerServiceBase.Warn(GetLogDetail(invocation));
            }
            else
            {
                _loggerServiceBase.Debug(GetLogDetail(invocation));
            }
        }
      
        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = _value,
                    Type = invocation.Arguments[i].GetType().Name
                }) ;
            }
           
            var logResult = new LogResult
            {
                MethodName = invocation.Method.Name,
                Message = invocation.ReturnValue!=null ? 
                (string)invocation.ReturnValue.GetType().GetProperty("Message").GetValue(invocation.ReturnValue, null):
                "null",
                Success = invocation.ReturnValue!=null?
                (bool)invocation.ReturnValue.GetType().GetProperty("Success").GetValue(invocation.ReturnValue, null)
                :false,
                LogParameters = logParameters
            };

            return logResult;
        }
    }
}
