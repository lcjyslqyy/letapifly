using InterfaceTool.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;


namespace InterfaceTool.Controllers
{
    public class MethodController : BaseController
    {
        /// <summary>
        /// 保存接口data
        /// </summary>
        public void Save_InterfaceData()
        {
            string InterfaceName = Request["InterfaceName"];
            string InterfaceUrl = Request["InterfaceUrl"];
            string xml_base64 = Request["xml_base64"];
            string chName = Request["chName"];
            string InterfaceDes = Request["InterfaceDes"];
            string InterfaceReDes = Request["InterfaceReDes"];
            string msgout = string.Empty;
            bool isupload = UploadXmlFile(xml_base64, InterfaceName, out msgout);
            if (isupload)
            {
                InterfaceModelList savelist = new InterfaceModelList();
                InterfaceModelList list = Get_InterFaceList();
                if (list != null)
                {
                    List<InterfaceModel> inlist = list.inlist;
                    // inlist.Remove(new InterfaceModel { InterfaceName = InterfaceName });
                    inlist.RemoveAll(x => x.InterfaceName == InterfaceName);//按Key删除
                    inlist.Add(new InterfaceModel { InterfaceReDes = InterfaceReDes,InterfaceName = InterfaceName, InterfaceUrl = InterfaceUrl, selecName = string.Format("{0}({1})", chName, InterfaceUrl), chName = chName, InterfaceDes = InterfaceDes });
                    savelist.inlist = inlist;
                    string path = HttpContext.Server.MapPath(AppsettingConfig.interlistpath);
                    CreateconfigFile(path);
                    FileStream fs = new FileStream(path, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(savelist));//写入文件,替换原文件
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                    CommonMethod.ResponseJsonResult(new { success = true, msg = "保存测试接口成功." });
                }
                else
                {
                    List<InterfaceModel> inlist = new List<InterfaceModel>();
                    inlist.Add(new InterfaceModel { InterfaceReDes = InterfaceReDes, InterfaceName = InterfaceName, InterfaceUrl = InterfaceUrl, selecName = string.Format("{0}({1})", chName, InterfaceUrl), chName = chName, InterfaceDes = InterfaceDes });
                    savelist.inlist = inlist;
                    string path = HttpContext.Server.MapPath(AppsettingConfig.interlistpath);
                    CreateconfigFile(path);
                    FileStream fs = new FileStream(path, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(savelist));//写入文件,替换原文件
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                    CommonMethod.ResponseJsonResult(new { success = true, msg = "保存测试接口成功." });
                }
            }
            else
            {
                msgout = msgout == "" ? "上传文件出错,请确定文件是否正确" : msgout;
                CommonMethod.ResponseJsonResult(new { success = false, msg = msgout });
            }
        }
        /// <summary>
        /// 上传xml文档
        /// </summary>
        /// <param name="base64str"></param>
        /// <returns></returns>
        private bool UploadXmlFile(string base64img, string savename, out string msg)
        {
            var img_data = base64img.Split(',');
            msg = "";
            if (img_data.Length == 2)
            {
                if (img_data[0] != "data:text/xml;base64")
                {
                    msg = "上传的文件格式不是xml文件.";
                    return false;
                }
                else
                {
                    var imgdata = img_data[1];
                    try
                    {
                        using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(imgdata)))
                        {
                            string basepath = string.Format(AppsettingConfig.uploadpath, DateTime.Now.ToString("yyyyMMdd"));
                            string dirpath = HttpContext.Server.MapPath(basepath);
                            string filename = String.Format("{0}.{1}", savename, "config");
                            string savepath = String.Format("{0}{1}", dirpath, filename);

                            if (!Directory.Exists(dirpath))
                            {
                                System.IO.Directory.CreateDirectory(dirpath);
                            }
                            FileStream fs = new FileStream(savepath, FileMode.Create);
                            BinaryWriter w = new BinaryWriter(fs);
                            w.Write(ms.ToArray());
                            fs.Close();
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        msg = ex.ToString();
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 判断原来是否存在这个xml文档
        /// </summary>
        public void Comfird_FileExist()
        {

            string InterfaceName = Request["InterfaceName"];
            string filepath = AppsettingConfig.xmlpath.Replace("{InterfaceKey}", InterfaceName);
            InterfaceModelList list = Get_InterFaceList();
            if (list != null)
            {
                List<InterfaceModel> inlist = list.inlist;
                bool isexist = inlist.Exists(x => x.InterfaceName == InterfaceName);
                CommonMethod.ResponseJsonResult(new { success = true, isexist = isexist });
            }
            else
            {
                CommonMethod.ResponseJsonResult(new { success = true, isexist = false });
            }
        }
       
        /// <summary>
        /// 获取接口的列表
        /// </summary>
        private InterfaceModelList Get_InterFaceList()
        {
            string path = HttpContext.Server.MapPath(AppsettingConfig.interlistpath);
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
                    return Newtonsoft.Json.JsonConvert.DeserializeObject<InterfaceModelList>(json);
                }
                else
                {
                    return null;
                }
            }

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
        /// <summary>
        /// 获取接口的链接与key值
        /// </summary>
        public void Get_InterfaceDataModel()
        {
            string InterfaceName = Request["InterfaceName"];
            if (String.IsNullOrEmpty(InterfaceName))
            {
                CommonMethod.ResponseJsonResult(new { success = false, msg = "请选择所需要接口!" });
            }
            else
            {
                InterfaceModelList list = Get_InterFaceList();
                if (list != null)
                {
                    List<InterfaceModel> inlist = list.inlist;
                    InterfaceModel model = inlist.First(x => x.InterfaceName == InterfaceName);
                    CommonMethod.ResponseJsonResult(new { success = true, datamodel = model });
                }
                else
                {
                    CommonMethod.ResponseJsonResult(new { success = false, msg = "不存在接口列表" });
                }
            }
        }
        /// <summary>
        /// 获取接口列表
        /// </summary>
        public void Get_InterfaceTestList()
        {
            InterfaceModelList list = Get_InterFaceList();
            if (list != null)
            {
                CommonMethod.ResponseJsonResult(list.inlist);
            }
            else
            {
                CommonMethod.ResponseJsonResult(new List<InterfaceModel>());
            }
        }
        /// <summary>
        /// 获取接口模型
        /// </summary>
        public void Get_InterfaceModel()
        {
            string InterfaceName = Request["InterfaceName"];
            XmlDocument doc1 = new XmlDocument();
            List<Hashtable> l_ht = new List<Hashtable>();
            Hashtable ht = null;
            string eee = this.HttpContext.Server.MapPath(AppsettingConfig.xmlpath.Replace("{InterfaceKey}", InterfaceName));
            doc1.Load(this.HttpContext.Server.MapPath(AppsettingConfig.xmlpath.Replace("{InterfaceKey}", InterfaceName)));
            XmlNodeList domList = doc1.SelectSingleNode(String.Format("/{0}", InterfaceName)).ChildNodes;//只选择与key对应的node
            foreach (XmlNode xn in domList)
            {
                ht = new Hashtable();
                ht.Add("name", xn.Name);
                ht.Add("value", xn.InnerText);
                ht.Add("title", xn.Attributes.Count > 1 ? xn.Attributes["title"].InnerText : "");
                ht.Add("des", xn.Attributes.Count > 0 ? xn.Attributes["des"].InnerText : "");
                l_ht.Add(ht);
            }
            CommonMethod.ResponseJsonResult(l_ht);
        }
        /// <summary>
        /// 确定调试接口
        /// </summary>
        public void Interface_ComfirdTest()
        {
            string testurl = Request["testurl"];
            //IDictionary<string, string> ymForm =Request.Params
            string postdata = Request["postdata"];
            string urlreplacedata = Request["urlreplacedata"];//替换的url的字段,用&分隔主的,用=分隔
            if(!String.IsNullOrEmpty(urlreplacedata))
            {
                string[] listprace = urlreplacedata.Split('&');
                if(listprace.Length>0)
                {
                    foreach(string s in listprace)
                    {
                        string[] replacestr = s.Split('=');
                        if(replacestr.Length==2)
                        {
                            testurl = testurl.Replace(replacestr[0], replacestr[1]);
                        }
                    }
                }
            }
            //string pppp = "[{\"name\":\"openid\","value":"o-1izs5aTRhzaYtESxLfx6jm7-6U"}]";
            string sss = BC_Common.HttpUtil.Post(postdata, testurl);
            CommonMethod.ResponseJsonResult(sss);
        }
        /// <summary>
        /// 返回用户信息分页
        /// </summary>
        public void Login_DataPage()
        {
            LoginInfoModelList list= Get_LoginInfoList();
            PageGrid pg = new PageGrid();
            if(list!=null)
            {
                pg.total = list.logindatalist.Count;
                pg.current = 1;
                pg.rowCount = list.logindatalist.Count;
                pg.rows = list.logindatalist;
                CommonMethod.ResponseJsonResult(pg);
            }
            else
            {
                CommonMethod.ResponseJsonResult(pg);
            }
        }
        /// <summary>
        /// 新增登录信息
        /// </summary>
        public void ADD_LoginData()
        {
            LoginInfoModel model = new LoginInfoModel();
            model.userid = System.Guid.NewGuid();
            model.userName = Request["userName"];
            model.logincode = Request["logincode"];
            model.passWord = CommonMethod.md5_32(Request["passWord"]);
            model.usertype =int.Parse( Request["usertype"]);
            model.status = 0;
            LoginInfoModelList savelist = new LoginInfoModelList();
            LoginInfoModelList list = Get_LoginInfoList();
            if (list != null)
            {
                List<LoginInfoModel> inlist = list.logindatalist;
                if (inlist.Exists(x=>x.logincode==model.logincode))
                {
                    CommonMethod.ResponseJsonResult(new { success = false, msg = "登录帐号已存在." });
                    return;
                }
                inlist.Add(model);
                savelist.logindatalist = inlist;
                string path = HttpContext.Server.MapPath(AppsettingConfig.logindatapath);
                CreateconfigFile(path);
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(savelist));//写入文件,替换原文件
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                CommonMethod.ResponseJsonResult(new { success = true, msg = "新增成功." });
            }
            else
            {
                List<LoginInfoModel> inlist = new List<LoginInfoModel>();
                inlist.Add(model);
                savelist.logindatalist = inlist;
                string path = HttpContext.Server.MapPath(AppsettingConfig.logindatapath);
                CreateconfigFile(path);
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(savelist));//写入文件,替换原文件
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                CommonMethod.ResponseJsonResult(new { success = true, msg = "新增成功." });
            }
        }
        public void GetLoginInfoModel(Guid id)
        {
             if (id != null)
            {
                LoginInfoModelList savelist = new LoginInfoModelList();
                LoginInfoModelList list = Get_LoginInfoList();
                if (list != null)
                {
                    List<LoginInfoModel> inlist = list.logindatalist;
                    if (!inlist.Exists(x => x.userid == id))
                    {
                        CommonMethod.ResponseJsonResult("");
                        return;
                    }
                    LoginInfoModel model = inlist.Find(x => x.userid == id);
                    CommonMethod.ResponseJsonResult(model);
                }
                else
                {
                    CommonMethod.ResponseJsonResult("");
                }
            }
             else
             {
                 CommonMethod.ResponseJsonResult("");
             }
        }
        /// <summary>
        /// 更新登录信息
        /// </summary>
        public void Update_LoginData(Guid id)
        {
            if (id != null)
            {
                LoginInfoModel model = new LoginInfoModel();
                model.userid = id;
                model.userName = Request["userName"];
                model.logincode = Request["logincode"];
                model.passWord = CommonMethod.md5_32(Request["passWord"]);
                model.usertype = int.Parse(Request["usertype"]);
                model.status = 0;
                LoginInfoModelList savelist = new LoginInfoModelList();
                LoginInfoModelList list = Get_LoginInfoList();
                if (list != null)
                {
                    List<LoginInfoModel> inlist = list.logindatalist;
                    if (!inlist.Exists(x => x.userid == model.userid))
                    {
                        CommonMethod.ResponseJsonResult(new { success = false, msg = "无此用户id导致更新失败!" });
                        return;
                    }
                    inlist.RemoveAll(x => x.userid == model.userid);//按uid删除
                    inlist.Add(model);
                    savelist.logindatalist = inlist;
                    string path = HttpContext.Server.MapPath(AppsettingConfig.logindatapath);
                    CreateconfigFile(path);
                    FileStream fs = new FileStream(path, FileMode.Create);
                    StreamWriter sw = new StreamWriter(fs);
                    //开始写入
                    sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(savelist));//写入文件,替换原文件
                    //清空缓冲区
                    sw.Flush();
                    //关闭流
                    sw.Close();
                    fs.Close();
                    CommonMethod.ResponseJsonResult(new { success = true, msg = "更新成功." });
                }
                else
                {                  
                    CommonMethod.ResponseJsonResult(new { success = true, msg = "无数据导致更新失败,请查找文件是否存在." });
                }
            }
            else
            {
                CommonMethod.ResponseJsonResult(new { success = false, msg = "更新失败!" });
            }
        }
        public void Update_PassWord()
        {
            string oldpassword = Request["oldpassword"];
            string newpassword = Request["newpassword"];
            LoginInfoModelList list = Get_LoginInfoList();
            LoginInfoModelList savelist = new LoginInfoModelList();
            if (list != null)
            {
                List<LoginInfoModel> inlist = list.logindatalist;
                if (!inlist.Exists(x => x.userid == logdata.userid))
                {
                    CommonMethod.ResponseJsonResult(new { success = false, msg = "无此用户id导致更新失败!" });
                    return;
                }
                LoginInfoModel model = inlist.Find(x => x.userid == logdata.userid);
                if (model.passWord == oldpassword)
                {
                    model.passWord = newpassword;
                }
                else
                {
                    CommonMethod.ResponseJsonResult(new { success = false, msg = "旧密码错误，请修改!" });
                    return;
                }
                inlist.RemoveAll(x => x.userid == logdata.userid);//按uid删除
                inlist.Add(model);
                savelist.logindatalist = inlist;
                string path = HttpContext.Server.MapPath(AppsettingConfig.logindatapath);
                CreateconfigFile(path);
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                //开始写入
                sw.Write(Newtonsoft.Json.JsonConvert.SerializeObject(savelist));//写入文件,替换原文件
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                CommonMethod.ResponseJsonResult(new { success = true, msg = "更新密码成功!" });
            }
            else
            {
                CommonMethod.ResponseJsonResult(new { success = true, msg = "无数据导致更新失败,请查找文件是否存在." });
            }
        }
        /// <summary>
        /// 创建文档
        /// </summary>
        /// <param name="path"></param>
        private void CreateconfigFile(string path)
        {
            if(!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path);
            }
        }
        public void Create_Code()
        {
            string InterfaceName = Request["InterfaceName"];
            XmlDocument doc1 = new XmlDocument();
            List<Hashtable> l_ht = new List<Hashtable>();
            StringBuilder strmodel = new StringBuilder();
            doc1.Load(this.HttpContext.Server.MapPath(AppsettingConfig.xmlpath.Replace("{InterfaceKey}", InterfaceName)));
            XmlNodeList domList = doc1.SelectSingleNode(String.Format("/{0}", InterfaceName)).ChildNodes;
            var Content = "#Property#";
            // "public partial class #TableName# \r\n    {\r\n#Property#\r\n\t}\r\n}\r\n";
            strmodel.Append("pulic class #TableName#\r\n{\r\n");
            foreach (XmlNode xn in domList)
            {
                string DataType;
                if (xn.Attributes.Count > 1)
                {
                    if (xn.Attributes["datatype"]!=null)
                    {
                        DataType = xn.Attributes["datatype"].InnerText;
                    }
                    else
                    {
                        DataType = "string";
                    }
                }
                else
                {
                    DataType = "string";
                }
                strmodel.Append(BuildColumn(GetPropertyType(DataType, false), xn.Name, Content));
            }
            strmodel.Append("\r\n}\r\n");
            Content = Content.Replace("#Property#", strmodel.ToString().Replace("#TableName#", InterfaceName));

            CommonMethod.ResponseJsonResult(Content);
        }        
        public string BuildColumn(string datatype, string name, string content)
        {
            StringBuilder sb = new StringBuilder();

            //if (!String.IsNullOrEmpty(content))
            //{
            //    sb.AppendLine("\t\t/// <summary>");
            //    sb.AppendLine("\t\t/// " + content);
            //    sb.AppendLine("\t\t/// </summary>");
            //}

            sb.AppendFormat(COLUMN_TEMPLATE, datatype, name);

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        const string COLUMN_TITLE = "/// <summary>";
        const string COLUMN_TEMPLATE = "\t\t public {0} {1} {{ get; set; }} \n\n";
        string GetPropertyType(string sqlType, bool IsNullable)
        {
            string sysType = "string";
            switch (sqlType)
            {
                case "bigint":
                    sysType = "long";
                    break;
                case "smallint":
                    sysType = "short";
                    break;
                case "int":
                    sysType = "int";
                    break;
                case "uniqueidentifier":
                    sysType = "Guid";
                    break;
                case "datetime":
                    sysType = "DateTime";
                    break;
                case "float":
                    sysType = "double";
                    break;
                case "real":
                    sysType = "float";
                    break;
                case "numeric":
                case "smallmoney":
                case "decimal":
                    sysType = "decimal";
                    break;
                case "money":
                    sysType = "decimal";
                    break;
                case "tinyint":
                    sysType = "byte";
                    break;
                case "bit":
                    sysType = "bool";
                    break;
            }

            if (IsNullable && !"string".Equals(sysType))
            {
                sysType += "?";
            }

            return sysType;
        }
    }
   

}
