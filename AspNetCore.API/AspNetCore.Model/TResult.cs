using System.Collections.Generic;

namespace AspNetCore.Model
{
    /// <summary>
    /// 分页参数
    /// </summary>
    public class Page
    {
        public Page()
        {
            PageIndex = 1;
            PageSize = 20;
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageToal { get; set; }
    }

    public class TPageResult<T> where T : class
    {
        public TPageResult()
        {
            PageIndex = 1;
            PageSize = 20;
        }

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageTotal { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public bool Success { get; set; }

        public IEnumerable<T> DataList { get; set; }
    }
}