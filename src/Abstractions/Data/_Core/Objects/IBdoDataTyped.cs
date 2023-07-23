using System;

namespace BindOpen.System.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoDataTyped
    {
        /// <summary>
        /// 
        /// </summary>
        BdoDataType DataType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        DataValueTypes DataValueType { get; }

        /// <summary>
        /// 
        /// </summary>
        Type DataClassType { get; }
    }
}