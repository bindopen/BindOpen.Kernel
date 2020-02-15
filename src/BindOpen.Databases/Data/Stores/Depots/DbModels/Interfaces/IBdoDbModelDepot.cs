using BindOpen.Data.Models;
using System.Collections.Generic;

namespace BindOpen.Data.Stores
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDbModelDepot : ITBdoDepot<BdoDbModel>
    {
        /// <summary>
        /// 
        /// </summary>
        List<BdoDbModel> Models { get; set; }

        /// <summary>
        /// Gets the database model with the specified name.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <returns>Returns the database query with the specified name.</returns>
        IBdoDbModel GetModel(string name);

        /// <summary>
        /// Gets the database model with the specified name.
        /// </summary>
        /// <returns>Returns the database query with the specified name.</returns>
        T GetModel<T>() where T : BdoDbModel;
    }
}