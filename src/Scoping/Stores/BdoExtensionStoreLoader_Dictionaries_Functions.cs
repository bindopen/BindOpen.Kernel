using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Functions;
using BindOpen.Scoping.Script;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the function dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="packageDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadFunctionDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition packageDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the entity dico from the assembly

            var dico = ExtractDictionaryFromAssembly<IBdoFunctionDefinition>(assembly, log);

            // we feach entity classes

            var types = assembly.GetTypes();
            int count = 0;

            foreach (var type in types)
            {
                var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static).ToList();
                methodInfos.AddRange(type.GetMethods(BindingFlags.Public));

                foreach (var methodInfo in methodInfos.Where(p => p.GetCustomAttributes(typeof(BdoFunctionAttribute)).Any()))
                {
                    var definition = new BdoFunctionDefinition(null, packageDefinition)
                    {
                        RuntimeClassType = type,
                        LibraryId = packageDefinition?.Identifier
                    };

                    definition.UpdateFrom(methodInfo);

                    var paramInfos = methodInfo.GetParameters();

                    try
                    {
                        if (paramInfos.Length == 0)
                        {
                            definition.RuntimeBasicFunction = methodInfo.CreateDelegate(
                                typeof(BdoFunctionDelegate)) as BdoFunctionDelegate;
                        }
                        else if (paramInfos.Length == 1
                            && typeof(IBdoScriptDomain).IsAssignableFrom(paramInfos[0].ParameterType))
                        {
                            definition.RuntimeScopedFunction = methodInfo.CreateDelegate(
                                typeof(BdoFunctionDomainedDelegate)) as BdoFunctionDomainedDelegate;
                        }
                        else
                        {
                            definition.RuntimeFunction = CreateDelegate(methodInfo);
                        }
                    }
                    catch (ArgumentException)
                    {
                        log?.AddEvent(EventKinds.Error,
                            "Function ('" + definition.Name + "') with unexpected parameters");
                    }

                    if (definition.RuntimeBasicFunction == null
                        && definition.RuntimeScopedFunction == null
                        && definition.RuntimeFunction == null)
                    {
                        log?.AddEvent(EventKinds.Error,
                            "Invalid function: Method not defined for function called '" + definition.UniqueName + "'",
                            resultCode: "SCRIPT_DEFINITION");
                    }

                    // we build parameter specs

                    int i = 0;
                    foreach (var paramInfo in paramInfos)
                    {
                        var schema = BdoData.NewSchema();
                        schema.UpdateFrom(paramInfo, typeof(BdoParameterAttribute));
                        schema.Index = i;

                        var isStatic = schema.GetFlagValue(BdoSchemaProperties.IsStatic);
                        var isScriptParameter = schema.GetFlagValue(BdoSchemaProperties.IsScriptParameter);

                        if (isStatic)
                        {
                            var type1 = paramInfo.ParameterType;
                            definition.ParentDataType = BdoData.NewDataType(type1);
                        }

                        if (isStatic || isScriptParameter ||
                            (schema.DataType?.ValueType != DataValueTypes.Any
                            && schema.IsCompatibleWithType(typeof(IBdoScriptDomain))))
                        {
                            definition.AdditionalSchemas ??= BdoData.NewItemSet<IBdoSchema>();
                            definition.AdditionalSchemas.Insert(schema);
                        }
                        else
                        {
                            definition.Insert(schema);
                        }

                        i++;
                    }

                    // if the method is an extension

                    var isExtensionMethod = methodInfo.IsDefined(typeof(ExtensionAttribute), false);
                    if (isExtensionMethod && definition.Count > 0)
                    {
                        var type1 = methodInfo.GetParameters()[0].ParameterType;
                        definition.ParentDataType = BdoData.NewDataType(type1);

                        var schema = definition.FirstOrDefault().SetFlagValue(BdoSchemaProperties.IsStatic);
                        if (definition.AdditionalSchemas?[0]?.Index != 0)
                        {
                            definition.Remove(schema?.Key());
                        }
                        definition.AdditionalSchemas ??= BdoData.NewItemSet<IBdoSchema>();
                        definition.AdditionalSchemas.Insert(schema);
                    }

                    definition.OutputDataType = BdoData.NewDataType(methodInfo.ReturnType);

                    // we build the runtime definition

                    if (dico != null)
                    {
                        var indexDefinition = dico.Get(definition.Name);
                        definition.Update(indexDefinition);
                    }

                    _store.Add(definition);

                    count++;
                }
            }

            return count;
        }

        static Delegate CreateDelegate(MethodInfo info)
        {
            if (info != null)
            {
                if (!info.IsStatic)
                {
                    throw new ArgumentException("The provided method must be static.", "method");
                }

                if (info.IsGenericMethod)
                {
                    throw new ArgumentException("The provided method must not be generic.", "method");
                }

                return info.CreateDelegate(Expression.GetDelegateType(
                    (from parameter in info.GetParameters() select parameter.ParameterType)
                    .Concat(new[] { info.ReturnType })
                    .ToArray()));
            }

            return null;
        }
    }
}
