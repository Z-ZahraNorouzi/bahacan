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
    public class RolePermissionDao : IRolePermissionDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<RolePermission> _repository;  
        public RolePermissionDao(DbContext context)  
        {  
            _repository = new GenericRepository<RolePermission>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(RolePermissionBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<RolePermission>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.RolePermissionId = obj.RolePermissionId;  
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
        public void Delete(RolePermissionBusinessModel input)  
        {  
            if (input.RolePermissionId.HasValue)  
                Delete(input.RolePermissionId.Value);  
        }  
        public void Update(RolePermissionBusinessModel input)  
        {  
            if (input.RolePermissionId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.RolePermissionId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<RolePermission>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<RolePermissionDataTransferModel>> GetAll(Expression<Func<RolePermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<RolePermissionDataTransferModel>>(result);  
        }  
        public async Task<IList<RolePermissionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<RolePermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "RolePermissionId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<RolePermissionDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<RolePermissionBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<RolePermissionBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<RolePermissionBusinessModel>(result);  
        }  
private RolePermission Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(RolePermissionBusinessModel), includeProperties);  
            return _repository.Get(x => x.RolePermissionId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<RolePermission> GetQueryable(Expression<Func<RolePermissionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<RolePermission, bool>>? filterExpr = Extensions.ConvertExpression<RolePermissionBusinessModel, RolePermission>(filter);  
            includeProperties = DaoMapper.Map(typeof(RolePermissionBusinessModel), includeProperties);  
            IQueryable<RolePermission> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<RolePermission>(orderBy, query);  
            return query;  
        }  
    }  
}  
