using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Entities;
using BindOpen.Extensions.Functions;
using BindOpen.Logging;
using BindOpen.Script;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BindOpen.Scopes.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoItem, IBdoExtensionStoreLoader
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

            var dico = ExtractDictionaryFromAssembly<IBdoEntityDefinition>(assembly, log);

            // we feach entity classes

            var types = assembly.GetTypes();
            int count = 0;

            foreach (var type in types)
            {
                var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                foreach (var methodInfo in methodInfos.Where(p => p.GetCustomAttributes(typeof(BdoFunctionAttribute)).Any()))
                {
                    var definition = new BdoFunctionDefinition(null, packageDefinition)
                    {
                        ClassReference = BdoData.Class(type),
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
                            definition.RuntimeFunction = CreateDelegate(methodInfo);
                    }
                    catch (ArgumentException)
                    {
                        log?.AddError(
                                title: "Incompatible function ('" + methodInfo.Name + "')",
                                description: "Function '" + definition.RuntimeFunctionName + "' in class '"
                                + definition.ClassReference?.ClassName + "' has inexpected parameters.");
                    }

                    if (definition.RuntimeBasicFunction == null
                        && definition.RuntimeScopedFunction == null
                        && definition.RuntimeFunction == null)
                    {
                        log?.AddError(
                            title: "Invalid function: Method not defined for function called '" + definition.UniqueName + "'",
                            resultCode: "SCRIPT_DEFINITION");
                    }

                    // we build parameter specs

                    foreach (var paramInfo in paramInfos)
                    {
                        var spec = BdoMeta.NewSpec();
                        spec.UpdateFrom(paramInfo, typeof(BdoParameterAttribute));
                        definition.Add(spec);
                    }

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
