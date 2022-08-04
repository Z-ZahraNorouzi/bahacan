using DataAccess.EntityFrameWork;  
using Microsoft.EntityFrameworkCore;  
using BusinessModel; 
using DataTransferModel.Security;  
using System.Linq.Expressions;  
using DataLayer.Interface;  
using AutoMapper;  
using DataAccess.EntityFramework.Model;
using Microsoft.Data.SqlClient;

namespace DataAccess.EntityFramework.MS_SQL  
{  
    public class ServiceDao : IServiceDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<Service> _repository;  
        public ServiceDao(DbContext context)  
        {  
            _repository = new GenericRepository<Service>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(ServiceBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<Service>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.ServiceId = obj.ServiceId;  
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
        public void Delete(ServiceBusinessModel input)  
        {  
            if (input.ServiceId.HasValue)  
                Delete(input.ServiceId.Value);  
        }  
        public void Update(ServiceBusinessModel input)  
        {  
            if (input.ServiceId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.ServiceId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<Service>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<ServiceDataTransferModel>> GetAll(Expression<Func<ServiceBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<ServiceDataTransferModel>>(result);  
        }  
        public async Task<IList<ServiceDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ServiceBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "ServiceId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<ServiceDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<ServiceBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<ServiceBusinessModel?> GetByKey(Int64 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<ServiceBusinessModel>(result);  
        }  
        private Service Get(Int64 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(ServiceBusinessModel), includeProperties);  
            return _repository.Get(x => x.ServiceId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<Service> GetQueryable(Expression<Func<ServiceBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<Service, bool>>? filterExpr = Extensions.ConvertExpression<ServiceBusinessModel, Service>(filter);  
            includeProperties = DaoMapper.Map(typeof(ServiceBusinessModel), includeProperties);  
            IQueryable<Service> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<Service>(orderBy, query);  
            return query;  
        }
        public async Task<IList<ServiceDataTransferModel>> GetByUserId(Int64 input)
        {
            TrPosContext roshaSoftCloudERPContext = new TrPosContext();
            SqlParameter UserInfoIdParameter = new SqlParameter("@UserInfoId", System.Data.SqlDbType.BigInt);
            UserInfoIdParameter.SqlValue = input;
            var result = roshaSoftCloudERPContext
            .Services
            .FromSqlRaw("exec security.sp_Services_GetByUserInfoId @UserInfoId", UserInfoIdParameter)
            .ToList();
            return _mapper.Map<IList<ServiceDataTransferModel>>(result);
        }

    }
}  
