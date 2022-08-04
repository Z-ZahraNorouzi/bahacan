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
    public interface IServiceItemDao  
    {  
        void Create(ServiceItemBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(ServiceItemBusinessModel input);  
        void Update(ServiceItemBusinessModel input);  
        Task<IList<ServiceItemDataTransferModel>> GetAll(Expression<Func<ServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<ServiceItemDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<ServiceItemBusinessModel, bool>>? filter = null);  
        Task<ServiceItemBusinessModel> GetByKey(Int64 input, string includeProperties = "");  
    }  
}  
