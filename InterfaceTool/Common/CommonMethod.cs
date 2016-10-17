using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace InterfaceTool.Common
{
    public class CommonMethod
    {
        /// <summary>
        /// 返回一个json
        /// </summary>
        /// <param name="obj"></param>
        public static void ResponseJsonResult(object obj)
        {
            System.Web.HttpContext.Current.Response.ContentType = "application/json";
            System.Web.HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj));
        }
        /// <summary>
        /// 返回一个json
        /// </summary>
        /// <param name="obj"></param>
        public static void ResponseJsonResult(object obj, IsoDateTimeConverter timeConverter)
        {
            System.Web.HttpContext.Current.Response.ContentType = "application/json";
            System.Web.HttpContext.Current.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(obj, timeConverter));
        }
        /// <summary>
        /// MD5加密 32位
        /// </summary>
        /// <param name="str">加密的字符串</param>
        /// <returns></returns>
        public static string md5_32(string str)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            byte[] inputByteArray1 = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(str));//md5加密,密钥
            return BitConverter.ToString(inputByteArray1).Replace("-", null).ToLower();
        }
    }
}