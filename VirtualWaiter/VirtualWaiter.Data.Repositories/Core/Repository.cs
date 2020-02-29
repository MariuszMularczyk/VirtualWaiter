using VirtualWaiter.Domain;
using VirtualWaiter.EntityFramework;
using VirtualWaiter.Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VirtualWaiter.Data
{
    public class Repository<TEntity, TDbContext> : IRepository<TEntity> where TEntity : class where TDbContext : DbContext
    {
        protected readonly DbSet<TEntity> _dbset;
        protected readonly TDbContext Context;

        [Inject]
        public MainContext MainContext { get; set; }

        public Repository(TDbContext context)
        {
            Context = context;
            _dbset = Context.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            if (entity is IAuditEntity)
            {
                var auditObject = entity as IAuditEntity;
                auditObject.CreatedById = MainContext.PersonId;

                if (auditObject.CreatedDate == DateTime.MinValue)
                {
                    auditObject.CreatedDate = DateTime.Now;
                }
            }
            _dbset.Add(entity);
        }
        public virtual void AddRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is IAuditEntity)
                {
                    var auditObject = entity as IAuditEntity;
                    auditObject.CreatedById = MainContext.PersonId;

                    if (auditObject.CreatedDate == DateTime.MinValue)
                    {
                        auditObject.CreatedDate = DateTime.Now;
                    }
                }
                _dbset.Add(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            _dbset.Remove(entity);
        }

        public void DeleteWhere(Expression<Func<TEntity, bool>> whereCondition)
        {
            Context.Set<TEntity>().RemoveRange(Context.Set<TEntity>().Where(whereCondition));
        }

        protected IQueryable<TEntity> GetQueryable()
        {
            return _dbset.AsQueryable();
        }


        public IList<TEntity> GetAll(bool tracking = false)
        {
            if (tracking)
            {
                return _dbset.ToList();
            }
            return _dbset.AsNoTracking().ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(bool tracking = false)
        {
            if (tracking)
            {
                return await _dbset.ToListAsync();
            }
            return await _dbset.AsNoTracking().ToListAsync();
        }


        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false)
        {
            if (tracking)
            {
                return _dbset.Where(whereCondition).ToList();
            }
            return _dbset.AsNoTracking().Where(whereCondition).ToList();

        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false)
        {
            if (tracking)
            {
                return await _dbset.Where(whereCondition).ToListAsync();
            }
            return await _dbset.AsNoTracking().Where(whereCondition).ToListAsync();

        }

        public TEntity GetSingle(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false)
        {
            if (tracking)
            {
                return _dbset.Where(whereCondition).FirstOrDefault();
            }
            return _dbset.AsNoTracking().Where(whereCondition).FirstOrDefault();
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false)
        {
            if (tracking)
            {
                return await _dbset.Where(whereCondition).FirstOrDefaultAsync();
            }
            return await _dbset.AsNoTracking().Where(whereCondition).FirstOrDefaultAsync();

        }

        public void Attach(TEntity entity)
        {
            _dbset.Attach(entity);
        }

        public void Detach(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Detached;
        }

        public IQueryable<TEntity> GetQueryable(bool tracking = false)
        {
            if (tracking == false)
            {
                return _dbset.AsNoTracking();
            }
            return _dbset.AsQueryable();

        }

        public IQueryable<TEntity> GetQueryable(Expression<Func<TEntity, bool>> whereCondition, bool tracking = false)
        {
            if (tracking)
            {
                return _dbset.AsQueryable().Where(whereCondition);
            }
            return _dbset.AsNoTracking().Where(whereCondition);
        }

        public int Count()
        {
            return _dbset.Count();
        }
        public async Task<int> CountAsync()
        {
            return await _dbset.CountAsync();
        }
        public bool Any()
        {
            return _dbset.Any();
        }
        public async Task<bool> AnyAsync()
        {
            return await _dbset.AnyAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _dbset.FirstOrDefaultAsync(whereCondition);
        }

        public int Count(Expression<Func<TEntity, bool>> whereCondition)
        {
            return _dbset.Where(whereCondition).Count();
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _dbset.Where(whereCondition).CountAsync();
        }
        public bool Any(Expression<Func<TEntity, bool>> whereCondition)
        {
            return _dbset.Any(whereCondition);
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> whereCondition)
        {
            return await _dbset.AnyAsync(whereCondition);
        }

        public void Edit(TEntity entity)
        {
            if (Context.Entry(entity).State != EntityState.Added)
            {
                Context.Entry(entity).State = EntityState.Modified;
                if (entity is IAuditEntity)
                {
                    var auditObject = entity as IAuditEntity;
                    auditObject.ModifiedById = MainContext.PersonId;
                    auditObject.ModifiedDate = DateTime.Now;
                }
            }
        }

        public void EditRange(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                Edit(entity);
            }
        }

        public void AddOrEdit<TEn>(TEn entity) where TEn : Entity, TEntity
        {
            if (entity.Id == 0 || entity.Id == -1)
            {
                Add(entity);
            }
            else
            {
                Edit(entity);
            }
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }

        public GetDataResult<TResult> GetData<TResult>(DxGridParams gridParams, IQueryable<TResult> query)
        {
            GetDataResult<TResult> result = new GetDataResult<TResult>();
            query = query.FilterByOptions(gridParams); 

            result.TotalCount = query.LongCount();

            query = query.SortByOptions(gridParams)   
              .PageByOptions(gridParams);

            result.ItemList = query.ToList<TResult>();

            return result;
        }

        public GetDataResult GetData(DxGridParams gridParams, IQueryable query)
        {
            IQueryable<dynamic> queryOb = query as IQueryable<dynamic>;
            GetDataResult result = new GetDataResult();
            queryOb = queryOb.FilterByOptions<dynamic>(gridParams);  

            result.TotalCount = queryOb.LongCount();

            queryOb = queryOb.SortByOptions(gridParams)    
              .PageByOptions(gridParams); 

            result.ItemList = queryOb.ToList<dynamic>();

            return result;
        }

        public GetDataResult GetData(DxGridParams gridParams, IQueryable<dynamic> query)
        {
            GetDataResult result = new GetDataResult();
            query = query.FilterByOptions<dynamic>(gridParams); 

            result.TotalCount = query.LongCount();

            query = query.SortByOptions(gridParams)  
              .PageByOptions(gridParams);    

            result.ItemList = query.ToList<object>();

            return result;
        }
        public TEntity Find(int id)
        {
            return _dbset.Find(id);
        }

    }

    public class Repository : IRepository
    {
        protected readonly MainDatabaseContext Context;

        [Inject]
        public MainContext MainContext { get; set; }
        public Repository(MainDatabaseContext context)
        {
            Context = context;
        }

        public GetDataResult GetData(DxGridParams gridParams, IQueryable query)
        {
            IQueryable<dynamic> queryOb = query as IQueryable<dynamic>;
            GetDataResult result = new GetDataResult();
            queryOb = queryOb.FilterByOptions<dynamic>(gridParams); 

            result.TotalCount = queryOb.LongCount();

            queryOb = queryOb.SortByOptions(gridParams)     
              .PageByOptions(gridParams);   

            result.ItemList = queryOb.ToList<dynamic>();

            return result;
        }

        public GetDataResult GetData(DxGridParams gridParams, IQueryable<dynamic> query)
        {
            GetDataResult result = new GetDataResult();
            query = query.FilterByOptions<dynamic>(gridParams);   

            result.TotalCount = query.LongCount();

            query = query.SortByOptions(gridParams)    
              .PageByOptions(gridParams);  

            result.ItemList = query.ToList<object>();

            return result;
        }
    }

    public class Repository<TDbContext> : IRepository where TDbContext : DbContext
    {
        protected readonly TDbContext Context;

        [Inject]
        public MainContext MainContext { get; set; }
        public Repository(TDbContext context)
        {
            Context = context;
        }
        public GetDataResult<TResult> GetData<TResult>(DxGridParams gridParams, IQueryable<TResult> query)
        {
            GetDataResult<TResult> result = new GetDataResult<TResult>();
            query = query.FilterByOptions(gridParams);   

            result.TotalCount = query.LongCount();

            query = query.SortByOptions(gridParams)     
              .PageByOptions(gridParams);    

            result.ItemList = query.ToList<TResult>();

            return result;
        }

        public GetDataResult GetData(DxGridParams gridParams, IQueryable query)
        {
            IQueryable<dynamic> queryOb = query as IQueryable<dynamic>;
            GetDataResult result = new GetDataResult();
            queryOb = queryOb.FilterByOptions<dynamic>(gridParams);   

            result.TotalCount = queryOb.LongCount();

            queryOb = queryOb.SortByOptions(gridParams)    
              .PageByOptions(gridParams);    

            result.ItemList = queryOb.ToList<dynamic>();

            return result;
        }



        public GetDataResult GetData(DxGridParams gridParams, IQueryable<dynamic> query)
        {
            GetDataResult result = new GetDataResult();
            query = query.FilterByOptions<dynamic>(gridParams); 

            result.TotalCount = query.LongCount();

            query = query.SortByOptions(gridParams)     
              .PageByOptions(gridParams);  

            result.ItemList = query.ToList<object>();

            return result;
        }

        public void Edit(object entity)
        {
            if (Context.Entry(entity).State != EntityState.Added)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
