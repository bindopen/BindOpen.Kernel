using Newtonsoft.Json;


namespace BindOpen.Tests.Databases.Dtos
{
    /// <summary>
    /// This class represents a  employee DTO.
    /// </summary>
    public class EmployeeDto
    {
        #region Properties

        /// <summary>
        /// The code of this instance.
        /// </summary>
        [JsonProperty("code")]
        public string Code { get; set; }

        /// <summary>
        /// The first name of this instance.
        /// </summary>
        [JsonProperty("fisrtName")]
        public string FisrtName { get; set; }

        /// <summary>
        /// The last name of this instance.
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// The staff number of this instance.
        /// </summary>
        [JsonProperty("staffNumber")]
        public string StaffNumber { get; set; }

        /// <summary>
        /// The contact email of this instance.
        /// </summary>
        [JsonProperty("contactEmail")]
        public string ContactEmail { get; set; }

        /// <summary>
        /// The regional direction code of this instance.
        /// </summary>
        [JsonProperty("regionalDirectorateCode")]
        public string RegionalDirectorateCode { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EmployeeDto class.
        /// </summary>
        public EmployeeDto()
        {
        }

        #endregion
    }
}