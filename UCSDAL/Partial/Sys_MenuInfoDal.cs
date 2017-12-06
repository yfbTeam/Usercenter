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
    public partial class Sys_MenuInfoDal : BaseDal<Sys_MenuInfo>, ISys_MenuInfoDal
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
                bool isroleid = ht.ContainsKey("RoleId");
                StringBuilder sbSql4org = new StringBuilder();
                sbSql4org.Append(@"select mi.*,(select count(1) from Sys_MenuInfo where Pid=mi.Id) as ChildCount ");
                if (isroleid)
                {
                    sbSql4org.Append(@" ,ISNULL(rm.MenuId,0) as ischeck,
                               (select STUFF((select ','+CAST(ISNULL(relbtn.MenuId,0)AS NVARCHAR(MAX))+'|'+CAST(mbtn.Id AS NVARCHAR(MAX))+'|'+ CAST(Name AS NVARCHAR(MAX)) from Sys_MenuInfo mbtn
                                left join Sys_RoleOfMenu relbtn on mbtn.Id = relbtn.MenuId and relbtn.RoleId=@RoleId where Pid = mi.Id and IsMenu = 1 ORDER By Name FOR xml path('')), 1, 1, '')) as ButtonField ");
                    pms.Add(new SqlParameter("@RoleId", ht["RoleId"].ToString()));
                }
                sbSql4org.Append(@" from Sys_MenuInfo mi ");
                if (isroleid)
                {
                    sbSql4org.Append(" left join Sys_RoleOfMenu rm on mi.Id=rm.MenuId and rm.RoleId=@RoleId ");
                }
                sbSql4org.Append(@" where 1=1 and mi.IsShow!=0 ");
                if (ht.ContainsKey("Pid") && !string.IsNullOrEmpty(ht["Pid"].ToString()))
                {
                    sbSql4org.Append(" and mi.Pid=@Pid ");
                    pms.Add(new SqlParameter("@Pid", ht["Pid"].ToString()));
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].ToString()))
                {
                    sbSql4org.Append(" and mi.Name like N'%' + @Name + '%' ");
                    pms.Add(new SqlParameter("@Name", ht["Name"].ToString()));
                }
                if (ht.ContainsKey("IsMenu") && !string.IsNullOrEmpty(ht["IsMenu"].ToString()))
                {
                    sbSql4org.Append(" and mi.IsMenu=@IsMenu ");
                    pms.Add(new SqlParameter("@IsMenu", ht["IsMenu"].ToString()));
                }
                if (ht.ContainsKey("IsShow") && !string.IsNullOrEmpty(ht["IsShow"].ToString()))
                {
                    sbSql4org.Append(" and mi.IsShow=@IsShow ");
                    pms.Add(new SqlParameter("@IsShow", ht["IsShow"].ToString()));
                }
                return SQLHelp.GetListByPage("(" + sbSql4org.ToString() + ")", Where, "Sort", StartIndex, EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                //写入日志
                //throw;
                return null;
            }
        }

        #region 获得导航处菜单信息
        /// <summary>
        /// 获得导航处菜单信息
        /// </summary>
        /// <param name="uniqueNo">用户唯一号</param>
        /// <param name="pid">菜单Pid</param>
        /// <param name="isAllLeaf">是否查找pid下的所有子菜单</param>
        /// <returns></returns>
        public DataTable GetNavigationMenu(string uniqueNo, string pid, bool isAllLeaf)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            if (uniqueNo == "00000000000000000X")//如果是默认最大权限的管理员
            {
                sql = @"select " + (isAllLeaf ? "" : " distinct ") + @" mi.* from Sys_MenuInfo mi where mi.IsMenu=0 and mi.isShow=3 " + (isAllLeaf ? "" : " and mi.Pid=@Pid ");
            }
            else
            {
                sql = @"select " + (isAllLeaf ? "" : " distinct ") + @"mi.* from Sys_RoleOfUser ru
                                inner join Sys_RoleOfMenu rm on ru.RoleId=rm.RoleId   
                                inner join Sys_MenuInfo mi on mi.Id=rm.MenuId                                                              
                                where mi.Id is not null and mi.IsMenu=0 and mi.isShow=3  and ru.UniqueNo=@UniqueNo " + (isAllLeaf ? "" : " and mi.Pid=@Pid ");
            }
            if (isAllLeaf)
            {
                sql = @"WITH r AS (
                        SELECT * FROM (" + sql + @"  and mi.Pid=@Pid ) as menu1  
                        UNION ALL
                        SELECT menu2.* FROM (" + sql + @") menu2 JOIN r ON menu2.Pid=r.id 
                       )SELECT distinct *,(select count(1) from Sys_MenuInfo where Pid=r.Id and IsMenu=0 and isShow=3) as ChildCount FROM r ORDER BY Sort";
            }
            else
            {
                sql += " order by mi.Sort ";
            }
            pms.Add(new SqlParameter("@UniqueNo", uniqueNo));
            pms.Add(new SqlParameter("@Pid", pid));
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 根据url查找父级
        /// <summary>
        /// 根据url查找父级
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="isMaxPar">true：查找最大的父级  false：查找所有父级</param>
        /// <returns></returns>
        public DataTable GetParentMenuByUrl(string url, bool isMaxPar)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            sql = @"with pmenu
                        as
                        (
                        select * from Sys_MenuInfo where Url=@Url
                        union all
                        select b.* 
                        from pmenu a ,Sys_MenuInfo b where a.Pid = b.Id
                        )select * from pmenu where 1=1 ";
            if (isMaxPar)
            {
                sql += " and Pid=0 ";
            }
            sql += " order by Sort ";
            pms.Add(new SqlParameter("@Url", url));
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 根据pid和用户唯一号查找菜单
        /// <summary>
        /// 根据pid和用户唯一号查找菜单
        /// </summary>  
        public DataTable GetMenuByPidAndUniqueNo(string uniqueNo, string pid)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select mi.*,ISNULL(um.MenuId,0) as IsOwner from Sys_MenuInfo mi                     
                     left join
					 ( select distinct rm.MenuId from Sys_RoleOfUser ru
                    inner join Sys_RoleOfMenu rm on ru.RoleId=rm.RoleId 
					inner join Sys_Role pr on pr.Id=rm.RoleId
					where ru.UniqueNo=@UniqueNo  ) um on mi.Id=um.MenuId
                    where 1=1 ");
            if (!string.IsNullOrEmpty(pid))
            {
                sbSql4org.Append(" and mi.Pid=@Pid ");
                pms.Add(new SqlParameter("@Pid", pid));
            }
            sbSql4org.Append(" order by mi.Id ");
            pms.Add(new SqlParameter("@UniqueNo", uniqueNo));
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 新建菜单
        /// <summary>
        /// 新建菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditMenu(Sys_MenuInfo model)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = {
                                new SqlParameter("@Id",model.Id),
                                new SqlParameter("@Name",model.Name),
                                new SqlParameter("@Pid",model.Pid),
                                new SqlParameter("@Url",model.Url),
                                new SqlParameter("@Description",model.Description),
                                new SqlParameter("@IsMenu",model.IsMenu),
                                new SqlParameter("@IsShow",model.IsShow),
                                new SqlParameter("@MenuCode",model.MenuCode)
                                };
                object obj = SQLHelp.ExecuteScalar("EditMenu", CommandType.StoredProcedure, param);
                result = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return result;
        }
        #endregion

        #region 删除菜单
        public int DeleteMenuInfo(int menuid)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = {
                                new SqlParameter("@MenuId",menuid)
                                };
                object obj = SQLHelp.ExecuteScalar("DeleteMenu", CommandType.StoredProcedure, param);
                result = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return result;
        }
        #endregion

        #region 根据url查找子级button
        public DataTable GetSubButtonByUrl(string purl, string uniqueNo, string menuCode)
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            if (uniqueNo == "00000000000000000X")//如果是默认最大权限的管理员
            {
                sql = @"select distinct mi.*
                        ,(select STUFF((select ','+CAST(ISNULL(mbtn.Id,0)AS NVARCHAR(MAX))+'|'+ CAST(Name AS NVARCHAR(MAX))+'|'+ CAST(MenuCode AS NVARCHAR(MAX)) from Sys_MenuInfo mbtn
                        where Pid = mi.Id and IsMenu = 1 ORDER By Name FOR xml path('')), 1, 1, '')) as ButtonField 
                        from Sys_MenuInfo mi 
                        where 1=1 and mi.IsShow!=0 and mi.Url like '%'+@Url ";
            }
            else
            {
                sql = @"select distinct mi.*
                        ,(select STUFF((select ','+CAST(ISNULL(btnrm.MenuId,0)AS NVARCHAR(MAX))+'|'+ CAST(Name AS NVARCHAR(MAX))+'|'+ CAST(MenuCode AS NVARCHAR(MAX)) from Sys_RoleOfUser btnru
                        inner join Sys_RoleOfMenu btnrm on btnru.RoleId=btnrm.RoleId   
                        inner join Sys_MenuInfo btnmi on btnmi.Id=btnrm.MenuId   
                        where Pid = mi.Id and IsMenu = 1 and btnru.UniqueNo=@UniqueNo ORDER By Name FOR xml path('')), 1, 1, '')) as ButtonField 
                        from Sys_RoleOfUser ru
                        inner join Sys_RoleOfMenu rm on ru.RoleId=rm.RoleId   
                        inner join Sys_MenuInfo mi on mi.Id=rm.MenuId                                                              
                        where mi.Id is not null and mi.IsShow!=0 and ru.UniqueNo=@UniqueNo and mi.Url like '%'+@Url ";
            }
            if (!string.IsNullOrEmpty(menuCode))
            {
                sql += "  and mi.MenuCode=@MenuCode ";
            }
            pms.Add(new SqlParameter("@UniqueNo", uniqueNo));
            pms.Add(new SqlParameter("@Url",  purl));
            pms.Add(new SqlParameter("@MenuCode", menuCode));
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion
    }
}
