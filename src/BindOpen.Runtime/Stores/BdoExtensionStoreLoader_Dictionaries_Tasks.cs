﻿using BindOpen.Extensions.Processing;
using BindOpen.Logging;
using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Runtime.Definition;
using System;
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
        /// Loads the task dico from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="extensionDefinition">The extension definition to consider.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadTaskDictionaryFromAssembly(
            Assembly assembly,
            IBdoExtensionDefinition extensionDefinition,
            IBdoLog log = null)
        {
            if (assembly == null)
            {
                return -1;
            }

            // we load the entity dico from the assembly

            var dico = ExtractDictionaryFromAssembly<IBdoTaskDefinition>(assembly, log);

            // we feach task classes

            int count = 0;

            var types = assembly.GetTypes().Where(p => typeof(IBdoTask).IsAssignableFrom(p));
            foreach (Type type in types)
            {
                var definition = new BdoTaskDefinition(null, extensionDefinition)
                {
                    ItemClass = type.FullName,
                    LibraryId = extensionDefinition?.Id,
                    RuntimeType = type
                };

                if (type.GetCustomAttributes(typeof(BdoTaskAttribute)).FirstOrDefault() is BdoTaskAttribute taskAttribute)
                {
                    UpdateDictionary(definition, taskAttribute);
                }

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoTaskInputAttribute)).Any()))
                {
                    definition.InputSpecification.Add(BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                foreach (PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoTaskOutputAttribute)).Any()))
                {
                    definition.OutputSpecification.Add(BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                if (dico != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add<IBdoTaskDefinition>(definition);

                count++;
            }

            return count;
        }
    }
}