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
    public partial class Sys_SysOfEntity_RelDal : BaseDal<Sys_SysOfEntity_Rel>, ISys_SysOfEntity_RelDal
    {
        #region 获取系统账户的实体信息        
        public DataTable GetEntityByAccountNo(string accountNo,string entityName)
        {
            StringBuilder sbSql4org;
            List<SqlParameter> pms = new List<SqlParameter>();
            sbSql4org = new StringBuilder();
            sbSql4org.Append(@"select entity.*,isnull(serel.Id,0) as EntityRelId,isnull(serel.FieldsEng,'') as Rel_FieldsEng,@AccountNo as AccountNo
                             ,isnull((select STUFF((select ','+CAST(isnull(fch.value,feng.value) AS NVARCHAR(MAX)) from func_split(entity.FieldsChina,',') fch
                             left join (select * from func_split(entity.FieldsEng,',')) feng on fch.row=feng.row
                             where feng.value in(select value from func_split(serel.FieldsEng,','))FOR xml path('')), 1, 1, '')),'') as Rel_FieldsChina
                             FROM View_AllEntity entity 
                             left join Sys_SysOfEntity_Rel serel on entity.EntityName=serel.EntityName and AccountNo=@AccountNo 
                             where 1=1 ");
            pms.Add(new SqlParameter("@AccountNo", accountNo));
            if (!string.IsNullOrEmpty(entityName))
            {
                sbSql4org.Append(" and entity.EntityName=@EntityName ");
                pms.Add(new SqlParameter("@EntityName", entityName));
            }
            return SQLHelp.ExecuteDataTable(sbSql4org.ToString(), CommandType.Text, pms.ToArray());
        }
        #endregion

        #region 实体权限配置
        public int SetEntityPermission(Sys_SysOfEntity_Rel entity)
        {
            int result = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@AccountNo", entity.AccountNo),
                                       new SqlParameter("@EntityNameStr", entity.EntityName)
                                   };
            object obj = SQLHelp.ExecuteScalar("SetEntityPermission", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion

        #region 实体字段配置
        public int EditEntityRel(Sys_SysOfEntity_Rel entity)
        {
            int result = 0;
            SqlParameter[] param = {
                                       new SqlParameter("@AccountNo", entity.AccountNo),
                                       new SqlParameter("@EntityName", entity.EntityName),
                                       new SqlParameter("@FieldsEng", entity.FieldsEng)
                                   };
            object obj = SQLHelp.ExecuteScalar("EditEntityRel", CommandType.StoredProcedure, param);
            result = Convert.ToInt32(obj);
            return result;
        }
        #endregion
    }
}
