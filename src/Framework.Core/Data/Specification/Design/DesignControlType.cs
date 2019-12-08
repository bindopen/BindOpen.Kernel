
using System;
using BindOpen.Framework.Core.Data.Common;

namespace BindOpen.Framework.Core.Data.Specification
{

    /// <summary>
    /// This enumeration lists the possible design control types.
    /// </summary>
    public enum DesignControlType
    {
        /// <summary>
        /// None.
        /// </summary>
        None,

        /// <summary>
        /// Auto.
        /// </summary>
        Auto,

        /// <summary>
        /// Text box.
        /// </summary>
        TextBox,

        /// <summary>
        /// Check box.
        /// </summary>
        CheckBox,

        /// <summary>
        /// Radio button list.
        /// </summary>
        RadioButtonList,

        /// <summary>
        /// Calendar.
        /// </summary>
        Calendar,

        /// <summary>
        /// List box.
        /// </summary>
        ListBox,

        /// <summary>
        /// Combo box.
        /// </summary>
        ComboBox,

        /// <summary>
        /// Label.
        /// </summary>
        Label,

        /// <summary>
        /// Linkbutton.
        /// </summary>
        LinkButton,

        /// <summary>
        /// Hyerplink.
        /// </summary>
        Hyperlink,

        /// <summary>
        /// Download Linkbutton.
        /// </summary>
        DownloadLinkButton,

        /// <summary>
        /// Memo Textbox.
        /// </summary>
        MemoTextBox,

        /// <summary>
        /// Password Textbox.
        /// </summary>
        PasswordTextBox,

        /// <summary>
        /// Entity editor.
        /// </summary>
        ObjectEditor,

        /// <summary>
        /// Progress bar.
        /// </summary>
        ProgressBar,

        /// <summary>
        /// Time selector.
        /// </summary>
        TimeSelector,

        /// <summary>
        /// Week day selector.
        /// </summary>
        WeekDaySelector,

        /// <summary>
        /// Global object editor.
        /// </summary>
        DictionaryEditor,

        /// <summary>
        /// Database table editor.
        /// </summary>
        DatabaseTableEditor,

        /// <summary>
        /// Email editor.
        /// </summary>
        EmailEditor,

        /// <summary>
        /// Data carrier editor.
        /// </summary>
        CarrierConfigurationEditor,

        /// <summary>
        /// Data source editor.
        /// </summary>
        DatasourceEditor,

        /// <summary>
        /// Document editor.
        /// </summary>
        DocumentEditor,

        /// <summary>
        /// Editor of list.
        /// </summary>
        ListEditor,

        /// <summary>
        /// Custom control using schema.
        /// </summary>
        Custom
    }


    // --------------------------------------------------
    // EXTENSION
    // --------------------------------------------------

    #region Extension

    /// <summary>
    /// This class represents an extension of the DataValueType enumeration.
    /// </summary>
    public static class DataControlTypeExtension
    {

        /// <summary>
        /// Gets the default control type.
        /// </summary>
        /// <param name="valueType">The object to consider.</param>
        /// <returns>The result object.</returns>
        public static DesignControlType GetDefaultControlType(this DataValueType valueType)
        {
            switch (valueType)
            {
                case DataValueType.Boolean:
                    return DesignControlType.CheckBox;
                case DataValueType.Carrier:
                    return DesignControlType.CarrierConfigurationEditor;
                case DataValueType.Datasource:
                    return DesignControlType.DatasourceEditor;
                case DataValueType.Date:
                    return DesignControlType.Calendar;
                case DataValueType.Dictionary:
                    return DesignControlType.DictionaryEditor;
                case DataValueType.Document:
                    return DesignControlType.DocumentEditor;
                case DataValueType.Object:
                    return DesignControlType.ObjectEditor;
                case DataValueType.Integer:
                case DataValueType.Number:
                case DataValueType.Text:
                    return DesignControlType.TextBox;
                case DataValueType.Schema:
                case DataValueType.SchemaZone:
                case DataValueType.StringValued:
                case DataValueType.Time:
                    return DesignControlType.TimeSelector;
            }

            return DesignControlType.None;
        }

        /// <summary>
        /// Gets the default control type.
        /// </summary>
        /// <param name="aType">The type to consider.</param>
        /// <returns>The result object.</returns>
        public static DesignControlType GetDefaultControlType(this Type aType)
        {
            return (aType== null ? DesignControlType.None : 
                (aType.IsArray ? DesignControlType.ListEditor : aType.GetValueType().GetDefaultControlType()));
        }

    }

    #endregion

}
