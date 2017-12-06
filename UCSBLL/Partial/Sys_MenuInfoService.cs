using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSDAL;
using UCSIBLL;
using UCSModel;

namespace UCSBLL
{
    public partial class Sys_MenuInfoService : BaseService<Sys_MenuInfo>, ISys_MenuInfoService
    {
        Sys_MenuInfoDal dal = new Sys_MenuInfoDal();
        #region 获得导航处菜单信息
        /// <summary>
        /// 获得导航处菜单信息
        /// </summary>
        /// <returns></returns>
        public JsonModel GetNavigationMenu(string uniqueNo, string pid = "0",bool isAllLeaf = false)
        {
            return GetJsonModelByDataTable(dal.GetNavigationMenu(uniqueNo, pid, isAllLeaf));
        }
        #endregion

        #region 根据url查找父级
        /// <summary>
        /// 根据url查找父级
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isMaxPar">true：查找最大的父级  false：查找所有父级</param>
        /// <returns></returns>
        public JsonModel GetParentMenuByUrl(string url, bool isMaxPar=true)
        {            
            return GetJsonModelByDataTable(dal.GetParentMenuByUrl(url, isMaxPar));
        }
        #endregion       

        #region 根据pid和用户唯一号查找菜单
        /// <summary>
        /// 根据pid和用户唯一号查找菜单
        /// </summary>
        /// <returns></returns>
        public JsonModel GetMenuByPidAndUniqueNo(string uniqueNo, string pid)
        {
            return GetJsonModelByDataTable(dal.GetMenuByPidAndUniqueNo(uniqueNo, pid));            
        }
        #endregion

        #region 新建菜单
        /// <summary>
        /// 新建菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel EditMenu(Sys_MenuInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditMenu(model);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = result == 0 ? "success" : "",
                    retData = "操作成功"
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 删除菜单        
        public JsonModel DeleteMenuInfo(int menuid)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.DeleteMenuInfo(menuid);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = result == 0 ? "success" : "",
                    retData = "操作成功"
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion

        #region 根据url查找子级button        
        public JsonModel GetSubButtonByUrl(string purl, string uniqueNo,string menuCode="")
        {           
            return GetJsonModelByDataTable(dal.GetSubButtonByUrl(purl, uniqueNo, menuCode));
        }
        #endregion            
    }
}
