using BindOpen.Data;
using BindOpen.Data.Helpers;

namespace BindOpen.Scoping
{
    /// <summary>
    /// 
    /// </summary>
    public static class BdoExtensionDefinitionExtensions
    {
        /// <summary>
        /// Updates this instance with the specified attribute information.
        /// </summary>
        /// <param key="definition">The definition to consider.</param>
        /// <param key="attribute">The attribute to consider.</param>
        public static void UpdateFrom(
            this IBdoExtensionDefinition def,
            MetaExtensionAttribute att)
        {
            if (def != null)
            {
                if (!string.IsNullOrEmpty(att.Name))
                {
                    def.Name = att.Name?.IndexOf("$") > 0 ?
                        att.Name[(att.Name.IndexOf("$") + 1)..] : att.Name;
                }

                if (!string.IsNullOrEmpty(att.Title))
                {
                    def.Title = BdoData.NewDictionary(att.Title);
                }

                if (!string.IsNullOrEmpty(att.Description))
                {
                    def.Description = BdoData.NewDictionary(att.Description);
                }

                if (!string.IsNullOrEmpty(att.CreationDate))
                {
                    def.CreationDate = att.CreationDate.ToDateTime();
                }

                if (!string.IsNullOrEmpty(att.LastModificationDate))
                {
                    def.LastModificationDate = att.LastModificationDate.ToDateTime();
                }
            }
        }
    }
}