using BindOpen.Data.Helpers;

namespace BindOpen.Data
{
    /// <summary>
    /// This class represents a data exp that can contain a literal and script texts.
    /// </summary>
    public class BdoExpression : BdoObject, IBdoExpression
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The value of this instance.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The kind of this instance.
        /// </summary>
        public BdoExpressionKind ExpressionKind { get; set; } = BdoExpressionKind.Auto;

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of DataExpression class.
        /// </summary>
        public BdoExpression()
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
        public static explicit operator string(BdoExpression exp)
        {
            return exp?.ToString();
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
            return Text;
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
            var obj = base.Clone().As<BdoExpression>();

            return obj;
        }

        #endregion
    }
}