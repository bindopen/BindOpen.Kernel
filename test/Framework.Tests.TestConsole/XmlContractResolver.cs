using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BindOpen.Framework.Labs.Platform.Data.Resolvers
{
    /// <summary>
    /// This class represents an application client.
    /// </summary>
    public class XmlContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            this.ConfigureProperty(member, property);
            return property;
        }

        private void ConfigureProperty(MemberInfo member, JsonProperty property)
        {
            Attribute attribute = null;

            if ((Attribute.IsDefined(member, typeof(XmlIgnoreAttribute), true))
                || (member?.Name.EndsWith("Specified", StringComparison.OrdinalIgnoreCase) == true))
            {
                property.Ignored = true;
            }
            else
            {
                property.ShouldSerialize =
                    instance =>
                    {
                        bool? isSpecified = instance.GetType().GetProperty(member?.Name + "Specified")?.GetValue(instance) as bool?;
                        return true; // isSpecified ?? true;
                    };

                if ((attribute = Attribute.GetCustomAttribute(member, typeof(XmlAttributeAttribute))) != null)
                {
                    property.PropertyName = (attribute as XmlAttributeAttribute)?.AttributeName;
                }
                else if ((attribute = Attribute.GetCustomAttribute(member, typeof(XmlElementAttribute))) != null)
                {
                    property.PropertyName = (attribute as XmlElementAttribute)?.ElementName;
                }
                else if ((attribute = Attribute.GetCustomAttribute(member, typeof(XmlArrayAttribute))) != null)
                {
                    property.PropertyName = (attribute as XmlArrayAttribute)?.ElementName;
                }
                else if ((attribute = Attribute.GetCustomAttribute(member, typeof(XmlTextAttribute))) != null)
                {
                    property.PropertyName = "#text";
                }

                if (((property.PropertyType.IsArray)
                    || (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>)))
                    && (!property.PropertyName.EndsWith("s")))
                {
                    property.PropertyName += "s";
                }
                //else if (property.PropertyType.IsEnum)
                //{
                //    property.Converter = new StringEnumConverter();
                //}
                //else if (property.PropertyType == typeof(DateTime))
                //{
                //    property.Converter = new JavaScriptDateTimeConverter();
                //}
            }
        }
    }
}
