using System;

namespace AspNetCore.Model
{
    /// <summary>
    /// 天气预报
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 华氏度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.556);

        /// <summary>
        /// 说明
        /// </summary>
        public string Summary { get; set; }
    }
}
