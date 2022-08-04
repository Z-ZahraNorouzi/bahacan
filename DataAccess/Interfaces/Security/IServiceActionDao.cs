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
    public interface IServiceActionDao  
    {  
        void Create(ServiceActionBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(ServiceActionBusinessModel input);  
        void Update(ServiceActionBusinessModel input);  
        Task<IList<ServiceActionDataTransferModel>> GetAll(Expression<Func<ServiceActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<ServiceActionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ServiceActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<ServiceActionBusinessModel, bool>>? filter = null);  
        Task<ServiceActionBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
