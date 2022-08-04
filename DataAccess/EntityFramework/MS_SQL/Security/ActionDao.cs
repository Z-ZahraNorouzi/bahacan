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
    public class ActionDao : IActionDao  
    {  
        private readonly IMapper _mapper;  
        private readonly DbContext _dbContext;  
        private GenericRepository<Model.Action> _repository;  
        public ActionDao(DbContext context)  
        {  
            _repository = new GenericRepository<Model.Action>(context);  
            _dbContext = context;  
            var mapperConfig = new MapperConfiguration(mc =>  
            {  
                mc.AddProfile(new AutoMapperConfig());  
            });  
            _mapper = mapperConfig.CreateMapper();  
        }  
        public void Create(ActionBusinessModel input)  
        {  
            CharacterCorrector.ReplacePersianCodePages(input);  
            var obj = _mapper.Map<Model.Action>(input);  
            _repository.Insert(obj);  
            _repository.Context.SaveChanges();  
            input.ActionId = obj.ActionId;  
        }  
        public void Delete(Int16 input)  
        {  
            var entityToDelete = Get(input);  
            if (entityToDelete != null)  
            {  
                _repository.Delete(entityToDelete);  
                _repository.Context.SaveChanges();  
            }  
        }  
        public void Delete(ActionBusinessModel input)  
        {  
            if (input.ActionId.HasValue)  
                Delete(input.ActionId.Value);  
        }  
        public void Update(ActionBusinessModel input)  
        {  
            if (input.ActionId.HasValue)  
            {  
                CharacterCorrector.ReplacePersianCodePages(input);  
                var entityToUpdate = Get(input.ActionId.Value);  
                if (entityToUpdate != null)  
                {  
                    var obj = _mapper.Map<Model.Action>(input);  
                    _repository.Update(entityToUpdate, obj);  
                    _repository.Context.SaveChanges();  
                }  
            }  
        }  
        public async Task<IList<ActionDataTransferModel>> GetAll(Expression<Func<ActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            var result = await GetQueryable(filter, orderBy, includeProperties).ToListAsync();  
            return _mapper.Map<IList<ActionDataTransferModel>>(result);  
        }  
        public async Task<IList<ActionDataTransferModel>> GetAll(int pageNumber, int pageSize, Expression<Func<ActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            orderBy = (string.IsNullOrEmpty(orderBy) ? "ActionId" : orderBy);  
            var result = await GetQueryable(filter, orderBy, includeProperties).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();  
            return _mapper.Map<IList<ActionDataTransferModel>>(result);  
        }  
        public async Task<int> GetAllCount(Expression<Func<ActionBusinessModel, bool>>? filter = null)  
        {  
            return await GetQueryable(filter).CountAsync();  
        }  
        public async Task<ActionBusinessModel?> GetByKey(Int16 input, string includeProperties = "")  
        {  
            var result = Get(input, includeProperties);  
            return _mapper.Map<ActionBusinessModel>(result);  
        }  
        private Model.Action Get(Int16 input, string includeProperties = "")  
        {  
            includeProperties = DaoMapper.Map(typeof(ActionBusinessModel), includeProperties);  
            return _repository.Get(x => x.ActionId == input, null, includeProperties).FirstOrDefault();  
        }  
        private IQueryable<Model.Action> GetQueryable(Expression<Func<ActionBusinessModel, bool>>? filter = null, string orderBy = "", string includeProperties = "")  
        {  
            Expression<Func<Model.Action, bool>>? filterExpr = Extensions.ConvertExpression<ActionBusinessModel, Model.Action>(filter);  
            includeProperties = DaoMapper.Map(typeof(ActionBusinessModel), includeProperties);  
            IQueryable<Model.Action> query = _repository.Get(filterExpr, null, includeProperties);  
            query = Extensions.OrderBy<Model.Action>(orderBy, query);  
            return query;  
        }  
    }  
}  
