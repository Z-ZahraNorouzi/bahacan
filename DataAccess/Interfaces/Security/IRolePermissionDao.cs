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
    public interface IRolePermissionDao  
    {  
        void Create(RolePermissionBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(RolePermissionBusinessModel input);  
        void Update(RolePermissionBusinessModel input);  
        Task<IList<RolePermissionDataTransferModel>> GetAll(Expression<Func<RolePermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<RolePermissionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<RolePermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<RolePermissionBusinessModel, bool>>? filter = null);  
        Task<RolePermissionBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
