using BusinessModel;  
using System;  
using System.Collections.Generic;  
using System.Linq;  
using System.Linq.Expressions;  
using System.Text;  
using System.Threading.Tasks;  
using DataTransferModel.Security;  
namespace DataLayer.Interface  
{  
    public interface IUserInfoDao  
    {  
        void Create(UserInfoBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(UserInfoBusinessModel input);  
        void Update(UserInfoBusinessModel input);  
        Task<IList<UserInfoDataTransferModel>> GetAll(Expression<Func<UserInfoBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<UserInfoDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserInfoBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<UserInfoBusinessModel, bool>>? filter = null);  
        Task<UserInfoBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
