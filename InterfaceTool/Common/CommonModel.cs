using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterfaceTool.Common
{
    /// <summary>
    /// 返回适用于Grid绑定格式分页集
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageGrid
    {
        /// <summary>
        /// 当前页码
        /// </summary>
        public long current { get; set; }
        /// <summary>
        /// 每一页行数
        /// </summary>
        public long rowCount { get; set; }
        /// <summary>
        /// 总行数
        /// </summary>
        public long total { get; set; }
        /// <summary>
        /// 返回数据集
        /// </summary>
        public object rows { get; set; }
    }
    /// <summary>
    /// 登录信息的model用于记录
    /// </summary>
    public class LoginInfoModelList
    {
        public List<LoginInfoModel> logindatalist { get; set; }
    }
    /// <summary>
    /// 登录信息的model用于记录
    /// </summary>
    public class LoginInfoModel
    {
        public Guid userid { get; set; }
        public string userName { get; set; }
        public string logincode { get; set; }
        public string passWord { get; set; }
        public int usertype { get; set; }
        //0为正常,1为禁用
        public int status { get; set; }
    }
    /// <summary>
    /// 接口列表model
    /// </summary>
    public class InterfaceModelList
    {
        public List<InterfaceModel> inlist { get; set; }
    }
    /// <summary>
    /// 接口model
    /// </summary>
    public class InterfaceModel
    {
        /// <summary>
        /// 接口key,唯一性
        /// </summary>
        public string InterfaceName { get; set; }
        /// <summary>
        /// 接口链接
        /// </summary>
        public string InterfaceUrl { get; set; }
        /// <summary>
        ///下接筛选的名称(由接口名称+接口链接组成)
        /// </summary>
        public string selecName { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string chName { get; set; }
        /// <summary>
        /// 接口描述
        /// </summary>
        public string InterfaceDes { get; set; }
        /// <summary>
        /// 接口返回参数说明
        /// </summary>
        public string InterfaceReDes { get; set; }
    }
    /// <summary>
    /// 登录的cookie信息
    /// </summary>
    public class LoginInfo
    {
        public string logincode { get; set; }
        public int usertype { get; set; }
        public Guid userid { get; set; }
    }
    /// <summary>
    /// 导航菜单列表 
    /// </summary>
    public class MenuList
    {
        public List<Menu> menulist { get; set; }
    }
    public class Menu
    {
        public int usertype { get; set; }
        public string navigationname { get; set; }
        public string controlurl { get; set; }
        public string icon { get; set; }
    }
}