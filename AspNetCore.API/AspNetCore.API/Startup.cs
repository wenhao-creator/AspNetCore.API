using AspNetCore.API.SetUpService;
using AspNetCore.Common.Extend;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // 此方法由运行时调用。使用此方法将服务添加到容器中。
        public void ConfigureServices(IServiceCollection services)
        {
            //添加 AppSetting 到容器
            services.AddSingleton(new AppSettings(Configuration));
            services.Configure<DBConnectionOption>(Configuration.GetSection("AppSettings"));

            //注册 Swagger
            services.AddSwagger();

            //注册 Jwt
            services.AddJwt();

            services.AddControllers();
        }

        /// <summary>
        ///  注册Autofac容器
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }

        // 此方法由运行时调用。使用此方法配置HTTP请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //添加 Swagger 到 HTTP 管道
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "AspNetCore.API v1.0");

                //配置路径，设置为空，表示直接在根域名（local host：8001）访问该文件，
                //注意local host:8001/swagger 访问不到，去launchSetting.json 把launchUrl
                //去掉，如需换路径，直接改名字即可
                //例如：c.RoutePrefix = "doc";

                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}