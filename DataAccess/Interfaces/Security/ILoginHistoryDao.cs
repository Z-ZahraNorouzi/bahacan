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
    public interface ILoginHistoryDao  
    {  
        void Create(LoginHistoryBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(LoginHistoryBusinessModel input);  
        void Update(LoginHistoryBusinessModel input);  
        Task<IList<LoginHistoryDataTransferModel>> GetAll(Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<LoginHistoryDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null);  
        Task<LoginHistoryBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
