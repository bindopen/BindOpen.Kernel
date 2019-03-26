using System;
using System.Collections.Generic;
using System.ComponentModel;
using BindOpen.Framework.Core.Application.Options;
using BindOpen.Framework.Core.Application.Scopes;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Helpers.Objects;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.Data.Items.Sets;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Scripting;
using BindOpen.Framework.Runtime.System;

namespace BindOpen.Framework.Runtime.Application.Modules
{

    /// <summary>
    /// This class represents an application module instance accessible by a visitor.
    /// </summary>
    public class ApplicationModuleInstance : DescribedDataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private AppModule _Module = null;

        private String _Uri = null;
        private String _AbsoluteUri = null;
        private String _ApplicationExecutionPath = null;

        private Boolean _IsLocal = false;
        private AccessibilityLevel _AccessibilityLevel = AccessibilityLevel.Public;
        private InstanceIndexation _Indexation = InstanceIndexation.None;
        private ApplicationModuleKind _Kind = ApplicationModuleKind.None;
        private ApplicationModuleSubKind _SubKind = ApplicationModuleSubKind.None;

        private OptionSet _OptionSet = null;

        //private DataItemSet<ApplicationModuleInstance> _SubModules = new DataItemSet<ApplicationModuleInstance>();
        private DataItemSet<AppSection> _Sections = null;

        #endregion

        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The module of this instance.
        /// </summary>
        public AppModule Module
        {

            get { return this._Module; }
            set { this._Module = value; }
        }

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public String ModuleName
        {
            get { return this._Module?.Name; }
        }

        // Location ---------------------------------

        /// <summary>
        /// The URI of this instance.
        /// </summary>
        public String Uri
        {
            get { return this._Uri; }
        }

        /// <summary>
        /// The URI of this instance.
        /// </summary>
        public String AbsoluteUri
        {
            get { return this._AbsoluteUri; }
        }

        /// <summary>
        /// The application execution path of this instance.
        /// </summary>
        public String ApplicationExecutionPath
        {
            get { return this._ApplicationExecutionPath; }
        }
        
        /// <summary>
        /// Indicates whether this instance is local.
        /// </summary>
        public Boolean IsLocal
        {
            get { return this._IsLocal; }
        }

        /// <summary>
        /// The accessibility level of this instance.
        /// </summary>
        public AccessibilityLevel AccessibilityLevel
        {
            get { return this._AccessibilityLevel; }
            set { this._AccessibilityLevel = value; }
        }

        /// <summary>
        /// Indexation of this instance.
        /// </summary>
        [Bindable(false)]
        [DefaultValue("")]
        public InstanceIndexation Indexation
        {
            get { return this._Indexation; }
            set { this._Indexation = value; }
        }

        /// <summary>
        /// Kind of this instance.
        /// </summary>
        public ApplicationModuleKind Kind
        {
            get { return this._Kind; }
            set { this._Kind = value; }
        }

        /// <summary>
        /// Sub kind of this instance.
        /// </summary>
        public ApplicationModuleSubKind SubKind
        {
            get { return this._SubKind; }
            set { this._SubKind = value; }
        }

        // Tree ------------------------------------

        ///// <summary>
        ///// The application modules of this instance.
        ///// </summary>
        //[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        //public DataItemSet<ApplicationModuleInstance> SubModules
        //{
        //    get { return this._SubModules; }
        //    set { this._SubModules = value; }
        //}

        /// <summary>
        /// The sections of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DataItemSet<AppSection> Sections
        {
            get { return this._Sections; }
            set { this._Sections = value; }
        }

        // Options ----------------------------------

        /// <summary>
        /// The otpions of this instance.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public OptionSet OptionSet
        {
            get { return this._OptionSet; }
        }

        #endregion

        // ----------------------------
        // CONSTRUCTORS
        // ----------------------------

        #region Constructors

