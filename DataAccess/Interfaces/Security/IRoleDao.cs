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
    public interface IRoleDao  
    {  
        void Create(RoleBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(RoleBusinessModel input);  
        void Update(RoleBusinessModel input);  
        Task<IList<RoleDataTransferModel>> GetAll(Expression<Func<RoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<RoleDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<RoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<RoleBusinessModel, bool>>? filter = null);  
        Task<RoleBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
