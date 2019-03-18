using System;

namespace BindOpen.Framework.Core.System.Assemblies
{

    /// <summary>
    /// This structure represents a string manager.
    /// </summary>
    public static class AssemblyHelper
    {

        // --------------------------------------------------
        // ENUMERATIONS
        // --------------------------------------------------

        #region Enumerations

        /// <summary>
        /// This structure represents an class reference.
        /// </summary>
        [Serializable()]
        public struct ClassReference
        {
            /// <summary>
            /// Library name.
            /// </summary>
            public String AssemblyName;

            /// <summary>
            /// Class name.
            /// </summary>
            public String ClassName;
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Dertermines the assembly reference from the specified complete class name.
        /// </summary>
        /// <param name="completeClassName">The complete class name to consider.</param>
        /// <returns></returns>
        public static ClassReference GetClassReference(String completeClassName)
        {
            if (completeClassName == null)
                return new ClassReference();

            String[] subStrings = completeClassName.Split(',');


            String className = completeClassName;
            if (subStrings.Length > 0)
                className = subStrings[0];

            String assemblyName ="";
            if (subStrings.Length > 1)
                assemblyName = subStrings[1];
            else if (completeClassName.Contains("."))
                assemblyName = className.Substring(0, completeClassName.IndexOf(".") - 1);
            else
                assemblyName = "ici";

            ClassReference assemblyReference = new ClassReference()
            {
                ClassName = className.Trim(),
                AssemblyName = assemblyName.Trim()
            };

            return assemblyReference;
        }

        #endregion

    }

}
