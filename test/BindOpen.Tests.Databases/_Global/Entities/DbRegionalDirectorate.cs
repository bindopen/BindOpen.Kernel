using System.Collections.Generic;

namespace BindOpen.Tests.Databases.Entities
{
    /// <summary>
    /// This class represents a regional direction in database.
    /// </summary>
    public class DbRegionalDirectorate
    {
        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public int RegionalDirectorateId { get; set; }

        /// <summary>
        /// The code of this instance.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The first name of this instance.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The phone of this instance.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// The email of this instance.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The fax of this instance.
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// The FR label of this instance.
        /// </summary>
        public string LabelFR { get; set; }

        /// <summary>
        /// The EN label of this instance.
        /// </summary>
        public string LabelEN { get; set; }

        // Bound entities ------------------------

        /// <summary>
        /// 
        /// </summary>
        public List<DbEmployee> Employees { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbRegionalDirectorate class.
        /// </summary>
        public DbRegionalDirectorate()
        {
        }

        #endregion
    }
}