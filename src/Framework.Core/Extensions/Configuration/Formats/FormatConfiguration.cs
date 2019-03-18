using BindOpen.Framework.Core.Extensions.Definition.Formats;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Configuration.Formats
{

    /// <summary>
    /// This class represents an format.
    /// </summary>
    [Serializable()]
    [XmlType("FormatConfiguration", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "format", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class FormatConfiguration : TAppExtensionTitledItemConfiguration<FormatDefinition>
    {

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        public FormatConfiguration()
            : this(null)
        {
        }

        /// <summary>
        /// This instantiates a new instance of the FormatConfiguration class.
        /// </summary>
        /// <param name="name">The name of this instance.</param>
        /// <param name="definitionName">The definition name to consider.</param>
        /// <param name="namePreffix">The name preffix to consider.</param>
        public FormatConfiguration(
            String name,
            String definitionName = null,
            String namePreffix = "format_")
            : base(name, definitionName, null, namePreffix)
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

  //      #endregion


        // ------------------------------------------
        // LOAD
        // ------------------------------------------

        #region Load

        /// <summary>
        /// Instantiates a new instance of SettingsDefinition class from a xml string.
        /// </summary>
        /// <param name="xmlString">The Xml string to load.</param>
        public static FormatConfiguration LoadFromXmlString(String xmlString)
        {
            FormatConfiguration settingsDefinition = null;
            try
            {
                // we parse the xml string
                XDocument xDocument = XDocument.Parse(xmlString);

                // then we load
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(FormatConfiguration));
                StringReader aStringReader = new StringReader(xmlString);
                settingsDefinition = (FormatConfiguration)xmlSerializer.Deserialize(XmlReader.Create(aStringReader));
            }
            catch
            {
            }

            return settingsDefinition;
        }

        #endregion


        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns the cloned metrics definition.</returns>
        public override Object Clone()
        {
            FormatConfiguration dataFormat = base.Clone() as FormatConfiguration;

            return dataFormat;
        }

        #endregion

    }

}
