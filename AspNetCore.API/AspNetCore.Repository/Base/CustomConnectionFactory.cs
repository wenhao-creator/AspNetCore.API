using System;
using System.Data;
using AspNetCore.Common.Enums;
using AspNetCore.Common.Extend;
using AspNetCore.IRepository.Base;
using Microsoft.Extensions.Options;

namespace AspNetCore.Repository.Base
{
    public class CustomConnectionFactory : ICustomConnectionFactory
    {
        private readonly IDbConnection connection;
        private readonly DBConnectionOption option;

        private int _IsSeed = 0;
        private readonly bool _IsSet = true;
        private static readonly object ObjectIsSet_Lock = new object();

        public CustomConnectionFactory(IDbConnection dbConnection, IOptionsMonitor<DBConnectionOption> options)
        {
            connection = dbConnection;
            option = options.CurrentValue;

            if(_IsSet)
            {
                lock(ObjectIsSet_Lock)
                {
                    if(_IsSet)
                    {
                        //应该保证  只有在CustomConnectionFactory 第一次初始化的时候，对其赋值；
                        _IsSeed = option.ReadConnectionList.Count;
                        _IsSet = false;
                    }
                }
            }
        }

        /// <summary>
        ///  数据库 连接
        /// </summary>
        /// <param name="writeRead"></param>
        /// <returns></returns>
        public IDbConnection GetDbConnection(WriteRead writeRead)
        {
            connection.ConnectionString = writeRead switch
            {
                WriteRead.Write => option.WriteConnection,//增删改 -- 主数据库
                WriteRead.Read => QueryStrategy(),// 如果是查询的时候，我们自由选择查询数据库
                _ => throw new ArgumentNullException(nameof(writeRead), "选择数据库 出错···")
            };
            return connection;
        }

        /// <summary>
        /// DB 选择策略
        /// </summary>
        /// <returns></returns>
        private string QueryStrategy()
        {
            return option.Strategy switch
            {
                Strategy.Polling => Polling(),
                Strategy.Random => Random(),
                _ => throw new ArgumentNullException(nameof(option.Strategy), "分库查询策略不存在·····")
            };
        }

        /// <summary>
        /// 轮询策略
        /// </summary>
        /// <returns></returns>
        private string Polling()
        {
            //达到一定量值后重置
            if(_IsSeed >= 7000000)
            {
                _IsSeed = option.ReadConnectionList.Count;
            }

            return option.ReadConnectionList[_IsSeed++ % option.ReadConnectionList.Count];
        }

        /// <summary>
        /// 随机策略
        /// </summary>
        /// <returns></returns>
        private string Random()
        {
            int index = new Random().Next(0, option.ReadConnectionList.Count);
            return option.ReadConnectionList[index];
        }
    }
}