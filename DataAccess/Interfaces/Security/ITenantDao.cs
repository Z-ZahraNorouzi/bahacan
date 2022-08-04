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
    public interface ITenantDao  
    {  
        void Create(TenantBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(TenantBusinessModel input);  
        void Update(TenantBusinessModel input);  
        Task<IList<TenantDataTransferModel>> GetAll(Expression<Func<TenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<TenantDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<TenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<TenantBusinessModel, bool>>? filter = null);  
        Task<TenantBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
