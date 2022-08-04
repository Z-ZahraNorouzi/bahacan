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
    public class LoginStatusDao : ILoginStatusDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<LoginStatus> _repository;  
        public LoginStatusDao(DbContext context)  
        {  
            _repository = new GenericRepository<LoginStatus>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(LoginStatusBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<LoginStatus>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.LoginStatusId = obj.LoginStatusId;  
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
        public void Delete(LoginStatusBusinessModel input)  
        {  
            if (input.LoginStatusId.HasValue)  
                Delete(input.LoginStatusId.Value);  
        }  
        public void Update(LoginStatusBusinessModel input)  
        {  
            if (input.LoginStatusId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.LoginStatusId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<LoginStatus>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<LoginStatusDataTransferModel>> GetAll(Expression<Func<LoginStatusBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<LoginStatusDataTransferModel>>(result);  
        }  
        public async Task<IList<LoginStatusDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<LoginStatusBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "LoginStatusId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<LoginStatusDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<LoginStatusBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<LoginStatusBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<LoginStatusBusinessModel>(result);  
        }  
private LoginStatus Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(LoginStatusBusinessModel), includeProperties);  
            return _repository.Get(x => x.LoginStatusId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<LoginStatus> GetQueryable(Expression<Func<LoginStatusBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<LoginStatus, bool>>? filterExpr = Extensions.ConvertExpression<LoginStatusBusinessModel, LoginStatus>(filter);  
            includeProperties = DaoMapper.Map(typeof(LoginStatusBusinessModel), includeProperties);  
            IQueryable<LoginStatus> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<LoginStatus>(orderBy, query);  
            return query;  
        }  
    }  
}  
