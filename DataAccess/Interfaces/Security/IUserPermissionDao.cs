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
    public interface IUserPermissionDao  
    {  
        void Create(UserPermissionBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(UserPermissionBusinessModel input);  
        void Update(UserPermissionBusinessModel input);  
        Task<IList<UserPermissionDataTransferModel>> GetAll(Expression<Func<UserPermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<UserPermissionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserPermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<UserPermissionBusinessModel, bool>>? filter = null);  
        Task<UserPermissionBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
