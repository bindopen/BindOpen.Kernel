using System;
using System.Collections.Generic;

namespace BindOpen.Data.Queries
{
    /// <summary>
    /// This class represents a Api script sorting definition.
    /// </summary>
    public class ApiScriptSortingDefinition : Dictionary<string, ApiScriptField>
    {
        /// <summary>
        /// Creates a new instance of the ApiScriptSortingDefinition class.
        /// </summary>
        public ApiScriptSortingDefinition() : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// Creates a new instance of the ApiScriptSortingDefinition class.
        /// </summary>
        /// <param name="clauses">The clauses to consider.</param>
        public ApiScriptSortingDefinition(
            params ApiScriptField[] clauses) : base(StringComparer.OrdinalIgnoreCase)
        {
            foreach (ApiScriptField clause in clauses)
            {
                if (clause != null)
                {
                    this.Add(clause.FieldAlias, clause);
                }
            }
        }
    }
}
