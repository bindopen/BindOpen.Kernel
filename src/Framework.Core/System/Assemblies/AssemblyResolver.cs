using System;
using System.Reflection;
using BindOpen.Framework.Core.Data.Items;

namespace BindOpen.Framework.Core.System.Assemblies
{

    /// <summary>
    /// This class represents an assembly resolver.
    /// </summary>
    [Serializable()]
    public class AssemblyResolver : DataItem
    {

        /// <summary>
        /// Resolves the domain i.e. initializes the assembly resolving routines.
        /// </summary>
        /// <param name="appDomain">The domain to consider.</param>
        static public ResolveEventHandler Resolve(
            AppDomain appDomain)
        {
            //if (appDomain != null)
            //{
            //    AssemblyResolver assemblyResolver =
            //        appDomain.CreateInstanceFromAndUnwrap(
            //          Assembly.GetExecutingAssembly().Location,
            //          typeof(AssemblyResolver).FullName) as AssemblyResolver;

            //    if (appDomain == AppDomain.CurrentDomain)
            //        return assemblyResolver.GetResolvedDomain(appDomain, appDomainSetup);
            //    else
            //        assemblyResolver.ResolveDomain(appDomain, appDomainSetup);
            //}

            return null;
        }

        private ResolveEventHandler GetResolvedDomain(
            AppDomain appDomain)
        {
            if (appDomain != null)
            {
                ResolveEventHandler aResolveEventHandler = this.BuildResolveEventHandler();
                appDomain.AssemblyResolve += aResolveEventHandler;

                return aResolveEventHandler;
            }

            return null;
        }

        private void ResolveDomain(
            AppDomain appDomain)
        {
            if (appDomain != null)
            {
                ResolveEventHandler aResolveEventHandler = this.BuildResolveEventHandler();
                appDomain.AssemblyResolve += aResolveEventHandler;
            }
        }

        private ResolveEventHandler BuildResolveEventHandler()
        {
            return new ResolveEventHandler(
                (object sender, ResolveEventArgs args) =>
                {
                    Assembly assembly = null;
                    //string filePath = "";

                    String assemblyName = (new AssemblyName(args.Name)).Name;
                    if ((assemblyName.EndsWith(".resources")) | (assemblyName.EndsWith(".XmlSerializers")))
                        return null;
                    String assemblyFullName = (new AssemblyName(args.Name)).FullName;

                    try
                    {
                        //filePath = appDomainSetup.PrivateBinPath + assemblyName + ".dll";
                        //if (!string.IsNullOrEmpty(appDomainSetup.PrivateBinPath) && (File.Exists(filePath)))
                        //    assembly = AppDomainPool.LoadAssemblyFromFile(AppDomain.CurrentDomain, filePath);
                        //else
                        //{
                        //    filePath = appDomainSetup.ApplicationBase + assemblyName + ".dll";
                        //    if (!string.IsNullOrEmpty(appDomainSetup.ApplicationBase) && (File.Exists(filePath)))
                        //        assembly = AppDomainPool.LoadAssemblyFromFile(AppDomain.CurrentDomain, filePath);
                        //}
                    }
                    catch (Exception ex)
                    {
                        String st = ex.ToString();
                    }

                    return assembly;
                });
        }


    }
}
