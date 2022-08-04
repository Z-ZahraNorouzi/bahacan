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
    public class UserInRoleDao : IUserInRoleDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<UserInRole> _repository;  
        public UserInRoleDao(DbContext context)  
        {  
            _repository = new GenericRepository<UserInRole>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(UserInRoleBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<UserInRole>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.UserInRoleId = obj.UserInRoleId;  
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
        public void Delete(UserInRoleBusinessModel input)  
        {  
            if (input.UserInRoleId.HasValue)  
                Delete(input.UserInRoleId.Value);  
        }  
        public void Update(UserInRoleBusinessModel input)  
        {  
            if (input.UserInRoleId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.UserInRoleId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<UserInRole>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<UserInRoleDataTransferModel>> GetAll(Expression<Func<UserInRoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<UserInRoleDataTransferModel>>(result);  
        }  
        public async Task<IList<UserInRoleDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserInRoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "UserInRoleId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<UserInRoleDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<UserInRoleBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<UserInRoleBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<UserInRoleBusinessModel>(result);  
        }  
private UserInRole Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(UserInRoleBusinessModel), includeProperties);  
            return _repository.Get(x => x.UserInRoleId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<UserInRole> GetQueryable(Expression<Func<UserInRoleBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<UserInRole, bool>>? filterExpr = Extensions.ConvertExpression<UserInRoleBusinessModel, UserInRole>(filter);  
            includeProperties = DaoMapper.Map(typeof(UserInRoleBusinessModel), includeProperties);  
            IQueryable<UserInRole> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<UserInRole>(orderBy, query);  
            return query;  
        }  
    }  
}  
