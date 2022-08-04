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
    public interface IUserTenantDao  
    {  
        void Create(UserTenantBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(UserTenantBusinessModel input);  
        void Update(UserTenantBusinessModel input);  
        Task<IList<UserTenantDataTransferModel>> GetAll(Expression<Func<UserTenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<UserTenantDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserTenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<UserTenantBusinessModel, bool>>? filter = null);  
        Task<UserTenantBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
