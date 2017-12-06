using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCSUtility;
using UCSDAL;
using UCSModel;
using UCSIBLL;
namespace UCSBLL
{

	/// </summary>
	///	
	/// </summary>
    public partial class Grad_Class_relService:BaseService<Grad_Class_rel>,IGrad_Class_relService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetGrad_Class_relDal();
        }
    }	

	/// </summary>
	///	学段年级关系业务类1
	/// </summary>
    public partial class Grade_Period_RelService:BaseService<Grade_Period_Rel>,IGrade_Period_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetGrade_Period_RelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class AssetManagementService:BaseService<AssetManagement>,IAssetManagementService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetAssetManagementDal();
        }
    }	

	/// </summary>
	///	组织机构业务类2
	/// </summary>
    public partial class Org_MechanismService:BaseService<Org_Mechanism>,IOrg_MechanismService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetOrg_MechanismDal();
        }
    }	

	/// </summary>
	///	组织机构用户关系业务类3
	/// </summary>
    public partial class Org_User_RelService:BaseService<Org_User_Rel>,IOrg_User_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetOrg_User_RelDal();
        }
    }	

	/// </summary>
	///	班级学生关系业务类4
	/// </summary>
    public partial class Student_Class_RelService:BaseService<Student_Class_Rel>,IStudent_Class_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetStudent_Class_RelDal();
        }
    }	

	/// </summary>
	///	菜单按钮类型业务类5
	/// </summary>
    public partial class Sys_ButtonTypeService:BaseService<Sys_ButtonType>,ISys_ButtonTypeService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_ButtonTypeDal();
        }
    }	

	/// </summary>
	///	班级历史业务类6
	/// </summary>
    public partial class Sys_ClassHistoryService:BaseService<Sys_ClassHistory>,ISys_ClassHistoryService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_ClassHistoryDal();
        }
    }	

	/// </summary>
	///	班级信息业务类7
	/// </summary>
    public partial class Sys_ClassInfoService:BaseService<Sys_ClassInfo>,ISys_ClassInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_ClassInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_DictionaryService:BaseService<Sys_Dictionary>,ISys_DictionaryService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_DictionaryDal();
        }
    }	

	/// </summary>
	///	年级信息业务类8
	/// </summary>
    public partial class Sys_GradeInfoService:BaseService<Sys_GradeInfo>,ISys_GradeInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_GradeInfoDal();
        }
    }	

	/// </summary>
	///	接口业务类9
	/// </summary>
    public partial class Sys_InterfaceService:BaseService<Sys_Interface>,ISys_InterfaceService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_InterfaceDal();
        }
    }	

	/// </summary>
	///	系统日志业务类10
	/// </summary>
    public partial class Sys_LogInfoService:BaseService<Sys_LogInfo>,ISys_LogInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_LogInfoDal();
        }
    }	

	/// </summary>
	///	菜单信息业务类11
	/// </summary>
    public partial class Sys_MenuInfoService:BaseService<Sys_MenuInfo>,ISys_MenuInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_MenuInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Sys_PeriodService:BaseService<Sys_Period>,ISys_PeriodService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_PeriodDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Edu_MajorInfoService:BaseService<Edu_MajorInfo>,IEdu_MajorInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEdu_MajorInfoDal();
        }
    }	

	/// </summary>
	///	角色业务类12
	/// </summary>
    public partial class Sys_RoleService:BaseService<Sys_Role>,ISys_RoleService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_RoleDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Edu_SubJectService:BaseService<Edu_SubJect>,IEdu_SubJectService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEdu_SubJectDal();
        }
    }	

	/// </summary>
	///	角色菜单关系业务类13
	/// </summary>
    public partial class Sys_RoleOfMenuService:BaseService<Sys_RoleOfMenu>,ISys_RoleOfMenuService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_RoleOfMenuDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Edu_Major_Sub_RelService:BaseService<Edu_Major_Sub_Rel>,IEdu_Major_Sub_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEdu_Major_Sub_RelDal();
        }
    }	

	/// </summary>
	///	角色用户关系业务类14
	/// </summary>
    public partial class Sys_RoleOfUserService:BaseService<Sys_RoleOfUser>,ISys_RoleOfUserService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_RoleOfUserDal();
        }
    }	

	/// </summary>
	///	学期业务类15
	/// </summary>
    public partial class Sys_StudySectionService:BaseService<Sys_StudySection>,ISys_StudySectionService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_StudySectionDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Edu_BookVersionService:BaseService<Edu_BookVersion>,IEdu_BookVersionService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEdu_BookVersionDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Edu_BookCatalogService:BaseService<Edu_BookCatalog>,IEdu_BookCatalogService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEdu_BookCatalogDal();
        }
    }	

	/// </summary>
	///	学生历史业务类16
	/// </summary>
    public partial class Sys_StuHistoryService:BaseService<Sys_StuHistory>,ISys_StuHistoryService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_StuHistoryDal();
        }
    }	

	/// </summary>
	///	系统账号与实体关系业务类17
	/// </summary>
    public partial class Sys_SysOfEntity_RelService:BaseService<Sys_SysOfEntity_Rel>,ISys_SysOfEntity_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_SysOfEntity_RelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class Edu_BookService:BaseService<Edu_Book>,IEdu_BookService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetEdu_BookDal();
        }
    }	

	/// </summary>
	///	系统账号与接口关系业务类18
	/// </summary>
    public partial class Sys_SysOfInter_RelService:BaseService<Sys_SysOfInter_Rel>,ISys_SysOfInter_RelService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_SysOfInter_RelDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class FeedBack_StuListService:BaseService<FeedBack_StuList>,IFeedBack_StuListService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetFeedBack_StuListDal();
        }
    }	

	/// </summary>
	///	系统账号业务类19
	/// </summary>
    public partial class Sys_SystemInfoService:BaseService<Sys_SystemInfo>,ISys_SystemInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_SystemInfoDal();
        }
    }	

	/// </summary>
	///	用户业务类20
	/// </summary>
    public partial class Sys_UserInfoService:BaseService<Sys_UserInfo>,ISys_UserInfoService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetSys_UserInfoDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class testService:BaseService<test>,ItestService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GettestDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class UserSkimLogService:BaseService<UserSkimLog>,IUserSkimLogService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetUserSkimLogDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class View_AllEntityService:BaseService<View_AllEntity>,IView_AllEntityService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetView_AllEntityDal();
        }
    }	

	/// </summary>
	///	
	/// </summary>
    public partial class View_GetLeftNavigationMenuService:BaseService<View_GetLeftNavigationMenu>,IView_GetLeftNavigationMenuService
    {
	 public override void SetCurrentDal()
        {
            CurrentDal = DalFactory.GetView_GetLeftNavigationMenuDal();
        }
    }	
}