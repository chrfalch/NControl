using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using SQLite.Net.Attributes;
using NControlDemo.Forms.Xamarin.Plugins.Repositories;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Models;

namespace NControlDemo.Forms.Xamarin.Plugins.Data.Tests.Mocks
{
    public class RepositoryMock<TModel>: IRepository<TModel>
		where TModel:RepositoryModel, new()
	{
		private readonly Dictionary<string, object> _dict = 
			new Dictionary<string, object>();

        public bool Initialized { get; set; }

		#region IRepository implementation

		/// <summary>
		/// Initializes the provider.
		/// </summary>
		/// <returns>The async.</returns>
		public Task InitializeAsync ()
		{
            if (!Initialized)
                Initialized = true;

			return Task.FromResult (true);
		}

		/// <summary>
		/// Updated the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task UpdateAsync (TModel entity)
		{
            EnsureInitialized();

			// No need, in memory
			return Task.FromResult (true);
		}

		/// <summary>
		/// Inserts the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task InsertAsync (TModel entity)
		{
            EnsureInitialized();

            // copy object so that we only store properties that Sqlite does not ignore
            var propertyList = entity.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite).ToList();
            var entityToInsert = new TModel();
            foreach (var prop in entityToInsert.GetType().GetProperties().Where(p => p.CanRead && p.CanWrite))
            {
                var myProp = propertyList.SingleOrDefault(p => p.Name == prop.Name);
                if (myProp == null)
                    continue;

                if (myProp.GetCustomAttributes(typeof(IgnoreAttribute), false).Any())
                    continue;

                var value = prop.GetValue(entity);
                myProp.SetValue(entityToInsert, value);
            }

            _dict.Add(entityToInsert.Id, entityToInsert);
            return Task.FromResult (true);
		}

		/// <summary>
		/// Deletes the entity
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="entity">Entity.</param>
		public Task DeleteAsync (TModel entity)
		{
            EnsureInitialized();

			_dict.Remove (entity.Id);
			return Task.FromResult (true);
		}

        /// <summary>
        /// Deletes all async.
        /// </summary>
        /// <returns>The all async.</returns>
        public Task DeleteAllAsync()
        {
            EnsureInitialized();

            _dict.Clear();
            return Task.FromResult(true);
        }

		/// <summary>
		/// Returns items async
		/// </summary>
		/// <returns>The items async.</returns>
		public Task<IEnumerable<TModel>> GetItemsAsync ()
		{
            EnsureInitialized();

			var retVal = new List<TModel> ();
			foreach (var value in _dict.Values)
				retVal.Add (value as TModel);

			return Task.FromResult (retVal.AsEnumerable());
		}

        /// <summary>
        /// Returns filtered items
        /// </summary>
        /// <param name="predExpr"></param>
        /// <returns></returns>
        public Task<IEnumerable<TModel>> GetItemsAsync(Expression<Func<TModel, bool>> predExpr)
        {
            EnsureInitialized();

            var retVal = new List<TModel>();
            foreach (var value in _dict.Values)
                retVal.Add(value as TModel);

            var func = predExpr.Compile();
            return Task.FromResult(retVal.Where(func).AsEnumerable());
        }

		/// <summary>
		/// Gets the item by identifier.
		/// </summary>
		/// <returns>The item by identifier.</returns>
		/// <param name="id">Identifier.</param>
		public Task<TModel> GetItemByIdAsync (string id)
        {
            EnsureInitialized();

			var retVal = new List<TModel> ();
			foreach (var value in _dict.Values.Where(mn => (mn as TModel).Id == id))
				retVal.Add (value as TModel);

			return Task.FromResult (retVal.FirstOrDefault());
		}

        /// <summary>
		/// Gets the count async.
		/// </summary>
		/// <returns>The count async.</returns>
		public Task<int> GetCountAsync ()
        {
            EnsureInitialized();
			
            return Task.FromResult (_dict.Values.Count);
		}
		#endregion

        /// <summary>
        /// Ensures that the class has been initialized and raises an exception if not
        /// </summary>
        private void EnsureInitialized()
        {
            if (Initialized)
                return;

            throw new InvalidOperationException("Repository needs to be initialized before it is used.");
        }
    }
}

