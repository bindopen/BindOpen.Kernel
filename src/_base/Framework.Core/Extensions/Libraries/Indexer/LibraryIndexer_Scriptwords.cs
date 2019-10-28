using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Indexes.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition;
using BindOpen.Framework.Core.Extensions.Items.Scriptwords.Definition.Dto;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Libraries
{
    /// <summary>
    /// This static class provices methods to index library items.
    /// </summary>
    public static partial class LibraryIndexer
    {
        /// <summary>
        /// References the specified items into the specified library.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="indexDto">The script word index to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static int IndexScriptwords(
            this ILibrary library,
            Assembly assembly,
            ScriptwordIndexDto indexDto = null,
            ILog log = null)
        {
            if ((library == null) || (assembly == null))
            {
                return -1;
            }

            // we define definitions

            List<IScriptwordDefinition> definitions = new List<IScriptwordDefinition>();

            int count = 0;

            if (indexDto == null)
            {
                log?.AddWarning(title: "Script word index missing");
            }
            else
            {
                var types = assembly.GetTypes().Where(p => p.GetCustomAttributes(typeof(ScriptwordDefinitionAttribute)).Any());
                foreach (Type type in types)
                {
                    // we feach methods
                    var methodInfos = type.GetMethods(BindingFlags.Public | BindingFlags.Static);
                    foreach (MethodInfo methodInfo in methodInfos)
                    {
                        if (methodInfo.GetCustomAttribute(typeof(ScriptwordAttribute)) is ScriptwordAttribute scriptWordAttribute)
                        {
                            // we determine the name of the definition

                            string definitionName = scriptWordAttribute.Name;

                            // we update the definition with the index if there is one

                            if (indexDto != null)
                            {
                                IScriptwordDefinitionDto definitionDto = indexDto.GetDefinition(definitionName, methodInfo.Name);

                                if (definitionDto == null)
                                {
                                    log?.AddError(title: "Script word '" + methodInfo.Name + "' not found in index");
                                }
                                else
                                {
                                    definitionDto.CallingClass = type.FullName;
                                    definitionDto.LibraryName = library?.Name;

                                    // we create the runtime definition

                                    ScriptwordDefinition definition = new ScriptwordDefinition(library, definitionDto);

                                    try
                                    {
                                        definition.RuntimeFunction += methodInfo.CreateDelegate(
                                            typeof(ScriptwordFunction), definition.RuntimeFunction) as ScriptwordFunction;

                                        definitions.Add(definition);

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

                // we build the tree

                library.BuildScriptwordTree(indexDto, definitions, log);
            }

            return count;
        }

        private static void BuildScriptwordTree(
            this ILibrary library,
            ScriptwordIndexDto indexDto,
            List<IScriptwordDefinition> allDefinitions,
            ILog log = null,
            IScriptwordDefinition parentDefinition = null)
        {
            if (library == null || indexDto==null) return;

            List<ScriptwordDefinitionDto> scriptWordDefinitionDtos = parentDefinition == null ? indexDto.Definitions : parentDefinition?.Dto.Children;

            // we recursively retrieve the sub script words
            foreach(IScriptwordDefinitionDto definitionDto in scriptWordDefinitionDtos)
            {
                // if the current script word is a reference then
                if (!string.IsNullOrEmpty(definitionDto.ReferenceUniqueName))
                {
                    // we retrieve the reference script word
                    IScriptwordDefinition referenceScriptwordDefinition = allDefinitions.Find(p=>p.KeyEquals(definitionDto.ReferenceUniqueName)==true);

                    if (referenceScriptwordDefinition == null)
                    {
                        log?.AddError(
                            title: "Child reference '" + definitionDto.ReferenceUniqueName + "' not found in index for script word '" + definitionDto.Key() + "'");
                    }
                    else
                    {
                        parentDefinition?.Children?.Add(referenceScriptwordDefinition);
                    }
                }
                else
                {
                    IScriptwordDefinition definition = allDefinitions.Find(p => p.Dto?.KeyEquals(definitionDto) == true);

                    if (definition==null)
                    {
                        log?.AddError(title: "Script word '" + definitionDto.Key() + "' not found in code");
                    }
                    else
                    {
                        if (parentDefinition != null)
                        {
                            parentDefinition.Children.Add(definition);
                            definition.Parent = parentDefinition;
                        }
                        else
                        {
                            library.Add<IScriptwordDefinition>(definition);
                        }

                        library.BuildScriptwordTree(indexDto, allDefinitions, log, definition);
                    }
                }
            }
        }
    }
}
