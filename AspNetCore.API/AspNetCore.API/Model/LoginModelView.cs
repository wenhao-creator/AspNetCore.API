namespace AspNetCore.API.Model
{
    /// <summary>
    /// Login视图模型
    /// </summary>
    public class LoginModelView
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }
    }
}