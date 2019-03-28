using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Data.Elements.Scalar;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Configuration.Scriptwords;
using BindOpen.Framework.Core.Extensions.Definition.Scriptwords;

namespace BindOpen.Framework.Core.Extensions.Indexes
{
    /// <summary>
    /// This class represents a script word index.
    /// </summary>
    [Serializable()]
    [XmlType("ScriptWordIndex", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "scriptWords.index", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class ScriptWordIndex : TAppExtensionItemIndex<ScriptWordDefinition>
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The definition class of this instance.
        /// </summary>
        [XmlElement("definitionClass")]
        public String DefinitionClass { get; set; } = "";

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptWordIndex class.
        /// </summary>
        public ScriptWordIndex()
        {
        }

        #endregion

        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// Returns the word definitions with the specified name.
        /// </summary>
        /// <param name="name">The name of the script words to return.</param>
        /// <param name="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public List<ScriptWordDefinition> GetDefinitionsWithExactName(
            String name,
            ScriptWordDefinition parentDefinition = null)
        {
            List<ScriptWordDefinition> matchingDefinitions = new List<ScriptWordDefinition>();

            if (name != null)
            {
                List<ScriptWordDefinition> poolScriptWordDefinitions = null;
                if (parentDefinition == null)
                    poolScriptWordDefinitions = this.Definitions;
                else if (parentDefinition.Children != null)
                    poolScriptWordDefinitions = parentDefinition.Children;

                if (poolScriptWordDefinitions != null)
                    matchingDefinitions = poolScriptWordDefinitions.Where(p => p.Name.KeyEquals(name)).ToList();
            }

            return matchingDefinitions;
        }

        /// <summary>
        /// Gets the word definitions approximatively with the specified name.
        /// </summary>
        /// <param name="name">The name of the script words to return.</param>
        /// <param name="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public List<ScriptWordDefinition> GetDefinitionsWithApproximativeName(
            String name,
            ScriptWordDefinition parentDefinition = null)
        {
            List<ScriptWordDefinition> matchingDefinitions = new List<ScriptWordDefinition>();
            if (name == null)
                return matchingDefinitions;

            List<ScriptWordDefinition> poolScriptWordDefinitions = null;
            if (parentDefinition == null)
                poolScriptWordDefinitions = this.Definitions;
            else if (parentDefinition.Children != null)
                poolScriptWordDefinitions = parentDefinition.Children;

            if (poolScriptWordDefinitions != null)
                matchingDefinitions = poolScriptWordDefinitions.Where(p => p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0).ToList();

            return matchingDefinitions;
        }

        /// <summary>
        /// Determines whether the specified script word corresponds to the specified definition.
        /// </summary>
        /// <param name="scriptWord">The script word to consider.</param>
        /// <param name="scriptWordDefinition">The script word definition to consider.</param>
        /// <returns></returns>
        public Boolean IsWordMatching(ScriptWord scriptWord, ScriptWordDefinition scriptWordDefinition)
        {
            if (scriptWordDefinition == null) return false;

            // we check the number of parameters
            if ((!scriptWordDefinition.IsRepeatedParameters) && ((scriptWord.ParameterDetail == null) && (scriptWordDefinition.ParameterSpecification != null) |
                (scriptWordDefinition.ParameterSpecification == null) && (scriptWord.ParameterDetail != null)))
            {
                return false;
            }

            if ((scriptWord.ParameterDetail == null) && ((!scriptWordDefinition.IsRepeatedParameters) || (scriptWordDefinition.ParameterSpecification == null)))
                return true;
            if ((!scriptWordDefinition.IsRepeatedParameters) && (scriptWord.ParameterDetail.Count != scriptWordDefinition.ParameterSpecification.Count))
                return false;

            if ((!scriptWordDefinition.IsRepeatedParameters) && ((scriptWordDefinition.MaxParameterNumber != -1) && (scriptWord.ParameterDetail.Count > scriptWordDefinition.MaxParameterNumber)))
                return false;
            if ((!scriptWordDefinition.IsRepeatedParameters) && ((scriptWordDefinition.MinParameterNumber != -1) && (scriptWord.ParameterDetail.Count < scriptWordDefinition.MinParameterNumber)))
                return false;

            // we search the defined script word parameters

            int parameterIndex = 0;
            //if ((scriptWordDefinition.IsRepeatedParameters) & (scriptWordDefinition.RepeatedParameterValueType != scriptWord.Definition.RepeatedParameterValueType))
            //    return false;
            if (scriptWordDefinition.ParameterSpecification.Items != null)
            {
                foreach (DataElementSpec parameterSpecification in scriptWordDefinition.ParameterSpecification.Items)
                {
                    ScalarElement scriptWordParameter = scriptWord.ParameterDetail[parameterIndex] as ScalarElement;

                    // we check that the value type of the current script word parameter corresponds to the defined one (considering the en-US culture info)
                    if (((scriptWordDefinition.IsRepeatedParameters) && (scriptWordDefinition.RepeatedParameterValueType == DataValueType.Text))
                        || ((!scriptWordDefinition.IsRepeatedParameters) && (parameterSpecification.ValueType == DataValueType.Text)))
                    {
                        String parameterValue = (scriptWordParameter.GetItem() ?? "").ToString().Trim();

                        if (parameterValue.Length < 2)
                            return false;
                        if ((!parameterValue.StartsWith("'")) || (!parameterValue.EndsWith("'")))
                            return false;
                    }
                    else
                    {
                        if ((!scriptWordDefinition.IsRepeatedParameters) && (parameterSpecification.ValueType != DataValueType.Any))
                        {
                            return (!parameterSpecification.CheckItem(scriptWordParameter.GetItem()).HasErrorsOrExceptions()) &&
                               (!scriptWordParameter.CheckItem().HasErrorsOrExceptions());
                        }
                    }
                    parameterIndex++;
                }
            }

            return true;
        }

        #endregion
    }
}
