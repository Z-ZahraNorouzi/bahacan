namespace DataTransferModel.Security   
{  
	public class LoginHistoryDataTransferModel  
	{  
		public Int64? LoginHistoryId { get; set; }  
		public Int64 UserId { get; set; }  
		public Int64 TenantId { get; set; }  
		public Int64 LoginStatusId { get; set; }  
		public string UserHostAddress { get; set; }  
		public Guid Token { get; set; }  
		public DateTime? LoginDateTime { get; set; }  
		public DateTime? LogoutDateTime { get; set; }  
		public DateTime? ExpireDateTime { get; set; }  
	}  
}  