        /// <summary>
        /// Initializes a new instance of ApplicationModule class.
        /// </summary>
        /// <param name="aParentApplicationModule">The parent of the visitor application module.</param>
        /// <param name="moduleName">Name of the instance.</param>
        /// <param name="url">URL of the instance.</param>
        public ApplicationModuleInstance(
            AppModule module,
            String uri)
        {
            this._Module= module;
            this._Uri = uri;
        }

        ///// <summary>
        ///// Initializes a new instance of ApplicationModule class.
        ///// </summary>
        ///// <param name="aParentApplicationModule">The parent of the visitor application module.</param>
        ///// <param name="moduleInstance">The application module instance row representing the instance.</param>
        ///// <param name="visitor">The visitor of the instance.</param>
        ///// <param name="aVisible">Indicates whether this instance is visible.</param>
        ///// <param name="aApplicationInstanceName">Name of the application instance.</param>
        //public ApplicationModuleInstance(
        //    ApplicationModuleInstance aParentApplicationModule,
        //    ApplicationModuleInstance moduleInstance,
        //    SphereVisitor visitor,
        //    Boolean aVisible,
        //    String aApplicationInstanceName)
        //{
        //    this._Parent = aParentApplicationModule;
        //    this._Visitor = visitor;
        //    this._IsAvailable = aVisible;

        //    // we determine the location
        //    if ((moduleInstance != null)&&(moduleInstance.APPLICATIONMODULERow!=null))
        //    {
        //        this._ApplicationModuleRow = moduleInstance.APPLICATIONMODULERow;
        //        this._ModuleName = (this._ApplicationModuleRow != null ? this._ApplicationModuleRow.NAME : "");
        //        this._ApplicationExecutionPath = (this._ApplicationModuleRow != null ? this._ApplicationModuleRow.EXECUTIONPATH : "");
        //        this._Kind = this.GetKind();
        //        this._SubKind = this.GetSubKind();
        //        this._Description = (this._ApplicationModuleRow.IsDESCRIPTIONNull() ? "" : this._ApplicationModuleRow.DESCRIPTION);
        //        this._Title = new DictionaryDataItem(this._ApplicationModuleRow.GLOBALLABELRow);
        //        this.UpdateOptions(
        //            (this._ApplicationModuleRow.IsOPTIONVALUESNull() ? "" : this._ApplicationModuleRow.OPTIONVALUES));
        //        this.UpdateOptions(
        //            (moduleInstance.IsOPTIONSTRINGNull() ? "" : moduleInstance.OPTIONSTRING));

        //        Boolean aIsCurrentApplicationModuleLocal = false;
        //        Boolean aIsCurrentApplicationModuleBrotherLocal = false;

        //        // we determine if an instance of the application module or one of its child is present in the current application instance
        //        aIsCurrentApplicationModuleLocal =
        //            (this._ApplicationModuleRow.EXECUTIONPATH!= String.Empty)&&
        //            (Directory.Exists(visitor.AppService.GetPath(AppService.ApplicationPathKind.AppFolder) + this._ApplicationModuleRow.EXECUTIONPATH.ToLower()) &
        //            moduleInstance.APPLICATIONINSTANCERow.NAME.ToUpper() == visitor.AppService.Configuration.ApplicationInstanceName.ToUpper());
        //        if (!aIsCurrentApplicationModuleLocal)
        //            foreach (ApplicationModule aCurrentAPPLICATIONMODULERow in this._ApplicationModuleRow.GetAPPLICATIONMODULERows())
        //                if (aIsCurrentApplicationModuleLocal =
        //                    (aCurrentAPPLICATIONMODULERow.EXECUTIONPATH != String.Empty) &&
        //                    (Directory.Exists(visitor.AppService.GetPath(AppService.ApplicationPathKind.AppFolder) + aCurrentAPPLICATIONMODULERow.EXECUTIONPATH.ToLower())) &
        //                    moduleInstance.APPLICATIONINSTANCERow.NAME.ToUpper() == visitor.AppService.Configuration.ApplicationInstanceName.ToUpper()
        //                    )
        //                    break;

