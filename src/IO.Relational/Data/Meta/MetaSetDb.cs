﻿using System.Collections.Generic;
<<<<<<<< HEAD:src/IO.Relational/Data/Meta/MetaSetDb.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
========
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
>>>>>>>> 40f26f9d0a8cdbc5927cf14208a3b5386203e2aa:src/IO.Dtos/Data/Meta/Definition/SpecSetDto.cs

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a meta set database entity.
    /// </summary>
    public class MetaSetDb : IBdoDb
    {
        // -------------------------------------------------------
        // PROPERTIES
        // -------------------------------------------------------

        #region Properties

        /// <summary>
        /// The identifier of this instance.
        /// </summary>
        [Key]
        [Column("MetaSetId")]
        public string Identifier { get; set; }

        /// <summary>
        /// Name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The elements of this instance.
        /// </summary>
<<<<<<<< HEAD:src/IO.Relational/Data/Meta/MetaSetDb.cs
        public List<MetaDataDb> Items { get; set; }
========
        [NotMapped]
        [JsonPropertyName("items")]
        [XmlElement("spec", Type = typeof(SpecDto))]
        public List<SpecDto> Items { get; set; }
>>>>>>>> 40f26f9d0a8cdbc5927cf14208a3b5386203e2aa:src/IO.Dtos/Data/Meta/Definition/SpecSetDto.cs

        #endregion

        // -------------------------------------------------------------
        // CONSTRUCTORS
        // -------------------------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the MetaSetDb class.
        /// </summary>
        public MetaSetDb() : base()
        {
        }

        #endregion
    }
}
