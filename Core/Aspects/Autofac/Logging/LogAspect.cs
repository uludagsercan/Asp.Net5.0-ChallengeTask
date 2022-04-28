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
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }
            if (invocation.ReturnValue==null)
            {
                var logDetail = new LogDetail
                {
                    MethodName = invocation.Method.Name,
                    LogParameters = logParameters

                };
                return logDetail;
            }
            var logResult = new LogResult
            {
                MethodName = invocation.Method.Name,
                Message = (string)invocation.ReturnValue.GetType().GetProperty("Message").GetValue(invocation.ReturnValue, null),
                Success = (bool)invocation.ReturnValue.GetType().GetProperty("Success").GetValue(invocation.ReturnValue, null),
                LogParameters = logParameters
            };

            return logResult;
        }
    }
}
