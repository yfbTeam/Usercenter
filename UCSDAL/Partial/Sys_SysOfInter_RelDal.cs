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
    public partial class Sys_SysOfInter_RelDal : BaseDal<Sys_SysOfInter_Rel>, ISys_SysOfInter_RelDal
    {
        #region 获取系统账户的接口信息        
        public DataTable GetInterfaceByAccountNo(string accountNo)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select ROW_NUMBER() OVER (order by inter.Id desc)AS rowNum, inter.*,isnull(rel.Id,0) as InterfaceId,@AccountNo as AccountNo
                              from Sys_Interface inter
                              left join Sys_SysOfInter_Rel rel on inter.Id=rel.InterfaceId and rel.AccountNo=@AccountNo 
                              where inter.IsDelete=0 ");
            pms.Add(new SqlParameter("@AccountNo", accountNo));
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 接口权限配置
        public int SetInterfacePermission(string accountNo, string interidStr)
        {
            int result = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@AccountNo", accountNo),
                                       new SqlParameter("@InteridStr", interidStr)
                                   };
            object obj = SQLHelp.ExecuteScalar("SetInterfacePermission", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 判断是否有访问系统方法的权限        
        public string IsHasInterAuth(string accountNo, string intername)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select count(1) from Sys_SysOfInter_Rel rel inner join Sys_Interface inter on rel.InterfaceId=inter.Id
                             where rel.AccountNo=@AccountNo and inter.Name=@InterName ");
            pms.Add(new SqlParameter("@AccountNo", accountNo));
            pms.Add(new SqlParameter("@InterName", intername));
            object obj = SQLHelp.ExecuteScalar(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
            return obj.ToString(); 
        }
        #endregion
    }
}
