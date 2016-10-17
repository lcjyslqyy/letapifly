using InterfaceTool.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InterfaceTool.Controllers
{
    public class InterfaceTestController : BaseController
    {
        //
        // GET: /InterfaceTest/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UploadInterfaceXml()
        {
            return View();
        }
        public ActionResult InterfaceList()
        {
            return View();
        }
        public ActionResult LogindataList()
        {
            return View();
        }
        public ActionResult EditLoginData(string id)
        {
            ViewBag.UID = id;
            return View();
        }
        public ActionResult EditPassword()
        {
            return View();
        }
    }
}
