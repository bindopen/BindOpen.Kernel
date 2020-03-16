using BindOpen.Extensions.Carriers;

namespace BindOpen.Tests.Databases.Entities
{
    /// <summary>
    /// This class represents an employee in database.
    /// </summary>
    [BdoDbTable(nameof(DbEmployee), "Crm")]
    public class DbEmployee
    {
        #region Properties

        /// <summary>
        /// The ID of this instance.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// The OD of the regional direction of this instance.
        /// </summary>
        public int RegionalDirectorateId { get; set; }


        /// <summary>
        /// The code of this instance.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The first name of this instance.
        /// </summary>
        public string FisrtName { get; set; }

        /// <summary>
        /// The last name of this instance.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The staff number of this instance.
        /// </summary>
        public string StaffNumber { get; set; }

        /// <summary>
        /// The contact email of this instance.
        /// </summary>
        public string ContactEmail { get; set; }

        // Bound entities ------------------------

        /// <summary>
        /// 
        /// </summary>
        public DbRegionalDirectorate RegionalDirectorate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the EmployeeDb class.
        /// </summary>
        public DbEmployee()
        {
        }

        #endregion
    }
}