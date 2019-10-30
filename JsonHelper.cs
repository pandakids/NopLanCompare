using System;
using System.Collections.Generic;
using System.Text;

namespace NopLanCompare
{
    //————————————————
    //版权声明：本文为CSDN博主「QFN-齐」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
    //原文链接：https://blog.csdn.net/u014518337/article/details/86241731

    public class JsonHelper
    {
        /// <summary>
        /// 将实体类序列化为JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        static public string SerializeJSON<T>(T data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// 反序列化JSON
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        static public T DeserializeJSON<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 日期转换为时间戳（时间戳单位秒）
        /// </summary>
        /// <param name="TimeStamp"></param>
        /// <returns></returns>
        public static long ConvertToTimeStamp(DateTime time)
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)(time.AddHours(-8) - Jan1st1970).TotalMilliseconds;
        }
    }
}
