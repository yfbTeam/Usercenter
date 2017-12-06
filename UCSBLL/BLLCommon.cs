using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UCSDAL;
using UCSModel;
using UCSUtility;

namespace UCSBLL
{
    public class BLLCommon
    {
        #region 判断是否有访问系统方法的权限并返回字段
        /// <summary>
        /// 判断是否有访问系统方法的权限并返回字段
        /// </summary>
        /// <param name="syskey"></param>
        /// <param name="indenkey"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public JsonModel IsHasInterAuth(string accountNo, string intername)
        {
            JsonModel jsonModel = null;
            try
            {
                if (string.IsNullOrEmpty(accountNo) || string.IsNullOrEmpty(intername))
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 3,
                        errMsg = "loss",
                        retData = ""
                    };
                    return jsonModel;
                }                
                string rtnresult = new Sys_SysOfInter_RelDal().IsHasInterAuth(accountNo, intername);
                if (rtnresult=="0")
                {
                    jsonModel = new JsonModel()
                    {
                        errNum = 2,
                        errMsg = "noauth",
                        retData = ""
                    };
                    return jsonModel;
                }
                jsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "success",
                    retData = rtnresult
                };
                return jsonModel;
            }
            catch (Exception ex)
            {
                jsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                return jsonModel;
            }
        }
        #endregion
        /// <summary>
        /// 根据第几页、每页条数增加起始条数、结束条数
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public Hashtable AddStartEndIndex(Hashtable ht)
        {
            try
            {
                int PageIndex = Convert.ToInt32(ht["PageIndex"]);
                int PageSize = Convert.ToInt32(ht["PageSize"]);
                ht.Add("StartIndex", (((PageIndex - 1) * PageSize) + 1).ToString());
                ht.Add("EndIndex", (PageIndex * PageSize).ToString());
            }
            catch (Exception)
            {

                throw;
            }
            return ht;
        }

        /// <summary>
        /// DataTable转换成Json格式
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>        
        /// <returns>Json字符串</returns>
        public string DataTableToJson(DataTable dt)
        {
            if (dt == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            sb.Append("{\"");
            sb.Append(dt.TableName);
            sb.Append("\":[");
            foreach (DataRow r in dt.Rows)
            {
                sb.Append("{");
                foreach (DataColumn c in dt.Columns)
                {
                    sb.Append("\"");
                    sb.Append(c.ColumnName);
                    sb.Append("\":\"");
                    sb.Append(r[c].ToString().Replace("\\", "//"));
                    sb.Append("\",");
                }
                sb.Remove(sb.Length - 1, 1);
                sb.Append("},");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append("]}");
            return sb.ToString();
        }

        /// <summary>
        /// DataTable转换成List
        /// </summary>
        /// <param name="dt">要转换的DataTable</param>        
        /// <returns>List<Dictionary<string, object>></returns>
        public List<Dictionary<string, object>> DataTableToList(DataTable dt)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return list;
        }

        /// <summary>
        /// JSON序列化
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary>
        /// JSON反序列化
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式
            string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
            MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
            Regex reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        /// <summary>
        /// 将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }

        /// <summary>
        /// 无验证-不分页
        /// </summary>
        /// <param name="ht"></param>
        /// <returns></returns>
        public JsonModel GetData_NoVerification(Hashtable ht)
        {
            BLLCommon com = new BLLCommon();
            JsonModel JsonModel;
            try
            {
                string SQL = " select " + ht["Columns"].ToString() + " from " + ht["TableName"].ToString() + " where 1=1 ";
                if (ht.Contains("Where") && !string.IsNullOrWhiteSpace(ht["Where"].ToString()))
                {
                    SQL += ht["Where"].ToString();
                }
                if (ht.Contains("Order") && !string.IsNullOrWhiteSpace(ht["Order"].ToString()))
                {
                    SQL += " order by " + ht["Order"].ToString();
                }
                DataTable dt = SQLHelp.ExecuteDataTable(SQL, CommandType.Text);
                if (dt == null)
                {
                    JsonModel = new JsonModel()
                    {
                        errNum = 1,
                        errMsg = "失败",
                        retData = ""
                    };
                    LogService.WriteErrorLog("DataTable为NULL");
                    return JsonModel;
                }
                if (dt.Rows.Count == 0)
                {
                    JsonModel = new JsonModel()
                    {
                        errNum = 999,
                        errMsg = "无数据",
                        retData = ""
                    };
                    return JsonModel;
                }
                JsonModel = new JsonModel()
                {
                    errNum = 0,
                    errMsg = "成功",
                    retData = com.DataTableToList(dt)
                };
                return JsonModel;
            }
            catch (Exception ex)
            {

                JsonModel = new JsonModel()
                {
                    errNum = 400,
                    errMsg = ex.Message,
                    retData = ""
                };
                LogService.WriteErrorLog(ex.Message);
                return JsonModel;
            }
        }
    }
}
