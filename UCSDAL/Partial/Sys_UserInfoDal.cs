using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSIDAL;
using UCSModel;
using UCSUtility;
using System.Collections;
namespace UCSDAL
{
    public partial class Sys_UserInfoDal : BaseDal<Sys_UserInfo>, ISys_UserInfoDal
    {
        //学生姓名，身份证号，年级，学期，班级，班主任，班长
        //select d.Name,d.idcard,c.GradeName,b.Academic,a.className,a.Headteacherno,a.monitorno from Sys_ClassInfo a,Grad_Class_rel R,Sys_StudySection b,Sys_GradeInfo c,Sys_UserInfo d where a.ClassNO=R.ClassNo and R.SectionID=b.Id and R.GradeID=c.ID
        //and  d.registerorg=a.classno and  d.id=1154
        public DataTable IsPwdTure(string UserName, string PassWord)
        {
            string PhotoPre = ConfigHelper.GetConfigString("PhotoPre");

            string strSql = "select Id,UniqueNo,UserType,Name,Nickname,Sex,Phone,Birthday,LoginName,Password,IDCard,HeadPic,RegisterOrg,AuthenType,Address,Remarks,CreateUID,EditUID,IsEnable,IsDelete,CheckMsg,'" + PhotoPre + "' +HeadPic as AbsHeadPic from Sys_UserInfo where LoginName='" +
                UserName + "' and Password='" + PassWord + "'";
            DataTable dt = SQLHelp.ExecuteDataTable(strSql, CommandType.Text, null);
            return dt;
        }

