using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSDAL;
using UCSIBLL;
using UCSModel;

namespace UCSBLL
{
    public partial class Sys_SystemInfoService : BaseService<Sys_SystemInfo>, ISys_SystemInfoService
    {
        Sys_SystemInfoDal dal = new Sys_SystemInfoDal();
        Sys_SysOfInter_RelDal inter_dal = new Sys_SysOfInter_RelDal();
        Sys_SysOfEntity_RelDal entity_dal = new Sys_SysOfEntity_RelDal();
        BLLCommon common = new BLLCommon();

        #region 获取系统账户的接口信息        
        public JsonModel GetInterfaceByAccountNo(string accountNo)
        {
            return GetJsonModelByDataTable(inter_dal.GetInterfaceByAccountNo(accountNo));
        }
        #endregion

        #region 修改系统账号
        /// <summary>
        /// 修改系统账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel EditSystemInfo(Sys_SystemInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditSystemInfo(model);
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

        #region 接口权限配置       
        public JsonModel SetInterfacePermission(string accountNo, string interidStr)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = inter_dal.SetInterfacePermission(accountNo, interidStr);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = "success",
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

        #region 获取系统账户的实体信息        
        public JsonModel GetEntityByAccountNo(string accountNo, string entityName="")
        {
            return GetJsonModelByDataTable(entity_dal.GetEntityByAccountNo(accountNo, entityName));
        }
        #endregion

        #region 实体权限配置       
        public JsonModel SetEntityPermission(Sys_SysOfEntity_Rel entity)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = entity_dal.SetEntityPermission(entity);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = "success",
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

        #region 实体字段配置       
        public JsonModel EditEntityRel(Sys_SysOfEntity_Rel entity)
        {
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = entity_dal.EditEntityRel(entity);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = "success",
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
    }
}
