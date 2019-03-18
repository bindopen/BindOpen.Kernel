using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Extensions.Configuration;
using BindOpen.Framework.Core.Extensions.Configuration.Formats;
using BindOpen.Framework.Core.Extensions.Definition.Formats;

namespace BindOpen.Framework.Core.Extensions.Runtime.Formats
{

    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
    [XmlType("Format", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public abstract class Format : FormatConfiguration, ITAppExtensionRuntimeItem<FormatDefinition>
    {
        // -----------------------------------------------
        // PROPERTIES
        // -----------------------------------------------

        #region Properties

        /// <summary>
        /// The application scope of this instance.
        /// </summary>
        [XmlIgnore()]
        public IAppScope AppScope { get; set; } = null;

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        public Format() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="configuration">The configuration to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        public Format(
            String name,
            String definitionName,
            FormatConfiguration configuration = null,
            String namePreffix = "format_",
            AppScope appScope = null)
            : base(name, definitionName, namePreffix)
        {
            this.AppScope = appScope;
            this.SetConfiguration(configuration);
        }

        #endregion

        //// --------------------------------------------------
        //// ACCESSORS
        //// --------------------------------------------------

        //#region Accessors

        ///// <summary>
        ///// Returns the string value of the specified settings.
        ///// </summary>
        ///// <param name="name">Name of the settings to consider.</param>
        ///// <returns>The string value of the specified settings.</returns>
        //public String GetStringValue(String name)
  //      {
  //          String stringValue="";

  //          PropertyInfo aInputProperty = this.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
  //          if (aInputProperty != null)
  //          {
  //              Object object1Value = aInputProperty.GetValue(this, null);
  //              if (object1Value != null)
  //                  stringValue = object1Value.ToString();
  //          }

  //          return stringValue;
  //      }

  //      /// <summary>
  //      /// Returns the object value of the specified settings.
  //      /// </summary>
  //      /// <param name="name">Name of the settings to consider.</param>
  //      /// <returns>The object value of the specified settings.</returns>
  //      public Object GetObjectValue(String name)
  //      {
  //          Object object1 = "";

  //          PropertyInfo aInputProperty = this.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
  //          if (aInputProperty != null)
  //              object1 = aInputProperty.GetValue(this, null);

  //          return object1;
  //      }

        //#endregion

        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specififed configuration.
        /// </summary>
        /// <param name="configuration">The configuration to consider.</param>
        public void SetConfiguration(TAppExtensionItemConfiguration<FormatDefinition> configuration)
        {
            if (configuration == null
                || (this.AppScope != null && configuration.KeyEquals(this.DefinitionUniqueId)))
            {
                configuration = this.AppScope.AppExtension.CreateConfiguration<FormatDefinition>(this.DefinitionUniqueId) as FormatConfiguration;
            }

            if (configuration != null)
                this.Update(configuration);
        }

        ///// <summary>
        ///// Sets the specified value.
        ///// </summary>
        ///// <param name="value">The value to set.</param>
        //public void Set(Object value)
        //{
        //    StackFrame caller = new StackFrame(1);

        //    if (this.Detail == null)
        //        this.Detail = new DataElementSet();

        //    this.Set(caller, this.Detail, value, this.AppScope);
        //}

        #endregion

        //// --------------------------------------------------
        //// ACCESSORS
        //// --------------------------------------------------

        //#region Accessors

        ///// <summary>
        ///// Gets the specified value.
        ///// </summary>
        //public T Get<T>()
        //{
        //    StackFrame caller = new StackFrame(1);
        //    return this.Get<T>(caller, this.Detail, typeof(DetailPropertyAttribute), this.AppScope);
        //}

        ///// <summary>
        ///// Gets the specified value.
        ///// </summary>
        ///// <param name="defaultValue">The default value to consider.</param>
        //public T Get<T>(T defaultValue = default(T)) where T : struct, IConvertible
        //{
        //    StackFrame caller = new StackFrame(1);
        //    return this.Get<T>(caller, this.Detail, typeof(DetailPropertyAttribute), this.AppScope, defaultValue);
        //}

        //#endregion
    }

}
