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
    public class UserTenantDao : IUserTenantDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<UserTenant> _repository;  
        public UserTenantDao(DbContext context)  
        {  
            _repository = new GenericRepository<UserTenant>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(UserTenantBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<UserTenant>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.UserTenantId = obj.UserTenantId;  
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
        public void Delete(UserTenantBusinessModel input)  
        {  
            if (input.UserTenantId.HasValue)  
                Delete(input.UserTenantId.Value);  
        }  
        public void Update(UserTenantBusinessModel input)  
        {  
            if (input.UserTenantId.HasValue)  
            {
                CharacterCorrector.ReplacePersianCodePages(input);
                var entityToUpdate = Get(input.UserTenantId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<UserTenant>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<UserTenantDataTransferModel>> GetAll(Expression<Func<UserTenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<UserTenantDataTransferModel>>(result);  
        }  
        public async Task<IList<UserTenantDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserTenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "UserTenantId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<UserTenantDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<UserTenantBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<UserTenantBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<UserTenantBusinessModel>(result);  
        }  
private UserTenant Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(UserTenantBusinessModel), includeProperties);  
            return _repository.Get(x => x.UserTenantId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<UserTenant> GetQueryable(Expression<Func<UserTenantBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<UserTenant, bool>>? filterExpr = Extensions.ConvertExpression<UserTenantBusinessModel, UserTenant>(filter);  
            includeProperties = DaoMapper.Map(typeof(UserTenantBusinessModel), includeProperties);  
            IQueryable<UserTenant> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<UserTenant>(orderBy, query);  
            return query;  
        }  
    }  
}  
