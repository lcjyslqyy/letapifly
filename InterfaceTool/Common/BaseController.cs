using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterfaceTool.Common
{
    public class BaseController : Controller
    {
        protected LoginInfo logdata = null;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            LoginUser(filterContext);
        }
        protected override void OnException(ExceptionContext filterContext)
        {           
            //if (!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
           
            if (!filterContext.ExceptionHandled)
            {
                filterContext.HttpContext.Response.ContentType = "application/json";
                filterContext.HttpContext.Response.Write(Newtonsoft.Json.JsonConvert.SerializeObject(new { success = false, msg = filterContext.Exception.ToString() }));
                filterContext.ExceptionHandled = true;
                
            }
            base.OnException(filterContext);

            
        }
        public void LoginUser(ActionExecutingContext filterContext)
        {
            logdata=GetLoginData(HttpContext);
            if(logdata==null)
            {
                filterContext.HttpContext.Response.Redirect("/Login/Index",true);
            }
        }
        private HttpCookie GetLoginCookie(HttpContextBase Context)
        {
            return Context.Request.Cookies[AppsettingConfig.cookiename];
        }
        /// <summary>
        /// 获取登录数据
        /// </summary>
        /// <returns></returns>
        private LoginInfo GetLoginData(HttpContextBase Context)
        {
            LoginInfo loginData = null;
            var loginCookie = GetLoginCookie(Context);
            if (loginCookie != null)
            {
                loginData = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginInfo>(DESEncrypt.Decrypt(loginCookie.Value));

            }
            return loginData;
        }
       
    }
}
