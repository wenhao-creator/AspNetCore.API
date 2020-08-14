using System.Data;
using AspNetCore.Common.Enums;

namespace AspNetCore.IRepository.Base
{
    /// <summary>
    /// 自定义连接工厂
    /// </summary>
    public interface ICustomConnectionFactory
    {
        /// <summary>
        /// 获得连接
        /// </summary>
        /// <param name="writeRead">读 写</param>
        /// <returns></returns>
        public IDbConnection GetDbConnection(WriteRead writeRead);
    }
}