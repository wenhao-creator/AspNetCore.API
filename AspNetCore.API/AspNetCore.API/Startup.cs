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

        // �˷���������ʱ���á�ʹ�ô˷�����������ӵ������С�
        public void ConfigureServices(IServiceCollection services)
        {
            //��� AppSetting ������
            services.AddSingleton(new AppSettings(Configuration));
            services.Configure<DBConnectionOption>(Configuration.GetSection("AppSettings"));

            //ע�� Swagger
            services.AddSwagger();

            //ע�� Jwt
            services.AddJwt();

            services.AddControllers();
        }

        /// <summary>
        ///  ע��Autofac����
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModuleRegister());
        }

        // �˷���������ʱ���á�ʹ�ô˷�������HTTP����ܵ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //��� Swagger �� HTTP �ܵ�
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "AspNetCore.API v1.0");

                //����·��������Ϊ�գ���ʾֱ���ڸ�������local host��8001�����ʸ��ļ���
                //ע��local host:8001/swagger ���ʲ�����ȥlaunchSetting.json ��launchUrl
                //ȥ�������軻·����ֱ�Ӹ����ּ���
                //���磺c.RoutePrefix = "doc";

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