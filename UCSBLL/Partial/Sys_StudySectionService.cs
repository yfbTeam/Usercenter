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
    public partial class Sys_StudySectionService : BaseService<Sys_StudySection>, ISys_StudySectionService
    {
        Sys_StudySectionDal dal = new Sys_StudySectionDal();

        #region 添加学期
        /// <summary>
        /// 添加学期
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Small"></param>
        /// <param name="SmallL"></param>
        /// <param name="Center"></param>
        /// <param name="CenterL"></param>
        /// <param name="High"></param>
        /// <param name="HighL"></param>
        /// <returns></returns>
        public JsonModel AddSection(Sys_StudySection model, string Small, string SmallL, string Center, string CenterL, string High, string HighL)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.AddSection(model, Small, SmallL, Center, CenterL, High, HighL);
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

        #region 删除学期
        /// <summary>
        /// 删除学期
        /// </summary>
        /// <param name="id">学期ID</param>
        /// <returns></returns>
        public JsonModel DelSection(string id)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.DelSection(id);
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

        #region 复制学期
        /// <summary>
        /// 复制学期
        /// </summary>
        /// <param name="model"></param>
        /// <param name="IsUp"></param>
        /// <returns></returns>
        public JsonModel CopySection(Sys_StudySection model, int IsUp)
        {
            JsonModel jsonModel = new JsonModel();
            string result = dal.CopySection(model, IsUp);
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

        #region 获取每学年学期学生人数        
        public JsonModel GetSectionStudentData()
        {
            return GetJsonModelByDataTable(dal.GetSectionStudentData());
        }
        #endregion
    }
}