        //        // we determine if an instance of its brother is present in the current application instance
        //        if ((!aIsCurrentApplicationModuleLocal) & (this._ApplicationModuleRow.APPLICATIONMODULERowParent != null))
        //            foreach (ApplicationModule aCurrentAPPLICATIONMODULERow in this._ApplicationModuleRow.APPLICATIONMODULERowParent.GetAPPLICATIONMODULERows())
        //                if (aIsCurrentApplicationModuleBrotherLocal =
        //                    (aCurrentAPPLICATIONMODULERow.EXECUTIONPATH != String.Empty )&&
        //                    Directory.Exists(visitor.AppService.GetPath(AppService.ApplicationPathKind.AppFolder) + aCurrentAPPLICATIONMODULERow.EXECUTIONPATH.ToLower()))
        //                    break;

        //        // the application module location is local if it/one of its children/one its brother is local
        //        if ((aIsCurrentApplicationModuleLocal) | (aIsCurrentApplicationModuleBrotherLocal))
        //            this._IsLocal = true;
        //        else
        //            this._IsLocal = false;

        //        // we determine the visibility and the indexation
        //        this._Visibility = ApplicationModuleInstance.GetAccessibilityLevel(moduleInstance.VISIBILITY, this._ApplicationModuleRow.DEFAULTVISIBILITY);
        //        this._Indexation = ApplicationModuleInstance.GetIndexation(moduleInstance.INDEXATION, this._ApplicationModuleRow.DEFAULTINDEXATION);

        //        // if the location of this instance is in another application instance then
        //        if (!this._IsLocal)
        //        {
        //            // we determine the URL of this instance.

        //            if (moduleInstance.IsFULLURLNull())
        //            {
        //                this._Url = "";
        //                this._AbsoluteUrl = "";
        //            }
        //            else
        //            {
        //                this._Url = moduleInstance.FULLURL;
        //                this._AbsoluteUrl = moduleInstance.FULLURL;
        //            }

