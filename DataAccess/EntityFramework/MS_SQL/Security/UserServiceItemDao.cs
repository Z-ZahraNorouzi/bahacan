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
    public class UserServiceItemDao : IUserServiceItemDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<UserServiceItem> _repository;  
        public UserServiceItemDao(DbContext context)  
        {  
            _repository = new GenericRepository<UserServiceItem>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(UserServiceItemBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<UserServiceItem>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.UserServiceItemId = obj.UserServiceItemId;  
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
        public void Delete(UserServiceItemBusinessModel input)  
        {  
            if (input.UserServiceItemId.HasValue)  
                Delete(input.UserServiceItemId.Value);  
        }  
        public void Update(UserServiceItemBusinessModel input)  
        {  
            if (input.UserServiceItemId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.UserServiceItemId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<UserServiceItem>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<UserServiceItemDataTransferModel>> GetAll(Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<UserServiceItemDataTransferModel>>(result);  
        }  
        public async Task<IList<UserServiceItemDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "UserServiceItemId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<UserServiceItemDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<UserServiceItemBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<UserServiceItemBusinessModel>(result);  
        }  
private UserServiceItem Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(UserServiceItemBusinessModel), includeProperties);  
            return _repository.Get(x => x.UserServiceItemId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<UserServiceItem> GetQueryable(Expression<Func<UserServiceItemBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<UserServiceItem, bool>>? filterExpr = Extensions.ConvertExpression<UserServiceItemBusinessModel, UserServiceItem>(filter);  
            includeProperties = DaoMapper.Map(typeof(UserServiceItemBusinessModel), includeProperties);  
            IQueryable<UserServiceItem> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<UserServiceItem>(orderBy, query);  
            return query;  
        }  
    }  
}  
