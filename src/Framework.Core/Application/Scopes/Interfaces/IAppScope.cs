using System;
using BindOpen.Framework.Core.Application.Depots.Datasources;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Context;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Extensions;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;

namespace BindOpen.Framework.Core.Application.Scopes
{
    /// <summary>
    /// This interface defines an application scope.
    /// </summary>
    public interface IAppScope
    {
        /// <summary>
        /// The application extension.
        /// </summary>
        IAppExtension Extension { get; set; }

        /// <summary>
        /// The data context.
        /// </summary>
        IDataContext Context { get; set; }

        /// <summary>
        /// The data source service.
        /// </summary>
        IDataSourceDepot DataSourceDepot { get; set; }

        /// <summary>
        /// The script interpreter.
        /// </summary>
        IScriptInterpreter Interpreter { get; set; }

        /// <summary>
        /// Sets the application domain of this instance.
        /// </summary>
        /// <param name="appDomain">The application domain to consider.</param>
        void Initialize(AppDomain appDomain=null);

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="specificationAreas"></param>
        /// <param name="updateModes"></param>
        /// <returns></returns>
        ILog Update<T>(T item = default, string[] specificationAreas = null, UpdateModes[] updateModes = null) where T : IDataItem;
    }
}