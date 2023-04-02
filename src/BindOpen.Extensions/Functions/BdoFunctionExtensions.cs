using BindOpen.Data;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scopes;
using BindOpen.Scopes.Stores;
using BindOpen.Script;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoFunctionExtensions
    {
        // Create

        public static object CallFunction(
            this IBdoScope scope,
            string functionName,
            IBdoMetaSet paramSet = null,
            IBdoMetaSet varSet = null)
        {
            var definition = scope?.ExtensionStore?.GetFunctionDefinition(functionName, paramSet);
            var result = scope.CallFunction(definition, paramSet, varSet);

            return result;
        }

        public static T CallFunction<T>(
            this IBdoScope scope,
            string functionName,
            IBdoMetaSet paramSet = null,
            IBdoMetaSet varSet = null)
        {
            var result = scope?.CallFunction(functionName, paramSet, varSet);
            return result.As<T>();
        }

        public static object CallFunction(
            this IBdoScope scope,
            IBdoScriptword word,
            IBdoMetaSet varSet = null)
        {
            IBdoFunctionDefinition definition;
            if (!string.IsNullOrEmpty(word?.DefinitionUniqueName))
            {
                definition = scope?.ExtensionStore?.GetDefinition<IBdoFunctionDefinition>(word?.DefinitionUniqueName);
            }
            else
            {
                definition = scope?.ExtensionStore?.GetFunctionDefinition(word?.Name, word);
            }

            var result = scope.CallFunction(definition, word, varSet);

            return result;
        }

        public static T CallFunction<T>(
            this IBdoScope scope,
            IBdoScriptword word,
            IBdoMetaSet varSet = null)
        {
            var result = scope?.CallFunction(word, varSet);
            return result.As<T>();
        }

        private static object CallFunction(
            this IBdoScope scope,
            IBdoFunctionDefinition definition,
            IBdoMetaSet paramSet = null,
            IBdoMetaSet varSet = null)
        {
            object result = null;

            if (definition != null)
            {
                if (definition.RuntimeBasicFunction != null)
                {
                    result = definition.RuntimeBasicFunction.DynamicInvoke();
                }
                else if (definition.RuntimeScopedFunction != null)
                {
                    IBdoScriptDomain domain = null;
                    if (paramSet is IBdoScriptword word)
                    {
                        domain = scope.NewScriptDomain(varSet, word);
                    }
                    result = definition.RuntimeScopedFunction.DynamicInvoke(domain);
                }
                else
                {
                    var objs = paramSet?.Select(q => q.GetData()).ToArray();
                    result = definition.RuntimeFunction.DynamicInvoke(objs);
                }
            }

            return result;
        }

        private static IBdoFunctionDefinition GetFunctionDefinition(
            this IBdoExtensionStore store,
            string functionName,
            IBdoMetaSet paramSet,
            IBdoLog log = null)
        {
            if (store != null)
            {
                // we try to find the corresponding defined function
                var functionDefinitions = store.GetFunctionDefinitionsWithName(functionName, true);

                if (functionDefinitions?.Any() != true)
                {
                    log?.AddError(
                        title: "Function named '" + functionName + "' not defined",
                        description: "Syntax error: Function named '" + functionName + "' not defined",
                        resultCode: "SCRIPT_NOTEXISTINGWORD");
                }
                else
                {
                    IBdoFunctionDefinition functionDefinition = null;
                    foreach (var definition in functionDefinitions)
                    {
                        if (definition?.IsCompatibleWith(paramSet) ?? false)
                        {
                            functionDefinition = definition;
                            break;
                        }
                    }

                    // if no defined script word match then
                    if (functionDefinition == null)
                    {
                        log?.AddError(
                            title: "Invalid arguments: Function called '" + functionName + "' has invalid parameters. Either the number of parameters does not match or their value types.",
                            resultCode: nameof(ArgumentException));
                    }
                    else
                    {
                        return functionDefinition;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the word definitions with the specified name.
        /// </summary>
        /// <param key="name">The name of the script words to return.</param>
        /// <param key="parentDefinition">The parent definition.</param>
        /// <returns>The script words with the specified name.</returns>
        public static IEnumerable<IBdoFunctionDefinition> GetFunctionDefinitionsWithName(
            this IBdoExtensionStore store,
            string name,
            bool isExact = false)
        {
            if (store != null && !string.IsNullOrEmpty(name))
            {
                var definitions = store.GetDefinitions<IBdoFunctionDefinition>();

                if (isExact)
                    return definitions.Where(p => p?.Name.BdoKeyEquals(name) == true);
                else
                    return definitions.Where(p => p?.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0);
            }

            return Enumerable.Empty<IBdoFunctionDefinition>();
        }
    }
}
