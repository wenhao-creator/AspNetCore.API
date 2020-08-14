using System;
using System.IO;
using AspNetCore.Common.Extend;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCore.API.SetUpService
{
    /// <summary>
    /// Swagger API文档管理
    ///
    ///  1.可以在代码中添加注释，且自动生成API文档，无需再次编写，友好的界面让API文档更易懂。
    ///  2.一站式服务，只需要访问swagger的地址，就可以看到所有的后台接口和功能，并且能测试接口状态，真正是彻底的前后端分离了。
    ///  3.内嵌调试，可以查看接口状态和返回值结果很方便。
    /// </summary>
    public static class SwaggerService
    {
        /// <summary>
        /// 注册 Swagger 服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSwaggerGen(s =>
            {
                //定义要由Swagger生成器创建的一个或多个文档
                s.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Title = "AspNetCore.API v1.0 接口文档——Netcore 3.1",
                    Version = "v1.0",
                    Description = "AspNetCore.API HTTP API v1.0"
                });

                //在将动作转换为Swagger格式之前，提供一个自定义策略来对它们进行排序
                s.OrderActionsBy(o => o.RelativePath);

                //
                IncludeXmlComments(s);


                //在header中添加token ，传递到后台
                s.OperationFilter<SecurityRequirementsOperationFilter>();

                #region Token绑定到configureServices

                s.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });

                #endregion Token绑定到configureServices
            });
        }

        private static void IncludeXmlComments(SwaggerGenOptions s)
        {
            string[] xmlPaths = AppSettings.App("AppSettings", "xmlPaths").Split(',');

            foreach(string path in xmlPaths)
            {
                s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{path}.xml"), true);
            }

            ////获取Api.xml注释文件的目录
            //string apiXmlPath = Path.Combine(AppContext.BaseDirectory, "AspNetCore.API.xml");
            ////基于XML注释文件为操作、参数和模式注入人类友好的描述
            //s.IncludeXmlComments(apiXmlPath, true);//默认的第二个参数是false，这个是controller的注释，记得修改

        }
    }
}