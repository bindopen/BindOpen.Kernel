using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Data.Schema;
using BindOpen.Logging;
using BindOpen.Scoping.Functions;
using BindOpen.Scoping.Script;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BindOpen.Scoping;

/// <summary>
/// This partial class handles the discovery and delegate creation for script functions within assemblies.
/// </summary>
internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
{
    /// <summary>
    /// Loads function definitions from the specified assembly.
    /// </summary>
    /// <param name="assembly">The assembly to scan.</param>
    /// <param name="packageDefinition">The package definition to associate with functions.</param>
    /// <param name="log">The log to populate with loading events.</param>
    /// <returns>Returns the number of functions successfully loaded.</returns>
    private int LoadFunctionDictionaryFromAssembly(
        Assembly assembly,
        IBdoPackageDefinition packageDefinition,
        IBdoLog log = null)
    {
        if (assembly == null) return -1;

        // 1. Extract function metadata from XML resource if available
        var dictionary = ExtractDictionaryFromAssembly<IBdoFunctionDefinition>(assembly, log);

        int count = 0;
        var types = assembly.GetTypes();

        foreach (var type in types)
        {
            // 2. Fetch all public methods (Static and Instance)
            var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance)
                .Where(m => m.GetCustomAttributes(typeof(BdoFunctionAttribute)).Any());

            foreach (var methodInfo in methodInfos)
            {
                var definition = new BdoFunctionDefinition(null, packageDefinition)
                {
                    RuntimeClassType = type,
                    LibraryId = packageDefinition?.Identifier,
                    OutputDataType = BdoData.NewDataType(methodInfo.ReturnType)
                };

                // Update metadata from [BdoFunction] attribute
                definition.UpdateFrom(methodInfo);

                var paramInfos = methodInfo.GetParameters();

                // 3. Create the appropriate runtime delegate
                try
                {
                    if (paramInfos.Length == 0)
                    {
                        definition.RuntimeBasicFunction = methodInfo.CreateDelegate(typeof(BdoFunctionDelegate)) as BdoFunctionDelegate;
                    }
                    else if (paramInfos.Length == 1 && typeof(IBdoScriptDomain).IsAssignableFrom(paramInfos[0].ParameterType))
                    {
                        definition.RuntimeScopedFunction = methodInfo.CreateDelegate(typeof(BdoFunctionDomainedDelegate)) as BdoFunctionDomainedDelegate;
                    }
                    else
                    {
                        definition.RuntimeFunction = CreateDelegate(methodInfo);
                    }
                }
                catch (ArgumentException)
                {
                    log?.AddEvent(BdoEventKinds.Error, $"Function '{definition.Name}' has unexpected parameters for the targeted delegate.");
                }

                if (definition.RuntimeBasicFunction == null && definition.RuntimeScopedFunction == null && definition.RuntimeFunction == null)
                {
                    log?.AddEvent(BdoEventKinds.Error, $"Invalid function: Method not defined for '{definition.UniqueName}'", resultCode: "SCRIPT_DEFINITION");
                    continue;
                }

                // 4. Build parameter specifications
                for (int i = 0; i < paramInfos.Length; i++)
                {
                    var paramInfo = paramInfos[i];
                    var schema = BdoData.NewSchema();
                    schema.UpdateFrom(paramInfo, typeof(BdoParameterAttribute));
                    schema.Index = i;

                    bool isStatic = schema.GetFlagValue(BdoSchemaProperties.IsStatic);
                    bool isScriptParameter = schema.GetFlagValue(BdoSchemaProperties.IsScriptParameter);

                    if (isStatic)
                    {
                        definition.ParentDataType = BdoData.NewDataType(paramInfo.ParameterType);
                    }

                    // Determine if it's a standard parameter or an additional metadata schema
                    if (isStatic || isScriptParameter || (schema.DataType?.ValueType != DataValueTypes.Any && schema.IsCompatibleWithType(typeof(IBdoScriptDomain))))
                    {
                        definition.AdditionalSchemas ??= BdoData.NewItemSet<IBdoSchema>();
                        definition.AdditionalSchemas.Insert(schema);
                    }
                    else
                    {
                        definition.Insert(schema);
                    }
                }

                // 5. Handle C# Extension Methods specifically
                if (methodInfo.IsDefined(typeof(ExtensionAttribute), false) && definition.Count > 0)
                {
                    var firstParamType = paramInfos[0].ParameterType;
                    definition.ParentDataType = BdoData.NewDataType(firstParamType);

                    var firstSchema = definition.FirstOrDefault();
                    firstSchema?.SetFlagValue(BdoSchemaProperties.IsStatic);

                    // Move first schema to additional schemas if it's the static target
                    if (definition.AdditionalSchemas?.Any(q => q.Index == 0) != true)
                    {
                        definition.Remove(firstSchema?.Key());
                        definition.AdditionalSchemas ??= BdoData.NewItemSet<IBdoSchema>();
                        definition.AdditionalSchemas.Insert(firstSchema);
                    }
                }

                // 6. Merge with XML dictionary metadata
                if (dictionary != null)
                {
                    var indexDef = dictionary.Get(definition.Name);
                    if (indexDef != null) definition.Update(indexDef);
                }

                _store?.Add(definition);
                count++;
            }
        }

        return count;
    }

    /// <summary>
    /// Creates a generic delegate for a static method using expression trees.
    /// </summary>
    private static Delegate CreateDelegate(MethodInfo methodInfo)
    {
        if (methodInfo == null) return null;

        if (!methodInfo.IsStatic)
            throw new ArgumentException("The provided method must be static for generic delegate creation.", nameof(methodInfo));

        if (methodInfo.IsGenericMethod)
            throw new ArgumentException("The provided method must not be generic.", nameof(methodInfo));

        var parameterTypes = methodInfo.GetParameters().Select(p => p.ParameterType)
            .Concat(new[] { methodInfo.ReturnType })
            .ToArray();

        return methodInfo.CreateDelegate(Expression.GetDelegateType(parameterTypes));
    }
}