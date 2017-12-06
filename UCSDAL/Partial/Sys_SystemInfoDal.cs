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
    public partial class Sys_SystemInfoDal : BaseDal<Sys_SystemInfo>, ISys_SystemInfoDal
    {
        #region 修改系统账号
        /// <summary>
        /// 修改系统账号
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int EditSystemInfo(Sys_SystemInfo model)
        {
            int result = 0;
            try
            {
                SqlParameter[] param = {
                                new SqlParameter("@Id",model.Id),
                                new SqlParameter("@Name",model.Name),
                                new SqlParameter("@AccountNo",model.AccountNo),
                                new SqlParameter("@EditUID",model.EditUID)
                                };
                object obj = SQLHelp.ExecuteScalar("EditSystemInfo", CommandType.StoredProcedure, param);
                result = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return result;
        }
        #endregion
    }
}
