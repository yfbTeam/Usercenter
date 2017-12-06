using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSIDAL;
using UCSModel;
using UCSUtility;

namespace UCSDAL
{
    public partial class Edu_SubJectDal : BaseDal<Edu_SubJect>, IEdu_SubJectDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select a.*,b.Name as MajorName,c.Name as SubjectName from Edu_Major_Sub_Rel a inner join Edu_MajorInfo b on a.MajorID=b.Id inner join Edu_SubJect c on a.SubID=c.Id ");
                int StartIndex = 0;
                int EndIndex = 0;

                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and c.Name like '%" + ht["Name"].SafeToString() + "%'");
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
    }
}
