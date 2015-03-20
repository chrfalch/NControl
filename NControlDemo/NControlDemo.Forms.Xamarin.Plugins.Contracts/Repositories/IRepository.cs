using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Models;

namespace NControlDemo.Forms.Xamarin.Plugins.Repositories
{
    public interface IRepository<TModel> where TModel: RepositoryModel, new()
	{
		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <returns>The async.</returns>
		Task InitializeAsync () ;

		/// <summary>
		/// Updated the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		Task UpdateAsync(TModel entity) ;

		/// <summary>
		/// Inserts the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		Task InsertAsync (TModel entity);

		/// <summary>
		/// Deletes the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		Task DeleteAsync (TModel entity);

        /// <summary>
        /// Deletes all async.
        /// </summary>
        /// <returns>The all async.</returns>
        Task DeleteAllAsync();

		/// <summary>
		/// Returns items async
		/// </summary>
		/// <returns>The items async.</returns>
        Task<IEnumerable<TModel>> GetItemsAsync();

		/// <summary>
		/// Gets the item by identifier.
		/// </summary>
		/// <returns>The item by identifier.</returns>
		/// <param name="id">Identifier.</param>
		Task<TModel> GetItemByIdAsync (string id);

        /// <summary>
        /// Returns items with a filter
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<IEnumerable<TModel>> GetItemsAsync(Expression<Func<TModel, bool>> predExpr);

		/// <summary>
		/// Gets the count async.
		/// </summary>
		/// <returns>The count async.</returns>
		Task<int> GetCountAsync();

	}
}

