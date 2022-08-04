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
    public class UserInfoDao : IUserInfoDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<UserInfo> _repository;  
        public UserInfoDao(DbContext context)  
        {  
            _repository = new GenericRepository<UserInfo>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(UserInfoBusinessModel input)
        {
            try
            {
                CharacterCorrector.ReplacePersianCodePages(input);
                var obj = _mapper.Map<UserInfo>(input);
                _repository.Insert(obj);
                _repository.Context.SaveChanges();
                input.UserInfoId = obj.UserInfoId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
              
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
        public void Delete(UserInfoBusinessModel input)  
        {  
            if (input.UserInfoId.HasValue)  
                Delete(input.UserInfoId.Value);  
        }  
        public void Update(UserInfoBusinessModel input)  
        {  
            if (input.UserInfoId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.UserInfoId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<UserInfo>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<UserInfoDataTransferModel>> GetAll(Expression<Func<UserInfoBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")
        {
            try
            {
                var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();
                return _mapper.Map<IList<UserInfoDataTransferModel>>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }  
        public async Task<IList<UserInfoDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<UserInfoBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "UserInfoId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<UserInfoDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<UserInfoBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<UserInfoBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<UserInfoBusinessModel>(result);  
        }  
private UserInfo Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(UserInfoBusinessModel), includeProperties);  
            return _repository.Get(x => x.UserInfoId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<UserInfo> GetQueryable(Expression<Func<UserInfoBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<UserInfo, bool>>? filterExpr = Extensions.ConvertExpression<UserInfoBusinessModel, UserInfo>(filter);  
            includeProperties = DaoMapper.Map(typeof(UserInfoBusinessModel), includeProperties);  
            IQueryable<UserInfo> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<UserInfo>(orderBy, query);  
            return query;  
        }  
    }  
}  
