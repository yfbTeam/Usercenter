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
    public partial class Sys_UserInfoService : BaseService<Sys_UserInfo>, ISys_UserInfoService
    {
        Sys_UserInfoDal dal = new Sys_UserInfoDal();
        BLLCommon common = new BLLCommon();
        public DataTable IsPwdTure(string UserName, string PassWord)
        {
            return dal.IsPwdTure(UserName, PassWord);
        }
        #region 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="LoginName">登录名</param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <returns></returns>
        public JsonModel UpdatePwd(string LoginName, string OldPwd, string NewPwd)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.UpdatePwd(LoginName, OldPwd, NewPwd);
            if (result == "")
            {
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "修改成功",
                    retData = ""
                };
            }
            else
            {
                jsonModel = new JsonModel
                {
                    errNum = 999,
                    errMsg = result,
                    retData = ""
                };
            }
            return jsonModel;

        }
        #endregion
        #region 数据导入
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public JsonModel ImportUser(string FilePath, string OrgNo)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.ImportUser(FilePath, OrgNo);
            jsonModel = new JsonModel()
            {
                errNum = 0,
                errMsg = result,
                retData = ""
            };
            return jsonModel;
        }
        #endregion

        #region 用户注册

        public JsonModel GetUserInfoByUniqueNo(Hashtable ht)
        {
            DataTable modList = dal.GetUserInfoByUniqueNo(ht);
            JsonModel jsonModel = null;
            if (modList == null || (modList != null && modList.Rows.Count <= 0))
            {
                jsonModel = new JsonModel()
                {
                    errNum = 999,
                    errMsg = "null"
                };
                return jsonModel;
            }
            List<string> list = new List<string>();
            list.Add(common.DataTableToJson(modList));
            jsonModel = new JsonModel()
            {
                errNum = 0,
                errMsg = "success",
                retData = list
            };
            return jsonModel;
        }

        #endregion

        #region 统计不同组织机构用户数目
        /// <summary>
        /// 统计不同组织机构用户数目
        /// </summary>
        /// <returns></returns>
        public JsonModel censusUser(string RegisterOrg, string AuthenType)
        {
            JsonModel jsonModel = new UCSModel.JsonModel();
            int result = dal.censusUser(RegisterOrg, AuthenType);
            //if (result != 0)
            //{
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "",
                    retData = result.ToString()
                };
            //}
            //else
            //{
            //    jsonModel = new JsonModel
            //    {
            //        errNum = 999,
            //        errMsg = result.ToString(),
            //        retData = ""
            //    };
            //}
            return jsonModel;

        }
        #endregion

        #region 统计活跃用户数
        /// <summary>
        /// 统计活跃用户数
        /// </summary>
        /// <returns></returns>
        public JsonModel censusActiveUser()
        {
            JsonModel jsonModel = new JsonModel();
            int result = dal.censusActiveUser();
            //if (result != 0)
            //{
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "",
                    retData = result.ToString()
                };
            //}
            //else
            //{
            //    jsonModel = new JsonModel
            //    {
            //        errNum = 999,
            //        errMsg = result.ToString(),
            //        retData = ""
            //    };
            //}
            return jsonModel;


        }
        #endregion

          #region 编辑用户
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel EditUserInfo(Sys_UserInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.EditUserInfo(model);
            if (result == "")
            {
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "修改成功",
                    retData = ""
                };
            }
            else
            {
                jsonModel = new JsonModel
                {
                    errNum = 999,
                    errMsg = result,
                    retData = ""
                };
            }
            return jsonModel;

        }
          #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel AddUserInfo(Sys_UserInfo model)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.AddUserInfo(model);
            if (result == "")
            {
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "添加成功",
                    retData = ""
                };
            }
            else
            {
                jsonModel = new JsonModel
                {
                    errNum = 999,
                    errMsg = result,
                    retData = ""
                };
            }
            return jsonModel;

        }
        #endregion

        #region CRM_Login
        public CRM_JsonModel CRM_Login(Hashtable ht, string where = "")
        {
            int RowCount = 0;
            BLLCommon common = new BLLCommon();
            try
            {                
                DataTable modList = CurrentDal.GetListByPage(ht, out RowCount, false, where);
                //定义JSON标准格式实体中
                CRM_JsonModel jsonModel = null;
                if (modList == null || modList.Rows.Count <= 0)
                {
                    jsonModel = new CRM_JsonModel()
                    {
                        errNum = 999,
                        errMsg = "无数据",
                        retData = ""
                    };
                    return jsonModel;
                }
                DataTable orgDt=new Sys_RoleOfUserDal().GetUserByRegisterOrg(modList.Rows[0]["RegisterOrg"].ToString());
                List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
                List<Dictionary<string, object>> orglist = new List<Dictionary<string, object>>();
                list = common.DataTableToList(modList);
                orglist=common.DataTableToList(orgDt);
                jsonModel = new CRM_JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = list,
                    orgData= orglist
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                CRM_JsonModel jsonModel = new CRM_JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion   

        #region 获取部门成员
        public JsonModel GetUserByRegisterOrg(string registerOrg)
        {            
            DataTable userDt = new Sys_RoleOfUserDal().GetUserByRegisterOrg(registerOrg);           
            return GetJsonModelByDataTable(userDt);
        }
        #endregion        
    }
}
