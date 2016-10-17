using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace InterfaceTool.Common
{
    public class ScriptsLoad
    {
        /// <summary>
        /// 加载scripts内容
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HtmlString Render(string[] url)
        {

            StringBuilder sb = new StringBuilder();
            string random = string.Empty;

            foreach (var a in url)
            {
                sb.AppendFormat("<script src=\"{0}Resources/Scripts/{1}{2}\"></script>", "/", a, random);
            }
            HtmlString sss = new HtmlString(sb.ToString());
            return sss;
        }
        #region 常用SCRIPT路径
        /// <summary>
        /// 引用jquery.min.js
        /// </summary>
        public const string Jquery = "Common/Jquery/v1.11.1/jquery.min.js";
        /// <summary>
        /// 引用commonFunc.js
        /// </summary>
        public const string CommonFunc = "Plugins/commonFunc.min.js";
        /// <summary>
        /// 引用jquery-ui.min.js
        /// </summary>
        public const string JqueryUI = "Common/jquery-ui-1.11.2/jquery-ui.min.js";
        /// <summary>
        /// 引用bootstrap.min.js
        /// </summary>
        public const string Bootstrap = "Common/bootstrap/bootstrap.min.js";
        /// <summary>
        /// 引用bootstrap-multiselect.min.js
        /// </summary>
        public const string BootstrapMultiselect = "Common/bootstrap-multiselect/bootstrap-multiselect.js";
        /// <summary>
        /// 引用layer.js
        /// </summary>
        public const string Layers = "Common/layer/v1.9.3/layer.min.js";
        /// <summary>
        /// 引用 jquery.tmpl.min.js
        /// </summary>
        public const string JqueryTmpl = "Common/Jquery.tmpl/jquery.tmpl.js";
        /// <summary>
        /// 引用 SerializeForms , 表单序列化与反序列化
        /// </summary>
        public const string SerializeForms = "Common/Jquery.SerializeForms/SerializeForms.min.js";
        /// <summary>
        /// 引用 JquerySuperSlide
        /// </summary>
        public const string JquerySuperSlide = "Common/Jquery.SuperSlide/SuperSlide.2.1.js";
        /// <summary>
        /// 引用 JqueryZtree 树形插件
        /// </summary>
        public const string JqueryZTree = "Common/Jquery.ztree/3.5.js";
        /// <summary>
        /// 引用 Jquery.bootgrid-1.2.0 分页插件
        /// </summary>
        public const string JqueryBootgrid_1_2_0 = "Common/Jquery.bootgrid-1.2.0/bootgrid.js";
        /// <summary>
        /// 引用bootstrapValidator验证插件
        /// </summary>
        public const string jQueryValidator = "Common/jquery.validate/jquery.validate.js";
        /// <summary>
        /// 引用 Plugins.js
        /// </summary>
        public const string Plugins = "Common/Plugins.js";
        /// <summary>
        /// 引用时间js
        /// </summary>
        public const string laydate = "Common/laydate/laydate.js";
        /// <summary>
        /// 引用Chart.js
        /// </summary>
        public const string Chart = "Common/Chart/Chart.min.js";

        #endregion
    }
}