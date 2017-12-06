using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSIDAL;
using UCSModel;
using UCSUtility;

namespace UCSDAL
{
    public partial class Org_MechanismDal : BaseDal<Org_Mechanism>, IOrg_MechanismDal
    {
        #region 获得首页组织架构信息
        /// <summary>
        /// 获得首页组织架构信息
        /// </summary>  
        public DataTable GetOrgMenu(string pid)
        {
            try
            {
                StringBuilder sbSql4org;
                List<SqlParameter> pms = new List<SqlParameter>();

                sbSql4org = new StringBuilder();
                sbSql4org.Append(@"select * from Org_Mechanism");
                sbSql4org.Append(" order by OrderNum");
                //pms.Add(new SqlParameter("@Pid", pid));
                return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
                return null;
            }

        }
        #endregion

        #region 组织架构添加
        /// <summary>
        /// 组织架构添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddOrg(Org_Mechanism model)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                new SqlParameter("@Name",model.Name),
                                new SqlParameter("@Pid",model.Pid),
                                new SqlParameter("@OrganType",model.OrganType),
                                };
                object obj = SQLHelp.ExecuteScalar("AddOrg", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 组织架构修改
        /// <summary>
        /// 组织架构修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditOrg(Org_Mechanism model)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                new SqlParameter("@ID",model.Id.ToString()),
                                new SqlParameter("@Name",model.Name),
                                new SqlParameter("@OrganNo",model.OrganNo.SafeToString()),
                                new SqlParameter("@Pid",model.Pid),
                                new SqlParameter("@OrganType",model.OrganType.ToString()),
                                new SqlParameter("@Introduce",model.Introduce.SafeToString()),
                                new SqlParameter("@LegalUID",model.LegalUID.SafeToString()),
                                new SqlParameter("@Establish",model.EstabLish.ToString()),
                                new SqlParameter("@ImageInfo",model.ImageInfo.SafeToString()),
                                new SqlParameter("@IsDelete",model.IsDelete.ToString()),
                                 new SqlParameter("@UserCount",model.UserCount.ToString())
                                };
                object obj = SQLHelp.ExecuteScalar("EditOrg", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 组织架构排序修改
        /// <summary>
        /// 组织架构排序修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditOrgOrder(int OrgID, string OrderType)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                new SqlParameter("@ID",OrgID),
                                new SqlParameter("@OrderType",OrderType)
                                };
                object obj = SQLHelp.ExecuteScalar("EditOrgOrder", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 统计不同类型组织机构数目
        /// <summary>
        /// 统计不同类型组织机构数目
        /// </summary>
        /// <returns></returns>
        public int censusOrg(string OrganType)
        {
            int retult = 0;
            try
            {
                string strSql = "select count(1) from [dbo].[Org_Mechanism] where OrganType in (" + OrganType + ")";
                object obj = SQLHelp.ExecuteScalar(strSql, CommandType.Text, null);
                retult = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 删除组织机构
        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="OrgID">ID</param>
        /// <returns></returns>
        public string DeleteOrg(int OrgID)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                new SqlParameter("@ID",OrgID),
                                };
                object obj = SQLHelp.ExecuteScalar("DelOrg", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion
    }
}
