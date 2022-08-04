namespace DataTransferModel.Security   
{  
	public class UserInfoDataTransferModel  
	{  
		public Int64? UserInfoId { get; set; }  
		public string UserName { get; set; }  
		public string MobileNo { get; set; }  
		public string Email { get; set; }  
		public string Password { get; set; }  
		public Int64 AvatarImageId { get; set; }  
		public Int16 Status { get; set; }  
		public string ActivationCode { get; set; }  
		public Int64? RealPersonId { get; set; }  
		public bool IsActive { get; set; }  
		public bool IsLock { get; set; }  
		public DateTime? FromDate { get; set; }  
		public DateTime? ToDate { get; set; }  
		public TimeSpan? FromTime { get; set; }  
		public TimeSpan? ToTime { get; set; }  
	}  
}  
