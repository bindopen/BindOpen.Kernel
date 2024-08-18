using BindOpen.Data;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using BindOpen.Scoping.Entities;
using System.Linq;
using System.Reflection;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a extension scope loader.
    /// </summary>
    internal partial class BdoExtensionStoreLoader : BdoObject, IBdoExtensionStoreLoader
    {
        /// <summary>
        /// Loads the entity dico from the specified assembly.
        /// </summary>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="packageDefinition">The extension definition to consider.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns></returns>
        private int LoadEntityDictionaryFromAssembly(
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

            var types = assembly.GetTypes().Where(p => typeof(IBdoEntity).IsAssignableFrom(p));
            int count = 0;

            foreach (var type in types)
            {
                var definition = new BdoEntityDefinition(null, packageDefinition)
                {
                    LibraryId = packageDefinition?.Identifier,
                };

                definition.UpdateFrom(type);

                foreach (var prop in type.GetProperties().Where(p => p.GetCustomAttributes(typeof(BdoPropertyAttribute)).Any()))
                {
                    var spec = BdoData.NewSpec();
                    spec.UpdateFrom(prop, typeof(BdoPropertyAttribute));
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

            return count;
        }
    }
}
