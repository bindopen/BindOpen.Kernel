using System;
using BindOpen.Framework.Core.Data.Helpers.Objects;

namespace BindOpen.Framework.Core.Extensions.Configuration
{
    /// <summary>
    /// This class represents an application extension item configuration.
    /// </summary>
    /// <typeparam name="T">The definition class of this instance.</typeparam>
    public static class TAppExtensionItemConfigurationFactory
    {
        /// <summary>
        /// Initializes the definition of this instance.
        /// </summary>
        public virtual void InitializeDefinition()
        {
        }

        /// <summary>
        /// Sets the definition of this instance.
        /// </summary>
        /// <param name="definition">The definition to consider.</param>
        /// <param name="isDefinitionChecked">Indicates whether the definition must be checked.</param>
        public virtual void SetDefinition(T definition=null, bool isDefinitionChecked = true)
        {
            if (!isDefinitionChecked || (definition?.KeyEquals(DefinitionUniqueId) == true))
            {
                _definition = definition;
                _definitionUniqueId = definition?.Key();
            }
            else
            {
                _definition = null;
            }
        }

        /// <summary>
        /// Sets the specififed definition.
        /// </summary>
        /// <param name="appExtension">The application extension to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        public void SetDefinition(AppExtension appExtension, String definitionName = null)
        {
            T definition = null;
            if (appExtension != null)
            {
                definitionName = (definitionName ?? _definitionUniqueId);
                definition = appExtension.GetItemDefinitionWithUniqueId<T>(definitionName) as T;
                if (definition != null)
                {
                    _definitionUniqueId = definition.Key();
                    SetDefinition(definition);
                }
            }
        }
    }
}
