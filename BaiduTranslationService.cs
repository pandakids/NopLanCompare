using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NopLanCompare
{
    public class BaiduTranslationService
    {
        public static string Translate(string keyword)
        {
            // 原文
            string q = keyword;
            // 源语言
            string from = "en";
            // 目标语言
            string to = "zh";
            // 改成您的APP ID
            string appId = "20191030000348860";
            Random rd = new Random();
            string salt = rd.Next(100000).ToString();
            // 改成您的密钥
            string secretKey = "OQtIAOJ6wJ6i7lXw60jx";
            string sign = EncryptString(appId + q + salt + secretKey);
            string url = "http://api.fanyi.baidu.com/api/trans/vip/translate?";
            url += "q=" + HttpUtility.UrlEncode(q);
            url += "&from=" + from;
            url += "&to=" + to;
            url += "&appid=" + appId;
            url += "&salt=" + salt;
            url += "&sign=" + sign;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = 6000;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        // 计算MD5值
        public static string EncryptString(string str)
        {
            MD5 md5 = MD5.Create();
            // 将字符串转换成字节数组
            byte[] byteOld = Encoding.UTF8.GetBytes(str);
            // 调用加密方法
            byte[] byteNew = md5.ComputeHash(byteOld);
            // 将加密结果转换为字符串
            StringBuilder sb = new StringBuilder();
            foreach (byte b in byteNew)
            {
                // 将字节转换成16进制表示的字符串，
                sb.Append(b.ToString("x2"));
            }
            // 返回加密的字符串
            return sb.ToString();
        }
    }

    public class TransResult
    {
        public string from { get; set; }

        public string to { get; set; }

        public List<trans_result> trans_result { get; set; }
    }

    public class trans_result
    {
        public string src { get; set; }

        public string dst { get; set; }
    }
}
