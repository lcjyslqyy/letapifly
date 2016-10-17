using InterfaceTool.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterfaceTool.Controllers
{
    public class NavigationController : BaseController
    {
        //
        // GET: /Navigation/

        public ActionResult Index()
        {
            //8ae4fda7-3476-49bc-b97e-0246f9d8c70d  00000000-0000-0000-0000-000000000000
           
            if (logdata!=null)
            {
                ViewBag.logincode = logdata.logincode;
            }
            return View();
        }
        public ActionResult BaseIndex()
        {
            return View();
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Get_Menu()
        {
            MenuList list = Get_MenuList();
            if(list!=null)
            {
                List<Menu> menulist = list.menulist;
                List<Menu> mymenulist = new List<Menu>();
                int logintype = logdata.usertype;
                if (logintype==0)//超管可管理登录信息
                {
                    mymenulist = menulist.FindAll(x => x.usertype == 0);
                }
                else if (logintype == 1)//接口管理员可上传接口
                {
                    mymenulist = menulist.FindAll(x => x.usertype == 1);
                }
                else//其它均默认为接口使用人员
                {
                    mymenulist = menulist.FindAll(x => x.usertype == 2);
                }
                return Json(mymenulist);
            }
            else
            {
                return Json("");
            }
        }
        /// <summary>
        /// 获取导航菜单列表
        /// </summary>
        private MenuList Get_MenuList()
        {
            string path = HttpContext.Server.MapPath(AppsettingConfig.menulist);
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
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<MenuList>(json);
                }
                else
                {
                    return null;
                }
            }

        }
    }
}
