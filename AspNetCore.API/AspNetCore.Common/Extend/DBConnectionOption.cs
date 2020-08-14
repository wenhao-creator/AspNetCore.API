using System.Collections.Generic;
using AspNetCore.Common.Enums;

namespace AspNetCore.Common.Extend
{
    /// <summary>
    /// DB连接选项
    /// </summary>
    public class DBConnectionOption
    {
        /// <summary>
        /// 编写连接
        /// </summary>
        public string WriteConnection { get; set; }
        /// <summary>
        /// 读连接列表
        /// </summary>
        public List<string> ReadConnectionList { get; set; }
        /// <summary>
        /// 轮询策略
        /// </summary>
        public Strategy Strategy { get; set; }
    }
}