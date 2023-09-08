using AutoMapper;
using BindOpen.Kernel.Data;
using System;
using System.Reflection;

namespace BindOpen.Kernel.Scoping.Tasks
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoTaskDefinitionExtensions
    {
        public static void UpdateFrom(
            this IBdoTaskDefinition definition,
            Type type)
        {
            if (definition != null && type != null)
            {
                definition.Name = type.Name;
                definition.ClassReference = BdoData.Class(type);
                definition.RuntimeType = type;

                // we update definition from task attribute

                foreach (var attribute in type.GetCustomAttributes(typeof(BdoTaskAttribute)))
                {
                    var taskAttribute = attribute as BdoTaskAttribute;
                    definition.UpdateFrom(taskAttribute);
                }
            }
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param key="definition">The definition to consider.</param>
        /// <param key="attribute">The attribute to consider.</param>
        public static void Update(
            this IBdoTaskDefinition definition,
            IBdoTaskDefinition refDef)
        {
            if (definition != null && refDef != null)
            {
                var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoTaskDefinition, IBdoTaskDefinition>()
                        .ForMember(q => q.Items, opt => opt.Ignore())
                        .ForMember(q => q.OutputSpecs, opt => opt.Ignore())
                        .ForAllMembers(x => x.Condition(
                          (src, dest, sourceValue) => sourceValue != null))
                );

                definition.Update(refDef);
                definition.OutputSpecs.Update(refDef.OutputSpecs);

                var mapper = new Mapper(config);
                mapper.Map(definition, refDef);
            }
        }
    }
}
