using BindOpen.Data;
using System;
using System.Reflection;

namespace BindOpen.Scoping.Tasks
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
                definition.Update(refDef);
                definition.Outputs.Update(refDef.Outputs);
            }
        }
    }
}
