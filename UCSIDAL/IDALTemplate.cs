using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSUtility;
using UCSModel;
using System.Configuration;
namespace UCSIDAL
{

	/// </summary>
	///	
	/// </summary>
    public interface IGrad_Class_relDal: IBaseDal<Grad_Class_rel>
    {
		
    }

	/// </summary>
	///	学段年级关系数据处理接口类1
	/// </summary>
    public interface IGrade_Period_RelDal: IBaseDal<Grade_Period_Rel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IAssetManagementDal: IBaseDal<AssetManagement>
    {
		
    }

	/// </summary>
	///	组织机构数据处理接口类2
	/// </summary>
    public interface IOrg_MechanismDal: IBaseDal<Org_Mechanism>
    {
		
    }

	/// </summary>
	///	组织机构用户关系数据处理接口类3
	/// </summary>
    public interface IOrg_User_RelDal: IBaseDal<Org_User_Rel>
    {
		
    }

	/// </summary>
	///	班级学生关系数据处理接口类4
	/// </summary>
    public interface IStudent_Class_RelDal: IBaseDal<Student_Class_Rel>
    {
		
    }

	/// </summary>
	///	菜单按钮类型数据处理接口类5
	/// </summary>
    public interface ISys_ButtonTypeDal: IBaseDal<Sys_ButtonType>
    {
		
    }

	/// </summary>
	///	班级历史数据处理接口类6
	/// </summary>
    public interface ISys_ClassHistoryDal: IBaseDal<Sys_ClassHistory>
    {
		
    }

	/// </summary>
	///	班级信息数据处理接口类7
	/// </summary>
    public interface ISys_ClassInfoDal: IBaseDal<Sys_ClassInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISys_DictionaryDal: IBaseDal<Sys_Dictionary>
    {
		
    }

	/// </summary>
	///	年级信息数据处理接口类8
	/// </summary>
    public interface ISys_GradeInfoDal: IBaseDal<Sys_GradeInfo>
    {
		
    }

	/// </summary>
	///	接口数据处理接口类9
	/// </summary>
    public interface ISys_InterfaceDal: IBaseDal<Sys_Interface>
    {
		
    }

	/// </summary>
	///	系统日志数据处理接口类10
	/// </summary>
    public interface ISys_LogInfoDal: IBaseDal<Sys_LogInfo>
    {
		
    }

	/// </summary>
	///	菜单信息数据处理接口类11
	/// </summary>
    public interface ISys_MenuInfoDal: IBaseDal<Sys_MenuInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ISys_PeriodDal: IBaseDal<Sys_Period>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_MajorInfoDal: IBaseDal<Edu_MajorInfo>
    {
		
    }

	/// </summary>
	///	角色数据处理接口类12
	/// </summary>
    public interface ISys_RoleDal: IBaseDal<Sys_Role>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_SubJectDal: IBaseDal<Edu_SubJect>
    {
		
    }

	/// </summary>
	///	角色菜单关系数据处理接口类13
	/// </summary>
    public interface ISys_RoleOfMenuDal: IBaseDal<Sys_RoleOfMenu>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_Major_Sub_RelDal: IBaseDal<Edu_Major_Sub_Rel>
    {
		
    }

	/// </summary>
	///	角色用户关系数据处理接口类14
	/// </summary>
    public interface ISys_RoleOfUserDal: IBaseDal<Sys_RoleOfUser>
    {
		
    }

	/// </summary>
	///	学期数据处理接口类15
	/// </summary>
    public interface ISys_StudySectionDal: IBaseDal<Sys_StudySection>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_BookVersionDal: IBaseDal<Edu_BookVersion>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_BookCatalogDal: IBaseDal<Edu_BookCatalog>
    {
		
    }

	/// </summary>
	///	学生历史数据处理接口类16
	/// </summary>
    public interface ISys_StuHistoryDal: IBaseDal<Sys_StuHistory>
    {
		
    }

	/// </summary>
	///	系统账号与实体关系数据处理接口类17
	/// </summary>
    public interface ISys_SysOfEntity_RelDal: IBaseDal<Sys_SysOfEntity_Rel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_BookDal: IBaseDal<Edu_Book>
    {
		
    }

	/// </summary>
	///	系统账号与接口关系数据处理接口类18
	/// </summary>
    public interface ISys_SysOfInter_RelDal: IBaseDal<Sys_SysOfInter_Rel>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IFeedBack_StuListDal: IBaseDal<FeedBack_StuList>
    {
		
    }

	/// </summary>
	///	系统账号数据处理接口类19
	/// </summary>
    public interface ISys_SystemInfoDal: IBaseDal<Sys_SystemInfo>
    {
		
    }

	/// </summary>
	///	用户数据处理接口类20
	/// </summary>
    public interface ISys_UserInfoDal: IBaseDal<Sys_UserInfo>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface ItestDal: IBaseDal<test>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IUserSkimLogDal: IBaseDal<UserSkimLog>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IView_AllEntityDal: IBaseDal<View_AllEntity>
    {
		
    }

	/// </summary>
	///	
	/// </summary>
    public interface IView_GetLeftNavigationMenuDal: IBaseDal<View_GetLeftNavigationMenu>
    {
		
    }
}