using System;
using System.Reflection;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.System.Diagnostics;

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
            public string AssemblyName;

            /// <summary>
            /// Class name.
            /// </summary>
            public string ClassName;
        }

        #endregion

        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        // Assemblies --------------------------------

        /// <summary>
        /// Gets the specified assembly.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="assemblyName">The name of the assembly to consider.</param>
        /// <returns></returns>
        public static Assembly GetAsssembly(this AppDomain appDomain, string assemblyName)
        {
            if (appDomain == null) return null;

            Assembly assembly = null;
            int i = appDomain.GetAssemblies().Length;
            if ((appDomain != null) && (assemblyName != null))
            {
                assemblyName = assemblyName.Trim();
                assembly =Array.Find(appDomain.GetAssemblies(), p => p.GetName().Name.KeyEquals(assemblyName));
            }

            return assembly;
        }

        // Class references --------------------------------

        /// <summary>
        /// Dertermines the assembly reference from the specified complete class name.
        /// </summary>
        /// <param name="completeClassName">The complete class name to consider.</param>
        /// <returns></returns>
        public static ClassReference GetClassReference(string completeClassName)
        {
            if (completeClassName == null)
                return new ClassReference();

            string[] subStrings = completeClassName.Split(',');

            string className = completeClassName;
            if (subStrings.Length > 0)
                className = subStrings[0];

            string assemblyName ="";
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

        // Instances --------------------------------

        ///// <summary>
        ///// Creates the instance of the specified extension object instance type.
        ///// </summary>
        ///// <param name="appDomain">The application domain to consider.</param>
        ///// <param name="classReference">The class reference to consider.</param>
        ///// <param name="object1">The object to consider.</param>
        ///// <param name="attributes">The attributes to consider.</param>
        //public static ILog CreateInstance(
        //    this AppDomain appDomain,
        //    AssemblyHelper.ClassReference classReference,
        //    out Object object1,
        //    params object[] attributes)
        //{
        //    ILog log = new Log();
        //    object1 = null;

        //    if (appDomain != null)
        //    {
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(classReference.AssemblyName))
        //            {
        //                Assembly assembly = appDomain?.GetAsssembly(classReference.AssemblyName);
        //                if ((assembly != null) && (!string.IsNullOrEmpty(classReference.ClassName)))
        //                {
        //                    object1 = assembly.CreateInstance(
        //                       classReference.ClassName,
        //                       true, BindingFlags.CreateInstance, null, attributes, null, null);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            log.AddException(ex);
        //        }
        //    }

        //    return log;
        //}

        /// <summary>
        /// Creates the instance of the specified extension object instance type.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="type">The type to consider.</param>
        /// <param name="object1">The object to consider.</param>
        /// <param name="attributes">The attributes to consider.</param>
        public static ILog CreateInstance(
            this AppDomain appDomain,
            Type type,
            out Object object1,
            params object[] attributes)
        {
            ILog log = new Log();
            object1 = null;

            if (appDomain != null)
            {
                try
                {
                    object1 = Activator.CreateInstance(type);
                }
                catch (Exception ex)
                {
                    log.AddException(ex);
                }
            }

            return log;
        }

        ///// <summary>
        ///// Loads the specified instance of the specified class from the specified Xml string.
        ///// </summary>
        ///// <param name="appDomain">The application domain to consider.</param>
        ///// <param name="classReference">The class reference to consider.</param>
        ///// <param name="xmlstring">The XML string to consider.</param>
        ///// <param name="object1">The object to consider.</param>
        //public static ILog LoadDataItemInstance(
        //    this AppDomain appDomain,
        //    AssemblyHelper.ClassReference classReference,
        //    string xmlstring,
        //    out Object object1)
        //{
        //    ILog log = new Log();
        //    object1 = null;

        //    if (appDomain != null)
        //    {
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(classReference.AssemblyName))
        //            {
        //                Assembly assembly = appDomain.GetAsssembly(classReference.AssemblyName);
        //                if (assembly != null)
        //                {
        //                    Type type = assembly.GetType(classReference.ClassName);
        //                    if (type != null)
        //                    {
        //                        MethodInfo methodInfo = typeof(XmlHelper).GetMethod("LoadFromstring", BindingFlags.Public | BindingFlags.Static);
        //                        if (methodInfo != null)
        //                        {
        //                            MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(
        //                                new Type[] { type });

        //                            object[] objects = new Object[3] { xmlstring, log, null };
        //                            object1 = genericMethodInfo.Invoke(null, objects);
        //                        }
        //                        else
        //                            log.AddError(
        //                                title: "'LoadFromXmlstring' function not found in the specified class",
        //                                description: "Could not find the static function called 'LoadFromXmlstring' in the specified type ('" + classReference.ClassName + "').");
        //                    }
        //                    else
        //                    {
        //                        log.AddError(
        //                           title: "Specified type not found",
        //                           description: "Could not retrieve the specified type ('" + classReference.ClassName + "') in the specified assembly ('" + assembly.FullName + "').");
        //                    }
        //                }
        //                else
        //                {
        //                    log.AddError("Could not retrieve the specified assembly ('" + assembly.FullName + "')");
        //                }
        //            }
        //            else
        //            {
        //                log.AddError("Assembly name '" + classReference.AssemblyName + "' missing");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            log.AddException(ex);
        //        }
        //    }

        //    return log;
        //}

        /// <summary>
        /// Gets the specified type from the specified assembly reference.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        /// <param name="classReference">The class reference to consider.</param>
        private static Type GetTypeFromAssemblyReference(
            this AppDomain appDomain,
            AssemblyHelper.ClassReference classReference)
        {
            if (appDomain == null) return null;

            Type type = null;
            try
            {
                if (!string.IsNullOrEmpty(classReference.AssemblyName))
                {
                    Assembly assembly = appDomain.GetAsssembly(classReference.AssemblyName);
                    if ((assembly != null) && (!string.IsNullOrEmpty(classReference.ClassName)))
                        type = assembly.GetType(classReference.ClassName);
                }
            }
            catch
            {
            }

            return type;
        }

        /// <summary>
        /// Gets the class name of the specified complete name.
        /// </summary>
        /// <param name="completeName">The complete name to consider.</param>
        /// <returns>Returns the cloned metrics definition.</returns>
        public static string GetClassName(string completeName)
        {
            string className = completeName;

            if (completeName != null)
            {
                if (completeName.Contains("."))
                {
                    className = className.Substring(
                       completeName.IndexOf('.') + 1);
                }

                if (completeName.Contains(","))
                    className = className.Substring(0, className.IndexOf(','));
            }

            return className ?? "";
        }

        #endregion
    }
}
