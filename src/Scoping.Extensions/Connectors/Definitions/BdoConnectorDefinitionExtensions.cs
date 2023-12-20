using AutoMapper;
using BindOpen.Data;
using BindOpen.Scoping.Connectors;
using System;
using System.Reflection;

namespace BindOpen.Scoping
{
    /// <summary>
    /// This class represents a connection service.
    /// </summary>
    public static partial class BdoConnectorDefinitionExtensions
    {
        public static void UpdateFrom(
            this IBdoConnectorDefinition definition,
            Type type)
        {
            if (definition != null && type != null)
            {
                definition.Name = type.Name;
                definition.ClassReference = BdoData.Class(type);
                definition.RuntimeType = type;

                // we update definition from connector attribute

                foreach (var attribute in type.GetCustomAttributes(typeof(BdoConnectorAttribute)))
                {
                    var connectorAttribute = attribute as BdoConnectorAttribute;
                    definition.UpdateFrom(connectorAttribute);
                }
            }
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param key="definition">The definition to consider.</param>
        /// <param key="attribute">The attribute to consider.</param>
        public static void Update(
            this IBdoConnectorDefinition definition,
            IBdoConnectorDefinition refDef)
        {
            if (definition != null && refDef != null)
            {
                var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoConnectorDefinition, IBdoConnectorDefinition>()
                        .ForMember(q => q.Items, opt => opt.Ignore())
                        .ForAllMembers(x => x.Condition(
                          (src, dest, sourceValue) => sourceValue != null))
                );

                definition.Update(refDef);

                var mapper = new Mapper(config);
                mapper.Map(definition, refDef);
            }
        }
    }
}
