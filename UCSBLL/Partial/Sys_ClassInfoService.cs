using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSDAL;
using UCSIBLL;
using UCSModel;

namespace UCSBLL
{
    public partial class Sys_ClassInfoService : BaseService<Sys_ClassInfo>, ISys_ClassInfoService
    {
        Sys_ClassInfoDal dal = new Sys_ClassInfoDal();
        #region 添加班级
        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel AddClass(Sys_ClassInfo model, int GradeId, int AcademicId)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.AddClass(model,GradeId,AcademicId);
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
        #region 修改年级
        /// <summary>
        /// 修改年级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel EditClass(Sys_ClassInfo model, int OldSectionID, int OldGradeID, int AcademicId, int GradeId)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.EditClass(model, OldSectionID, OldGradeID,AcademicId,GradeId);
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
        #region 删除班级
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public JsonModel DelClass(string ID)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.DelClass(ID);
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
