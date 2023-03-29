using BindOpen.Data;
using BindOpen.Extensions;
using BindOpen.Extensions.Functions;
using BindOpen.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
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

            var dico = ExtractDictionaryFromAssembly<IBdoFunctionDefinition>(assembly, log) as BdoFunctionDictionary;

            // we define definitions

            int count = 0;

            if (dico == null)
            {
                log?.AddWarning(title: "No function dico was found");
            }
            else
            {
                var functionDefinitions = new List<BdoFunctionDefinition>();

                var types = assembly.GetTypes().Where(p => p.GetCustomAttributes(typeof(BdoFunctionAttribute)).Any());
                foreach (var type in types)
                {
                    // we feach methods

                    var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    foreach (var methodInfo in methodInfos)
                    {
                        if (methodInfo.GetCustomAttribute(typeof(BdoFunctionAttribute)) is BdoFunctionAttribute functionWordAttribute)
                        {
                            // we determine the name of the definition

                            string definitionName = functionWordAttribute.Name;

                            // we update the definition with the dico if there is one

                            if (dico != null)
                            {
                                var definition = dico.GetDefinition(definitionName, methodInfo.Name);

                                if (definition == null)
                                {
                                    log?.AddError(title: "Function '" + methodInfo.Name + "' not found in dico");
                                }
                                else
                                {
                                    definition.ClassReference = BdoData.Class(type);
                                    definition.LibraryId = packageDefinition?.Id;

                                    // we create the runtime definition

                                    var itemDefinition = new BdoFunctionDefinition(null, packageDefinition);

                                    try
                                    {
                                        if (methodInfo.GetParameters().Length == 0)
                                        {
                                            itemDefinition.RuntimeBasicFunction += methodInfo.CreateDelegate(
                                                typeof(BdoFunctionDelegate)) as BdoFunctionDelegate;
                                        }
                                        else
                                        {
                                            itemDefinition.RuntimeScopedFunction += methodInfo.CreateDelegate(
                                                typeof(BdoFunctionDomainedDelegate)) as BdoFunctionDomainedDelegate;
                                        }

                                        functionDefinitions.Add(itemDefinition);

                                        count++;
                                    }
                                    catch (ArgumentException)
                                    {
                                        log?.AddError(
                                                title: "Incompatible function ('" + methodInfo.Name + "')",
                                                description: "Function '" + definition.RuntimeFunctionName + "' in class '"
                                                + definition.ClassReference?.ClassName + "' has inexpected parameters.");
                                    }
                                }
                            }
                        }
                    }
                }

                // we recursively retrieve the sub function words

                foreach (var definition in dico)
                {
                    _store.Add(definition);
                }
            }

            return count;
        }
    }
}
