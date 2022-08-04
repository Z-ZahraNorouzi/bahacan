using DataAccess.EntityFrameWork;  
using Microsoft.EntityFrameworkCore;  
using BusinessModel; 
using DataTransferModel.Security;  
using System.Linq.Expressions;  
using DataLayer.Interface;  
using AutoMapper;  
using DataAccess.EntityFramework.Model;  
namespace DataAccess.EntityFramework.MS_SQL  
{  
    public class ServiceActionDao : IServiceActionDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<ServiceAction> _repository;  
        public ServiceActionDao(DbContext context)  
        {  
            _repository = new GenericRepository<ServiceAction>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(ServiceActionBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<ServiceAction>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.ServiceActionId = obj.ServiceActionId;  
        }  
        public void Delete(Int64 input)  
        {  
            var entityToDelete = Get(input);  
            if (entityToDelete != null)  
            {  
                _repository.Delete(entityToDelete);  
                _repository.Context.SaveChanges();  
            }  
        }  
        public void Delete(ServiceActionBusinessModel input)  
        {  
            if (input.ServiceActionId.HasValue)  
                Delete(input.ServiceActionId.Value);  
        }  
        public void Update(ServiceActionBusinessModel input)  
        {  
            if (input.ServiceActionId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.ServiceActionId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<ServiceAction>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<ServiceActionDataTransferModel>> GetAll(Expression<Func<ServiceActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<ServiceActionDataTransferModel>>(result);  
        }  
        public async Task<IList<ServiceActionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ServiceActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "ServiceActionId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<ServiceActionDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<ServiceActionBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<ServiceActionBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<ServiceActionBusinessModel>(result);  
        }  
private ServiceAction Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(ServiceActionBusinessModel), includeProperties);  
            return _repository.Get(x => x.ServiceActionId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<ServiceAction> GetQueryable(Expression<Func<ServiceActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<ServiceAction, bool>>? filterExpr = Extensions.ConvertExpression<ServiceActionBusinessModel, ServiceAction>(filter);  
            includeProperties = DaoMapper.Map(typeof(ServiceActionBusinessModel), includeProperties);  
            IQueryable<ServiceAction> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<ServiceAction>(orderBy, query);  
            return query;  
        }  
    }  
}  
