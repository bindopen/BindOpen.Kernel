using System;
using System.Linq;
using System.Reflection;
using BindOpen.Framework.Core.Data.Elements.Factories;
using BindOpen.Framework.Core.Extensions.Attributes;
using BindOpen.Framework.Core.Extensions.Definition.Connectors;
using BindOpen.Framework.Core.Extensions.Items.Connectors;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Libraries
{
    /// <summary>
    /// This static class provices methods to index library items.
    /// </summary>
    public static partial class LibraryIndexer
    {
        /// <summary>
        /// References the specified connectors into the specified library.
        /// </summary>
        /// <param name="library">The library to consider.</param>
        /// <param name="assembly">The assembly to consider.</param>
        /// <param name="isIndexLoaded">Indicates whether item indexes must be loaded.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns></returns>
        public static int IndexConnectors(
            this ILibrary library,
            Assembly assembly,
            bool isIndexLoaded = false,
            ILog log = null)
        {
            if ((library==null)||(assembly==null))
            {
                return -1;
            }

            log = log ?? new Log();

            // we feach connector classes

            var types = assembly.GetTypes().Where(p => typeof(Connector).IsAssignableFrom(p));
            int count = 0;
            foreach(Type type in types)
            {
                IConnectorDefinitionDto definition = new ConnectorDefinitionDto();

                if (type.GetCustomAttribute(typeof(ConnectorAttribute)) is ConnectorAttribute connectorAttribute)
                {
                    definition.Update(connectorAttribute);
                }

                foreach(PropertyInfo property in type.GetProperties().Where(p => p.GetCustomAttribute(typeof(DetailPropertyAttribute)) != null))
                {
                    definition.DatasourceDetailSpec.Add(ElementSpecFactory.Create(property.PropertyType));
                }

                if (isIndexLoaded)
                {
                    //definition.Update()
                }

                count++;
            }

            return count;
        }
    }
}
