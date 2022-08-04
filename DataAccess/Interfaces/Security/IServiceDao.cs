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
    public interface IServiceDao  
    {  
        void Create(ServiceBusinessModel input);  
        void Delete(Int64 input);  
        void Delete(ServiceBusinessModel input);  
        void Update(ServiceBusinessModel input);  
        Task<IList<ServiceDataTransferModel>> GetAll(Expression<Func<ServiceBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<ServiceDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ServiceBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<ServiceBusinessModel, bool>>? filter = null);  
        Task<ServiceBusinessModel> GetByKey(Int64 input, string includeProperties = "");
        Task<IList<ServiceDataTransferModel>> GetByUserId(Int64 input);
    }  
}  
