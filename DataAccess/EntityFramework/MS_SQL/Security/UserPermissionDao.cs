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
    public class UserPermissionDao : IUserPermissionDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<UserPermission> _repository;  
        public UserPermissionDao(DbContext context)  
        {  
            _repository = new GenericRepository<UserPermission>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(UserPermissionBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<UserPermission>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.UserPermissionId = obj.UserPermissionId;  
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
        public void Delete(UserPermissionBusinessModel input)  
        {  
            if (input.UserPermissionId.HasValue)  
                Delete(input.UserPermissionId.Value);  
        }  
        public void Update(UserPermissionBusinessModel input)  
        {  
            if (input.UserPermissionId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.UserPermissionId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<UserPermission>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<UserPermissionDataTransferModel>> GetAll(Expression<Func<UserPermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<UserPermissionDataTransferModel>>(result);  
        }  
        public async Task<IList<UserPermissionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserPermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "UserPermissionId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<UserPermissionDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<UserPermissionBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<UserPermissionBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<UserPermissionBusinessModel>(result);  
        }  
private UserPermission Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(UserPermissionBusinessModel), includeProperties);  
            return _repository.Get(x => x.UserPermissionId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<UserPermission> GetQueryable(Expression<Func<UserPermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<UserPermission, bool>>? filterExpr = Extensions.ConvertExpression<UserPermissionBusinessModel, UserPermission>(filter);  
            includeProperties = DaoMapper.Map(typeof(UserPermissionBusinessModel), includeProperties);  
            IQueryable<UserPermission> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<UserPermission>(orderBy, query);  
            return query;  
        }  
    }  
}  
