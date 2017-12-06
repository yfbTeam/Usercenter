using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSDAL;
using UCSIBLL;
using UCSModel;
using UCSUtility;

namespace UCSBLL
{
    public partial class Sys_LogInfoService : BaseService<Sys_LogInfo>, ISys_LogInfoService
    {
        Sys_LogInfoDal dal = new Sys_LogInfoDal();
        #region 记录操作日志
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="accountNo">账号</param>
        /// <param name="loginName">用户登录名</param>
        /// <param name="operation">操作内容</param>
        /// <param name="logType">日志类型(0登录日志；1本地日志；2 接口日志)</param>
        /// <param name="remarks">备注</param>
        /// <param name="OperationObj">对象表</param>
        /// <param name="OperationUniqueID">对象唯一值</param>
        /// <param name="OperationMsg">详细操作内容</param>
        public void WriteLog(string accountNo, string loginName, string operation, string logType = "2", string remarks = "", string OperationObj = "", string OperationUniqueID = "", string OperationMsg = "")
        {
            Sys_LogInfo log = new Sys_LogInfo();
            log.AccountNo = accountNo;
            log.LoginName = loginName;
            log.IP = IPHelper.GetIPAddress();          
            log.Operation = operation;
            log.LogType = Convert.ToByte(logType);
            log.CreateTime = DateTime.Now;
            log.Remarks = remarks;
            log.OperationObj = OperationObj;
            log.OperationUniqueID =OperationUniqueID;
            log.OperationMsg = OperationMsg;
            base.CurrentDal.Add(log);
        }
        #endregion
    }
}
