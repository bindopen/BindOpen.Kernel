using BindOpen.Data.Queries;
using System;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents a database context.
    /// </summary>
    public static class BdoDbModelExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="intializer"></param>
        /// <returns></returns>
        public static IDbStoredQuery UseQuery(this BdoDbModel model, string name, Func<IBdoDbModel, IDbQuery> intializer)
        {
            if (model == null) return null;

            var query = model.Query(name);
            if (query == null)
            {
                var simpleQuery = intializer?.Invoke(model);
                (new BdoDbModelBuilder(model)).AddQuery(name, simpleQuery);
                query = model.Query(name);
            }

            return query;
        }
    }
}