        #region 分页获取用户信息
        /// <summary>
        /// 分页获取用户信息
        /// </summary>
        /// <param name="ht"></param>
        /// <param name="RowCount"></param>
        /// <param name="IsPage"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public override DataTable GetListByPage(Hashtable ht, out int RowCount, bool IsPage = true, string Where = "")
        {
            RowCount = 0;
            List<SqlParameter> pms = new List<SqlParameter>();
            DataTable dt = new DataTable();
            try
            {
                StringBuilder str = new StringBuilder();

                int StartIndex = 0;
                int EndIndex = 0;
                string PhotoPre = ConfigHelper.GetConfigString("PhotoPre");
                if (ht.ContainsKey("IsStu") && !string.IsNullOrEmpty(ht["IsStu"].SafeToString()))
                {
                    if (ht["IsStu"].SafeToString().ToUpper() == "FALSE")//老师
                    {
                        str.Append(@"select userInfo.Id,UniqueNo,UserType,userInfo.Name,userInfo.Email,Nickname,Sex,Phone,Birthday,LoginName,Password,IDCard,HeadPic,RegisterOrg,AuthenType,Address,Remarks,userInfo.CreateUID,userInfo.EditUID,IsEnable,userInfo.IsDelete,CheckMsg,'"
                            + PhotoPre + "' +HeadPic as AbsHeadPic,org.Name as OrgName,b.HeadteacherNO from Sys_UserInfo userInfo inner join Org_Mechanism org on userInfo.RegisterOrg=org.OrganNo left join Sys_ClassInfo b on userInfo.UniqueNo=b.HeadteacherNO where userInfo.UserType <>2");

                        if (ht.ContainsKey("OrgNo") && !string.IsNullOrEmpty(ht["OrgNo"].SafeToString()))
                        {
                            GetAllOrgNo(ht["OrgNo"].SafeToString());
                            string orgArry = "'" + sb.ToString().Replace(",", "','") + "'";
                            str.Append(" and userInfo.RegisterOrg in (" + orgArry + ")");
                        }
                        if (ht.ContainsKey("TeaNo") && !string.IsNullOrEmpty(ht["TeaNo"].SafeToString()))
                        {
                            str.Append(" and userInfo.RegisterOrg in (select OrganNo from Org_Mechanism where LegalUID='" + ht["TeaNo"] + "')");
                        }
                        if (ht.ContainsKey("HeadteacherNO") && !string.IsNullOrEmpty(ht["HeadteacherNO"].SafeToString()))
                        {
                            str.Append(" and  UniqueNo in (select distinct HeadteacherNO from Sys_ClassInfo)");
                        }
                    }
                    else//查询学生信息
                    {
                        str.Append(@"select userInfo.Id,UniqueNo,UserType,userInfo.Name,userInfo.Email,Nickname,Sex,Phone,Birthday,LoginName,Password,IDCard,HeadPic,RegisterOrg,AuthenType,Address,Remarks,userInfo.CreateUID,userInfo.EditUID,IsEnable,userInfo.IsDelete,CheckMsg,'" + PhotoPre +
                            "' +HeadPic as AbsHeadPic,org.ClassName as OrgName,grade.GradeName,b.HeadteacherNO from Sys_UserInfo userInfo inner join Sys_ClassInfo org on userInfo.RegisterOrg=org.ClassNo inner join  Grad_Class_rel rel on org.ClassNO=rel.ClassNO inner join Sys_GradeInfo grade on grade.ID=rel.GradeID left join Sys_ClassInfo b on userInfo.UniqueNo=b.HeadteacherNO  where 1=1");
                        str.Append(" and userInfo.UserType =2 ");

                        string AcademicId = ht["AcademicId"].SafeToString();
                        if (AcademicId == "0")
                        {
                            AcademicId = GetCurrentTerm();
                        }
                        if (ht.ContainsKey("OrgNo") && !string.IsNullOrEmpty(ht["OrgNo"].SafeToString()))
                        {
                            str.Append(" and userInfo.RegisterOrg in (select ClassNo from Grad_Class_rel where SectionID=" +
                                AcademicId + " and gradeid=" + ht["OrgNo"] + " union select '" + ht["OrgNo"] + "' as ClassNo)");
                        }
                        if (ht.ContainsKey("AcademicId") && !string.IsNullOrEmpty(ht["AcademicId"].SafeToString()))
                        {
                            str.Append("  and rel.SectionID=" + AcademicId + " and userInfo.RegisterOrg in (select ClassNo from Grad_Class_rel where SectionID=" + AcademicId + ")");
                        }
                        if (ht.ContainsKey("TeaNo") && !string.IsNullOrEmpty(ht["TeaNo"].SafeToString()))
                        {
                            str.Append(" and userInfo.RegisterOrg in (select ClassNo from Sys_ClassInfo where HeadteacherNO='" + ht["TeaNo"] + "')");
                        }
                    }

                }
                else
                {
                    str.Append(@"select userInfo.Id,UniqueNo,UserType,userInfo.Name,userInfo.Email,Nickname,Sex,Phone,Birthday,LoginName,Password,IDCard,HeadPic,RegisterOrg,AuthenType,Address,Remarks,userInfo.CreateUID,userInfo.EditUID,IsEnable,userInfo.IsDelete,CheckMsg,'"
                        + PhotoPre + "' +HeadPic as AbsHeadPic from Sys_UserInfo userInfo where 1=1");
                }
                if (ht.ContainsKey("ID") && !string.IsNullOrEmpty(ht["ID"].SafeToString()))
                {
                    str.Append(" and userInfo.ID = " + ht["ID"].SafeToString());
                }
                if (ht.ContainsKey("Status") && !string.IsNullOrEmpty(ht["Status"].SafeToString()))
                {
                    str.Append(" and userInfo.IsEnable=" + ht["Status"].SafeToString());
                }
                if (ht.ContainsKey("Name") && !string.IsNullOrEmpty(ht["Name"].SafeToString()))
                {
                    str.Append(" and userInfo.Name like '%" + ht["Name"].SafeToString() + "%'");
                }
                if (ht.ContainsKey("Phone") && !string.IsNullOrEmpty(ht["Phone"].SafeToString()))
                {
                    str.Append(" and userInfo.Phone = '" + ht["Phone"].SafeToString() + "'");
                }
                if (ht.ContainsKey("AuthenType") && !string.IsNullOrEmpty(ht["AuthenType"].SafeToString()))
                {
                    str.Append(" and userInfo.AuthenType in (" + ht["AuthenType"].SafeToString() + ")");
                }
                if (ht.ContainsKey("LoginName") && !string.IsNullOrWhiteSpace(ht["LoginName"].SafeToString()))
                {
                    str.Append(" and userInfo.LoginName='" + ht["LoginName"].SafeToString() + "'");
                }
                if (ht.ContainsKey("Password") && !string.IsNullOrWhiteSpace(ht["Password"].SafeToString()))
                {
                    str.Append(" and userInfo.Password='" + ht["Password"].SafeToString() + "'");
                }
                if (ht.ContainsKey("Key") && !string.IsNullOrWhiteSpace(ht["Key"].SafeToString()))
                {
                    str.Append(" and (userInfo.Name like '%" + ht["Key"].SafeToString() + "%' or userInfo.LoginName like '%" + ht["Key"].SafeToString() + "%')");
                }
                if (ht.ContainsKey("LoginName") && !string.IsNullOrEmpty(ht["LoginName"].SafeToString()))
                {
                    str.Append(" and userInfo.LoginName = '" + ht["LoginName"].SafeToString() + "'");
                }
                if (ht.ContainsKey("NoFeedBack") && !string.IsNullOrEmpty(ht["NoFeedBack"].SafeToString()))
                {
                    str.Append(" and userInfo.UniqueNo not in (select StuNo from FeedBack_StuList where Status=0)");
                }
                if (ht.ContainsKey("IDCard") && !string.IsNullOrWhiteSpace(ht["IDCard"].SafeToString()))
                {
                    StringBuilder strFirst = new StringBuilder();

                    string[] IDCards = ht["IDCard"].SafeToString().Split(',');
                    for (int i = 0; i < IDCards.Length; i++)
                    {
                        strFirst.Append("@IDCard" + i + ",");
                        pms.Add(new SqlParameter("@IDCard" + i, IDCards[i]));
                    }
                    str.Append(string.Format(" and userInfo.IDCard {1} in({0})", strFirst.ToString().TrimEnd(','), ht["JoinNoConn"].SafeToString()));

                    //str.Append(" and userInfo.IDCard='" + ht["IDCard"].SafeToString() + "'");
                }
                if (ht.ContainsKey("equalsName") && !string.IsNullOrWhiteSpace(ht["equalsName"].SafeToString()))
                {
                    str.Append(" and userInfo.Name = '" + ht["equalsName"].SafeToString() + "'");
                }
                if (ht.ContainsKey("KaNo") && !string.IsNullOrWhiteSpace(ht["KaNo"].SafeToString()))
                {
                    str.Append(" and userInfo.KaNo='" + ht["KaNo"].SafeToString() + "'");
                }

                if (ht.Contains("UniqueNo") && !string.IsNullOrWhiteSpace(ht["UniqueNo"].SafeToString()))
                {
                    StringBuilder strFirst = new StringBuilder();

                    string[] UniqueNos = ht["UniqueNo"].SafeToString().Split(',');
                    for (int i = 0; i < UniqueNos.Length; i++)
                    {
                        strFirst.Append("@UniqueNo" + i + ",");
                        pms.Add(new SqlParameter("@UniqueNo" + i, UniqueNos[i]));
                    }
                    str.Append(string.Format(" and userInfo.UniqueNo {1} in({0})", strFirst.ToString().TrimEnd(','), ht["JoinNoConn"].SafeToString()));
                }
                if (IsPage)
                {
                    StartIndex = Convert.ToInt32(ht["StartIndex"].ToString());
                    EndIndex = Convert.ToInt32(ht["EndIndex"].ToString());
                }
                dt = SQLHelp.GetListByPage("(" + str.ToString() + ")", Where, "", StartIndex,
                    EndIndex, IsPage, pms.ToArray(), out RowCount);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return dt;
        }
        #endregion

        public string GetCurrentTerm()
        {
            string strSql = "select Id from Sys_StudySection where StartDate<getdate() and EndDate>getdate()";
            object Obj = SQLHelp.ExecuteScalar(strSql, CommandType.Text, null);
            return Obj.ToString();
        }
        StringBuilder sb = null;

        private void GetAllOrgNo(string OrgNo)
        {
            sb = new StringBuilder();
            sb.Append(OrgNo + ",");
            string strSql = " select id,OrganNo from Org_Mechanism where pid=(select id from Org_Mechanism where  OrganNo='" + OrgNo + "')";
            DataTable dt = SQLHelp.ExecuteDataTable(strSql, CommandType.Text, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["OrganNo"].SafeToString();
                if (orgNo.Length > 0)
                {
                    sb.Append(dt.Rows[i]["OrganNo"] + ",");
                }
                GetChildOrgNo(dt.Rows[i]["id"].ToString());
            }

        }
        private void GetChildOrgNo(string pid)
        {
            string strSql = " select id,OrganNo from Org_Mechanism where pid=" + pid;
            DataTable dt = SQLHelp.ExecuteDataTable(strSql, CommandType.Text, null);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string orgNo = dt.Rows[i]["OrganNo"].SafeToString();
                if (orgNo.Length > 0)
                {
                    sb.Append(dt.Rows[i]["OrganNo"] + ",");
                }
                GetChildOrgNo(dt.Rows[i]["id"].ToString());
            }
        }
        #region  修改登陆密码
        /// <summary>
        /// 修改登陆密码
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OldPwd">旧密码</param>
        /// <param name="NewPwd">新密码</param>
        /// <returns></returns>
        public string UpdatePwd(string LoginName, string OldPwd, string NewPwd)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                new SqlParameter("@LoginName",LoginName),
                                new SqlParameter("@OldPwd",OldPwd),
                                new SqlParameter("@NewPwd",NewPwd)
                                };
                object obj = SQLHelp.ExecuteScalar("UpdatePwd", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region
        public DataTable GetUserInfoByUniqueNo(Hashtable ht)
        {
            try
            {
                StringBuilder str = new StringBuilder();
                SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@UniqueNo",ht["UniqueNo"].ToString()),
                new SqlParameter("@LoginName",ht["LoginName"].ToString()),
                new SqlParameter("@Password",ht["Password"].ToString()),
                new SqlParameter("@AuthenType",((int)AuthenType.用户已激活))
                };
                DataTable dt = SQLHelp.ExecuteDataTable("UpdateUserInfoByUniqueNo", CommandType.StoredProcedure, param);
                return dt;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        /*
        #region 判断用户存在
        public bool IsExistUser(string IDCard)
        {
            bool Flag = false;
            string str = "select count(1) from Sys_UserInfo where IDCard='" + IDCard + "'";
            object Count = SQLHelp.ExecuteScalar(str, CommandType.Text, null);
            if (Count.ToString() != "0")
            {
                Flag = true;
            }
            return Flag;
        }
        #endregion
        */
        #region 数据导入
        /// <summary>
        /// 数据导入
        /// </summary>
        /// <returns></returns>
        public string ImportUser(string FilePath, string OrgNo)
        {


            string result = "";
            ExcelHelper excelHelp = new ExcelHelper();
            try
            {
                DataTable dt = excelHelp.ExcelToDataTable(FilePath);
                bool Flag = Import(dt);
                if (Flag)
                {
                    SqlParameter[] param = new SqlParameter[]
                    {
                        new SqlParameter("@OrgNo",OrgNo)
                    };
                    object obj = SQLHelp.ExecuteScalar("ImportUserInfo", CommandType.StoredProcedure, param);
                    result = obj.ToString();
                }
                else
                {
                    result = "数据导入失败";
                }
                /*StringBuilder FileR = new StringBuilder();

                int SucNum = 0;
                int ErrNum = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    try
                    {
                        string IDCard = dr["身份证号"].SafeToString().Trim();
                        string Name = dr["姓名"].SafeToString().Trim();
                        string RegisterOrg = dr["组织机构号"].ToString().Trim();//组织机构号
                        string InsertReslt = "";
                        string Password = dr["密码"].SafeToString();
                        Byte IsEnable= Convert.ToByte(dr["状态"].ToString().Trim());//状态
                        string Phone = dr["电话"].ToString().Trim();//电话
                        //不存在添加
                        Sys_UserInfo userInfo = new Sys_UserInfo();
                        userInfo.IDCard = dr["身份证号"].ToString().Trim();//登录名
                        userInfo.LoginName = dr["登录账号"].ToString().Trim();//登录账号
                        userInfo.RegisterOrg = RegisterOrg;
                        userInfo.Password = EncryptHelper.Md5By32(Password);//密码
                        userInfo.IsEnable = IsEnable;
                        userInfo.Name = dr["姓名"].ToString().Trim();//姓名
                        if (dr["性别"].ToString().Trim() == "男")
                        {
                            userInfo.Sex = 0;//性别
                        }
                        else
                        {
                            userInfo.Sex = 1;//性别
                        }
                        userInfo.Address = dr["家庭住址"].ToString().Trim();//家庭住址
                        userInfo.Phone = Phone;//电话
                        userInfo.Nickname = dr["昵称"].ToString().Trim();//昵称
                        string UType = dr["用户类型"].SafeToString();
                        if (UType.Length > 0)
                        {
                            userInfo.UserType = Convert.ToByte(UType);//用户类型
                        }
                        else
                        {
                            userInfo.UserType = 0;
                        }
                        string Birthday = dr["出生日期"].SafeToString();
                        if (Birthday.Length > 0)
                        {
                            try
                            {
                                userInfo.Birthday = Convert.ToDateTime(Birthday);
                            }
                            catch (Exception ex)
                            {
                                userInfo.Birthday = null;
                            }
                        }
                        else
                        {
                            userInfo.Birthday = DateTime.Now;
                        }
                        userInfo.Remarks = dr["简介"].ToString().Trim();//简介
                        userInfo.AuthenType = 1;
                        userInfo.IsDelete = 0;//是否删除
                        userInfo.HeadPic = "";
                        userInfo.CreateUID = "";
                        if (string.IsNullOrWhiteSpace(IDCard) && string.IsNullOrWhiteSpace(Name) && string.IsNullOrWhiteSpace(UType) && string.IsNullOrWhiteSpace(Phone)
                            && string.IsNullOrWhiteSpace(RegisterOrg) && string.IsNullOrWhiteSpace(Birthday) && string.IsNullOrWhiteSpace(IsEnable.ToString()))
                        {
                            continue;
                        }
                        InsertReslt = AddUserInfo(userInfo);
                        //}
                        if (InsertReslt == "")
                        {
                            SucNum++;
                        }
                        else
                        {
                            ErrNum++;
                            FileR.Append("行号：" + (i + 1).ToString() + "失败原因：" + InsertReslt + ",\n");
                        }
                    }
                    catch (Exception)
                    {
                        ErrNum++;
                        FileR.Append((i + 1).ToString() + ",");
                    }
                }
                result = "成功" + SucNum + "条，失败" + ErrNum + "条";

                if (FileR.Length != 0)
                {
                    result += "\n失败日志：\n" + FileR.ToString();
                }*/
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public bool Import(DataTable dt)
        {
            bool Flag = true;
            try
            {
                //用bcp导入数据 
                using (System.Data.SqlClient.SqlBulkCopy bcp = new System.Data.SqlClient.SqlBulkCopy(ConfigHelper.GetConfigString("connStr.ucc")))
                {
                    //bcp.SqlRowsCopied += new System.Data.SqlClient.SqlRowsCopiedEventHandler(bcp_SqlRowsCopied);
                    bcp.BatchSize = 200;//每次传输的行数 
                    bcp.NotifyAfter = 100;//进度提示的行数 
                    bcp.DestinationTableName = "test";//目标表 
                    bcp.WriteToServer(dt);
                    Flag = true;
                }
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            return Flag;
        }
        //进度显示 
        //void bcp_SqlRowsCopied(object sender, System.Data.SqlClient.SqlRowsCopiedEventArgs e)
        //{
        //    this.Text = e.RowsCopied.ToString();
        //    this.Update();
        //}
        #endregion

        #region 统计不同组织机构用户数目（非学生）
        /// <summary>
        /// 统计不同组织机构用户数目
        /// </summary>
        /// <returns></returns>
        public int censusUser(string RegisterOrg, string AuthenType)
        {
            int retult = 0;
            try
            {
                string strSql = "select count(1) from Sys_UserInfo where 1=1";
                if (RegisterOrg.Length > 0)
                {
                    GetAllOrgNo(RegisterOrg);
                    string orgArry = "'" + sb.ToString().Replace(",", "','") + "'";
                    //str.Append(" and userInfo.RegisterOrg in (" + orgArry + ")");

                    strSql += " and RegisterOrg in (" + orgArry + ")";
                }
                if (AuthenType.Length > 0)
                {
                    strSql += " and AuthenType in (" + AuthenType + ")";
                }
                object obj = SQLHelp.ExecuteScalar(strSql, CommandType.Text, null);
                retult = Convert.ToInt32(obj);
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 统计活跃用户数
        /// <summary>
        /// 统计活跃用户数
        /// </summary>
        /// <returns></returns>
        public int censusActiveUser()
        {
            int retult = 0;
            try
            {
                string strSql = "select distinct(loginName) from Sys_LogInfo where createtime>dateadd(day,-3,getdate())";

                DataTable dt = SQLHelp.ExecuteDataTable(strSql, CommandType.Text, null);
                retult = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 添加用户
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string AddUserInfo(Sys_UserInfo model)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@IDCard",model.IDCard),
                                        new SqlParameter("@LoginName",model.LoginName),
                                        new SqlParameter("@RegisterOrg",model.RegisterOrg),
                                        new SqlParameter("@Password",model.Password),
                                        new SqlParameter("@IsEnable",1),
                                        new SqlParameter("@Name",model.Name),
                                        new SqlParameter("@Sex",model.Sex),
                                        new SqlParameter("@Address",model.Address),
                                        new SqlParameter("@Phone",model.Phone),
                                        new SqlParameter("@Nickname",model.Nickname),
                                        new SqlParameter("@UserType",model.UserType),
                                        new SqlParameter("@Birthday",model.Birthday),
                                        new SqlParameter("@Remarks",model.Remarks),
                                        new SqlParameter("@AuthenType",model.AuthenType),
                                        new SqlParameter("@IsDelete",model.IsDelete),
                                        new SqlParameter("@HeadPic",model.HeadPic),
                                        new SqlParameter("@CreateUID",model.CreateUID),
                                        new SqlParameter("@Email",model.Email)
                                       
                                };
                object obj = SQLHelp.ExecuteScalar("AddUserInfo", CommandType.StoredProcedure, pa);
                retult = obj.SafeToString();
            }
            catch (Exception ex)
            {
                LogService.WriteErrorLog(ex.Message);
            }
            return retult;
        }
        #endregion

        #region 编辑用户
        /// <summary>
        /// 编辑用户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string EditUserInfo(Sys_UserInfo model)
        {
            string retult = "";
            try
            {
                SqlParameter[] pa = { 
                                        new SqlParameter("@ID",model.Id),
                                        new SqlParameter("@IDCard",model.IDCard),
                                        new SqlParameter("@LoginName",model.LoginName),
                                        new SqlParameter("@Name",model.Name),
                                        new SqlParameter("@Sex",model.Sex),
                                        new SqlParameter("@Address",model.Address),
                                        new SqlParameter("@Phone",model.Phone),
                                        new SqlParameter("@Nickname",model.Nickname),
                                        new SqlParameter("@UserType",model.UserType),
                                        new SqlParameter("@Birthday",model.Birthday.SafeToString()==""?DateTime.Now:model.Birthday),
                                        new SqlParameter("@Remarks",model.Remarks),
                                        new SqlParameter("@HeadPic",model.HeadPic),
                                        new SqlParameter("@Password",model.Password),
                                        new SqlParameter("@IsFirstLogin",model.IsFirstLogin),
                                        new SqlParameter("@Email",model.Email.SafeToString())
                                };
                object obj = SQLHelp.ExecuteScalar("EditUserInfo", CommandType.StoredProcedure, pa);
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
