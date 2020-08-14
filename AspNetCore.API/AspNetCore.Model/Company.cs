using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetCore.Model
{
    /// <summary>
    /// 公司 实体类
    /// </summary>
    [Table("Company")]
    public class Company
    {
        /// <summary>
        /// id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创造者Id
        /// </summary>
        public int? CreatorId { get; set; }

        /// <summary>
        /// 最后的修饰符Id
        /// </summary>
        public int? LastModifierId { get; set; }

        /// <summary>
        /// 最后时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}