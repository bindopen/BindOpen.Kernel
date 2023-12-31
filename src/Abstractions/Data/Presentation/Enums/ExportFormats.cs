using System;

namespace BindOpen.Data
{
    /// <summary>
    /// This enumeration lists the possible exporter formats.
    /// </summary>
    [Flags]
    public enum ExportFormats
    {
        /// <summary>
        /// Undefined.
        /// </summary>
        Undefined = 0,

        /// <summary>
        /// Json.
        /// </summary>
        Json = 0x01 << 0,

        /// <summary>
        /// Tsv.
        /// </summary>
        Tsv = 0x01 << 1,

        /// <summary>
        /// Pdf.
        /// </summary>
        Pdf = 0x01 << 2,

        /// <summary>
        /// Excel.
        /// </summary>
        Excel = 0x01 << 3,

        /// <summary>
        /// Any.
        /// </summary>
        Any = Json | Tsv | Pdf | Excel
    }
}