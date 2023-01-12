using BindOpen.Meta;
using BindOpen.Meta.Items;
using BindOpen.Runtime.Definition;
using System.Collections.Generic;

namespace BindOpen.Extensions
{
    /// <summary>
    /// This class represents a BindOpen extension titled item configuration.
    /// </summary>
    public abstract class TBdoExtensionTitledItemConfiguration<T>
        : TBdoExtensionItemConfiguration<T>,
        ITBdoExtensionTitledItemConfiguration<T>
        where T : IBdoExtensionItemDefinition
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionTitledItemConfiguration class.
        /// </summary>
        protected TBdoExtensionTitledItemConfiguration() : this(BdoExtensionItemKind.Any, null)
        {
        }

        /// <summary>
        /// Instantiates a new instance of the TBdoExtensionTitledItemConfiguration class.
        /// </summary>
        /// <param name="kind">The kind to consider.</param>
        /// <param name="definitionUniqueId">The definition unique ID to consider.</param>
        protected TBdoExtensionTitledItemConfiguration(
            BdoExtensionItemKind kind,
            string definitionUniqueId) : base(kind, definitionUniqueId)
        {
        }

        #endregion

        // ------------------------------------------
        // IDataItem Implementation
        // ------------------------------------------

        #region IDataItem

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override object Clone(params string[] areas)
        {
            ITBdoExtensionTitledItemConfiguration<T> dto = base.Clone(areas) as TBdoExtensionTitledItemConfiguration<T>;
            dto.WithTitle(Title?.Clone<BdoDictionary>());

            return dto;
        }

        #endregion

        // ------------------------------------------
        // IGloballyTitled Implementation
        // ------------------------------------------

        #region IGloballyTitled

        /// <summary>
        /// 
        /// </summary>
        public IBdoDictionary Title { get; set; }

        public ITBdoExtensionTitledItemConfiguration<T> AddTitle(KeyValuePair<string, string> item)
        {
            Title ??= BdoMeta.NewDictionary();
            Title.Add(item);
            return this;
        }

        public ITBdoExtensionTitledItemConfiguration<T> WithTitle(IBdoDictionary dico)
        {
            Title = dico;
            return this;
        }

        public string GetTitleText(string key = "*", string defaultKey = "*")
        {
            return Title?[key, defaultKey];
        }

        #endregion
    }

}
