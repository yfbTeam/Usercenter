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
    public partial class Edu_BookCatalogDal : BaseDal<Edu_BookCatalog>, IEdu_BookCatalogDal
    {
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();
                str.Append(@"select * from Edu_BookCatalog where 1=1 ");
                int StartIndex = 0;
                int EndIndex = 0;

                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }

                if (ht.ContainsKey("BookID") && !string.IsNullOrEmpty(ht["BookID"].SafeToString()))
                {
                    str.Append(" and BookID = " + ht["BookID"].SafeToString());
                }
                if (ht.ContainsKey("Pid") && !string.IsNullOrEmpty(ht["Pid"].SafeToString()))
                {
                    str.Append(" and Pid = " + ht["Pid"].SafeToString());
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and Name like '%" + ht["Name"].SafeToString() + "%'");
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where," ID", StartIndex,
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
