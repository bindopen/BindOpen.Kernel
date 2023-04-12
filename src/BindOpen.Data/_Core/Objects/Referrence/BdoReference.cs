using BindOpen.Data.Meta;
using BindOpen.Script;

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

        public BdoReferenceKind Kind { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoExpression Expression { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public IBdoScriptword Word { get; set; }

        /// <summary>
        /// The script word of this instance.
        /// </summary>
        public string VariableName { get; set; }

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
            return reference.Kind switch
            {
                BdoReferenceKind.Expression => reference.Expression?.ToString(),
                BdoReferenceKind.Variable => reference.VariableName,
                BdoReferenceKind.MetaData => reference.MetaData?.ToString(),
                BdoReferenceKind.Word => reference.Word?.ToString(),
                _ => null
            };
        }

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
            if (Word != null) return Word.ToString();

            return base.ToString();
        }

        #endregion
    }
}