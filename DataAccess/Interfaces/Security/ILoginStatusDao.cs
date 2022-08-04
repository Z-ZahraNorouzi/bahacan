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
    public interface ILoginStatusDao  
    {  
        void Create(LoginStatusBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(LoginStatusBusinessModel input);  
        void Update(LoginStatusBusinessModel input);  
        Task<IList<LoginStatusDataTransferModel>> GetAll(Expression<Func<LoginStatusBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<LoginStatusDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<LoginStatusBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<LoginStatusBusinessModel, bool>>? filter = null);  
        Task<LoginStatusBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
