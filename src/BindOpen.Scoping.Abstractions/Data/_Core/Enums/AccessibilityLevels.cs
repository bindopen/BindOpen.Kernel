﻿using System;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Data
{
    /// <summary>
    /// This enumerates the possible levels of accesibility.
    /// </summary>
    [Flags]
    [XmlType("AccessibilityLevels", Namespace = "https://storage.bindopen.org/xsd/bindopen")]
    public enum AccessibilityLevels
    {
        /// <summary>
        /// Anyone can access.
        /// </summary>
        Public = 0x0,

        /// <summary>
        /// None.
        /// </summary>
        None = 0x1 << 0,

        /// <summary>
        /// Inherited.
        /// </summary>
        Inherited = 0x1 << 1,

        /// <summary>
        /// Only authentized people can access.
        /// </summary>
        Private = 0x1 << 2,

        /// <summary>
        /// Only allowed people can access.
        /// </summary>
        Restricted = 0x1 << 3,

        /// <summary>
        /// Only people who bought for it can access.
        /// </summary>
        Payable = 0x1 << 4,

        /// <summary>
        /// Any accessibility level.
        /// </summary>
        Any = Inherited | Public | Private | Restricted | Payable
    }
}