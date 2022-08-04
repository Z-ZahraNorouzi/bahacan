namespace DataTransferModel.Security   
{  
	public class ServiceActionDataTransferModel  
	{  
		public Int64? ServiceActionId { get; set; }  
		public Int16 ActionId { get; set; }  
		public Int64 ServiceId { get; set; }  
		public string Title { get; set; }  
		public string ApiController { get; set; }  
		public string ApiService { get; set; }  
	}  
}  
