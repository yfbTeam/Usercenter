using System;
using System.Collections;
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
    public partial class Sys_RoleService : BaseService<Sys_Role>, ISys_RoleService
    {
        Sys_RoleDal dal = new Sys_RoleDal();
        Sys_RoleOfUserDal ruser_dal = new Sys_RoleOfUserDal();
        BLLCommon common = new BLLCommon();
        #region 获取全部角色，返回DataTable
        /// <summary>
        /// 获取全部角色，返回DataTable
        /// </summary>
        public DataTable GetAllRoleList()
        {
            return dal.GetAllRoleList();
        }
        #endregion

        #region 删除角色并删除该角色的相关数据
        /// <summary>
        /// 删除角色并删除该角色的相关数据
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public JsonModel DeleteRole(int roleid)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result=dal.DeleteRole(roleid);  
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

        #region 设置角色成员
        /// <summary>
        /// 设置角色成员
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <param name="uniqueNoStr">用户唯一号字符串，以逗号连接</param>
        /// <returns>返回 JsonModel</returns>
        public JsonModel SetRoleMember(string roleid, string uniqueNoStr)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = ruser_dal.SetRoleMember(roleid, uniqueNoStr);
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

        #region 编辑角色及菜单        
        public JsonModel EditRole(Sys_Role model, string menuids)
        {
            //定义JSON标准格式实体中
            JsonModel jsonModel = new JsonModel();
            try
            {
                int result = dal.EditRole(model, menuids);
                jsonModel = new JsonModel()
                {
                    errNum = result,
                    errMsg = result == 0 ? "success" : "",
                    retData = ""
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

        #region 获取某用户的角色信息
        /// <summary>
        /// 获取某用户的角色信息
        /// </summary>
        public JsonModel GetRoleByUser(Hashtable ht)
        {
            return GetJsonModelByDataTable(dal.GetRoleByUser(ht));            
        }
        #endregion

        #region 删除关系数据， 删单条
        /// <summary>
        /// 删除关系数据， 删单条
        /// </summary>
        /// <returns>返回 JsonModel</returns>
        public JsonModel DeleteUserRelation(string ids)
        {
            JsonModel jsonModel = null;
            try
            {
                bool result = ruser_dal.DeleteUserRelation(ids);
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = result
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

