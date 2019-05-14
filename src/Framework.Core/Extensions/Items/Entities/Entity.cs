using BindOpen.Framework.Core.Data.Elements._Object;
using BindOpen.Framework.Core.Data.Elements;
using BindOpen.Framework.Core.Extensions.Items.Entities.Definition;
using BindOpen.Framework.Core.System.Diagnostics;

namespace BindOpen.Framework.Core.Extensions.Items.Entities
{
    /// <summary>
    /// This class represents an data entity item.
    /// </summary>
    public abstract class Entity : TAppExtensionItem<EntityDefinition>, IEntity
    {
        new public IEntityConfiguration Configuration { get => base.Configuration as IEntityConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        protected Entity() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Entity class.
        /// </summary>
        /// <param name="config">The configuration of this instance.</param>
        protected Entity(IEntityConfiguration config) : base(config)
        {
        }

        #endregion

        //// ------------------------------------------
        //// MUTATORS
        //// ------------------------------------------

        //#region Mutators

        ///// <summary>
        ///// Sets the specififed configuration.
        ///// </summary>
        ///// <param name="configuration">The configuration to consider.</param>
        //public void SetConfiguration(TAppExtensionItemDto<EntityDefinition> configuration)
        //{
        //    if (configuration == null
        //        || (AppScope != null && configuration.KeyEquals(this.DefinitionUniqueId)))
        //    {
        //        configuration = this.AppScope.AppExtension.CreateConfiguration<EntityDefinition>(this.DefinitionUniqueId) as EntityConfiguration;
        //    }

        //    if (configuration != null)
        //        this.Update(configuration);
        //}

        ///// <summary>
        ///// Sets the specified value.
        ///// </summary>
        ///// <param name="value">The value to set.</param>
        ///// <param name="propertyName">The calling property name to consider.</param>
        //public void Set(object value, [CallerMemberName] String propertyName = null)
        //{
        //    if (propertyName != null)
        //    {
        //        DataElementAttribute attribute = null;
        //        PropertyInfo propertyInfo = this.GetPropertyInfo(
        //            this.GetType(),
        //            propertyName,
        //            new Type[] { typeof(DetailPropertyAttribute) },
        //            ref attribute,
        //            this.AppScope);

        //        if (attribute is DetailPropertyAttribute)
        //        {
        //            (this.Detail ?? (this.Detail = new DataElementSet())).AddElement(attribute.Name, value, propertyInfo.PropertyType.GetValueType());
        //        }
        //    }
        //}

        //#endregion

        //// --------------------------------------------------
        //// ACCESSORS
        //// --------------------------------------------------

        //#region Accessors

        /// <summary>
        /// Returns a data element representing this instance.
        /// </summary>
        /// <param name="name">The name of the element to create.</param>
        /// <param name="log">The log of the operation.</param>
        /// <returns>Retuns the data element that represents this instace.</returns>
        public IObjectElement AsElement(string name=null, ILog log = null)
        {
            return ElementFactory.CreateObject(name ?? Name, base.Configuration as IEntityConfiguration);
        }
        ///// <summary>
        ///// Gets the specified value.
        ///// </summary>
        ///// <param name="propertyName">The calling property name to consider.</param>
        //public T Get<T>([CallerMemberName] String propertyName = null)
        //{
        //    if (propertyName != null)
        //    {
        //        DataElementAttribute attribute = null;
        //        PropertyInfo propertyInfo = this.GetPropertyInfo(
        //            this.GetType(),
        //            propertyName,
        //        new Type[] { typeof(DetailPropertyAttribute) },
        //        ref attribute,
        //        this.AppScope);

        //        if (attribute is DetailPropertyAttribute)
        //        {
        //            Object value = this.Detail?.GetElementItemObject(attribute.Name, this.AppScope);
        //            if (value is T)
        //                return (T)value;
        //        }
        //    }

        //    return default(T);
        //}

        ///// <summary>
        ///// Gets the specified value.
        ///// </summary>
        ///// <param name="propertyName">The calling property name to consider.</param>
        ///// <param name="defaultValue">The default value to consider.</param>
        //public T Get<T>(T defaultValue, [CallerMemberName] String propertyName = null) where T : struct, IConvertible
        //{
        //    if (propertyName != null)
        //    {
        //        DataElementAttribute attribute = null;
        //        PropertyInfo propertyInfo = this.GetPropertyInfo(
        //            this.GetType(),
        //            propertyName,
        //            new Type[] { typeof(DetailPropertyAttribute) },
        //            ref attribute,
        //            this.AppScope);

        //        if (attribute is DetailPropertyAttribute)
        //            return (this.Detail.GetElementItemObject(attribute.Name, this.AppScope) as string).ToEnum<T>(defaultValue); ;
        //    }

        //    return default(T);
        //}

        //#endregion
    }
}