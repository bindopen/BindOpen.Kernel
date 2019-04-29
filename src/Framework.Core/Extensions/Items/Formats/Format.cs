using System;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Extensions.Items.Formats.Definition;

namespace BindOpen.Framework.Core.Extensions.Items.Formats
{
    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
    [XmlType("Format", Namespace = "https://bindopen.org/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "https://bindopen.org/xsd", IsNullable = false)]
    public abstract class Format : TAppExtensionItem<IFormatDefinition>, IFormat
    {
        new public IFormatConfiguration Configuration { get => base.Configuration as IFormatConfiguration; }

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        protected Format() : base()
        {
        }

        /// <summary>
        /// Instantiates a new instance of the Format class.
        /// </summary>
        /// <param name="dto">The DTO item of this instance.</param>
        protected Format(IFormatConfiguration dto)
        {
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
        //public string GetStringValue(String name)
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
        //      public object GetObjectValue(String name)
        //      {
        //          Object object1 = "";

        //          PropertyInfo aInputProperty = this.GetType().GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
        //          if (aInputProperty != null)
        //              object1 = aInputProperty.GetValue(this, null);

        //          return object1;
        //      }

        //#endregion

        //// ------------------------------------------
        //// MUTATORS
        //// ------------------------------------------

        //#region Mutators

        /////// <summary>
        /////// Sets the specified value.
        /////// </summary>
        /////// <param name="value">The value to set.</param>
        ////public void Set(object value)
        ////{
        ////    StackFrame caller = new StackFrame(1);

        ////    if (this.Detail == null)
        ////        this.Detail = new DataElementSet();

        ////    this.Set(caller, this.Detail, value, this.AppScope);
        ////}

        //#endregion

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
