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
    public partial class Sys_GradeInfoService : BaseService<Sys_GradeInfo>, ISys_GradeInfoService
    {
        Sys_GradeInfoDal dal = new Sys_GradeInfoDal();
        public DataTable GetGradClass(string Pid, string AcademicId)
        {
            return dal.GetGradClass(Pid, AcademicId);
        }

        #region 删除年级
        /// <summary>
        /// 删除年级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel DelGrade(string ID)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.DelGrade(ID);
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
