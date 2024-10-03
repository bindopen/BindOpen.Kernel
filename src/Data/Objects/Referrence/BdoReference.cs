using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public class BdoReference : BdoObject, IBdoReference
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        public BdoReferenceKind Kind { get; set; } = BdoReferenceKind.Expression;

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        public IBdoMetaData MetaData { get; set; }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of BdoReference class.
        /// </summary>
        public BdoReference()
        {
        }

        #endregion

        // -----------------------------------------------
        // Converters
        // -----------------------------------------------

        #region Converters

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator string(BdoReference reference)
        {
            return reference?.ToString();
        }

        /// <summary>
        /// Converts from string.
        /// </summary>
        /// <param key="st">The string to consider.</param>
        public static explicit operator BdoReference(BdoExpression exp)
        {
            return BdoData.NewRef(exp);
        }

        #endregion

        // ------------------------------------------
        // IReferenced Implementation
        // ------------------------------------------

        #region IReferenced

        /// <summary>
        /// 
        /// </summary>
        public virtual string Key() => Kind switch
        {
            BdoReferenceKind.Identifier => Identifier,
            BdoReferenceKind.MetaData => MetaData?.Name,
            _ => null
        };

        #endregion

        // ------------------------------------------
        // IIdentified Implementation
        // ------------------------------------------

        #region IIdentified Implementation

        /// <summary>
        /// 
        /// </summary>
        public string Identifier { get; set; }

        #endregion

        // ------------------------------------------
        // ACCCESSORS
        // ------------------------------------------

        #region Accessors

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Kind switch
            {
                BdoReferenceKind.Expression => Expression?.ToString(),
                BdoReferenceKind.Identifier => "{{id=" + Identifier + "}}",
                BdoReferenceKind.MetaData => MetaData?.ToString(),
                _ => null
            };
        }

        #endregion

        // --------------------------------------------------
        // CLONING
        // --------------------------------------------------

        #region Cloning

        /// <summary>
        /// Clones this instance.
        /// </summary>
        /// <returns>Returns a cloned instance.</returns>
        public override object Clone()
        {
            var obj = base.Clone().As<BdoReference>();

            obj.Expression = Expression?.Clone<BdoExpression>();
            obj.MetaData = MetaData?.Clone<BdoMetaData>();

            return obj;
        }

        #endregion

    }
}