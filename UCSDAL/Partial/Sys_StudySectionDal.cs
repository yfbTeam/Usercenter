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
    public partial class Sys_StudySectionDal : BaseDal<Sys_StudySection>, ISys_StudySectionDal
    {

        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select * from Sys_StudySection where 1=1");

                int StartIndex = 0;
                int EndIndex = 0;

                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and ID = " + ht["ID"].SafeToString());
                }
                if (ht.ContainsKey("Status") && !string.IsNullOrEmpty(ht["Status"].SafeToString()))
                {
                    str.Append(" and IsDelete = " + ht["Status"].SafeToString());
                }
                
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex,
                    EndIndex, IsPage, null, out RowCount);
                dt.Columns.Add("PeriodName");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["PeriodName"] = dt.Rows[i]["PeriodIDs"].SafeToString().Replace("1", "小学").Replace("2", "初中").Replace("3", "高中");
                }
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }

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
        public string AddSection(Sys_StudySection model, string Small, string SmallL, string Center, string CenterL, string High, string HighL)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@Academic",model.Academic),
                                        new SqlParameter("@Semester",model.Semester),
                                        new SqlParameter("@StartDate",model.StartDate),
                                        new SqlParameter("@EndDate",model.EndDate),
                                        new SqlParameter("@IsDelete",model.IsDelete),
                                        new SqlParameter("@PeriodIDs",model.PeriodIDs),
                                        new SqlParameter("@Small",Small),
                                        new SqlParameter("@SmallL",SmallL),
                                        new SqlParameter("@Center",Center),
                                        new SqlParameter("@CenterL",CenterL),
                                        new SqlParameter("@High",High),
                                        new SqlParameter("@HighL",HighL)		
                                };
                object obj = SQLHelp.ExecuteScalar("AddSection", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 删除学期
        /// <summary>
        /// 删除学期
        /// </summary>
        /// <param name="id">学期ID</param>
        /// <returns></returns>
        public string DelSection(string id)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@SectionId",int.Parse(id))
                                };
                object obj = SQLHelp.ExecuteScalar("DelSection", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 复制学期
        /// <summary>
        /// 复制学期
        /// </summary>
        /// <param name="id">学期ID</param>
        /// <returns></returns>
        public string CopySection(Sys_StudySection model, int IsUp)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@OldSectionID",model.Id),
                                        new SqlParameter("@Academic",model.Academic),
                                        new SqlParameter("@Semester",model.Semester),
                                        new SqlParameter("@StartDate",model.StartDate),
                                        new SqlParameter("@EndDate",model.EndDate),
                                        new SqlParameter("@IsDelete",model.IsDelete),
                                        new SqlParameter("@IsUp",IsUp),
                                };
                object obj = SQLHelp.ExecuteScalar("CopySection", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 获取每学年学期学生人数
        public DataTable GetSectionStudentData()
        {
            List<SqlParameter> pms = new List<SqlParameter>();
            string sql = string.Empty;
            sql = @"select learn.Id,learn.Academic,stu.StuCount from Sys_StudySection learn
               left join (select SectionID,count(1) as StuCount from Grad_Class_rel group by SectionID)stu on learn.Id=stu.SectionID ";
            return SQLHelp.ExecuteDataTable(sql, CommandType.Text, pms.ToArray());
        }
        #endregion
    }
}