        //            // we add sub modules and universes
        //            foreach (ApplicationModule aChildAPPLICATIONMODULERow in
        //                this._ApplicationModuleRow.GetAPPLICATIONMODULERows())
        //            {
        //                // we search the child sub application module instance that has the same indexation
        //                ApplicationModuleInstance aChildAPPLICATIONMODULEINSTANCERow =
        //                    (from ApplicationModuleInstance aCurrentAPPLICATIONMODULEINSTANCERow
        //                     in this._Visitor.AppService.PlatformDataSet.APPLICATIONMODULEINSTANCE
        //                     where
        //                        (aCurrentAPPLICATIONMODULEINSTANCERow.APPLICATIONMODULE_ID == aChildAPPLICATIONMODULERow.ID) &
        //                        (
        //                            (
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.INDEXATION.ToUpper() == this._Indexation.ToString().ToUpper()) &
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.INDEXATION.ToUpper() != "DEFAULT")
        //                            ) |
        //                            (
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.INDEXATION.ToUpper() == "DEFAULT") &
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.APPLICATIONMODULERow.DEFAULTINDEXATION.ToUpper() == this._Indexation.ToString().ToUpper())
        //                            )
        //                        )
        //                     select aCurrentAPPLICATIONMODULEINSTANCERow).FirstOrDefault();

        //                if (aChildAPPLICATIONMODULEINSTANCERow != null)
        //                {
        //                    ApplicationModuleInstance visitorApplicationSubModule = new ApplicationModuleInstance(
        //                        this,
        //                        aChildAPPLICATIONMODULEINSTANCERow,
        //                        visitor,
        //                        true,
        //                        aApplicationInstanceName);

        //                    if (aChildAPPLICATIONMODULERow.SUBKIND.ToUpper().Equals("SUBMODULE"))
        //                        // we add the sub application module
        //                        this._SubModules.Add(
        //                            new ApplicationModuleInstance(
        //                                this,
        //                                visitorApplicationSubModule.ModuleName,
        //                                visitorApplicationSubModule.Url,
        //                                aChildAPPLICATIONMODULERow));
        //                    else if (aChildAPPLICATIONMODULERow.SUBKIND.ToUpper().Equals("UNIVERSE"))
        //                        // we add the application universe
        //                        this._Universes.Add(
        //                            new ApplicationModuleInstance(
        //                                this,
        //                                visitorApplicationSubModule.ModuleName,
        //                                visitorApplicationSubModule.Url,
        //                                aChildAPPLICATIONMODULERow));
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // we define the sections
        //            String aRemoteUrl = null;
        //            if (aIsCurrentApplicationModuleBrotherLocal)
        //                if (moduleInstance.IsFULLURLNull())
        //                    aRemoteUrl = "";
        //                else
        //                    aRemoteUrl = moduleInstance.FULLURL;

        //            // we consider first the own sections of the application module
        //            foreach (PlatformDataSet.APPLICATIONMODULESECTIONRow aAPPLICATIONMODULESECTIONRow in
        //                this._ApplicationModuleRow.GetAPPLICATIONMODULESECTIONRows())
        //                if (aAPPLICATIONMODULESECTIONRow.IsPARENT_IDNull())
        //                {
        //                    SphereApplicationModuleSection moduleSection = new SphereApplicationModuleSection(
        //                        this,
        //                        aAPPLICATIONMODULESECTIONRow,
        //                        null,
        //                        visitor,
        //                        aRemoteUrl);
        //                    if (moduleSection.IsVisible)
        //                        this.Sections.Add(moduleSection);
        //                }

        //            // we consider first the sections of the child application modules by rank order
        //            foreach (ApplicationModule aChildAPPLICATIONMODULERow in this._ApplicationModuleRow.GetAPPLICATIONMODULERows())
        //            {
        //                // we search the child sub application module instance that has the same indexation
        //                ApplicationModuleInstance aChildAPPLICATIONMODULEINSTANCERow =
        //                    (from ApplicationModuleInstance aCurrentAPPLICATIONMODULEINSTANCERow
        //                     in this._Visitor.AppService.PlatformDataSet.APPLICATIONMODULEINSTANCE
        //                     where
        //                        (aCurrentAPPLICATIONMODULEINSTANCERow.APPLICATIONMODULE_ID == aChildAPPLICATIONMODULERow.ID) &
        //                        (
        //                            (
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.INDEXATION.ToUpper() == this._Indexation.ToString().ToUpper()) &
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.INDEXATION.ToUpper() != "DEFAULT")
        //                            )|
        //                            (
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.INDEXATION.ToUpper() == "DEFAULT") &
        //                                (aCurrentAPPLICATIONMODULEINSTANCERow.APPLICATIONMODULERow.DEFAULTINDEXATION.ToUpper() == this._Indexation.ToString().ToUpper())
        //                            )
        //                        )
        //                     select aCurrentAPPLICATIONMODULEINSTANCERow).FirstOrDefault();

        //                if ((aChildAPPLICATIONMODULEINSTANCERow != null) &&
        //                    (aChildAPPLICATIONMODULERow.SUBKIND.ToUpper().Equals("SUBMODULE")))
        //                {
        //                    Boolean moduleUserPermissionValue = this._Visitor.GetRightStatement().GetRuleValue(
        //                        UserPermissionEntityKind.Section.ToString(),
        //                        aChildAPPLICATIONMODULERow.NAME,
        //                        ActionKind.View.ToString());
        //                    if (moduleUserPermissionValue)
        //                    {

        //                        // we add the current application module
        //                        ApplicationModuleInstance aChildApplicationModule = new ApplicationModuleInstance(
        //                            this,
        //                            aChildAPPLICATIONMODULEINSTANCERow,
        //                            visitor,
        //                            true,
        //                            aApplicationInstanceName);

        //                        // we merge the current application module's sections with the current child one
        //                        this.MergeSections(aChildApplicationModule.Sections, this.Sections);

        //                        // we add the child application module to this one
        //                        this._SubModules.Add(aChildApplicationModule);
        //                    }
        //                }
        //                else if (aChildAPPLICATIONMODULERow.SUBKIND.ToUpper().Equals("UNIVERSE"))
        //                {
        //                    // we add the current application module
        //                    ApplicationModuleInstance visitorApplicationModuleUniverse = new ApplicationModuleInstance(
        //                        this,
        //                        aChildAPPLICATIONMODULEINSTANCERow,
        //                        visitor,
        //                        true,
        //                        aApplicationInstanceName);

        //                    this._Universes.Add(visitorApplicationModuleUniverse);
        //                }
        //            }

        //            if ((this._SubKind == ApplicationModuleSubKind.Parent) & ((this._SubModules.Count > 0) | (this._Universes.Count > 0)))
        //                aRemoteUrl = moduleInstance.FULLURL;

        //            if (aRemoteUrl != null)
        //            {
        //                this._Url = aRemoteUrl;
        //                this._AbsoluteUrl = aRemoteUrl;
        //            }
        //            else
        //            {
        //                if (this._ApplicationModuleRow.EXECUTIONPATH == String.Empty)
        //                    this._Url = moduleInstance.FULLURL;
        //                else
        //                    this._Url = 
        //                        ((this._Options.ContainsKey("ISURLREWRITTEN")&&
        //                        (this._Options["ISURLREWRITTEN"].ToString().ToUpper()=="%TRUE()")) ?
        //                        "~/" : "~/" + this._ApplicationModuleRow.EXECUTIONPATH.ToLower());
        //                if (moduleInstance.IsFULLURLNull())
        //                    this._AbsoluteUrl = "";
        //                else
        //                    this._AbsoluteUrl = moduleInstance.FULLURL;
        //            }
        //        }

        //        // we determine the relative and absolute URLs
        //        this._Url = (
        //            ((this._Url != "") && (this._Url[this._Url.Length - 1] == '/')) ?
        //            this._Url :
        //            this._Url + "/");
        //        this._AbsoluteUrl = (
        //            ((this._AbsoluteUrl != "") && (this._AbsoluteUrl[this._AbsoluteUrl.Length - 1] == '/')) ?
        //            this._AbsoluteUrl :
        //            this._AbsoluteUrl + "/");

        //        // we check whether this application module instance corresponds to a visitor sphere bind if the indexation is Box or Cloud
        //        ApplicationModuleInstance aBaseApplicationModuleInstance = 
        //            this.GetBaseApplicationModuleInstance();

        //        long aAccountDomainId = (this._Visitor.SessionManager.CurrentUserRoom==null ? -1 :  this._Visitor.SessionManager.CurrentUserRoom.AccountDomainId);
        //        long aSphereId = (this._Visitor.SessionManager.CurrentUserRoom == null ? -1 : this._Visitor.SessionManager.CurrentUserRoom.SphereId);
        //        long aBindDomainId = (this._Visitor.SessionManager.CurrentUserRoom == null ? -1 : this._Visitor.SessionManager.CurrentUserRoom.BindDomainId);
        //        Boolean aIsAvailable = false;

        //        if ((this._Indexation == InstanceIndexation.Box) |
        //            (this._Indexation == InstanceIndexation.Cloud))
        //            if (aBaseApplicationModuleInstance != null)
        //            {
        //                // first we check that the module of this instance is bound to the service define in the current room
        //                ApplicationModule aAPPLICATIONMODULERow = moduleInstance.APPLICATIONMODULERow;
        //                if ((aAPPLICATIONMODULERow != null) &&
        //                    ((!aAPPLICATIONMODULERow.IsPLATFORMSERVICE_IDNull()) &&
        //                    (aAPPLICATIONMODULERow.PLATFORMSERVICE_ID == aSphereId)))
        //                {
        //                    // then we check that the sphere account domain is well the one specified in the user room
        //                    SessionDataSet.PLATFORMSPHERERow aPLATFORMSPHERERow =
        //                        this._Visitor.SessionManager.SessionDataSet.PLATFORMSPHERE.Where(p => (p.ID == aSphereId) & (p.ACCOUNTDOMAIN_ID == aAccountDomainId)).FirstOrDefault();
        //                    if (aPLATFORMSPHERERow != null)
        //                    {
        //                        // we check that there is a bind is available for the log visitor
        //                        aIsAvailable = false;

        //                        IEnumerable<SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTORow>
        //                            somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows =
        //                            (from SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTORow aCurrentPLATFORMSPHEREBIND_USER_ASSIGNEDTORow
        //                                in this._Visitor.SessionManager.SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTO.Where(
        //                                    p => (p.PLATFORMSPHERE_ID == aSphereId) & (p.BINDDOMAIN_ID == aBindDomainId))
        //                             join SessionDataSet.PLATFORMUSER_PLATFORMUSERWORKGROUP_ASSIGNEDTORow aCurrentPLATFORMUSER_PLATFORMUSERWORKGROUP_ASSIGNEDTORow
        //                                in this._Visitor.SessionManager.SessionDataSet.PLATFORMUSER_PLATFORMUSERWORKGROUP_ASSIGNEDTO
        //                                on aCurrentPLATFORMSPHEREBIND_USER_ASSIGNEDTORow.PLATFORMUSERWORKGROUP_ID equals aCurrentPLATFORMUSER_PLATFORMUSERWORKGROUP_ASSIGNEDTORow.PLATFORMUSERWORKGROUP_ID
        //                                into JoinedASSIGNEDTORow
        //                             from aCurrentPLATFORMUSER_PLATFORMUSERWORKGROUP_ASSIGNEDTORow in JoinedASSIGNEDTORow.DefaultIfEmpty()
        //                             where
        //                                (aCurrentPLATFORMSPHEREBIND_USER_ASSIGNEDTORow.PLATFORMUSER_ID == this._Visitor.SessionManager.LogVisitor.GetUserIntId()) |
        //                                (aCurrentPLATFORMUSER_PLATFORMUSERWORKGROUP_ASSIGNEDTORow.PLATFORMUSER_ID == this._Visitor.SessionManager.LogVisitor.GetUserIntId()) |
        //                                (aCurrentPLATFORMSPHEREBIND_USER_ASSIGNEDTORow.PLATFORMDOMAIN_ID == this._Visitor.DomainId)
        //                             select aCurrentPLATFORMSPHEREBIND_USER_ASSIGNEDTORow);
        //                        SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTORow aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_User = null;
        //                        aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_User =
        //                            somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows.Where(p =>
        //                                p.PLATFORMUSER_ID == this._Visitor.SessionManager.LogVisitor.GetUserIntId()).FirstOrDefault();
        //                        if (aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_User != null)
        //                            aIsAvailable = aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_User.VALUE;
        //                        else
        //                        {
        //                            IEnumerable<SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTORow> somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows_UserWorkgroups =
        //                                somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows.Where(p =>
        //                                p.IsPLATFORMUSER_IDNull() & !p.IsPLATFORMUSERWORKGROUP_IDNull());
        //                            if (somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows_UserWorkgroups.Count() > 0)
        //                            {
        //                                aIsAvailable = true;
        //                                foreach (SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTORow aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_UserWorkgroup
        //                                    in somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows_UserWorkgroups)
        //                                    aIsAvailable &= aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_UserWorkgroup.VALUE;
        //                            }
        //                            else
        //                            {
        //                                SessionDataSet.PLATFORMSPHEREBIND_USER_ASSIGNEDTORow aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_UserDomain = null;
        //                                aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_UserDomain =
        //                                    somePLATFORMSPHEREBIND_USER_ASSIGNEDTORows.Where(p =>
        //                                         p.IsPLATFORMUSER_IDNull() & p.IsPLATFORMUSERWORKGROUP_IDNull()).FirstOrDefault();
        //                                if (aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_UserDomain != null)
        //                                    aIsAvailable = aPLATFORMSPHEREBIND_USER_ASSIGNEDTORow_UserDomain.VALUE;
        //                            }
        //                        }
        //                    }
        //                }

        //                this._IsAvailable = aIsAvailable;
        //            }

        //        // we determine whether this instance is visible
        //        aIsAvailable = true;

        //        // the user rule is N if the application module is back
        //        if (this._Visibility == InstanceVisibility.BackOffice)
        //            aIsAvailable = false;
        //        else
        //        {
        //            aIsAvailable = this._Visitor.GetRightStatement().GetRuleValue(
        //                UserPermissionEntityKind.Section.ToString(),
        //                this._ApplicationModuleRow.NAME,
        //                ActionKind.View.ToString());

        //            // we add the application module if this visitor can access it
        //            // if the application module is payable then we check the sphere statement
        //            switch (this.Visibility)
        //            {
        //                case InstanceVisibility.Public:
        //                    aIsAvailable &= true;
        //                    break;
        //                case InstanceVisibility.Private:
        //                    //aIsAvailable &= (this._Visitor.SessionManager.IsLogged);
        //                    break;
        //                case InstanceVisibility.Internal:
        //                    aIsAvailable &= (this._Visitor.DomainKind == SphereVisitor.SphereUserDomainKind.Provider);
        //                    break;
        //                case InstanceVisibility.Payable:
        //                    aIsAvailable &= (this._Visitor.DomainKind == SphereVisitor.SphereUserDomainKind.Consumer);
        //                    break;
        //            }
        //        }
        //        this._IsAvailable &= aIsAvailable;
        //    }
        //}

        #endregion

        // ----------------------------
        // MUTATORS
        // ----------------------------

        #region Mutators

        /// <summary>
        /// Updates this instance.
        /// </summary>
        /// <param name="item">The item to consider.</param>
        /// <param name="specificationAreas">The specification areas to consider.</param>
        /// <param name="updateModes">The update modes to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <returns>Log of the operation.</returns>
        /// <remarks>Put reference collections as null if you do not want to repair this instance.</remarks>
        public override Log Update<T>(
            T item = null,
            List<String> specificationAreas = null,
            List<UpdateMode> updateModes = null,
            IAppScope appScope = null,
            ScriptVariableSet scriptVariableSet = null)
        {
            Log log = new Log();

            if (item is ApplicationModuleInstance)
            {
                ApplicationModuleInstance moduleInstance = item as ApplicationModuleInstance;
                this.Module.Update(moduleInstance.Module);
            }

            return log;
        }

        #endregion

        // ----------------------------
        // ACCESSORS
        // ----------------------------

        #region Accessors

        /// <summary>
        /// Returns the application section of this instance with the specified complete name.
        /// </summary>
        /// <returns>The application section of this instance with the specified complete name.</returns>
        public AppSection GetSectionWithCompleteName(String completeName)
        {
            if (completeName.Contains("$"))
                completeName = completeName.Substring(completeName.IndexOf('$') + 1);
            else
                completeName = "";
            String[] names = completeName.Split(new char[] { '$' });

            AppSection moduleSection = null;
            foreach (String name in names)
                if (!String.IsNullOrEmpty(name))
                {
                    if (moduleSection == null)
                        moduleSection = this.GetSectionWithName(name);
                    else
                        moduleSection = moduleSection.GetSubSectionWithName(name);
                }
            return moduleSection;
        }

        /// <summary>
        /// Returns the application section of this instance with the specified name.
        /// </summary>
        /// <returns>The application section of this instance with the specified name.</returns>
        public AppSection GetSectionWithName(String name)
        {
            if (this._Sections != null)
                foreach (AppSection moduleSection in this._Sections.Items)
                    if (moduleSection.Name.KeyEquals(name.ToUpper()))
                        return moduleSection;

            return null;
        }

        #endregion
    }
}