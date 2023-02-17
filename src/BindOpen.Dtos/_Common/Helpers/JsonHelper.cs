using BindOpen.Logging;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BindOpen.Dtos.Json
{
    public static class JsonHelper
    {
        /// <summary>
        /// Saves the JSON string of this instance.
        /// </summary>
        /// <param name="dto">The DTO to save.</param>
        /// <param name="log">The saving log to consider.</param>
        /// <returns>The Json string of this instance.</returns>
        public static string ToJson<T>(this T dto, IBdoLog log = null) where T : class
        {
            if (dto == null) return null;

            var st = string.Empty;

            var options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            options.Converters.Add(new JsonStringEnumConverter());

            try
            {
                st = JsonSerializer.Serialize(dto, options);
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
        /// Saves this instance to the specified file path.
        /// </summary>
        /// <param name="dto">The DTO to save.</param>
        /// <param name="filePath">Path of the file to save.</param>
        /// <param name="log">The log to consider.</param>
        /// <returns>True if the saving operation has been done. False otherwise.</returns>
        public static bool SaveJson<T>(this T dto, string filePath, IBdoLog log = null) where T : class
        {
            if (dto == null) return false;

            if (!string.IsNullOrEmpty(filePath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                var options = new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };
                options.Converters.Add(new JsonStringEnumConverter());

                try
                {
                    using var createStream = File.Create(filePath);
                    using var utf8JsonWriter = new Utf8JsonWriter(createStream);

                    JsonSerializer.Serialize(utf8JsonWriter, dto, options);
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
        /// Loads a data item from the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the Json file to load.</param>
        /// <param name="log">The output log of the method.</param>
        /// <param name="mustFileExist">Indicates whether the file must exist.</param>
        /// <returns>The loaded log.</returns>
        /// <remarks>If the XML schema set is null then the schema is not checked.</remarks>
        public static T LoadJson<T>(
            string filePath,
            IBdoLog log = null,
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
                var options = new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };
                options.Converters.Add(new JsonStringEnumConverter());

                try
                {
                    using FileStream openStream = File.OpenRead(filePath);
                    dto = JsonSerializer.Deserialize<T>(openStream, options);
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
        /// Loads the data item from the specified file path.
        /// </summary>
        /// <typeparam name="T">The data item class to consider.</typeparam>
        /// <param name="jsonString">The Json string to load.</param>
        /// <param name="log">The output log of the load method.</param>
        /// <returns>The loaded log.</returns>
        /// <remarks>If the XML schema set is null then the schema is not checked.</remarks>
        public static T LoadJsonFromString<T>(
            string jsonString,
            IBdoLog log = null) where T : class
        {
            T dto = default;

            if (jsonString != null)
            {
                var options = new JsonSerializerOptions()
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
                };
                options.Converters.Add(new JsonStringEnumConverter());

                try
                {
                    dto = JsonSerializer.Deserialize<T>(jsonString, options);
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
    }
}