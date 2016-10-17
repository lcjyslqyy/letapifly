using BC_Common;
using InterfaceTool.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterfaceTool.Controllers
{
    public class LoginController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public void Logout()
        {
            var cookie = HttpContext.Request.Cookies[AppsettingConfig.cookiename];

            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Response.Cookies.Add(cookie);
            }
            Response.Redirect("/Login/Index");
        }
        public void Login()
        {
            string user = Request["user"],
                password = CommonMethod.md5_32(Request["Password"]);
            string msg = "";
            int usertype = -1;
            Guid uid=new Guid();
            if (IsLogin(user, password,ref msg,ref usertype,ref uid))
            {
                //登录成功
                string url = "";
                url = "/Navigation/Index/";
                LoginInfo data = new LoginInfo();
                data.logincode = user;
                data.usertype = usertype;
                data.userid = uid;
                string loginStr = DESEncrypt.Encrypt(Newtonsoft.Json.JsonConvert.SerializeObject(data));
                HttpCookie cookie = new HttpCookie(AppsettingConfig.cookiename, loginStr);
                HttpContext.Response.Cookies.Add(cookie);
                CommonMethod.ResponseJsonResult(new { success = true, url = url, msg = msg });
            }
            else
            {
                CommonMethod.ResponseJsonResult(new { success = false, msg = msg });
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool IsLogin(string user, string password, ref string msg, ref int usertype, ref Guid uid)
        {
            bool islogin = false;
            LoginInfoModelList modellist = Get_LoginInfoList();
            if(modellist==null)
            {
                msg = "无帐号密码数据信息!";
                return islogin;
            }
            List<LoginInfoModel> inlist = modellist.logindatalist;

            bool isexist = inlist.Exists(x => x.logincode == user && x.passWord == password);
            if (isexist)
            {
                LoginInfoModel model = inlist.Find(x => x.logincode == user && x.passWord == password);
                if (model.status==1)
                {
                    islogin = false;
                    msg = "帐号已被禁用!";
                }
                else
                {
                    islogin = true;
                    msg = "登录成功!";
                    usertype = model.usertype;
                    uid = model.userid;
                }
               
            }
            else
            {
                msg = "帐号或密码错误!";
            }
            return islogin;
        }
        public void ViewTest()
        {
            //
           string eee= HttpUtil.Get("http://localhost:54243/");
        }
        /// <summary>
        /// 返回用户信息分页列表
        /// </summary>
        /// <returns></returns>
        private LoginInfoModelList Get_LoginInfoList()
        {
            string path = HttpContext.Server.MapPath(AppsettingConfig.logindatapath);
            string json = string.Empty;
            if (!System.IO.File.Exists(path))
            {
                return null;
            }
            else
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    json = sr.ReadToEnd();
                }
                if (!String.IsNullOrEmpty(json))
                {
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<LoginInfoModelList>(json);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
