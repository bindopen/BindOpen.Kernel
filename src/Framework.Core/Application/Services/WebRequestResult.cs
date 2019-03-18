using System;
using System.Runtime.Serialization;

namespace BindOpen.Framework.Core.Application.Services
{
    /// <summary>
    /// This class represents the result of a Web request.
    /// </summary>
    [DataContract]
    public class WebRequestResult
    {

        // --------------------------------------------------
        // VARIABLES
        // --------------------------------------------------

        #region Variables
        
        private String _Value = "";
        private String _Log = "";

        #endregion


        // --------------------------------------------------
        // PROPERTIES
        // --------------------------------------------------

        #region Properties

        /// <summary>
        /// Value of this instance.
        /// </summary>
        [DataMember]
        public String Value
        {
            get { return this._Value; }
            set { this._Value = value; }
        }

        /// <summary>
        /// Log of this instance.
        /// </summary>
        [DataMember]
        public String Log
        {
            get { return this._Log; }
            set { this._Log = value; }
        }

        #endregion

    }

}
