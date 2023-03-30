using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Extensions;
using BindOpen.Extensions.Entities;
using BindOpen.Extensions.Tasks;
using BindOpen.Logging;
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
        /// Loads the task dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="packageDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadTaskDictionaryFromAssembly(
            Assembly assembly,
            IBdoPackageDefinition packageDefinition,
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
                var definition = new BdoTaskDefinition(null, packageDefinition)
                {
                    LibraryId = packageDefinition?.Id,
                };

                definition.UpdateFrom(type);

                foreach (var prop in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoInputAttribute)).Any()))
                {
                    var spec = BdoMeta.NewSpec();
                    spec.UpdateFrom(prop, typeof(BdoInputAttribute));
                    definition.Add(spec);
                }

                definition.OutputSpecs ??= BdoMeta.NewSpecSet();
                foreach (var prop in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoOutputAttribute)).Any()))
                {
                    var spec = BdoMeta.NewSpec();
                    spec.UpdateFrom(prop, typeof(BdoOutputAttribute));
                    definition.OutputSpecs.Add(spec);
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

            return count;
        }
    }
}
