using System;
using System.Collections.Generic;
using System.Linq;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes.Scriptwords
{
    /// <summary>
    /// This class represents a script word index.
    /// </summary>
    public class ScriptwordIndex : TAppExtensionItemIndex<IScriptwordDefinition>, IScriptwordIndex
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordIndex class.
        /// </summary>
        public ScriptwordIndex() : base()
        {
            Id = IdentifiedDataItem.NewGuid();
        }

        /// <summary>
        /// Instantiates a new instance of the ScriptwordIndex class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        public ScriptwordIndex(IScriptwordIndexDto dto)
        {
        }

        #endregion

        // Script word definitions ---------------------------

        /// <summary>
        /// Returns the possible parent definitions of the specified script word definition.
        /// </summary>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="libraryNames">The names of libraries to consider.</param>
        /// <returns>The parent definitions of the specified script word definition.</returns>
        public List<IScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            string[] libraryNames = null)
        {
            return GetParentScriptwordDefinitions(definitionName, null, libraryNames).Distinct().ToList();
        }

        private List<IScriptwordDefinition> GetParentScriptwordDefinitions(
            string definitionName,
            IScriptwordDefinition parentFeachDefinition,
            string[] libraryNames = null)
        {
            List<IScriptwordDefinition> parentDefinitions = new List<IScriptwordDefinition>();

            if (definitionName != null)
            {
                List<IScriptwordDefinition> definitions =
                    (parentFeachDefinition == null ? Definitions :
                    new List<IScriptwordDefinition>(parentFeachDefinition.Children));
                foreach (IScriptwordDefinition currentScriptwordDefinition in definitions)
                {
                    if (currentScriptwordDefinition.KeyEquals(definitionName) && parentFeachDefinition != null)
                        parentDefinitions.Add(parentFeachDefinition);

                    parentDefinitions.AddRange(GetParentScriptwordDefinitions(definitionName, currentScriptwordDefinition, libraryNames));
                }
            }

            return parentDefinitions;
        }

        /// <summary>
        /// Returns the word definitions with the specified name.
        /// </summary>
        /// <param name="name">The name of the script words to return.</param>
        /// <param name="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public List<IScriptwordDefinition> GetDefinitionsWithExactName(
            string name,
            IScriptwordDefinition parentDefinition = null)
        {
            List<IScriptwordDefinition> matchingDefinitions = new List<IScriptwordDefinition>();

            if (name != null)
            {
                List<IScriptwordDefinition> poolScriptwordDefinitions = null;
                if (parentDefinition == null)
                    poolScriptwordDefinitions = Definitions;
                else if (parentDefinition.Children != null)
                    poolScriptwordDefinitions = new List<IScriptwordDefinition>(parentDefinition.Children);

                if (poolScriptwordDefinitions != null)
                    matchingDefinitions = poolScriptwordDefinitions.Where(p => p?.Dto?.Name.KeyEquals(name) == true).ToList();
            }

            return matchingDefinitions;
        }

        /// <summary>
        /// Gets the word definitions approximatively with the specified name.
        /// </summary>
        /// <param name="name">The name of the script words to return.</param>
        /// <param name="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public List<IScriptwordDefinition> GetDefinitionsWithApproximativeName(
            string name,
            IScriptwordDefinition parentDefinition = null)
        {
            List<IScriptwordDefinition> matchingDefinitions = new List<IScriptwordDefinition>();
            if (name == null)
                return matchingDefinitions;

            List<IScriptwordDefinition> poolScriptwordDefinitions = null;
            if (parentDefinition == null)
                poolScriptwordDefinitions = Definitions;
            else if (parentDefinition.Children != null)
                poolScriptwordDefinitions = new List<IScriptwordDefinition>(parentDefinition.Children);

            if (poolScriptwordDefinitions != null)
                matchingDefinitions = poolScriptwordDefinitions.Where(p => p?.Dto?.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0).ToList();

            return matchingDefinitions;
        }

    }
}
