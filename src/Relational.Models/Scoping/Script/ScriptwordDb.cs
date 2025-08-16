using BindOpen.Data;
using BindOpen.Data.Meta;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace BindOpen.Scoping.Script
{
    /// <summary>
    /// This class represents a script word database entity.
    /// </summary>
    public class ScriptwordDb : MetaDataDb
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        /// <example>Script word, syntax, text...</example>
        public ScriptTokenKinds TokenKind { get; set; } = ScriptTokenKinds.None;

        // Expression ----------------------------------

        /// <summary>
        /// The script word child of this instance.
        /// </summary>
        [NotMapped]
        public ExpressionDb Expression { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [Column("ExpressionItemId")]
        public string ExpressionIdentifier { get; set; }

        // Tree ----------------------------------

        /// <summary>
        /// The script word child of this instance.
        /// </summary>
        [ForeignKey(nameof(ChildWordId))]
        public ScriptwordDb Child { get; set; }

        /// <summary>
        /// The class name of this instance.
        /// </summary>
        [JsonIgnore]
        [XmlIgnore]
        public string ChildWordId { get; set; }

        // Expression

        /// <summary>
        /// The text of this instance.
        /// </summary>
        public string Text { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the ScriptwordDb class.
        /// </summary>
        public ScriptwordDb()
        {
        }

        #endregion

        public MetaDataDb UpdateTree()
        {
            if (Child != null)
            {
                Child.Parent = this;
                Child.UpdateTree();
            }

            return this;
        }

    }
}
