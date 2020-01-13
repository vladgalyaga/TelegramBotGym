using System;
using System.Collections.Generic;
using System.Text;

namespace Haliaha.DAL.Core.Interfaces
{
	/// <summary>
	/// Represents an unit container for all repositories
	/// </summary>
	public interface IUnitOfWork : IDisposable, ISaveable
	{
		/// <summary>
		/// Returns a repository to interaction with <typeparamref name="TEntity"/> instances
		/// </summary>
		/// <typeparam name="TEntity">Entity type, for which repository is required</typeparam>
		/// <typeparam name="TKey">Entity's id type</typeparam>
		/// <returns></returns>
		IRepository<TEntity, TKey> GetRepository<TEntity, TKey>(bool createSpecificRepositiry = true)
			where TEntity : class, IKeyable<TKey>;
	}
}