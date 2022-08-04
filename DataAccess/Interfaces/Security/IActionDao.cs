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
    public interface IActionDao  
    {  
        void Create(ActionBusinessModel input);  
        void Delete(Int16 input);  
        void Delete(ActionBusinessModel input);  
        void Update(ActionBusinessModel input);  
        Task<IList<ActionDataTransferModel>> GetAll(Expression<Func<ActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<IList<ActionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "");  
        Task<int> GetAllCount(Expression<Func<ActionBusinessModel, bool>>? filter = null);  
        Task<ActionBusinessModel> GetByKey(Int16 input, string includeProperties = "");  
    }  
}  
