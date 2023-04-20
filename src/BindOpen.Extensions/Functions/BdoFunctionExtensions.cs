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
        // Call function

        public static object CallFunction(
            this IBdoScope scope,
            string functionName,
            IBdoMetaSet paramSet = null,
            IBdoMetaSet varSet = null,
            IBdoBaseLog log = null)
        {
            var definition = scope?.ExtensionStore?.GetFunctionDefinition(functionName, paramSet);

            object result = null;

            if (definition == null)
            {
                log?.AddEvent(EventKinds.Error,
                    "Function named '" + functionName + "' not defined",
                    "Syntax error: Function named '" + functionName + "' not defined",
                    resultCode: "SCRIPT_NOTEXISTINGWORD");
            }
            else
            {
                result = scope.CallFunction(definition, paramSet, varSet);
            }

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
            IBdoMetaSet varSet = null,
            IBdoBaseLog log = null)
        {
            IBdoFunctionDefinition definition;
            if (!string.IsNullOrEmpty(word?.DefinitionUniqueName))
            {
                definition = scope?.ExtensionStore?.GetDefinition<IBdoFunctionDefinition>(word?.DefinitionUniqueName);
            }
            else
            {
                var parentDataType = BdoData.NewDataType(word?.Parent?.GetData()?.GetType());
                definition = scope?.ExtensionStore?.GetFunctionDefinition(word?.Name, word, parentDataType, log);
            }

            object result = null;

            if (definition == null)
            {
                var functionName = word?.DefinitionUniqueName ?? word?.Name;

                log?.AddEvent(EventKinds.Error,
                    "Function named '" + functionName + "' not defined",
                    "Syntax error: Function named '" + functionName + "' not defined",
                    resultCode: "SCRIPT_NOTEXISTINGWORD");
            }
            else
            {
                result = scope.CallFunction(definition, word, varSet, log);
            }

            return result;
        }

        public static T CallFunction<T>(
            this IBdoScope scope,
            IBdoScriptword word,
            IBdoMetaSet varSet = null,
            IBdoBaseLog log = null)
        {
            var result = scope?.CallFunction(word, varSet, log);
            return result.As<T>();
        }

        private static object CallFunction(
            this IBdoScope scope,
            IBdoFunctionDefinition definition,
            IBdoMetaSet paramSet = null,
            IBdoMetaSet varSet = null,
            IBdoBaseLog log = null)
        {
            object result = null;

            if (scope != null && definition != null)
            {
                try
                {
                    if (definition.RuntimeBasicFunction != null)
                    {
                        result = definition.RuntimeBasicFunction.DynamicInvoke();
                    }
                    else if (definition.RuntimeScopedFunction != null)
                    {
                        IBdoScriptDomain domain = scope.NewScriptDomain(varSet, log: log);
                        if (paramSet is IBdoScriptword word)
                        {
                            domain.Scriptword = word;
                        }
                        result = definition.RuntimeScopedFunction.DynamicInvoke(domain);
                    }
                    else
                    {
                        var objs = paramSet?.Select(q => q.GetData()).ToList();

                        // if function is static

                        if (definition.AdditionalSpecs != null)
                        {
                            foreach (var spec in definition.AdditionalSpecs)
                            {
                                object obj = null;
                                if (spec.DataType.IsScope())
                                    obj = scope;
                                else if (spec.DataType.IsScriptDomain())
                                    obj = scope.NewScriptDomain(varSet, paramSet as IBdoScriptword, log);
                                else if (spec.DataType.IsScriptword())
                                    obj = paramSet as IBdoScriptword;
                                else
                                    obj = (paramSet as IBdoScriptword)?.Parent?.GetData();

                                objs.Insert(spec.Index ?? 0, obj);
                            }
                        }

                        result = definition.RuntimeFunction.DynamicInvoke(objs.ToArray());
                    }
                }
                catch (ApplicationException ex)
                {
                    log?.AddEvent(EventKinds.Error,
                        "Bad argument",
                        ex.ToString(),
                        resultCode: "SCRIPT_NOTEXISTINGWORD");
                }
            }

            return result;
        }

        private static IBdoFunctionDefinition GetFunctionDefinition(
            this IBdoExtensionStore store,
            string functionName,
            IBdoMetaSet paramSet,
            BdoDataType parentDataType = default,
            IBdoBaseLog log = null)
        {
            if (store != null)
            {
                // we try to find the corresponding defined function
                var functionDefinitions = store.GetFunctionDefinitionsWithName(functionName, true);

                if (functionDefinitions?.Any() == true)
                {
                    IBdoFunctionDefinition functionDefinition = null;
                    foreach (var definition in functionDefinitions)
                    {
                        if ((parentDataType == DataValueTypes.None || parentDataType == DataValueTypes.Any || definition?.ParentDataType <= parentDataType)
                            && (definition.RuntimeFunction != null || (definition?.IsCompatibleWith(paramSet, log: log) ?? false)))
                        {
                            functionDefinition = definition;
                            break;
                        }
                    }

                    return functionDefinition;
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
