namespace DataTransferModel.Security   
{  
	public class UserPermissionDataTransferModel  
	{  
		public Int64? UserPermissionId { get; set; }  
		public Int64 UserId { get; set; }  
		public Int64 TenantId { get; set; }  
		public Int64? ServiceActionId { get; set; }  
	}  
}  
