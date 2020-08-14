using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace AspNetCore.Common.Extend
{
    /// <summary>
    /// AppSettings 配置文件 读取
    /// </summary>
    public class AppSettings
    {
        private static IConfiguration configuration;

        public AppSettings(string contentPath)
        {
            string path = "appsettings.json";

            //如果你把配置文件 是 根据环境变量来分开了，可以这样写
            //Path = $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json";

            configuration = new ConfigurationBuilder()
                .SetBasePath(contentPath)
                .Add(new JsonConfigurationSource { Path = path, Optional = false, ReloadOnChange = true })//这样的话，可以直接读目录里的json文件，而不是 bin 文件夹下的，所以不用修改复制属性
                .Build();
        }

        public AppSettings(IConfiguration Configuration)
        {
            configuration = Configuration;
        }

        /// <summary>
        /// 封装要操作的字符
        /// </summary>
        /// <param name="sections">节点配置</param>
        /// <returns></returns>
        public static string App(params string[] sections)
        {
            try
            {
                if(sections.Any())
                {
                    return configuration[string.Join(":", sections)];
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
        }

        /// <summary>
        /// 递归获取配置信息数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sections"></param>
        /// <returns></returns>
        public static List<T> App<T>(params string[] sections)
        {
            List<T> result = new List<T>();
            configuration.Bind(string.Join(":", sections), result);
            return result;
        }
    }
}