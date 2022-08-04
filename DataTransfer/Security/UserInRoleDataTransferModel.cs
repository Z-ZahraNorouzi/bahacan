namespace DataTransferModel.Security   
{  
	public class UserInRoleDataTransferModel  
	{  
		public Int64? UserInRoleId { get; set; }  
		public Int64 RoleId { get; set; }  
		public Int64 UserId { get; set; }  
		public Int64 TenantId { get; set; }  
	}  
}  
