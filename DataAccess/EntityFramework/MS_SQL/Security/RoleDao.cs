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
    public class RoleDao : IRoleDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<Role> _repository;  
        public RoleDao(DbContext context)  
        {  
            _repository = new GenericRepository<Role>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(RoleBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<Role>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.RoleId = obj.RoleId;  
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
        public void Delete(RoleBusinessModel input)  
        {  
            if (input.RoleId.HasValue)  
                Delete(input.RoleId.Value);  
        }  
        public void Update(RoleBusinessModel input)  
        {  
            if (input.RoleId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.RoleId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<Role>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<RoleDataTransferModel>> GetAll(Expression<Func<RoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<RoleDataTransferModel>>(result);  
        }  
        public async Task<IList<RoleDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<RoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "RoleId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<RoleDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<RoleBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<RoleBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<RoleBusinessModel>(result);  
        }  
private Role Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(RoleBusinessModel), includeProperties);  
            return _repository.Get(x => x.RoleId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<Role> GetQueryable(Expression<Func<RoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<Role, bool>>? filterExpr = Extensions.ConvertExpression<RoleBusinessModel, Role>(filter);  
            includeProperties = DaoMapper.Map(typeof(RoleBusinessModel), includeProperties);  
            IQueryable<Role> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<Role>(orderBy, query);  
            return query;  
        }  
    }  
}  
