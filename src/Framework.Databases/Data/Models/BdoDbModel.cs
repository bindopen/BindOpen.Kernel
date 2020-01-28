using BindOpen.Framework.Data.Items;
using BindOpen.Framework.Data.Queries;
using BindOpen.Framework.Extensions.Carriers;
using System.Collections.Generic;

namespace BindOpen.Framework.Data.Models
{
    /// <summary>
    /// This class represents a database context.
    /// </summary>
    public partial class BdoDbModel : IdentifiedDataItem, IBdoDbModel
    {
        // Properties ---------------------------------------

        internal Dictionary<string, DbTable> TableDictionary = new Dictionary<string, DbTable>();
        internal Dictionary<string, DbQueryJoinCondition> JoinConditionDictionary = new Dictionary<string, DbQueryJoinCondition>();
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

            return table;
        }

        // Join conditions ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="aliases"></param>
        /// <returns></returns>
        public DbQueryJoinCondition JoinCondition(string name, params (string fieldName, string fieldAlias)[] aliases)
        {
            JoinConditionDictionary.TryGetValue(name, out DbQueryJoinCondition condition);

            return condition;
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

            return fields;
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

            return query;
        }
    }
}
