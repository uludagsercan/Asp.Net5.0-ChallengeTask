using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using DataAccess.Abstract;
using DataAccess.Concrete.MongoDb;


namespace Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CustomerManager>().As<ICustomerService>();
            builder.RegisterType<MdCustomerDal>().As<ICustomerDal>();

            builder.RegisterType<OrderManager>().As<IOrderService>();
            builder.RegisterType<MdOrderDal>().As<IOrderDal>();

            builder.RegisterType<LogManager>().As<ILogService>();
            builder.RegisterType<MdLogItemDal>().As<ILogItemDal>();
     
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
