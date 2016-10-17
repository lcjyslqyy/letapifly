using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace InterfaceTool.Common
{
    public class StylesLoad
    {
        /// <summary>
        /// 加载css内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HtmlString Render(string[] url)
        {

            StringBuilder sb = new StringBuilder();
            string random = string.Empty;

            foreach (var a in url)
            {
                sb.AppendFormat("<link rel=\"stylesheet\" href=\"{0}Resources/Styles/{1}\">", "/", a);
            }
            HtmlString sss = new HtmlString(sb.ToString());
            return sss;
        }
        #region 常用CSS路径

        /// <summary>
        /// 引用bootstrap.min.css
        /// </summary>
        public const string Bootstrap = "Public/bootstrap/css/bootstrap.min.css";
        /// <summary>
        /// 引用bootstrap.Multiselect.min.css
        /// </summary>
        public const string BootstrapMultiselect = "Public/bootstrap-multiselect/bootstrap-multiselect.css";
        /// <summary>
        /// 引用common.css
        /// </summary>
        public const string Common = "Public/common.css";
        /// <summary>
        /// 引用jquery.ztree.css
        /// </summary>
        public const string JqueryZTree = "Public/jquery.ztree/metro.min.css";

        #endregion
    }
}