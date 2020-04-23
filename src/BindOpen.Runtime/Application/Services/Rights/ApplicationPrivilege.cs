using BindOpen.Data.Helpers.Objects;
using BindOpen.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace BindOpen.Application.Rights
{
    /// <summary>
    /// This structure respresents a right granted by an application.
    /// </summary>
    public class ApplicationPrivilege : DataItem
    {

        // ------------------------------------------
        // ENUMERATIONS
        // ------------------------------------------

        #region Enumerations

        /// <summary>
        /// This enumeration lists the possible entity kinds of application privilege.
        /// </summary>
        public enum ApplicationPrivilegeEntityKind
        {
            /// <summary>
            /// Any.
            /// </summary>
            Any,

            /// <summary>
            /// Page or page section.
            /// </summary>
            Section,

            /// <summary>
            /// Object class.
            /// </summary>
            Class
        };

        #endregion


        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<UserPermission> _DefaultPermissions = new List<UserPermission>();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Entity unique ID of this instance.
        /// </summary>
        [XmlIgnore()]
        public string EntityUniqueName
        {
            get
            {
                return (this.EntityKind.ToString() + "#" + (this.EntityName ?? "")).ToLower();
            }
        }

        /// <summary>
        /// Entity kind of this instance.
        /// </summary>
        [XmlElement("entityKind")]
        public string EntityKind
        {
            set;
            get;
        }

        /// <summary>
        /// Entity name of this instance.
        /// </summary>
        [XmlElement("entityName")]
        public string EntityName
        {
            set;
            get;
        }

        /// <summary>
        /// Default privileges of this instance.
        /// </summary>
        [XmlArray("defaultPermissions")]
        [XmlArrayItem("add")]
        public List<UserPermission> DefaultPermissions
        {
            set
            {
                this._DefaultPermissions = value;
            }
            get
            {
                return this._DefaultPermissions;
            }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Creates a new instance of the ApplicationPrivilege class.
        /// </summary>
        public ApplicationPrivilege()
        {
        }

        /// <summary>
        /// Creates a new instance of the ActionPermission class.
        /// </summary>
        /// <param name="entityKind">The entity kind to consider.</param>
        /// <param name="entityName">The entity name to consider.</param>
        /// <param name="defaultPermissions">The default permissions to add.</param>
        public ApplicationPrivilege(
            String entityKind,
            String entityName,
            params UserPermission[] defaultPermissions)
        {
            this.EntityKind = entityKind;
            this.EntityName = entityName;
            this.DefaultPermissions = (defaultPermissions == null ? new List<UserPermission>() : defaultPermissions.ToList());
        }

        #endregion


        // --------------------------------------------------
        // MUTATORS
        // --------------------------------------------------

        #region Mutators

        /// <summary>
        /// Sets the specified permission.
        /// </summary>
        /// <param name="permission">The action rule.</param>
        public void SetDefaultPermission(
            UserPermission permission)
        {
            // we add the action
            if (permission == null) return;
            this.DefaultPermissions.RemoveAll(p => string.Equals(p.ActionName, permission.ActionName, StringComparison.OrdinalIgnoreCase));
            this.DefaultPermissions.Add(permission);
        }

        /// <summary>
        /// Sets the specified default permission.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <param name="value">Value.</param>
        public void SetDefaultPermission(
            String actionName,
            Boolean value)
        {
            this.SetDefaultPermission(new UserPermission(actionName, value));
        }

        /// <summary>
        /// Sets the specified default permission.
        /// </summary>
        /// <param name="actionName">Name of the action to consider.</param>
        /// <param name="stringValue">String value to consider.</param>
        public void SetDefaultPermission(
            String actionName,
            String stringValue)
        {
            this.SetDefaultPermission(new UserPermission(actionName, stringValue));
        }

        #endregion


        // --------------------------------------------------
        // ACCESSORS
        // --------------------------------------------------

        #region Accessors

        /// <summary>
        /// Return the default privilege of this instance.
        /// </summary>
        /// <param name="actionName">Action name to consider.</param>
        /// <returns>The specified action rule of this instance.</returns>
        public UserPermission GetDefaultPermission(String actionName)
        {
            // we build the complete entity name
            if (actionName == null) return null;
            return this.DefaultPermissions.FirstOrDefault(p => p.KeyEquals(actionName));
        }

        #endregion

    }
}