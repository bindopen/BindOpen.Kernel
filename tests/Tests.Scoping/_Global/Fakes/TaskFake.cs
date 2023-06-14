using BindOpen.System.Data;
using BindOpen.System.Data.Meta;
using BindOpen.System.Scoping.Tasks;
using BindOpen.System.Scoping.Tasks;

namespace BindOpen.System.Tests.Scoping
{
    /// <summary>
    /// This class represents a database data field.
    /// </summary>
    [BdoTask(
        Name = "taskFake",
        Description = "Test task.",
        CreationDate = "2023-02-24"
    )]
    public class TaskFake : BdoTask
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The boolean value of this instance.
        /// </summary>
        [BdoProperty(Name = "boolValue")]
        public bool BoolValue { get; set; }

        /// <summary>
        /// The string value of this instance.
        /// </summary>
        [BdoOutput(Name = "stringValue")]
        public string StringValue { get; set; }

        /// <summary>
        /// The integer value of this instance.
        /// </summary>
        [BdoProperty(Name = "intValue")]
        public int IntValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoInput(Name = "enumValue")]
        public ActionPriorities EnumValue { get; set; }

        /// <summary>
        /// Enumeration value of this instance.
        /// </summary>
        [BdoOutput(Name = "output")]
        public BdoMetaSet Outputs { get; set; }

        /// <summary>
        /// The sub task of this instance.
        /// </summary>
        [BdoProperty(Name = "subTask")]
        public TaskFake SubTask { get; set; }

        /// <summary>
        /// The information flag of this instance.
        /// </summary>
        [BdoOutput("flag")]
        public string Flag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param key="flag"></param>
        /// <returns></returns>
        public IBdoTask WithFlag(string flag)
        {
            Flag = flag;

            return this;
        }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the TaskFake class.
        /// </summary>
        public TaskFake() : base()
        {
        }

        #endregion
    }
}
