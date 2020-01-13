using System;
using System.Collections.Generic;
using System.Text;

namespace Haliaha.DAL.Core.Interfaces
{
	/// <summary>
	/// Represents the identifiable entity
	/// </summary>
	/// <typeparam name="TKey">Identifier type</typeparam>
	public interface IKeyable<TKey>
	{
		/// <summary>
		/// Gets object's id
		/// </summary>
		TKey Id { get; }
	}
}
