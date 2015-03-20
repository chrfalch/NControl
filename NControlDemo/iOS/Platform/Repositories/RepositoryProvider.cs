using System;
using NControlDemo.Forms.Xamarin.Plugins.Data.Repositories;
using System.Threading.Tasks;
using System.IO;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Models;
using NControlDemo.Forms.Xamarin.Plugins.Contracts.Repositories;

namespace NControlDemo.Forms.Xamarin.Plugins.iOS.Platform.Repositories
{
	/// <summary>
	/// Repositoryi OS platform.
	/// </summary>
    public class RepositoryProvider: IRepositoryProvider
	{
        #region Private Members

        /// <summary>
        /// The connection.
        /// </summary>
        private SQLiteConnectionWithLock _connection;

        #endregion

        #region IRepositoryProvider implementation

        /// <summary>
        /// Gets the SQL connection.
        /// </summary>
        /// <returns>The SQL connection.</returns>
        public SQLiteConnectionWithLock GetSQLConnection()
        {
            if (_connection != null)
                return _connection;

            var folder = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            var filename = Path.Combine (folder, "storage.db");
            _connection = new SQLiteConnectionWithLock (
                new SQLitePlatformIOS(), new SQLiteConnectionString(
                    filename, false, null));

            return _connection;
        }

        #endregion

	}
}

