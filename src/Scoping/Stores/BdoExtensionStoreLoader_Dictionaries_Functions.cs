using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping;
using BindOpen.System.Scoping.Entities;
using BindOpen.System.Scoping.Functions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using BindOpen.System.Logging;
using BindOpen.System.Logging;
using BindOpen.System.Scoping.Script;

namespace BindOpen.System.Scoping.Stores
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
                        LibraryId = packageDefinition?.Id
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
                        var spec = BdoMeta.NewSpec();
                        spec.UpdateFrom(paramInfo, typeof(BdoParameterAttribute));
                        spec.Index = i;

                        if (spec.IsStatic)
                        {
                            var type1 = paramInfo.ParameterType;
                            definition.ParentDataType = BdoData.NewDataType(type1);
                        }

                        if (spec.IsStatic || spec.DataType.IsScope() || spec.DataType.IsScriptDomain())
                        {
                            definition.AdditionalSpecs ??= BdoData.NewSet<IBdoSpec>();
                            definition.AdditionalSpecs.Add(spec);
                        }
                        else
                        {
                            definition.Add(spec);
                        }

                        i++;
                    }

                    // if the method is an extension

                    var isExtensionMethod = methodInfo.IsDefined(typeof(ExtensionAttribute), false);
                    if (isExtensionMethod && definition.Count > 0)
                    {
                        var type1 = methodInfo.GetParameters()[0].ParameterType;
                        definition.ParentDataType = BdoData.NewDataType(type1);

                        var spec = definition.FirstOrDefault().AsStatic();
                        if (definition.AdditionalSpecs?[0]?.Index != 0)
                        {
                            definition.Remove(spec?.Key());
                        }
                        definition.AdditionalSpecs ??= BdoData.NewSet<IBdoSpec>();
                        definition.AdditionalSpecs.Add(spec);
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
