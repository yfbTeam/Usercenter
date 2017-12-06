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
    public partial class Org_MechanismService : BaseService<Org_Mechanism>, IOrg_MechanismService
    {
        Org_MechanismDal dal = new Org_MechanismDal();
        #region 获得首页组织架构
        /// <summary>
        /// 获得首页左侧导航处菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetOrgMenu(string pid = "0")
        {
            return dal.GetOrgMenu(pid);
        }
        #endregion

        #region 组织架构添加
        /// <summary>
        /// 组织架构添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel AddOrg(Org_Mechanism model)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.AddOrg(model);
            if (result.IndexOf("添加成功") > 0)
            {
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "添加成功",
                    retData = result.Split('-')[0]
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

        #region 组织架构修改
        /// <summary>
        /// 组织架构修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel EditOrg(Org_Mechanism model)
        {
            JsonModel jsonModel = new UCSModel.JsonModel();
            string result = dal.EditOrg(model);
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

        #region 组织架构排序修改
        /// <summary>
        /// 组织架构排序修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel EditOrgOrder(int OrgID, string OrderType)
        {
            JsonModel jsonModel = new UCSModel.JsonModel();
            string result = dal.EditOrgOrder(OrgID, OrderType);
            if (result == "")
            {
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "操作成功",
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

        #region 统计不同类型组织机构数目
        /// <summary>
        /// 统计不同类型组织机构数目
        /// </summary>
        /// <returns></returns>
        public JsonModel censusOrg(string OrganType)
        {
            JsonModel jsonModel = new UCSModel.JsonModel();
            int result = dal.censusOrg(OrganType);
            //if (result !=0)
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

        #region 删除组织机构
        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="OrgID">ID</param>
        /// <returns></returns>
        public JsonModel DeleteOrg(int OrgID)
        {
            JsonModel jsonModel = new UCSModel.JsonModel();
            string result = dal.DeleteOrg(OrgID);
            if (result == "")
            {
                jsonModel = new JsonModel
                {
                    errNum = 0,
                    errMsg = "删除成功",
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

    }
}
