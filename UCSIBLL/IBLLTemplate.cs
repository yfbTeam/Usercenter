using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSModel;
namespace UCSIBLL
{

	/// </summary>
	///	
	/// </summary>
    public interface IGrad_Class_relService:IBaseService<Grad_Class_rel>
    {

    }	

	/// </summary>
	///	学段年级关系业务类1
	/// </summary>
    public interface IGrade_Period_RelService:IBaseService<Grade_Period_Rel>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IAssetManagementService:IBaseService<AssetManagement>
    {

    }	

	/// </summary>
	///	组织机构业务类2
	/// </summary>
    public interface IOrg_MechanismService:IBaseService<Org_Mechanism>
    {

    }	

	/// </summary>
	///	组织机构用户关系业务类3
	/// </summary>
    public interface IOrg_User_RelService:IBaseService<Org_User_Rel>
    {

    }	

	/// </summary>
	///	班级学生关系业务类4
	/// </summary>
    public interface IStudent_Class_RelService:IBaseService<Student_Class_Rel>
    {

    }	

	/// </summary>
	///	菜单按钮类型业务类5
	/// </summary>
    public interface ISys_ButtonTypeService:IBaseService<Sys_ButtonType>
    {

    }	

	/// </summary>
	///	班级历史业务类6
	/// </summary>
    public interface ISys_ClassHistoryService:IBaseService<Sys_ClassHistory>
    {

    }	

	/// </summary>
	///	班级信息业务类7
	/// </summary>
    public interface ISys_ClassInfoService:IBaseService<Sys_ClassInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ISys_DictionaryService:IBaseService<Sys_Dictionary>
    {

    }	

	/// </summary>
	///	年级信息业务类8
	/// </summary>
    public interface ISys_GradeInfoService:IBaseService<Sys_GradeInfo>
    {

    }	

	/// </summary>
	///	接口业务类9
	/// </summary>
    public interface ISys_InterfaceService:IBaseService<Sys_Interface>
    {

    }	

	/// </summary>
	///	系统日志业务类10
	/// </summary>
    public interface ISys_LogInfoService:IBaseService<Sys_LogInfo>
    {

    }	

	/// </summary>
	///	菜单信息业务类11
	/// </summary>
    public interface ISys_MenuInfoService:IBaseService<Sys_MenuInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ISys_PeriodService:IBaseService<Sys_Period>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_MajorInfoService:IBaseService<Edu_MajorInfo>
    {

    }	

	/// </summary>
	///	角色业务类12
	/// </summary>
    public interface ISys_RoleService:IBaseService<Sys_Role>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_SubJectService:IBaseService<Edu_SubJect>
    {

    }	

	/// </summary>
	///	角色菜单关系业务类13
	/// </summary>
    public interface ISys_RoleOfMenuService:IBaseService<Sys_RoleOfMenu>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_Major_Sub_RelService:IBaseService<Edu_Major_Sub_Rel>
    {

    }	

	/// </summary>
	///	角色用户关系业务类14
	/// </summary>
    public interface ISys_RoleOfUserService:IBaseService<Sys_RoleOfUser>
    {

    }	

	/// </summary>
	///	学期业务类15
	/// </summary>
    public interface ISys_StudySectionService:IBaseService<Sys_StudySection>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_BookVersionService:IBaseService<Edu_BookVersion>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_BookCatalogService:IBaseService<Edu_BookCatalog>
    {

    }	

	/// </summary>
	///	学生历史业务类16
	/// </summary>
    public interface ISys_StuHistoryService:IBaseService<Sys_StuHistory>
    {

    }	

	/// </summary>
	///	系统账号与实体关系业务类17
	/// </summary>
    public interface ISys_SysOfEntity_RelService:IBaseService<Sys_SysOfEntity_Rel>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IEdu_BookService:IBaseService<Edu_Book>
    {

    }	

	/// </summary>
	///	系统账号与接口关系业务类18
	/// </summary>
    public interface ISys_SysOfInter_RelService:IBaseService<Sys_SysOfInter_Rel>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IFeedBack_StuListService:IBaseService<FeedBack_StuList>
    {

    }	

	/// </summary>
	///	系统账号业务类19
	/// </summary>
    public interface ISys_SystemInfoService:IBaseService<Sys_SystemInfo>
    {

    }	

	/// </summary>
	///	用户业务类20
	/// </summary>
    public interface ISys_UserInfoService:IBaseService<Sys_UserInfo>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface ItestService:IBaseService<test>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IUserSkimLogService:IBaseService<UserSkimLog>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IView_AllEntityService:IBaseService<View_AllEntity>
    {

    }	

	/// </summary>
	///	
	/// </summary>
    public interface IView_GetLeftNavigationMenuService:IBaseService<View_GetLeftNavigationMenu>
    {

    }	
}