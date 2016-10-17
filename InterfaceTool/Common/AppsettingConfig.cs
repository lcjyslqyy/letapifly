using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace InterfaceTool.Common
{
    public class AppsettingConfig
    {
        /// <summary>
        /// 接口文档上传的路径
        /// </summary>
        public static string xmlpath
        {
            get
            {
                return ConfigurationManager.AppSettings["xmlpath"].Trim();
            }
        }
        //保存接口信息文件
        public static string interlistpath
        {
            get
            {
                return ConfigurationManager.AppSettings["interlistpath"].Trim();
            }
        }
        /// <summary>
        /// 上传的文件的路径
        /// </summary>
        public static string uploadpath
        {
            get
            {
                return ConfigurationManager.AppSettings["uploadpath"].Trim();
            }
        }
        /// <summary>
        /// 登录信息的文件路径
        /// </summary>
        public static string logindatapath
        {
            get
            {
                return ConfigurationManager.AppSettings["logindatapath"].Trim();
            }
        }
        public static string encryptKey
        {
            get
            {
                return ConfigurationManager.AppSettings["encryptKey"].Trim();
            }
        }
        public static string cookiename
        {
            get
            {
                return ConfigurationManager.AppSettings["cookiename"].Trim();
            }
        }
        public static string menulist
        {
            get
            {
                return ConfigurationManager.AppSettings["menulist"].Trim();
            }
        }
    }
}