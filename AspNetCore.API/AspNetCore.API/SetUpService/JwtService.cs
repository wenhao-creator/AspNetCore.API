using System;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore.API.SetUpService
{
    /// <summary>
    /// Jwt 注册服务类
    /// </summary>
    public static class JwtService
    {
        /// <summary>
        /// Jwt 注册服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddJwt(this IServiceCollection services)
        {
            if(services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            byte[] KeyByteArray = Encoding.ASCII.GetBytes(JwtAppSetting.SecretKey());
            SymmetricSecurityKey signingKey = new SymmetricSecurityKey(KeyByteArray);

            //令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = JwtAppSetting.Issuer(),//发行人
                ValidateAudience = true,
                ValidAudience = JwtAppSetting.Audience(),//订阅人
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30), //token过期后 还可以继续多访问30秒
                RequireExpirationTime = true,
            };

            //2.1 [认证] core 自带官方认证
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                // 添加JwtBearer服务
                .AddJwtBearer(o =>
                {
                    o.TokenValidationParameters = tokenValidationParameters;

                    o.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            // 如果过期，则把<是否过期>添加到，返回头信息中
                            if(context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}