using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Haliaha.DAL.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Haliaha.DAL.Core
{
	/// <summary>
	/// Provides CRUD actions with <typeparamref name="TEntity"/>
	/// </summary>
	/// <typeparam name="TEntity"></typeparam>
	public class EntityRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IKeyable<TKey>
	{
		protected IEntitiesDbContext m_DbContext;
		private bool m_IsDisposed;
		public event DisposeMe DisposeMe;

		public EntityRepository(IEntitiesDbContext dbContext)
		{
			this.DbContext = dbContext;
		}

		protected DbSet<TEntity> DbEntitySet { get; private set; }

		protected IQueryable<TEntity> queryableEntity;
		protected virtual IQueryable<TEntity> QueryableEntity { get { return queryableEntity; } private set { queryableEntity = value; } }

		public IEntitiesDbContext DbContext
		{
			get { return m_DbContext; }
			set
			{
				if (value != null)
				{
					this.m_DbContext = value;
				}
				else throw new ArgumentNullException(nameof(DbContext));

				this.DbEntitySet = value.TryGetSet<TEntity, TKey>();
				QueryableEntity = DbEntitySet.AsQueryable();
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		protected virtual void Dispose(bool disposing)
		{
			if (!m_IsDisposed && disposing)
			{
				DisposeMe?.Invoke(typeof(TEntity).Name);
				// DbContext.Dispose();
			}
			m_IsDisposed = true;
		}

		public virtual Task<bool> ContainsAsync(TEntity entity)
		{
			return Task.Run(() => QueryableEntity.FirstOrDefault(p => p.Id.Equals(entity.Id)) != null);
		}
		public virtual TEntity Find(params Object[] keyValues)
		{
			return DbEntitySet.Find(keyValues);
		}

		public virtual Task<List<TEntity>> GetAllAsync()
		{
			return QueryableEntity.ToListAsync();
		}
		public virtual List<TEntity> GetAll()
		{
			return QueryableEntity.ToList();

		}

		public virtual Task<List<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate)
		{
			return Task.Run(() => QueryableEntity.Where(predicate).ToList());
		}
		
		public virtual List<TEntity> GetWhere(Func<TEntity, bool> predicate)
		{
			return QueryableEntity.Where(predicate).ToList();
		}

		public virtual List<TEntity> GetWhereAsNoTracking(Func<TEntity, bool> predicate)
		{

			return QueryableEntity.AsNoTracking()?.Where(predicate).ToList();

		}
		public virtual List<TEntity> GetWhereAsNoTracking<TProperty>(Expression<Func<TEntity, TProperty>> includePredicate, Func<TEntity, bool> predicate)
		{
			return QueryableEntity.AsNoTracking().Include(includePredicate).Where(predicate).ToList();
		}

		public Task<TEntity> FindByIdAsync(TKey id)
		{
			return QueryableEntity.FirstOrDefaultAsync(p => p.Id.ToString().Equals(id.ToString()));
		}

		public virtual async Task CreateAsync(TEntity entity)
		{
			// DbContext.AddEntity<TEntity, TKey>(entity);
			DbContext.Entry(entity).State = EntityState.Added;
			await SaveChangesAsync();
			//    DbContext.Entry(entity).State = EntityState.Added;
		}

		public virtual void Create(TEntity entity)
		{
			//  DbContext.AddEntity<TEntity, TKey>(entity);
			//   SaveChanges();
			DbContext.Entry(entity).State = EntityState.Added;
			SaveChanges();
		}
		public virtual void CreateRange(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				//  DbContext.AddEntity<TEntity, TKey>(entity);
				DbContext.Entry(entity).State = EntityState.Added;
			}
			SaveChanges();
		}
		public async Task UpdateAsync(TEntity entity)
		{
			DbContext.UpdateEntity<TEntity, TKey>(entity);
			await SaveChangesAsync();
		}
		public void Update(TEntity entity)
		{
			DbContext.UpdateEntity<TEntity, TKey>(entity);

			SaveChanges();

		}

		public async Task DeleteAsync(TEntity entity)
		{
			DbContext.DeleteEntity<TEntity, TKey>(entity);
			await SaveChangesAsync();


		}
		public async Task DeleteAsync(TKey entityId)
		{
			TEntity entity = QueryableEntity.FirstOrDefault(p => p.Id.ToString().Equals(entityId.ToString()));
			await DeleteAsync(entity);
			await SaveChangesAsync();

		}
		public virtual TEntity GetFirstOrDefault(Func<TEntity, bool> predicate)
		{
			return QueryableEntity.FirstOrDefault(predicate);
		}
		public int SaveChanges()
		{
			return DbContext.SaveChanges();
		}
		public Task<int> SaveChangesAsync()
		{
			return DbContext.SaveChangesAsync();
		}

		public virtual TEntity GetSingleOrDefault(Func<TEntity, bool> predicate)
		{
			return QueryableEntity.SingleOrDefault(predicate);
		}
		public void RemoveRange(TKey[] ids)
		{
			if (ids == null) return;
			RemoveRange(GetWhere(x => ids.Contains(x.Id)));
		}
		public void RemoveRange(IEnumerable<TEntity> entities)
		{
			var ent = entities.ToArray();
			for (int i = 0; i < ent.Length; i++)
			{
				DbEntitySet.Remove(ent[i]);
				DbContext.Entry<TEntity>((ent[i])).State = EntityState.Deleted;
			}
			SaveChanges();
		}
		public void Remove(TEntity entity)
		{
			DbEntitySet.Remove(entity);
			SaveChanges();
		}
		public IQueryable<TEntity> Include(string path)
		{
			return QueryableEntity.Include(path);
		}
		public virtual TEntity GetFirst(Func<TEntity, bool> predicate)
		{
			return QueryableEntity.First(predicate);
		}

		public virtual bool Any(Func<TEntity, bool> predicate)
		{
			return QueryableEntity.Any(predicate);
		}
		public virtual IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter = null, string includeProperties = "")
		{
			IQueryable<TEntity> query = DbEntitySet.AsNoTracking();
			foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
			{
				query = query.Include(include);
			}

			if (filter != null)
				query = query.Where(filter);

			return query;
		}
	}
}