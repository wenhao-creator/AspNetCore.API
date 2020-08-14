using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using AspNetCore.Common.Extend;
using Autofac;
using Autofac.Extras.DynamicProxy;

namespace AspNetCore.API.SetUpService
{
    /// <summary>
    /// Autofac 模块注册
    /// </summary>
    public class AutofacModuleRegister : Autofac.Module
    {
        /// <summary>
        ///  重写Load函数
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlConnection>().As<IDbConnection>();

            string[] assblyStrings = AppSettings.App("AppSettings", "Autofac").Split(',');

            Assembly[] assemblies = new Assembly[assblyStrings.Length];

            for(int i = 0; i < assemblies.Length; i++)
            {
                assemblies[i] = Assembly.LoadFrom(Path.Combine(AppContext.BaseDirectory, $"{assblyStrings[i]}.dll"));
            }

            builder.RegisterAssemblyTypes(assemblies)
                .InstancePerDependency()//默认模式，每次调用，都会重新实例化对象；每次请求都创建一个新的对象
                .AsImplementedInterfaces()//是以接口方式进行注入,注入这些类的所有的公共接口作为服务（除了释放资源）
                .EnableInterfaceInterceptors(); //引用Autofac.Extras.DynamicProxy;应用拦截器
        }
    }
}