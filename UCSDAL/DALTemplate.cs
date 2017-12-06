using UCSIDAL;
using UCSModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UCSDAL
{


    /// </summary>
    ///	
    /// </summary>
    public partial class Grad_Class_relDal : BaseDal<Grad_Class_rel>, IGrad_Class_relDal
    {


    }

    public partial class DalFactory
    {
        public static IGrad_Class_relDal GetGrad_Class_relDal()
        {
            return new Grad_Class_relDal();
        }
    }


    /// </summary>
    ///	学段年级关系数据处理类1
    /// </summary>
    public partial class Grade_Period_RelDal : BaseDal<Grade_Period_Rel>, IGrade_Period_RelDal
    {


    }

    public partial class DalFactory
    {
        public static IGrade_Period_RelDal GetGrade_Period_RelDal()
        {
            return new Grade_Period_RelDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class AssetManagementDal : BaseDal<AssetManagement>, IAssetManagementDal
    {


    }

    public partial class DalFactory
    {
        public static IAssetManagementDal GetAssetManagementDal()
        {
            return new AssetManagementDal();
        }
    }


    /// </summary>
    ///	组织机构数据处理类2
    /// </summary>
    public partial class Org_MechanismDal : BaseDal<Org_Mechanism>, IOrg_MechanismDal
    {


    }

    public partial class DalFactory
    {
        public static IOrg_MechanismDal GetOrg_MechanismDal()
        {
            return new Org_MechanismDal();
        }
    }


    /// </summary>
    ///	组织机构用户关系数据处理类3
    /// </summary>
    public partial class Org_User_RelDal : BaseDal<Org_User_Rel>, IOrg_User_RelDal
    {


    }

    public partial class DalFactory
    {
        public static IOrg_User_RelDal GetOrg_User_RelDal()
        {
            return new Org_User_RelDal();
        }
    }


    /// </summary>
    ///	班级学生关系数据处理类4
    /// </summary>
    public partial class Student_Class_RelDal : BaseDal<Student_Class_Rel>, IStudent_Class_RelDal
    {


    }

    public partial class DalFactory
    {
        public static IStudent_Class_RelDal GetStudent_Class_RelDal()
        {
            return new Student_Class_RelDal();
        }
    }


    /// </summary>
    ///	菜单按钮类型数据处理类5
    /// </summary>
    public partial class Sys_ButtonTypeDal : BaseDal<Sys_ButtonType>, ISys_ButtonTypeDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_ButtonTypeDal GetSys_ButtonTypeDal()
        {
            return new Sys_ButtonTypeDal();
        }
    }


    /// </summary>
    ///	班级历史数据处理类6
    /// </summary>
    public partial class Sys_ClassHistoryDal : BaseDal<Sys_ClassHistory>, ISys_ClassHistoryDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_ClassHistoryDal GetSys_ClassHistoryDal()
        {
            return new Sys_ClassHistoryDal();
        }
    }


    /// </summary>
    ///	班级信息数据处理类7
    /// </summary>
    public partial class Sys_ClassInfoDal : BaseDal<Sys_ClassInfo>, ISys_ClassInfoDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_ClassInfoDal GetSys_ClassInfoDal()
        {
            return new Sys_ClassInfoDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Sys_DictionaryDal : BaseDal<Sys_Dictionary>, ISys_DictionaryDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_DictionaryDal GetSys_DictionaryDal()
        {
            return new Sys_DictionaryDal();
        }
    }


    /// </summary>
    ///	年级信息数据处理类8
    /// </summary>
    public partial class Sys_GradeInfoDal : BaseDal<Sys_GradeInfo>, ISys_GradeInfoDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_GradeInfoDal GetSys_GradeInfoDal()
        {
            return new Sys_GradeInfoDal();
        }
    }


    /// </summary>
    ///	接口数据处理类9
    /// </summary>
    public partial class Sys_InterfaceDal : BaseDal<Sys_Interface>, ISys_InterfaceDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_InterfaceDal GetSys_InterfaceDal()
        {
            return new Sys_InterfaceDal();
        }
    }


    /// </summary>
    ///	系统日志数据处理类10
    /// </summary>
    public partial class Sys_LogInfoDal : BaseDal<Sys_LogInfo>, ISys_LogInfoDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_LogInfoDal GetSys_LogInfoDal()
        {
            return new Sys_LogInfoDal();
        }
    }


    /// </summary>
    ///	菜单信息数据处理类11
    /// </summary>
    public partial class Sys_MenuInfoDal : BaseDal<Sys_MenuInfo>, ISys_MenuInfoDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_MenuInfoDal GetSys_MenuInfoDal()
        {
            return new Sys_MenuInfoDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Sys_PeriodDal : BaseDal<Sys_Period>, ISys_PeriodDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_PeriodDal GetSys_PeriodDal()
        {
            return new Sys_PeriodDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Edu_MajorInfoDal : BaseDal<Edu_MajorInfo>, IEdu_MajorInfoDal
    {


    }

    public partial class DalFactory
    {
        public static IEdu_MajorInfoDal GetEdu_MajorInfoDal()
        {
            return new Edu_MajorInfoDal();
        }
    }


    /// </summary>
    ///	角色数据处理类12
    /// </summary>
    public partial class Sys_RoleDal : BaseDal<Sys_Role>, ISys_RoleDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_RoleDal GetSys_RoleDal()
        {
            return new Sys_RoleDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Edu_SubJectDal : BaseDal<Edu_SubJect>, IEdu_SubJectDal
    {


    }

    public partial class DalFactory
    {
        public static IEdu_SubJectDal GetEdu_SubJectDal()
        {
            return new Edu_SubJectDal();
        }
    }


    /// </summary>
    ///	角色菜单关系数据处理类13
    /// </summary>
    public partial class Sys_RoleOfMenuDal : BaseDal<Sys_RoleOfMenu>, ISys_RoleOfMenuDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_RoleOfMenuDal GetSys_RoleOfMenuDal()
        {
            return new Sys_RoleOfMenuDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Edu_Major_Sub_RelDal : BaseDal<Edu_Major_Sub_Rel>, IEdu_Major_Sub_RelDal
    {


    }

    public partial class DalFactory
    {
        public static IEdu_Major_Sub_RelDal GetEdu_Major_Sub_RelDal()
        {
            return new Edu_Major_Sub_RelDal();
        }
    }


    /// </summary>
    ///	角色用户关系数据处理类14
    /// </summary>
    public partial class Sys_RoleOfUserDal : BaseDal<Sys_RoleOfUser>, ISys_RoleOfUserDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_RoleOfUserDal GetSys_RoleOfUserDal()
        {
            return new Sys_RoleOfUserDal();
        }
    }


    /// </summary>
    ///	学期数据处理类15
    /// </summary>
    public partial class Sys_StudySectionDal : BaseDal<Sys_StudySection>, ISys_StudySectionDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_StudySectionDal GetSys_StudySectionDal()
        {
            return new Sys_StudySectionDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Edu_BookVersionDal : BaseDal<Edu_BookVersion>, IEdu_BookVersionDal
    {


    }

    public partial class DalFactory
    {
        public static IEdu_BookVersionDal GetEdu_BookVersionDal()
        {
            return new Edu_BookVersionDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Edu_BookCatalogDal : BaseDal<Edu_BookCatalog>, IEdu_BookCatalogDal
    {


    }

    public partial class DalFactory
    {
        public static IEdu_BookCatalogDal GetEdu_BookCatalogDal()
        {
            return new Edu_BookCatalogDal();
        }
    }


    /// </summary>
    ///	学生历史数据处理类16
    /// </summary>
    public partial class Sys_StuHistoryDal : BaseDal<Sys_StuHistory>, ISys_StuHistoryDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_StuHistoryDal GetSys_StuHistoryDal()
        {
            return new Sys_StuHistoryDal();
        }
    }


    /// </summary>
    ///	系统账号与实体关系数据处理类17
    /// </summary>
    public partial class Sys_SysOfEntity_RelDal : BaseDal<Sys_SysOfEntity_Rel>, ISys_SysOfEntity_RelDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_SysOfEntity_RelDal GetSys_SysOfEntity_RelDal()
        {
            return new Sys_SysOfEntity_RelDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class Edu_BookDal : BaseDal<Edu_Book>, IEdu_BookDal
    {


    }

    public partial class DalFactory
    {
        public static IEdu_BookDal GetEdu_BookDal()
        {
            return new Edu_BookDal();
        }
    }


    /// </summary>
    ///	系统账号与接口关系数据处理类18
    /// </summary>
    public partial class Sys_SysOfInter_RelDal : BaseDal<Sys_SysOfInter_Rel>, ISys_SysOfInter_RelDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_SysOfInter_RelDal GetSys_SysOfInter_RelDal()
        {
            return new Sys_SysOfInter_RelDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class FeedBack_StuListDal : BaseDal<FeedBack_StuList>, IFeedBack_StuListDal
    {


    }

    public partial class DalFactory
    {
        public static IFeedBack_StuListDal GetFeedBack_StuListDal()
        {
            return new FeedBack_StuListDal();
        }
    }


    /// </summary>
    ///	系统账号数据处理类19
    /// </summary>
    public partial class Sys_SystemInfoDal : BaseDal<Sys_SystemInfo>, ISys_SystemInfoDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_SystemInfoDal GetSys_SystemInfoDal()
        {
            return new Sys_SystemInfoDal();
        }
    }


    /// </summary>
    ///	用户数据处理类20
    /// </summary>
    public partial class Sys_UserInfoDal : BaseDal<Sys_UserInfo>, ISys_UserInfoDal
    {


    }

    public partial class DalFactory
    {
        public static ISys_UserInfoDal GetSys_UserInfoDal()
        {
            return new Sys_UserInfoDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class testDal : BaseDal<test>, ItestDal
    {


    }

    public partial class DalFactory
    {
        public static ItestDal GettestDal()
        {
            return new testDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class UserSkimLogDal : BaseDal<UserSkimLog>, IUserSkimLogDal
    {


    }

    public partial class DalFactory
    {
        public static IUserSkimLogDal GetUserSkimLogDal()
        {
            return new UserSkimLogDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class View_AllEntityDal : BaseDal<View_AllEntity>, IView_AllEntityDal
    {


    }

    public partial class DalFactory
    {
        public static IView_AllEntityDal GetView_AllEntityDal()
        {
            return new View_AllEntityDal();
        }
    }


    /// </summary>
    ///	
    /// </summary>
    public partial class View_GetLeftNavigationMenuDal : BaseDal<View_GetLeftNavigationMenu>, IView_GetLeftNavigationMenuDal
    {


    }

    public partial class DalFactory
    {
        public static IView_GetLeftNavigationMenuDal GetView_GetLeftNavigationMenuDal()
        {
            return new View_GetLeftNavigationMenuDal();
        }
    }
}