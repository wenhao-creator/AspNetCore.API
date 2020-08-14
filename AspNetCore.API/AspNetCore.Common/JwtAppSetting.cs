using AspNetCore.Common.Extend;

namespace AspNetCore.Common
{
    /// <summary>
    /// Jwt JwtAppSetting 辅助类
    /// </summary>
    public static class JwtAppSetting
    {
        /// <summary>
        /// 发行人
        /// </summary>
        /// <returns></returns>
        public static string Issuer() => AppSettings.App("AppSettings", "JwtSetting", "Issuer");

        /// <summary>
        /// 观众
        /// </summary>
        /// <returns></returns>
        public static string Audience() => AppSettings.App("AppSettings", "JwtSetting", "Audience");

        /// <summary>
        /// 密钥
        /// </summary>
        /// <returns></returns>
        public static string SecretKey() => AppSettings.App("AppSettings", "JwtSetting", "SecretKey");
    }
}