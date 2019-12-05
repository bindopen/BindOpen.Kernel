using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Definition.Dictionaries;
using BindOpen.Framework.Core.Extensions.Definition.Extensions;
using BindOpen.Framework.Core.Extensions.Definition.Items;
using BindOpen.Framework.Core.Extensions.Runtime.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Framework.Core.Extensions.Runtime.Stores
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : DataItem, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the script word dictionary from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadScripwordDictionaryFromAssembly(
            Assembly assembly,
            IBdoExtensionDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the carrier dictionary from the assembly

            IBdoScriptwordDictionaryDto dictionaryDto = (IBdoScriptwordDictionaryDto)ExtractDictionaryFromAssembly<BdoScriptwordDefinitionDto>(assembly, log);

            // we define definitions

            int count = 0;

            if (dictionaryDto == null)
            {
                log?.AddWarning(title: "No script word dictionary was found");
            }
            else
            {
                List<BdoScriptwordDefinition> scriptwordDefinitions = new List<BdoScriptwordDefinition>();

                var types = assembly.GetTypes().Where(p => p.GetCustomAttributes(typeof(BdoScriptwordDefinitionAttribute)).Any());
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

                            // we update the definition with the dictionary if there is one

                            if (dictionaryDto != null)
                            {
                                IBdoScriptwordDefinitionDto definitionDto = dictionaryDto.GetDefinition(definitionName, methodInfo.Name);

                                if (definitionDto == null)
                                {
                                    log?.AddError(title: "Script word '" + methodInfo.Name + "' not found in dictionary");
                                }
                                else
                                {
                                    definitionDto.CallingClass = type.FullName;
                                    definitionDto.LibraryId = extensionDefinition?.Dto?.Id;

                                    // we create the runtime definition

                                    BdoScriptwordDefinition itemDefinition = new BdoScriptwordDefinition(extensionDefinition, definitionDto);

                                    try
                                    {
                                        itemDefinition.RuntimeFunction += methodInfo.CreateDelegate(
                                            typeof(BdoScriptwordFunction), itemDefinition.RuntimeFunction) as BdoScriptwordFunction;


                                        scriptwordDefinitions.Add(itemDefinition);

                                        count++;
                                    }
                                    catch
                                    {
                                        log?.AddError(
                                                title: "Incompatible function ('" + methodInfo.Name + "')",
                                                description: "Function '" + definitionDto.RuntimeFunctionName + "' in class '" + definitionDto.CallingClass + "' has inexpected parameters.");
                                    }
                                }
                            }
                        }
                    }
                }

                // we build the script word tree

                BuildScriptwordTree(dictionaryDto, scriptwordDefinitions, log);
            }

            return count;
        }

        private void BuildScriptwordTree(
            IBdoScriptwordDictionaryDto dictionaryDto,
            List<BdoScriptwordDefinition> allDefinitions,
            IBdoLog log = null,
            IBdoScriptwordDefinition parentDefinition = null)
        {
            if (dictionaryDto == null) return;

            List<BdoScriptwordDefinitionDto> scriptWordDefinitionDtos = parentDefinition == null ? dictionaryDto.Definitions : parentDefinition?.Dto.Children;

            // we recursively retrieve the sub script words
            foreach (IBdoScriptwordDefinitionDto definitionDto in scriptWordDefinitionDtos)
            {
                // if the current script word is a reference then
                if (!string.IsNullOrEmpty(definitionDto.ReferenceUniqueName))
                {
                    // we retrieve the reference script word
                    IBdoScriptwordDefinition referenceScriptwordDefinition = allDefinitions.Find(p => p.KeyEquals(definitionDto.ReferenceUniqueName) == true);

                    if (referenceScriptwordDefinition == null)
                    {
                        log?.AddError(
                            title: "Child reference '" + definitionDto.ReferenceUniqueName + "' not found in index for script word '" + definitionDto.Key() + "'");
                    }
                    else
                    {
                        parentDefinition?.Children?.Add(referenceScriptwordDefinition.UniqueId?.ToUpper(), referenceScriptwordDefinition);
                    }
                }
                else
                {
                    IBdoScriptwordDefinition definition = allDefinitions.Find(p => p.Dto?.KeyEquals(definitionDto) == true);

                    if (definition == null)
                    {
                        log?.AddError(title: "Script word '" + definitionDto.Key() + "' not found in code");
                    }
                    else
                    {
                        if (parentDefinition != null)
                        {
                            parentDefinition.Children.Add(definition.UniqueId?.ToUpper(), definition);
                            definition.Parent = parentDefinition;
                        }
                        else
                        {
                            _store.Add<IBdoScriptwordDefinition>(definition);
                        }

                        BuildScriptwordTree(dictionaryDto, allDefinitions, log, definition);
                    }
                }
            }
        }
    }
}
