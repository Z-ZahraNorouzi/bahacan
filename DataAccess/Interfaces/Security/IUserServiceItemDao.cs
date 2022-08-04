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
    public interface IUserServiceItemDao  
    {  
        void Create(UserServiceItemBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(UserServiceItemBusinessModel input);  
        void Update(UserServiceItemBusinessModel input);  
        Task<IList<UserServiceItemDataTransferModel>> GetAll(Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<UserServiceItemDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null);  
        Task<UserServiceItemBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
