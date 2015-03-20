using System;
using SQLite.Net.Async;
using System.Threading.Tasks;
using SQLite.Net;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using NControlDemo.Forms.Xamarin.Plugins.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Models;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Repositories;

namespace NControlDemo.Forms.Xamarin.Plugins.Data.Repositories
{
	/// <summary>
	/// Implements the base Repository class for building SQLite database repositories 
	/// </summary>
	public class Repository<TModel>: IRepository<TModel>
        where TModel  : RepositoryModel, new()			
	{
		#region Private Members

		/// <summary>
		/// The connection.
		/// </summary>
		private SQLiteAsyncConnection _connection;

        /// <summary>
        /// The initialized flag.
        /// </summary>
        private bool _initializedFlag = false;

        /// <summary>
        /// The repository provider.
        /// </summary>
        private readonly IRepositoryProvider _repositoryProvider;

		#endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="NControlDemo.Forms.Xamarin.Plugins.Data.Repositories.Repository`1"/> class.
        /// </summary>
        /// <param name="repositoryProvider">Repository provider.</param>
        public Repository(IRepositoryProvider repositoryProvider)
        {
            _repositoryProvider = repositoryProvider;
        }

		#region IRepository implementation

		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <returns>The async.</returns>
		public Task InitializeAsync()
		{
            var connection = _repositoryProvider.GetSQLConnection();

            _initializedFlag = true;
			_connection = new SQLiteAsyncConnection (() => connection);
			return _connection.CreateTableAsync<TModel> ();
		}

		/// <summary>
		/// Updated the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task UpdateAsync (TModel entity) 
		{
            EnsureInitialized();

			return _connection.UpdateAsync (entity);
		}

		/// <summary>
		/// Inserts the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task InsertAsync (TModel entity) 
		{
            EnsureInitialized();

			return _connection.InsertAsync (entity);
		}

		/// <summary>
		/// Deletes the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task DeleteAsync (TModel entity) 
		{
            EnsureInitialized();

			return _connection.DeleteAsync (entity);
		}

        /// <summary>
        /// Deletes all entities
        /// </summary>
        public Task DeleteAllAsync () 
        {
            EnsureInitialized();

            return _connection.DeleteAllAsync<TModel>(default(CancellationToken));
        }

		/// <summary>
		/// Returns items async
		/// </summary>
		/// <returns>The items async.</returns>
		public async Task<IEnumerable<TModel>> GetItemsAsync () 
		{
            EnsureInitialized();

			var retVal = await _connection.Table<TModel> ().ToListAsync();
			return retVal.Cast<TModel> ();
		}

		/// <summary>
		/// Gets the item by identifier.
		/// </summary>
		/// <returns>The item by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public async Task<TModel> GetItemByIdAsync (string id) 
		{
            EnsureInitialized();

			var retVal = await _connection.Table<TModel> ().Where (mn => mn.Id == id).FirstOrDefaultAsync ();
			if (retVal == null)
				return default(TModel);

			return (TModel)retVal;
		}

		/// <summary>
		/// Returns the number of items in the repository
		/// </summary>
		/// <returns>The count async.</returns>
		public Task<int> GetCountAsync()
		{
            EnsureInitialized();

			return _connection.Table<TModel> ().CountAsync ();
		}

        /// <summary>
        /// Filtered items
        /// </summary>
        /// <param name="predExpr"></param>
        /// <returns></returns>
        public async Task<IEnumerable<TModel>> GetItemsAsync(Expression<Func<TModel, bool>> predExpr)
        {
            EnsureInitialized();

            var retVal = await _connection.Table<TModel>().Where(predExpr).ToListAsync();
            return retVal.Cast<TModel>();
        }
		#endregion

        #region Private Members

        /// <summary>
        /// Ensures that the class has been initialized and raises an exception if not
        /// </summary>
        private void EnsureInitialized()
        {
            if (_initializedFlag)
                return;

            throw new InvalidOperationException("Repository needs to be initialized before it is used.");
        }
        #endregion
    }
}

