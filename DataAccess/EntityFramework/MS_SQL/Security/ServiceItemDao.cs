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
    public class ServiceItemDao : IServiceItemDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<ServiceItem> _repository;  
        public ServiceItemDao(DbContext context)  
        {  
            _repository = new GenericRepository<ServiceItem>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(ServiceItemBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<ServiceItem>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.ServiceItemId = obj.ServiceItemId;  
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
        public void Delete(ServiceItemBusinessModel input)  
        {  
            if (input.ServiceItemId.HasValue)  
                Delete(input.ServiceItemId.Value);  
        }  
        public void Update(ServiceItemBusinessModel input)  
        {  
            if (input.ServiceItemId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.ServiceItemId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<ServiceItem>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<ServiceItemDataTransferModel>> GetAll(Expression<Func<ServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<ServiceItemDataTransferModel>>(result);  
        }  
        public async Task<IList<ServiceItemDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "ServiceItemId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<ServiceItemDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<ServiceItemBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<ServiceItemBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<ServiceItemBusinessModel>(result);  
        }  
private ServiceItem Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(ServiceItemBusinessModel), includeProperties);  
            return _repository.Get(x => x.ServiceItemId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<ServiceItem> GetQueryable(Expression<Func<ServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<ServiceItem, bool>>? filterExpr = Extensions.ConvertExpression<ServiceItemBusinessModel, ServiceItem>(filter);  
            includeProperties = DaoMapper.Map(typeof(ServiceItemBusinessModel), includeProperties);  
            IQueryable<ServiceItem> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<ServiceItem>(orderBy, query);  
            return query;  
        }  
    }  
}  
