using System;
using SQLite.Net;

namespace NControlDemo.Forms.Xamarin.Plugins.Contracts.Repositories
{
    /// <summary>
    /// Provider for the repository
    /// </summary>
    public interface IRepositoryProvider
    {
        /// <summary>
        /// Gets the SQL connection.
        /// </summary>
        /// <returns>The SQL connection.</returns>
        SQLiteConnectionWithLock GetSQLConnection();
    }
}

