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
    public class TenantDao : ITenantDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<Tenant> _repository;  
        public TenantDao(DbContext context)  
        {  
            _repository = new GenericRepository<Tenant>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(TenantBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<Tenant>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.TenantId = obj.TenantId;  
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
        public void Delete(TenantBusinessModel input)  
        {  
            if (input.TenantId.HasValue)  
                Delete(input.TenantId.Value);  
        }  
        public void Update(TenantBusinessModel input)  
        {  
            if (input.TenantId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.TenantId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<Tenant>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<TenantDataTransferModel>> GetAll(Expression<Func<TenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<TenantDataTransferModel>>(result);  
        }  
        public async Task<IList<TenantDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<TenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "TenantId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<TenantDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<TenantBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<TenantBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<TenantBusinessModel>(result);  
        }  
private Tenant Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(TenantBusinessModel), includeProperties);  
            return _repository.Get(x => x.TenantId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<Tenant> GetQueryable(Expression<Func<TenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<Tenant, bool>>? filterExpr = Extensions.ConvertExpression<TenantBusinessModel, Tenant>(filter);  
            includeProperties = DaoMapper.Map(typeof(TenantBusinessModel), includeProperties);  
            IQueryable<Tenant> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<Tenant>(orderBy, query);  
            return query;  
        }  
    }  
}  
