using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Extensions.Scripting;
using BindOpen.Logging;
using BindOpen.Runtime.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoItem, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the script word dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="extensionDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadScripwordDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the entity dico from the assembly

            var dico = ExtractDictionaryFromAssembly<IBdoScriptwordDefinition>(assembly, log) as BdoScriptwordDictionary;

            // we define definitions

            int count = 0;

            if (dico == null)
            {
                log?.AddWarning(title: "No script word dico was found");
            }
            else
            {
                var scriptwordDefinitions = new List<BdoScriptwordDefinition>();

                var types = assembly.GetTypes().Where(p => p.GetCustomAttributes(typeof(BdoScriptwordAttribute)).Any());
                foreach (Type type in types)
                {
                    // we feach methods
                    var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    foreach (MethodInfo methodInfo in methodInfos)
                    {
                        if (methodInfo.GetCustomAttribute(typeof(BdoScriptwordAttribute)) is BdoScriptwordAttribute scriptWordAttribute)
                        {
                            // we determine the name of the definition

                            string definitionName = scriptWordAttribute.Name;

                            // we update the definition with the dico if there is one

                            if (dico != null)
                            {
                                var definition = dico.GetDefinition(definitionName, methodInfo.Name);

                                if (definition == null)
                                {
                                    log?.AddError(title: "Script word '" + methodInfo.Name + "' not found in dico");
                                }
                                else
                                {
                                    definition.ClassReference = BdoData.Class(type);
                                    definition.LibraryId = extensionDefinition?.Id;

                                    // we create the runtime definition

                                    var itemDefinition = new BdoScriptwordDefinition(null, extensionDefinition);

                                    try
                                    {
                                        if (methodInfo.GetParameters().Length == 0)
                                        {
                                            itemDefinition.RuntimeBasicFunction += methodInfo.CreateDelegate(
                                                typeof(BdoScriptwordDelegate)) as BdoScriptwordDelegate;
                                        }
                                        else
                                        {
                                            itemDefinition.RuntimeScopedFunction += methodInfo.CreateDelegate(
                                                typeof(BdoScriptwordDomainedDelegate)) as BdoScriptwordDomainedDelegate;
                                        }

                                        scriptwordDefinitions.Add(itemDefinition);

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

                // we recursively retrieve the sub script words

                foreach (var definition in dico)
                {
                    _store.Add(definition);
                }
            }

            return count;
        }
    }
}
