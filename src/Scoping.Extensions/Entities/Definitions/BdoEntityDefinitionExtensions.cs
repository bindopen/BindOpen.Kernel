using BindOpen.Data;
using BindOpen.Scoping.Entities;
using System;
using System.Reflection;

namespace BindOpen.Scoping
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
                definition.Name = type.Name;
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
                definition.Update(refDef);
            }

            return definition;
        }
    }
}
