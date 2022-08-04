namespace DataTransferModel.Security   
{  
	public class TenantDataTransferModel  
	{  
		public Int64? TenantId { get; set; }  
		public string Title { get; set; }  
		public Int64? CompanyId { get; set; }  
		public string Description { get; set; }  
		public bool IsActive { get; set; }  
	}  
}  
