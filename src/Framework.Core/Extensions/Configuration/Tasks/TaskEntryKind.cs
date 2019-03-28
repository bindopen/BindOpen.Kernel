using System;
using System.Xml.Serialization;

namespace BindOpen.Framework.Core.Extensions.Configuration.Tasks
{

    /// <summary>
    /// This enumeration represents the possible task entry kinds.
    /// </summary>
    [Serializable()]
    [XmlType("TaskEntryKind", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    public enum TaskEntryKind
    {
        /// <summary>
        /// Any.
        /// </summary>
        Any,

        /// <summary>
        /// Input.
        /// </summary>
        Input,

        /// <summary>
        /// Output.
        /// </summary>
        Output,

        /// <summary>
        /// Scalar output.
        /// </summary>
        ScalarOutput,

        /// <summary>
        /// Non-scalar output.
        /// </summary>
        NonScalarOutput

    };

    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the TaskEntryKind enumeration.
    /// </summary>
    public static class TaskEntryKindExtension
    {

        /// <summary>
        /// Returns the title of the specified entry kind.
        /// </summary>
        /// <param name="taskEntryKind">The entry kind to consider.</param>
        /// <returns>The result object.</returns>
        public static String GetTitle(this TaskEntryKind taskEntryKind)
        {
            switch (taskEntryKind)
            {
                case TaskEntryKind.Input:
                    return "input";
                case TaskEntryKind.Output:
                    return "output";
                case TaskEntryKind.ScalarOutput:
                    return "scalar output";
                case TaskEntryKind.NonScalarOutput:
                    return "non-scalar output";
            }

            return "";
        }

   }

    #endregion
}