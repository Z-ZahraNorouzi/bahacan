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
    public interface IUserInRoleDao  
    {  
        void Create(UserInRoleBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(UserInRoleBusinessModel input);  
        void Update(UserInRoleBusinessModel input);  
        Task<IList<UserInRoleDataTransferModel>> GetAll(Expression<Func<UserInRoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<UserInRoleDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserInRoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<UserInRoleBusinessModel, bool>>? filter = null);  
        Task<UserInRoleBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
