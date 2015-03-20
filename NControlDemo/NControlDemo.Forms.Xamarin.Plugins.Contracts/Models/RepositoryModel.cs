using System;
using SQLite.Net.Attributes;

namespace NControlDemo.Forms.Xamarin.Plugins.Contracts.Models
{
    /// <summary>
    /// I repository model.
    /// </summary>
    public class RepositoryModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        [PrimaryKey]
        public string Id {get; set;}
    }
}

