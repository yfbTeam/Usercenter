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
    public partial class Sys_RoleDal : BaseDal<Sys_Role>, ISys_RoleDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
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
                sbSql4org.Append(@" select srole.*,isnull(urel.UserCount,0) as UserCount,
                        (select STUFF((select ';' + CAST(su.Name+'('+su.LoginName+')' AS NVARCHAR(MAX)) from Sys_UserInfo su 
                        left join Sys_RoleOfUser rel on su.UniqueNo=rel.UniqueNo where rel.RoleId=srole.Id FOR xml path('')), 1, 1, '')) as UserNames
                        from Sys_Role srole
                        left join (select RoleId,count(1) as UserCount from Sys_RoleOfUser group by RoleId)urel on srole.Id=urel.RoleId ");
                sbSql4org.Append(@" where 1=1 ");
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and srole.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("IsDelete") && !string.IsNullOrEmpty(ht["IsDelete"].ToString()))
                {
                    sbSql4org.Append(" and srole.IsDelete=@IsDelete ");
                    pms.Add(new SqlParameter("@IsDelete", ht["IsDelete"].ToString()));
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

        #region 获取全部角色，返回DataTable
        /// <summary>
        /// 获取全部角色，返回DataTable
        /// </summary>
        public DataTable GetAllRoleList()
        {
            StringBuilder sbSql4org;
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select distinct * from Sys_Role where IsDelete=0 ");
            sbSql4org.Append(" order by Id desc ");
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, null);
        }
        #endregion

        #region 获取某用户的角色信息
        /// <summary>
        /// 获取某用户的角色信息
        /// </summary>
        public DataTable GetRoleByUser(Hashtable ht)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select distinct sys_role.* 
                                from Sys_RoleOfUser rel
                                inner join Sys_Role sys_role on rel.RoleId=sys_role.Id and sys_role.IsDelete=0
                                where 1=1 ");
            if (ht.ContainsKey("UniqueNo") && !string.IsNullOrEmpty(ht["UniqueNo"].ToString()))
            {
                sbSql4org.Append(" and rel.UniqueNo=@UniqueNo  ");
                pms.Add(new SqlParameter("@UniqueNo", ht["UniqueNo"].ToString()));
            }
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 删除角色并删除该角色的相关数据
        public int DeleteRole(int roleid)
        {
            int result = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@RoleId", roleid)
                                   };
            object obj = SQLHelp.ExecuteScalar("DeleteRole", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 编辑角色及菜单
        public int EditRole(Sys_Role model, string menuids)
        {
            int result = 0;
            SqlParameter[] param = {
                                        new SqlParameter("@Name", model.Name),
                                        new SqlParameter("@CreateUID", model.CreateUID??""),
                                        new SqlParameter("@EditUID", model.EditUID??""),
                                        new SqlParameter("@RoleId", model.Id),
                                        new SqlParameter("@Menuids", menuids)
            };
            object obj = SQLHelp.ExecuteScalar("EditRole", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
