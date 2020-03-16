using Newtonsoft.Json;


namespace BindOpen.Tests.Databases.Dtos
{
    /// <summary>
    /// This class represents a regional direction DTO.
    /// </summary>
    public class RegionalDirectorateDto
    {
        #region Properties

        /// <summary>
        /// The code of this instance.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// The address of this instance.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// The phone of this instance.
        /// </summary>
        [JsonProperty("phone")]
        public string Phone { get; set; }

        /// <summary>
        /// The email of this instance.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// The fax of this instance.
        /// </summary>
        [JsonProperty("fax")]
        public string Fax { get; set; }

        /// <summary>
        /// The label of this instance.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the RegionalDirectorateDto class.
        /// </summary>
        public RegionalDirectorateDto()
        {
        }

        #endregion
    }
}