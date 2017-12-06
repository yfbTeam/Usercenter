using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSIDAL;
using UCSModel;
using System.Data;
using System.Collections;
using UCSUtility;
using System.Data.SqlClient;

namespace UCSDAL
{
    public partial class Sys_ClassInfoDal : BaseDal<Sys_ClassInfo>, ISys_ClassInfoDal
    {
        Sys_UserInfoDal dal = new Sys_UserInfoDal();
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select a.*,b.Academic,c.GradeName,r.SectionID,c.ID as GID,uinfo.Name as TeaName from Sys_ClassInfo a inner join Grad_Class_rel R on a.ClassNO=R.ClassNo
 inner join Sys_StudySection b on R.SectionID=b.Id inner join Sys_GradeInfo c
on R.GradeID=c.ID left join Sys_UserInfo uinfo on a.HeadteacherNO=uinfo.UniqueNo where 1=1");
                int StartIndex = 0;
                int EndIndex = 0;
                string AcademicId = "";
                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and a.ID = " + ht["ID"].SafeToString());
                }  
                if (ht.ContainsKey("AcademicId") && !string.IsNullOrEmpty(ht["AcademicId"].SafeToString()))
                {
                    AcademicId = ht["AcademicId"].SafeToString();
                }
                else
                    AcademicId = dal.GetCurrentTerm();
                str.Append(" and R.SectionID = " + AcademicId + " and c.AcademicId = " + AcademicId);

                if (ht.ContainsKey("ClassName") && !string.IsNullOrEmpty(ht["ClassName"].SafeToString()))
                {
                    str.Append(" and a.ClassName like '%" + ht["ClassName"].SafeToString() + "%'");
                }
                if (ht.ContainsKey("HeadteacherNO") && !string.IsNullOrEmpty(ht["HeadteacherNO"].SafeToString()))
                {
                    str.Append(" and HeadteacherNO like '%" + ht["HeadteacherNO"].SafeToString() + "%'");
                }
                
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex,
                    EndIndex, IsPage, null, out RowCount);

            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #region 添加班级
        /// <summary>
        /// 添加班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddClass(Sys_ClassInfo model, int GradeId, int AcademicId)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@ClassName",model.ClassName),
                                        new SqlParameter("@ClassNO",model.ClassNO),
                                        new SqlParameter("@HeadteacherNO",model.HeadteacherNO),
                                        new SqlParameter("@MonitorNO",model.MonitorNO),
                                        new SqlParameter("@CreateUID",model.CreateUID),
                                        new SqlParameter("@GradeId",GradeId),
                                        new SqlParameter("@AcademicId",AcademicId),
                                       
                                };
                object obj = SQLHelp.ExecuteScalar("AddClass", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion
        #region 修改班级
        /// <summary>
        /// 修改班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditClass(Sys_ClassInfo model, int OldSectionID, int OldGradeID, int AcademicId, int GradeId)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = {   new SqlParameter("@ID",model.Id),
                                        new SqlParameter("@AcademicId",AcademicId),
                                        new SqlParameter("@ClassName",model.ClassName),
                                        new SqlParameter("@ClassNO",model.ClassNO),
                                        new SqlParameter("@HeadteacherNO",model.HeadteacherNO),
                                        new SqlParameter("@GradeId",GradeId),
                                        new SqlParameter("@OldSectionID",OldSectionID),
                                        new SqlParameter("@OldGradeID",OldGradeID)
                                };
                object obj = SQLHelp.ExecuteScalar("EditClass", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion
        #region 删除班级
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DelClass(string id)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@ID",id),
                                };
                object obj = SQLHelp.ExecuteScalar("DelClass", CommandType.StoredProcedure, pa);
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
