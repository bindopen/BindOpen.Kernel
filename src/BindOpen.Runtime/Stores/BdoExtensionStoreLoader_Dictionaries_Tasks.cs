using BindOpen.Data;
using BindOpen.Data.Items;
using BindOpen.Data.Meta;
using BindOpen.Extensions.Processing;
using BindOpen.Logging;
using BindOpen.Runtime.Definitions;
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
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="extensionDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadTaskDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition extensionDefinition,
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
            foreach (var type in types)
            {
                var definition = new BdoTaskDefinition(null, extensionDefinition)
                {
                    ClassReference = BdoData.Class(type),
                    LibraryId = extensionDefinition?.Id,
                    RuntimeType = type
                };

                if (type.GetCustomAttributes(typeof(BdoTaskAttribute)).FirstOrDefault() is BdoTaskAttribute taskAttribute)
                {
                    UpdateDictionary(definition, taskAttribute);
                }

                foreach (var property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoInputAttribute)).Any()))
                {
                    definition.InputSpecDetail ??= BdoMeta.NewSpecSet();
                    definition.InputSpecDetail.Add(BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                foreach (var property in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoOutputAttribute)).Any()))
                {
                    definition.OutputSpecDetail ??= BdoMeta.NewSpecSet();
                    definition.OutputSpecDetail.Add(BdoMeta.NewSpec(property.Name, property.PropertyType));
                }

                // we build the runtime definition

                if (dico != null)
                {
                    // retrieve the definition index

                    // update definition with index
                }

                _store.Add(definition);

                count++;
            }

            return count;
        }
    }
}
