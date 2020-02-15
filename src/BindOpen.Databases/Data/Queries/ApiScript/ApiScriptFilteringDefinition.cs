using System;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a Api script filtering definition.
    /// </summary>
    public class ApiScriptFilteringDefinition : Dictionary<string, ApiScriptClause>
    {
        /// <summary>
        /// Creates a new instance of the ApiScriptFilteringDefinition class.
        /// </summary>
        public ApiScriptFilteringDefinition() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Creates a new instance of the ApiScriptFilteringDefinition class.
        /// </summary>
        /// <param name="clauses">The clauses to consider.</param>
        public ApiScriptFilteringDefinition(
            params ApiScriptClause[] clauses) : base(StringComparer.OrdinalIgnoreCase)
        {
            foreach (ApiScriptClause clause in clauses)
            {
                if (clause != null)
                {
                    this.Add(clause.FieldAlias, clause);
                }
            }
        }
    }
}
