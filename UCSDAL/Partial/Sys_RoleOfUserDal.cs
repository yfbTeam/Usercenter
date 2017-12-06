using System;
using System.Collections;
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
    public partial class Sys_RoleOfUserDal : BaseDal<Sys_RoleOfUser>, ISys_RoleOfUserDal
    {
        #region 查询方法
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            RowCount = 0;
            int StartIndex = 0;
            int EndIndex = 0;
            if (IsPage)
            {
                StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
            }           
            try
            {
                StringBuilder sbSql4org = new StringBuilder();
                sbSql4org.Append(@"select rel.*,u.Name,u.LoginName from Sys_RoleOfUser rel inner join Sys_UserInfo u on rel.UniqueNo=u.UniqueNo where 1=1 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and u.Name like N'%'+@Name+'%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("RoleId") && !string.IsNullOrEmpty(ht["RoleId"].ToString()))
                {
                    sbSql4org.Append(" and rel.RoleId=@RoleId ");                    
                    pms.Add(new SqlParameter("@RoleId", ht["RoleId"].ToString()));
                }
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, "", StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;                
                return null;
            }
        }
        #endregion

        #region 删除角色用户关系数据       
        public bool DeleteUserRelation(string ids)
        {
            try
            {
                string sql = string.Empty;
                StringBuilder strFirst = new StringBuilder();
                List<SqlParameter> pms = new List<SqlParameter>();
                string[] idArray = ids.Split(',');
                foreach (string item in idArray)
                {
                    strFirst.Append("@id" + item.ToString() + ",");
                    pms.Add(new SqlParameter("@id" + item.ToString(), item));
                }
                sql = string.Format("DELETE FROM Sys_RoleOfUser WHERE Id in({0})",strFirst.ToString().TrimEnd(','));
                
                return SQLHelp.ExecuteNonQuery(sql, CommandType.Text, pms.ToArray()) > 0;
            }
            catch (Exception)
            {
                //写入日志
                //throw;
                return false;
            }
        }
        #endregion

        #region 设置角色成员
        public int SetRoleMember(string roleid,string uniqueNoStr)
        {
            int result = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@RoleId", roleid),
                                       new SqlParameter("@UniqueNoStr", uniqueNoStr)
                                   };
            object obj = SQLHelp.ExecuteScalar("SetRoleMember", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 获取部门成员
        public DataTable GetUserByRegisterOrg(string registerOrg)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            sql = @"select distinct u.UniqueNo,u.Name
                    ,(select STUFF((select ',' + CAST(r.Name AS NVARCHAR(MAX)) from Sys_RoleOfUser ru 
                      left join Sys_Role r on r.Id=ru.RoleId
                      where u.UniqueNo=ru.UniqueNo FOR xml path('')), 1, 1, '')) as RoleName
                     from Sys_UserInfo u 
                     where u.RegisterOrg=@RegisterOrg and u.IsEnable=1 ";
            pms.Add(new SqlParameter("@RegisterOrg", registerOrg));
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion        
    }
}
