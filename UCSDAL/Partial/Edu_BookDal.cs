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
    public partial class Edu_BookDal : BaseDal<Edu_Book>, IEdu_BookDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select a.*,b.Name as VersionName,c.Name as SubName,d.Name as MajorName from Edu_Book a inner join Edu_BookVersion b on a.VersionID=b.ID inner join Edu_SubJect c on a.SubID=c.ID inner join Edu_MajorInfo d on d.ID=a.MajorID where 1=1");
                int StartIndex = 0;
                int EndIndex = 0;

                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and a.Name like '%" + ht["Name"].SafeToString() + "%'");
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
