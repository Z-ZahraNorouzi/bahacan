namespace DataTransferModel.Security   
{  
	public class RolePermissionDataTransferModel  
	{  
		public Int64? RolePermissionId { get; set; }  
		public Int64 RoleId { get; set; }  
		public Int64 TenantId { get; set; }  
		public Int64 ServiceActionId { get; set; }  
	}  
}  
