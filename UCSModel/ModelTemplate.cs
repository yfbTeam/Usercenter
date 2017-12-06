  
using System;
namespace UCSModel
{    

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Grad_Class_rel
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? GradeNum { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ClassNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SectionID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? GradeID { get; set; }
    }

	/// </summary>
	///	学段年级关系实体类
	/// </summary>
	[Serializable]
    public partial class Grade_Period_Rel
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///学段Id 
		/// </summary>
		public int? PeriodID { get; set; }
		/// <summary>
		///年级Id 
		/// </summary>
		public int? GradeID { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class AssetManagement
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string AssetModel { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Number { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string AdressName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UseUnits { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? WarrantyDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Principal { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AcquisitionDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string SourceEquipment { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Creator { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Editor { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? UpdateTime { get; set; }
		/// <summary>
		///0 正常;1 删除;2归档 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		///0 未使用;1 使用 
		/// </summary>
		public Byte? UseStatus { get; set; }
    }

	/// </summary>
	///	组织机构实体类
	/// </summary>
	[Serializable]
    public partial class Org_Mechanism
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///组织机构号 
		/// </summary>
		public string OrganNo { get; set; }
		/// <summary>
		///父Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///组织类型 
		/// </summary>
		public int? OrganType { get; set; }
		/// <summary>
		///法人/负责人 
		/// </summary>
		public string LegalUID { get; set; }
		/// <summary>
		///成立时间 
		/// </summary>
		public DateTime? EstabLish { get; set; }
		/// <summary>
		///图片信息 
		/// </summary>
		public string ImageInfo { get; set; }
		/// <summary>
		///用户数 
		/// </summary>
		public int? UserCount { get; set; }
		/// <summary>
		///排序 
		/// </summary>
		public int? OrderNum { get; set; }
		/// <summary>
		///介绍 
		/// </summary>
		public string Introduce { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	组织机构用户关系实体类
	/// </summary>
	[Serializable]
    public partial class Org_User_Rel
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///组织机构号 
		/// </summary>
		public string OrganNo { get; set; }
		/// <summary>
		///用户唯一值 
		/// </summary>
		public string UniqueNo { get; set; }
    }

	/// </summary>
	///	班级学生关系实体类
	/// </summary>
	[Serializable]
    public partial class Student_Class_Rel
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///学生唯一值 
		/// </summary>
		public int? UserID { get; set; }
		/// <summary>
		///班级Id 
		/// </summary>
		public int? ClassID { get; set; }
    }

	/// </summary>
	///	菜单按钮类型实体类
	/// </summary>
	[Serializable]
    public partial class Sys_ButtonType
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///名称 
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		///值 
		/// </summary>
		public string Value { get; set; }
    }

	/// </summary>
	///	班级历史实体类
	/// </summary>
	[Serializable]
    public partial class Sys_ClassHistory
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///学年 
		/// </summary>
		public int? AcademicId { get; set; }
		/// <summary>
		///学期 
		/// </summary>
		public int? SemesterId { get; set; }
		/// <summary>
		///班号 
		/// </summary>
		public string ClassNO { get; set; }
		/// <summary>
		///班级名称 
		/// </summary>
		public string ClassName { get; set; }
		/// <summary>
		///班主任 
		/// </summary>
		public string HeadteacherNO { get; set; }
		/// <summary>
		///班长 
		/// </summary>
		public string MonitorNO { get; set; }
		/// <summary>
		///年级Id 
		/// </summary>
		public int? GradeId { get; set; }
		/// <summary>
		///学生唯一值 
		/// </summary>
		public string StudentNos { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	班级信息实体类
	/// </summary>
	[Serializable]
    public partial class Sys_ClassInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///班号 
		/// </summary>
		public string ClassNO { get; set; }
		/// <summary>
		///班级名称 
		/// </summary>
		public string ClassName { get; set; }
		/// <summary>
		///班主任 
		/// </summary>
		public string HeadteacherNO { get; set; }
		/// <summary>
		///班长 
		/// </summary>
		public string MonitorNO { get; set; }
		/// <summary>
		///是否当前使用 
		/// </summary>
		public Byte? IsUse { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_Dictionary
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///父级Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///关键 
		/// </summary>
		public string Key { get; set; }
		/// <summary>
		///值 
		/// </summary>
		public string Value { get; set; }
		/// <summary>
		///类型 
		/// </summary>
		public Byte? Type { get; set; }
    }

	/// </summary>
	///	年级信息实体类
	/// </summary>
	[Serializable]
    public partial class Sys_GradeInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///年级名称 
		/// </summary>
		public string GradeName { get; set; }
		/// <summary>
		///组织机构号 
		/// </summary>
		public string OrganNo { get; set; }
		/// <summary>
		///是否最后班级 
		/// </summary>
		public Byte? IsGraduate { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		///学段ID 
		/// </summary>
		public int? PeriodID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public Byte? AcademicId { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Leader { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? GradNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? MajorID { get; set; }
    }

	/// </summary>
	///	接口实体类
	/// </summary>
	[Serializable]
    public partial class Sys_Interface
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///接口名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///接口描述 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	系统日志实体类
	/// </summary>
	[Serializable]
    public partial class Sys_LogInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///账号 
		/// </summary>
		public string AccountNo { get; set; }
		/// <summary>
		///用户登录名 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		///当前IP 
		/// </summary>
		public string IP { get; set; }
		/// <summary>
		///操作内容 
		/// </summary>
		public string Operation { get; set; }
		/// <summary>
		///日志类型 
		/// </summary>
		public Byte? LogType { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///备注 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///操作对象 
		/// </summary>
		public string OperationObj { get; set; }
		/// <summary>
		///操作对象唯一值描述 
		/// </summary>
		public string OperationUniqueID { get; set; }
		/// <summary>
		///操作描述 
		/// </summary>
		public string OperationMsg { get; set; }
    }

	/// </summary>
	///	菜单信息实体类
	/// </summary>
	[Serializable]
    public partial class Sys_MenuInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///菜单名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///父级Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///菜单Url 
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		///菜单描述 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///是否菜单 
		/// </summary>
		public Boolean? IsMenu { get; set; }
		/// <summary>
		///是否显示菜单 
		/// </summary>
		public Byte? IsShow { get; set; }
		/// <summary>
		///排序 
		/// </summary>
		public int? Sort { get; set; }
		/// <summary>
		///菜单编码 
		/// </summary>
		public string MenuCode { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Sys_Period
    {

		/// <summary>
		/// 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? LSchool { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Edu_MajorInfo
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	角色实体类
	/// </summary>
	[Serializable]
    public partial class Sys_Role
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///角色名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Edu_SubJect
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? createtime { get; set; }
    }

	/// </summary>
	///	角色菜单关系实体类
	/// </summary>
	[Serializable]
    public partial class Sys_RoleOfMenu
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///角色Id 
		/// </summary>
		public int? RoleId { get; set; }
		/// <summary>
		///菜单Id 
		/// </summary>
		public int? MenuId { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Edu_Major_Sub_Rel
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? MajorID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SubID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	角色用户关系实体类
	/// </summary>
	[Serializable]
    public partial class Sys_RoleOfUser
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///角色Id 
		/// </summary>
		public int? RoleId { get; set; }
		/// <summary>
		///用户唯一值 
		/// </summary>
		public string UniqueNo { get; set; }
    }

	/// </summary>
	///	学期实体类
	/// </summary>
	[Serializable]
    public partial class Sys_StudySection
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///学年 
		/// </summary>
		public string Academic { get; set; }
		/// <summary>
		///学期 
		/// </summary>
		public string Semester { get; set; }
		/// <summary>
		///起始时间 
		/// </summary>
		public DateTime? StartDate { get; set; }
		/// <summary>
		///结束时间 
		/// </summary>
		public DateTime? EndDate { get; set; }
		/// <summary>
		///组织机构号 
		/// </summary>
		public string OrganNo { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		///学段编号集合 
		/// </summary>
		public string PeriodIDs { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Edu_BookVersion
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Edu_BookCatalog
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? BookID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	学生历史实体类
	/// </summary>
	[Serializable]
    public partial class Sys_StuHistory
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///学年 
		/// </summary>
		public int? AcademicId { get; set; }
		/// <summary>
		///学期 
		/// </summary>
		public int? SemesterId { get; set; }
		/// <summary>
		///原班号 
		/// </summary>
		public string FromClassNO { get; set; }
		/// <summary>
		///原班级名称 
		/// </summary>
		public string FromClassName { get; set; }
		/// <summary>
		///学生唯一值 
		/// </summary>
		public string StudentNo { get; set; }
		/// <summary>
		///新班号 
		/// </summary>
		public string ToClassNO { get; set; }
		/// <summary>
		///新班级名称 
		/// </summary>
		public string ToClassName { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	系统账号与实体关系实体类
	/// </summary>
	[Serializable]
    public partial class Sys_SysOfEntity_Rel
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///账号 
		/// </summary>
		public string AccountNo { get; set; }
		/// <summary>
		///实体名称 
		/// </summary>
		public string EntityName { get; set; }
		/// <summary>
		///可访问字段英文名称 
		/// </summary>
		public string FieldsEng { get; set; }
		/// <summary>
		///启用/禁用 
		/// </summary>
		public Byte? IsEnable { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class Edu_Book
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? MajorID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SubID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? VersionID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	系统账号与接口关系实体类
	/// </summary>
	[Serializable]
    public partial class Sys_SysOfInter_Rel
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///账号 
		/// </summary>
		public string AccountNo { get; set; }
		/// <summary>
		///接口Id 
		/// </summary>
		public int? InterfaceId { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class FeedBack_StuList
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StuNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? Status { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
    }

	/// </summary>
	///	系统账号实体类
	/// </summary>
	[Serializable]
    public partial class Sys_SystemInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///系统名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///账号 
		/// </summary>
		public string AccountNo { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
    }

	/// </summary>
	///	用户实体类
	/// </summary>
	[Serializable]
    public partial class Sys_UserInfo
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///用户唯一值 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		///用户类型 
		/// </summary>
		public Byte? UserType { get; set; }
		/// <summary>
		///姓名 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///昵称 
		/// </summary>
		public string Nickname { get; set; }
		/// <summary>
		///性别 
		/// </summary>
		public Byte? Sex { get; set; }
		/// <summary>
		///联系电话 
		/// </summary>
		public string Phone { get; set; }
		/// <summary>
		///出生日期 
		/// </summary>
		public DateTime? Birthday { get; set; }
		/// <summary>
		///用户账号 
		/// </summary>
		public string LoginName { get; set; }
		/// <summary>
		///密码 
		/// </summary>
		public string Password { get; set; }
		/// <summary>
		///身份证件号 
		/// </summary>
		public string IDCard { get; set; }
		/// <summary>
		///头像 
		/// </summary>
		public string HeadPic { get; set; }
		/// <summary>
		///注册的组织机构 
		/// </summary>
		public string RegisterOrg { get; set; }
		/// <summary>
		///认证类型 
		/// </summary>
		public Byte? AuthenType { get; set; }
		/// <summary>
		///现住址 
		/// </summary>
		public string Address { get; set; }
		/// <summary>
		///备注 
		/// </summary>
		public string Remarks { get; set; }
		/// <summary>
		///创建人 
		/// </summary>
		public string CreateUID { get; set; }
		/// <summary>
		///创建时间 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		///修改人 
		/// </summary>
		public string EditUID { get; set; }
		/// <summary>
		///修改时间 
		/// </summary>
		public DateTime? EditTime { get; set; }
		/// <summary>
		///启用/禁用 
		/// </summary>
		public Byte? IsEnable { get; set; }
		/// <summary>
		///是否删除 
		/// </summary>
		public Byte? IsDelete { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string CheckMsg { get; set; }
		/// <summary>
		///卡号 
		/// </summary>
		public string KaNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string AbsHeadPic { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? IsFirstLogin { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Email { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ShowPwd { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class test
    {

		/// <summary>
		/// 
		/// </summary>
		public string 身份证号 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 用户类型 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 姓名 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 性别 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 家庭住址 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 电话 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 昵称 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 出生日期 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 简介 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string 登录账号 { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class UserSkimLog
    {

		/// <summary>
		/// 
		/// </summary>
		public int? ID { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UniqueNo { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string UserName { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string ToUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string FromUrl { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public int? SkinLong { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string WebSite { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class View_AllEntity
    {

		/// <summary>
		///实体Id 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///实体名称 
		/// </summary>
		public string EntityName { get; set; }
		/// <summary>
		///实体中文名称 
		/// </summary>
		public string EntityChina { get; set; }
		/// <summary>
		///英文属性 
		/// </summary>
		public string FieldsEng { get; set; }
		/// <summary>
		///属性 
		/// </summary>
		public string FieldsChina { get; set; }
    }

	/// </summary>
	///	
	/// </summary>
	[Serializable]
    public partial class View_GetLeftNavigationMenu
    {

		/// <summary>
		///主键 
		/// </summary>
		public int? Id { get; set; }
		/// <summary>
		///菜单名称 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		///父级Id 
		/// </summary>
		public int? Pid { get; set; }
		/// <summary>
		///菜单Url 
		/// </summary>
		public string Url { get; set; }
		/// <summary>
		///菜单描述 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		///是否菜单 
		/// </summary>
		public Boolean? IsMenu { get; set; }
		/// <summary>
		///是否显示菜单 
		/// </summary>
		public Byte? IsShow { get; set; }
		/// <summary>
		///排序 
		/// </summary>
		public int? Sort { get; set; }
    }
}
