using BindOpen.Data.Expression;
using BindOpen.Data.Helpers.Strings;
using BindOpen.Data.Queries;
using BindOpen.Extensions.Carriers;
using System.Reflection;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents a database model.
    /// </summary>
    public class BdoDbModelBuilder : IBdoDbModelBuilder
    {
        IBdoDbModel _model = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public BdoDbModelBuilder(IBdoDbModel model)
        {
            _model = model;
        }

        // Tables ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable(DbTable table)
            => AddTable(null, table);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTable(string name, DbTable table)
        {
            if (table != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = table.Schema.ConcatenateIfFirstNotEmpty(".") + table.Name;
                }

                (_model as BdoDbModel).TableDictionary.Remove(name);
                (_model as BdoDbModel).TableDictionary.Add(name, table);
            }

            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual IBdoDbModelBuilder AddTable<T>(string name = null) where T : class
        {
            var table = new DbTable();

            string schema = null;
            string dataModuleName = null;

            var type = typeof(T);

            if (type.GetCustomAttribute(typeof(BdoDbTableAttribute)) is BdoDbTableAttribute tableAttribute)
            {
                name = string.IsNullOrEmpty(name) ? tableAttribute.Name : name;
                schema = tableAttribute.Schema;
                dataModuleName = tableAttribute.DataModule;
            }
            else
            {
                name = string.IsNullOrEmpty(name) ? type.Name : name;
            }

            table.Name = name;
            table.DataModule = dataModuleName;
            table.Schema = schema;

            AddTable(table);

            return this;
        }

        // Join conditions ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddJoinCondition(string name, DataExpression condition)
        {
            if (condition != null)
            {
                (_model as BdoDbModel).JoinConditionDictionary.Remove(name);
                (_model as BdoDbModel).JoinConditionDictionary.Add(name, condition);
            }

            return this;
        }

        // Tuples ---------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddTuple(string name, DbField[] fields)
        {
            if (fields != null)
            {
                (_model as BdoDbModel).TupleDictionary.Remove(name);
                (_model as BdoDbModel).TupleDictionary.Add(name, fields);
            }

            return this;
        }

        // Queries ---------------------------------------

        /// <summary>
        /// Adds the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddQuery(IDbQuery query)
            => AddQuery(null, query);

        /// <summary>
        /// Adds the specified query.
        /// </summary>
        /// <param name="query">The query to consider.</param>
        /// <param name="name"></param>
        /// <returns></returns>
        public IBdoDbModelBuilder AddQuery(string name, IDbQuery query)
        {
            if (query != null)
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = query.GetName();
                }

                (_model as BdoDbModel).QueryDictionary.Remove(name);
                (_model as BdoDbModel).QueryDictionary.Add(name, new DbStoredQuery(query, name));
            }
            return this;
        }
    }
}
