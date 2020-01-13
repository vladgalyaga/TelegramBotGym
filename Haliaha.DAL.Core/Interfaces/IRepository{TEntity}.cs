using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Haliaha.DAL.Core.Interfaces
{
	/// <summary>
	/// Provides a method for CRUD actions with <typeparamref name="TEntity"/> repository
	/// </summary>
	/// <typeparam name="TEntity">Type of entity, that stores in the repository</typeparam>
	/// <typeparam name="TKey">Type of entity's identifier</typeparam>
	public interface IRepository<TEntity, TKey> : ISaveable, IDisposable where TEntity : class, IKeyable<TKey>
	{
		IQueryable<TEntity> BuildQuery(Expression<Func<TEntity, bool>> filter, string includeProperties = "");
		event DisposeMe DisposeMe;

		void CreateRange(IEnumerable<TEntity> entities);
		bool Any(Func<TEntity, bool> predicate);
		/// <summary>
		/// Returns the first element of the sequence that satisfies the specified condition, or the default value, if no such element is found.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		TEntity GetFirstOrDefault(Func<TEntity, bool> predicate);
		/// <summary>
		/// returns the first object in the specified collection. Returns Nothing if no first object exists, for example, if there are no objects in the collection.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		TEntity GetFirst(Func<TEntity, bool> predicate);
		/// <summary>
		/// Specifies the related objects included in query results.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		IQueryable<TEntity> Include(string path);
		/// <summary>
		/// Removes the given entity from the context underlying the set with each entity being put into the Deleted state such that it will be deleted from the database when SaveChanges is called.
		/// </summary>
		/// <param name="entity"></param>
		void Remove(TEntity entity);
		/// <summary>
		/// Removes the given collection of entities from the context underlying the set with each entity being put into the Deleted state such that it will be deleted from the database when SaveChanges is called.
		/// </summary>
		/// <param name="entities">Ids of entities to remove</param>
		void RemoveRange(TKey[] entities);
		/// <summary>
		/// Removes the given collection of entities from the context underlying the set with each entity being put into the Deleted state such that it will be deleted from the database when SaveChanges is called.
		/// </summary>
		/// <param name="entities">entities to remove</param>
		void RemoveRange(IEnumerable<TEntity> entities);
		/// <summary>
		/// Returns a single element of the sequence or the default value if the sequence is empty; This method throws an exception if there are more than one element in the sequence.
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		TEntity GetSingleOrDefault(Func<TEntity, bool> predicate);
		/// <summary>
		/// Finds an entity with the given primary key values. If an entity with the given primary key values exists in the context, then it is returned immediately without making a request to the store. Otherwise, a request is made to the store for an entity with the given primary key values and this entity, if found, is attached to the context and returned. If no entity is found in the context or the store, then null is returned.
		/// </summary>
		/// <param name="keyValues"></param>
		/// <returns></returns>
		TEntity Find(params Object[] keyValues);
		/// <summary>
		/// Returns true if entity exist in the repository
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		Task<bool> ContainsAsync(TEntity entity);

		/// <summary>
		/// Gets all instances
		/// </summary>
		/// <returns></returns>

		Task<List<TEntity>> GetAllAsync();
		/// <summary>
		/// Gets all instances
		/// </summary>
		/// <returns></returns>
		List<TEntity> GetAll();
		/// <summary>
		/// Returns a list of elements, that satisfy a predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		/// 
		List<TEntity> GetWhere(Func<TEntity, bool> predicate);

		/// <summary>
		/// Returns a list of elements, that satisfy a predicate witout tracking
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		List<TEntity> GetWhereAsNoTracking(Func<TEntity, bool> predicate);
		List<TEntity> GetWhereAsNoTracking<TProperty>(Expression<Func<TEntity, TProperty>> includePredicate, Func<TEntity, bool> predicate);

		/// <summary>
		/// Returns a list of elements, that satisfy a predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		Task<List<TEntity>> GetWhereAsync(Func<TEntity, bool> predicate);

		/// <summary>
		/// Look for an entity
		/// </summary>
		/// <param name="id">Entity's identifier</param>
		/// <returns>Instance of <typeparamref name="TEntity"/> if success, else null</returns>
		Task<TEntity> FindByIdAsync(TKey id);

		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <param name="entity"></param>
		Task CreateAsync(TEntity entity);
		/// <summary>
		/// Insert an entity
		/// </summary>
		/// <param name="entity"></param>
		void Create(TEntity entity);
		/// <summary>
		/// Update an entity
		/// </summary>
		/// <param name="entity"></param>
		void Update(TEntity entity);
		/// <summary>
		/// Update an entity
		/// </summary>
		/// <param name="entity"></param>
		Task UpdateAsync(TEntity entity);

		/// <summary>
		/// Delete an entity by it's key
		/// </summary>
		/// <param name="entityId">Entity identifier</param>
		Task DeleteAsync(TKey entityId);
	}
}
