using AutoMapper;
using BindOpen.Data;
using System;
using System.Reflection;

namespace BindOpen.Extensions.Entities
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoEntityDefinitionExtensions
    {
        public static void UpdateFrom(
            this IBdoEntityDefinition definition,
            Type type)
        {
            if (definition != null && type != null)
            {
                definition.ClassReference = BdoData.Class(type);
                definition.RuntimeType = type;

                // we update definition from entity attribute

                foreach (var attribute in type.GetCustomAttributes(typeof(BdoEntityAttribute)))
                {
                    var entityAttribute = attribute as BdoEntityAttribute;
                    definition.UpdateFrom(entityAttribute);
                }
            }
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param key="definition">The definition to consider.</param>
        /// <param key="attribute">The attribute to consider.</param>
        public static T Update<T>(
            this T definition,
            IBdoEntityDefinition refDef)
            where T : IBdoEntityDefinition
        {
            if (definition != null && refDef != null)
            {
                var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoEntityDefinition, IBdoEntityDefinition>()
                        .ForMember(q => q.Items, opt => opt.Ignore())
                        .ForAllMembers(x => x.Condition(
                          (src, dest, sourceValue) => sourceValue != null))
                );

                definition.Update(refDef);

                var mapper = new Mapper(config);
                mapper.Map(refDef, definition);
            }

            return definition;
        }
    }
}
