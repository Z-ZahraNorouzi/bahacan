namespace DataTransferModel.Security   
{  
	public class ServiceDataTransferModel  
	{  
		public Int64? ServiceId { get; set; }  
		public Int64? ParentId { get; set; }  
		public string Title { get; set; }  
		public string Controller { get; set; }  
		public string Icon { get; set; }  
		public bool IsService { get; set; }  
		public Int64 ItemOrder { get; set; }  
	}  
}  
