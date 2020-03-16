using BindOpen.Data.Expression;
using BindOpen.Data.Items;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Carriers;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents a database context.
    /// </summary>
    public class BdoDbModel : IdentifiedDataItem, IBdoDbModel
    {
        // Properties ---------------------------------------

        internal Dictionary<string, DbTable> TableDictionary = new Dictionary<string, DbTable>();
        internal Dictionary<string, DataExpression> JoinConditionDictionary = new Dictionary<string, DataExpression>();
        internal Dictionary<string, DbField[]> TupleDictionary = new Dictionary<string, DbField[]>();
        internal Dictionary<string, IDbStoredQuery> QueryDictionary = new Dictionary<string, IDbStoredQuery>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public virtual void OnCreating(IBdoDbModelBuilder builder)
        {
        }

        // Tables ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public DbTable Table(string name, string alias = null)
        {
            TableDictionary.TryGetValue(name, out DbTable table);

            return table?.Clone<DbTable>();
        }

        // Join conditions ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public DataExpression JoinCondition(string name, params (string fieldName, string fieldAlias)[] aliases)
        {
            JoinConditionDictionary.TryGetValue(name, out DataExpression condition);

            return condition?.Clone<DataExpression>();
        }

        // Tuples ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public DbField[] Tuple(string name, string alias)
            => Tuple(name, (null, alias));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public DbField[] Tuple(string name, params (string fieldName, string fieldAlias)[] aliases)
        {
            TupleDictionary.TryGetValue(name, out DbField[] fields);

            return fields?.Select(q => q.Clone<DbField>()).ToArray();
        }

        // Queries ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public IDbStoredQuery Query(string name)
        {
            QueryDictionary.TryGetValue(name, out IDbStoredQuery query);

            return query?.Clone<IDbStoredQuery>();
        }
    }
}
