using AutoMapper;
using System.Reflection;

namespace BindOpen.Extensions.Functions
{
    /// <summary>
    /// This class represents an application 
    /// </summary>
    public static class BdoFunctionDefinitionExtensions
    {
        public static void UpdateFrom(
            this IBdoFunctionDefinition definition,
            MemberInfo info)
        {
            if (definition != null && info != null)
            {
                // we update definition from function attribute

                foreach (var attribute in info.GetCustomAttributes(typeof(BdoFunctionAttribute)))
                {
                    var functionAttribute = attribute as BdoFunctionAttribute;
                    definition.UpdateFrom(functionAttribute);
                }

                definition.Name ??= info.Name;
            }
        }

        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param key="definition">The definition to consider.</param>
        /// <param key="attribute">The attribute to consider.</param>
        public static void Update(
            this IBdoFunctionDefinition definition,
            IBdoFunctionDefinition refDef)
        {
            if (definition != null && refDef != null)
            {
                var config = new MapperConfiguration(
                    cfg => cfg.CreateMap<IBdoFunctionDefinition, IBdoFunctionDefinition>()
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
