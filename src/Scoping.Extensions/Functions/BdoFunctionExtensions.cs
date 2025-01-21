using BindOpen.Data;
using BindOpen.Data.Assemblies;
using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping.Functions;
using BindOpen.Scoping.Script;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BindOpen.Scoping
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
            IBdoMetaNode paramSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            var definition = scope.GetFunctionDefinition(functionName, paramSet);

            object result = null;

            if (definition == null)
            {
                log?.AddEvent(BdoEventKinds.Error,
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
            IBdoMetaNode paramSet = null,
            IBdoMetaSet metaSet = null)
        {
            var result = scope?.CallFunction(functionName, paramSet, metaSet);
            return result.As<T>();
        }

        public static object CallFunction(
            this IBdoScope scope,
            IBdoScriptword word,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
        {
            IBdoFunctionDefinition definition;

            var definitionUniqueName = word.DataType?.DefinitionUniqueName;

            if (!string.IsNullOrEmpty(definitionUniqueName))
            {
                definition = scope?.ExtensionStore?.GetDefinition<IBdoFunctionDefinition>(definitionUniqueName);
            }
            else
            {
                var parentDataType = word?.Parent == null ? null : BdoData.NewDataType(word?.Parent?.GetData()?.GetType());
                definition = scope.GetFunctionDefinition(word?.Name, word, parentDataType, log);
            }

            object result = null;

            if (definition == null)
            {
                var functionName = definitionUniqueName ?? word?.Name;

                log?.AddEvent(BdoEventKinds.Error,
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
            IBdoLog log = null)
        {
            var result = scope?.CallFunction(word, varSet, log);
            return result.As<T>();
        }

        private static object CallFunction(
            this IBdoScope scope,
            IBdoFunctionDefinition definition,
            IBdoMetaNode paramSet = null,
            IBdoMetaSet varSet = null,
            IBdoLog log = null)
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

                        if (definition.AdditionalSchemas != null)
                        {
                            foreach (var schema in definition.AdditionalSchemas)
                            {
                                object obj = null;
                                if (schema.GetFlagValue(BdoSchemaProperties.IsStatic))
                                    obj = (paramSet as IBdoScriptword)?.Parent?.GetData();
                                else if (schema.IsCompatibleWithType(typeof(IBdoScope)))
                                    obj = scope;
                                else if (schema.IsCompatibleWithType(typeof(IBdoScriptDomain)))
                                    obj = scope.NewScriptDomain(varSet, paramSet as IBdoScriptword, log);
                                else if (schema.IsCompatibleWithType(typeof(IBdoScriptword)))
                                    obj = paramSet as IBdoScriptword;
                                else if (schema.IsCompatibleWithType(typeof(IBdoMetaSet)))
                                    obj = varSet;
                                else if (schema.IsCompatibleWithType(typeof(IBdoLog)))
                                    obj = log;

                                objs.Insert(int.Min(objs.Count, schema.Index ?? 0), obj);
                            }
                        }

                        var method = definition.RuntimeFunction.Method;
                        var lastParam = method.GetParameters().LastOrDefault();
                        if (lastParam?.GetCustomAttributes(typeof(ParamArrayAttribute), false).Any() == true)
                        {
                            var parameters = method.GetParameters();

                            var i = Array.IndexOf(parameters, lastParam);
                            if (i > -1 && objs.Count >= i + 1)
                            {
                                var list = objs.GetRange(i, objs.Count - i);
                                objs.RemoveRange(i, objs.Count - i);
                                objs.Add(list?.ToArray());
                            }
                        }

                        result = definition.RuntimeFunction.DynamicInvoke(objs.ToArray());
                    }
                }
                catch (ApplicationException ex)
                {
                    log?.AddEvent(BdoEventKinds.Error,
                        "Bad argument",
                        ex.ToString(),
                        resultCode: "SCRIPT_BADARGUMENT");
                }
                catch (ArgumentException ex)
                {
                    log?.AddEvent(BdoEventKinds.Error,
                        "Bad argument",
                        ex.ToString(),
                        resultCode: "SCRIPT_BADARGUMENT");
                }
            }

            return result;
        }

        private static IBdoFunctionDefinition GetFunctionDefinition(
            this IBdoScope scope,
            string functionName,
            IBdoMetaNode paramSet,
            IBdoDataType parentDataType = default,
            IBdoLog log = null)
        {
            if (scope?.ExtensionStore != null)
            {
                var validator = scope.CreateValidator();

                // we try to find the corresponding defined function
                var functionDefinitions = scope.GetFunctionDefinitionsWithName(functionName, true);

                if (functionDefinitions?.Any() == true)
                {
                    IBdoFunctionDefinition functionDefinition = null;
                    foreach (var definition in functionDefinitions)
                    {
                        var schema = BdoData.NewSchema().WithChildren(definition.Items?.ToArray());

                        if ((parentDataType == null || parentDataType.IsCompatibleWithType(definition?.ParentDataType) == true)
                            && (definition.RuntimeFunction != null || (validator?.Check(paramSet, schema, log: log) ?? false)))
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
            this IBdoScope scope,
            string name,
            bool isExact = false)
        {
            if (scope?.ExtensionStore != null && !string.IsNullOrEmpty(name))
            {
                var definitions = scope.ExtensionStore.GetDefinitions<IBdoFunctionDefinition>();

                if (isExact)
                    return definitions.Where(p => p?.Name.BdoKeyEquals(name) == true);
                else
                    return definitions.Where(p => p?.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) > 0);
            }

            return Enumerable.Empty<IBdoFunctionDefinition>();
        }
    }
}
