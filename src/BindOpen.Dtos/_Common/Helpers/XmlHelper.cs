using BindOpen.Logging;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BindOpen.Dtos.Xml
{
    public static class XmlHelper
    {
        /// <summary>
        /// Saves the XML string of this instance.
        /// </summary>
        /// <param key="dto">The DTO to save.</param>
        /// <param key="log">The saving log to consider.</param>
        /// <returns>The Xml string of this instance.</returns>
        public static string ToXml(this object dto, IBdoLog log = null)
        {
            if (dto == null) return null;
            XmlSerializer serializer = new(dto.GetType());

            var st = string.Empty;
            try
            {
                using var writer = new StringWriter();
                serializer.Serialize(writer, dto);
                st = writer.ToString();
            }
            catch (JsonException ex)
            {
                log?.AddException(
                    "Exception occured while serializing object",
                    description: ex.ToString());
            }

            return st;
        }

        /// <summary>
        /// Saves this instance to the specified XML file path.
        /// </summary>
        /// <param key="dto">The DTO to save.</param>
        /// <param key="filePath">Path of the file to save.</param>
        /// <param key="log">The log to consider.</param>
        /// <returns>True if the saving operation has been done. False otherwise.</returns>
        public static bool SaveXml(this object dto, string filePath, IBdoLog log = null)
        {
            if (dto == null) return false;

            if (!string.IsNullOrEmpty(filePath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                try
                {
                    XmlSerializer serializer = new(dto.GetType());
                    using var writer = new StreamWriter(filePath, false, Encoding.UTF8);
                    serializer.Serialize(writer, dto);
                }
                catch (JsonException ex)
                {
                    log?.AddException(
                        "Exception occured while serializing object",
                        description: ex.ToString());
                }

                return true;
            }

            return false;
        }

        // Deserialiaze ----------------------------

        /// <summary>
        /// Loads a data item from the specified XML file path.
        /// </summary>
        /// <param key="filePath">The path of the file to load.</param>
        /// <param key="log">The output log of the method.</param>
        /// <param key="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <param key="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The loaded log.</returns>
        /// <remarks>If the XML schema set is null then the schema is not checked.</remarks>
        public static T LoadXml<T>(
            string filePath,
            IBdoLog log = null,
            XmlSchemaSet xmlSchemaSet = null,
            bool mustFileExist = true) where T : class
        {
            T dto = default;

            if (!File.Exists(filePath))
            {
                if (mustFileExist)
                {
                    log?.AddError("File not found");
                }
            }
            else
            {
                if (xmlSchemaSet != null)
                {
                    XDocument document = XDocument.Load(filePath);
                    document?.Validate(xmlSchemaSet, (o, e) => log?.AddError("Invalid file"));
                }

                try
                {
                    XmlSerializer serializer = new(typeof(T));
                    using var reader = new StreamReader(filePath);
                    dto = serializer.Deserialize(XmlReader.Create(reader)) as T;
                }
                catch (JsonException ex)
                {
                    log?.AddException(
                        "Exception occured while deserializing file",
                        description: ex.ToString());
                }
            }

            return dto;
        }

        /// <summary>
        /// Loads the data item from the specified XML file path.
        /// </summary>
        /// <typeparam name="T">The data item class to consider.</typeparam>
        /// <param key="xmlString">The XML string to load.</param>
        /// <param key="log">The output log of the load method.</param>
        /// <param key="xmlSchemaSet">The XML schema set to consider for checking.</param>
        /// <returns>The loaded log.</returns>
        /// <remarks>If the XML schema set is null then the schema is not checked.</remarks>
        public static T LoadXmlFromString<T>(
            string xmlString,
            IBdoLog log = null,
            XmlSchemaSet xmlSchemaSet = null) where T : class
        {
            T dto = default;

            if (xmlString != null)
            {
                if (xmlSchemaSet != null)
                {
                    XDocument document = XDocument.Parse(xmlString);
                    document.Validate(xmlSchemaSet, (o, e) =>
                    {
                        log?.AddError(
                            title: "Invalid Xml string",
                            description: e.Message);
                    });
                }

                try
                {
                    XmlSerializer serializer = new(typeof(T));
                    using var reader = new StreamReader(xmlString);
                    dto = serializer.Deserialize(XmlReader.Create(reader)) as T;
                }
                catch (JsonException ex)
                {
                    log?.AddException(
                        "Exception occured while deserializing string",
                        description: ex.ToString());
                }
            }

            return dto;
        }

        /// <summary>
        /// Loads the specified XML schema set.
        /// </summary>
        /// <param key="xmlSchemaSet">The XML schema set to consider.</param>
        /// <param key="assembly">The assembly to consider.</param>
        /// <param key="xsdResources">The XSD resources to consider.</param>
        /// <returns>The XML schema set.</returns>
        public static XmlSchemaSet LoadXmlSchemaSet(
            XmlSchemaSet xmlSchemaSet,
            Assembly assembly,
            List<string> xsdResources)
        {
            xmlSchemaSet ??= new XmlSchemaSet();

            foreach (string currentXsdResource in xsdResources)
            {
                var stream = assembly.GetManifestResourceStream(currentXsdResource);
                xmlSchemaSet.Add("https://xsd.bindopen.org", XmlReader.Create(new StreamReader(stream)));
            }

            return xmlSchemaSet;
        }
    }
}