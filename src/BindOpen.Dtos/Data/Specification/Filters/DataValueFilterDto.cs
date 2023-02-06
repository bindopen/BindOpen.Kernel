﻿using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Data.Specification
{
    /// <summary>
    /// This class specifies the data value filter.
    /// </summary>
    [XmlType("DataValueFilter", Namespace = "https://xsd.bindopen.org")]
    [XmlRoot(ElementName = "value.filter", Namespace = "https://xsd.bindopen.org", IsNullable = false)]
    public class DataValueFilterDto
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The added values of this instance.
        /// </summary>
        /// <remarks>If empty then all the values are added.</remarks>
        [JsonPropertyName("add")]
        [XmlElement("add")]
        public List<string> AddedValues { get; set; }

        /// <summary>
        /// The removed values of this instance.
        /// </summary>
        /// <remarks>If empty then no value is removed.</remarks>
        [JsonPropertyName("remove")]
        [XmlElement("remove")]
        public List<string> RemovedValues { get; set; }

        #endregion

        // --------------------------------------------------
        // CONSTRUCTORS
        // --------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DataValueFilterDto class.
        /// </summary>
        public DataValueFilterDto()
        {
        }

        #endregion
    }

}