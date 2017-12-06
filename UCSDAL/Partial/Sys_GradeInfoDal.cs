using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSIDAL;
using UCSModel;
using UCSUtility;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace UCSDAL
{
    public partial class Sys_GradeInfoDal : BaseDal<Sys_GradeInfo>, ISys_GradeInfoDal
    {
        private string GetCurrentTerm()
        {
            string strSql = "select Id from Sys_StudySection where StartDate<getdate() and EndDate>getdate()";
            object Obj = SQLHelp.ExecuteScalar(strSql, CommandType.Text, null);
            return Obj.ToString();
        }
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select a.*,b.Academic from Sys_GradeInfo a inner join Sys_StudySection b on a.AcademicId=b.id where 1=1");

                int StartIndex = 0;
                int EndIndex = 0;

                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and a.ID = " + ht["ID"].SafeToString());
                }
                if (ht.ContainsKey("Major") && !string.IsNullOrEmpty(ht["Major"].SafeToString()))
                {
                    str.Append(" and a.MajorID = " + ht["Major"].SafeToString());
                }
                if (ht.ContainsKey("AcademicId") && !string.IsNullOrEmpty(ht["AcademicId"].SafeToString()))
                {
                    str.Append(" and a.AcademicId = " + ht["AcademicId"].SafeToString());
                }
                else
                {
                    str.Append(" and a.AcademicId = " + GetCurrentTerm());
                }
                if (ht.ContainsKey("GradeName") && !string.IsNullOrEmpty(ht["GradeName"].SafeToString()))
                {
                    str.Append(" and a.GradeName like '%" + ht["GradeName"].SafeToString() + "%'");
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
        public DataTable GetGradClass(string Pid, string AcademicId)
        {
            if (AcademicId == "")
            {
                AcademicId = GetCurrentTerm();
            }
            string strSql = "";
            if (Pid == "0")
            {
                strSql = "select Id,GradeName as Name,0 as Pid,'' as HeadteacherNO from Sys_GradeInfo where AcademicId=" + AcademicId;
               
            }
            else
            {
                strSql = "select a.classNo as Id,ClassName as Name,b.GradeNum as Pid,HeadteacherNO from Sys_ClassInfo a,Grad_Class_rel b where a.classNo=b.classNo and b.GradeID=" + Pid;
            }
            
            DataTable dt = SQLHelp.ExecuteDataTable(strSql, CommandType.Text, null);
            return dt;
        }
      
        #region 删除班级
        /// <summary>
        /// 删除班级
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string DelGrade(string id)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@ID",id),
                                };
                object obj = SQLHelp.ExecuteScalar("DelGrade", CommandType.StoredProcedure, pa);
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
