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
    public class LoginHistoryDao : ILoginHistoryDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<LoginHistory> _repository;  
        public LoginHistoryDao(DbContext context)  
        {  
            _repository = new GenericRepository<LoginHistory>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(LoginHistoryBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<LoginHistory>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.LoginHistoryId = obj.LoginHistoryId;  
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
        public void Delete(LoginHistoryBusinessModel input)  
        {  
            if (input.LoginHistoryId.HasValue)  
                Delete(input.LoginHistoryId.Value);  
        }  
        public void Update(LoginHistoryBusinessModel input)  
        {  
            if (input.LoginHistoryId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.LoginHistoryId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<LoginHistory>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<LoginHistoryDataTransferModel>> GetAll(Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<LoginHistoryDataTransferModel>>(result);  
        }  
        public async Task<IList<LoginHistoryDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "LoginHistoryId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<LoginHistoryDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<LoginHistoryBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<LoginHistoryBusinessModel>(result);  
        }  
private LoginHistory Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(LoginHistoryBusinessModel), includeProperties);  
            return _repository.Get(x => x.LoginHistoryId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<LoginHistory> GetQueryable(Expression<Func<LoginHistoryBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<LoginHistory, bool>>? filterExpr = Extensions.ConvertExpression<LoginHistoryBusinessModel, LoginHistory>(filter);  
            includeProperties = DaoMapper.Map(typeof(LoginHistoryBusinessModel), includeProperties);  
            IQueryable<LoginHistory> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<LoginHistory>(orderBy, query);  
            return query;  
        }  
    }  
}  
