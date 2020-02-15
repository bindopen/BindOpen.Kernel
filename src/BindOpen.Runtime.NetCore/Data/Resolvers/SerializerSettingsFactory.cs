using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;

namespace BindOpen.Runtime.AspNetCore.Data.Resolvers
{
    /// <summary>
    /// This class represents a startup helpers
    /// </summary>
    public static class SerializerSettingsFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static JsonSerializerSettings InitializeSettings(JsonSerializerSettings settings)
        {
            if (settings == null) return null;

            settings.ContractResolver = new XmlContractResolver();
            settings.Converters = new List<JsonConverter>
            {
                    new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() },
                    new JavaScriptDateTimeConverter()
            };
            settings.NullValueHandling = NullValueHandling.Ignore;

            return settings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings CreateSettings() => InitializeSettings(new JsonSerializerSettings());
    }
}
