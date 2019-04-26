using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using BindOpen.Framework.Core.Data.Common;
using BindOpen.Framework.Core.Data.Items;
using BindOpen.Framework.Core.System.Diagnostics;
using BindOpen.Framework.Core.System.Diagnostics.Interfaces;

namespace BindOpen.Framework.Core.Data.Elements
{
    /// <summary>
    /// This static class provides methods to handle data elements.
    /// </summary>
    public static class DataElementHandler
    {
        // --------------------------------------------------
        // ITEMS
        // --------------------------------------------------

        #region Items

        /// <summary>
        /// Gets the items of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the items of this instance.</returns>
        public List<object> GetItems(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            log= (log?? new Log());

            object object1 = null;
            List<object> items = new List<object>();
            switch(ItemizationMode)
            {
                case DataItemizationMode.Valued:
                    items = _items;
                    break;
                case DataItemizationMode.Referenced:
                    if (appScope == null)
                        log.AddError(title: "Application scope missing");
                    else if (appScope.ScriptInterpreter == null)
                        log.AddError(title: "Script interpreter missing");
                    else if (ItemReference == null)
                        log.AddWarning(title: "Reference missing");
                    else
                        SetItem(ItemReference.Get(appScope, scriptVariableSet, log));
                    break;
                case DataItemizationMode.Script:
                    if (appScope == null)
                        log.AddError(title: "Application scope missing");
                    else if (appScope.ScriptInterpreter == null)
                        log.AddError(title: "Script interpreter missing");
                    else if (string.IsNullOrEmpty(ItemScript))
                        log.AddWarning(title: "Script missing");
                    else
                    {
                        object1 = appScope.ScriptInterpreter.Interprete(ItemScript, scriptVariableSet, log);
                        if (object1 != null)
                            if (object1.GetType().IsArray)
                                items = object1 as List<object>;
                            else
                                items = new List<object>() { object1 };
                    }
                    break;
            }

            return items;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public virtual object GetItem(
            object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            List<object> objects = GetItems(appScope, scriptVariableSet, log);

            if (objects == null)
            {
                return null;
            }
            else if (indexItem == null)
            {
                return objects.Count > 0 ? objects[0] : null;
            }
            else if (indexItem is int)
            {
                int index = (indexItem as int?).Value;
                return objects.Count > index ? objects[objects.Count - 1] : objects[index - 1];
            }

            return null;
        }

        /// <summary>
        /// Gets the item object of this instance.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the item object.</returns>
        public object GetItemObject(
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if (Specification?.IsValueList != true)
                return GetItem(null, appScope, scriptVariableSet, log);
            else
                return GetItems(appScope, scriptVariableSet, log);
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public virtual string GetStringFromObject(
            object object1,
            ILog log = null)
        {
            return "";
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public virtual object GetObjectFromString(
            string stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            return null;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
            {
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            }
            else if (indexItem is string)
            {
                return this.GetItems(appScope, scriptVariableSet, log)
                    .Any(p => p is Items.Documents.Document && string.Equals((p as Items.Documents.Document)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override object GetItem(
            object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
            {
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            }
            else if (indexItem is string)
            {
                return this.GetItems(appScope, scriptVariableSet, log)
                   .Any(p => p is NamedDataItem && string.Equals((p as NamedDataItem)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        /// <summary>
        /// Returns the item with the specified key.
        /// </summary>
        /// <param name="key">The key of the element to return.</param>
        /// <returns>Returns the element with the specified key.</returns>
        public override IDataElement GetItem(string key)
        {
            return Elements?.FirstOrDefault(p =>
                p.KeyEquals(key)
                || (p.Specification != null && p.Specification.Aliases != null
                    && p.Specification.Aliases.Any(q => q.KeyEquals(key))));
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override object GetItem(
            object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            else if (indexItem is string)
                return this.Items.Any(p => p is DataElement && string.Equals((p as DataElement)?.Name ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));

            return null;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            else if (indexItem is string)
            {
                Object item = this.GetObjectFromString(indexItem as string);
                return this.Items.First(p => p.KeyEquals(item));
            }

            return null;
        }

        /// <summary>
        /// Returns the specified item of this instance.
        /// </summary>
        /// <param name="indexItem">The index item to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="scriptVariableSet">The script variable set to use.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>Returns the specified item of this instance.</returns>
        public override object GetItem(
            Object indexItem = null,
            IAppScope appScope = null,
            IScriptVariableSet scriptVariableSet = null,
            ILog log = null)
        {
            if ((indexItem == null) || (indexItem is int))
            {
                return base.GetItem(indexItem, appScope, scriptVariableSet, log);
            }
            else if (indexItem is string)
            {
                return this.GetItems(appScope, scriptVariableSet, log)
                   .Any(p => p is IDataSource && string.Equals((p as IDataSource)?.Key() ?? "", indexItem.ToString(), StringComparison.OrdinalIgnoreCase));
            }

            return null;
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override string GetStringFromObject(
            object object1,
            ILog log = null)
        {
            string stringValue = "";

            if (object1 is DataItem)
            {
                DataItem item = object1 as DataItem;
                if (item != null)
                    stringValue = item.ToXml();
                else if (log != null)
                    log.AddError(title: "object expected", description: "The specified object is not an entity.");
            }

            return stringValue;
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public override object GetObjectFromString(
            string stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            object object1 = null;

            //if (stringValue != null)
            //    if ((this.Specification == null || this.Specification is ObjectElementSpec)
            //        && (appScope != null && appScope.AppExtension!= null))
            //        appScope.AppExtension.LoadDataItemInstance(AssemblyHelper.GetClassReference(this.ClassFullName), stringValue, out object1);

            return object1;
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override string GetStringFromObject(
            Object object1,
            ILog log = null)
        {
            String stringValue = "";

            if (object1 is EntityConfiguration)
            {
                EntityConfiguration dataEntityItem = object1 as EntityConfiguration;
                if (dataEntityItem != null)
                    stringValue = dataEntityItem.ToXml();
                else if (log != null)
                    log.AddError(title: "Entity expected", description: "The specified object is not an entity.");
            }

            return stringValue;
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public override object GetObjectFromString(
            string stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            Object object1 = null;

            //if (stringValue != null && appScope?.AppExtension != null)
            //{
            //    appScope.AppExtension.CreateConfiguration<ConnectorDefinition>(null, stringValue, log);
            //}

            return object1;
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override string GetStringFromObject(
            object object1,
            ILog log = null)
        {
            string stringValue = "";

            if (object1 is CarrierConfiguration)
            {
                CarrierConfiguration dataCarrier = object1 as CarrierConfiguration;
                if (dataCarrier != null)
                    stringValue = dataCarrier.ToXml();
                else if (log != null)
                    log.AddError(title: "Entity expected", description: "The specified object is not an entity.");
            }

            return stringValue;
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public override object GetObjectFromString(
            string stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            Object object1 = null;

            if (stringValue != null)
            {
                object1 = appScope.LoadConfiguration<CarrierDefinition>(stringValue, log);
            }

            return object1;
        }

        #endregion

        // --------------------------------------------------
        // SERIALIZATION
        // --------------------------------------------------

        #region Serialization

        /// <summary>
        /// Updates information for storage.
        /// </summary>
        /// <param name="log">The log to update.</param>
        public override void UpdateStorageInfo(ILog log = null)
        {
            _propertyDetail?.UpdateStorageInfo(log);

            ItemReference?.UpdateStorageInfo(log);

            // we serialize items

            const string root = "<?xml version=\"1.0\" encoding=\"utf-16\"?>";
            const string xsd = " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"";
            const string xsi = " xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"";
            const string xmlns = " xmlns=\"http://www.w3.org/2001/bdo.xsd\"";

            if (Items?.Count > 0)
            {
                string st = "";
                if (ValueType.IsScalar())
                {
                    st = "<values>";
                    foreach (object item in Items)
                        st += "<add>" + (GetStringFromObject(item, log) ?? "") + "</add>";
                    st += "</values>";
                }
                else
                {
                    st = "<objects>";
                    foreach (object item in Items)
                        st += (GetStringFromObject(item, log) ?? "").Replace(root, "").Replace(xsd, "").Replace(xsi, "").Replace(xmlns, "");//.Replace(xmlnsEmpty,"");
                    st += "</objects>";
                }

                ItemXElement = XElement.Parse(st, LoadOptions.SetBaseUri);
                //XNamespace aXNamespace = "http://meltingsoft.com/bindopen/xsd";
                if (ItemXElement != null)
                    ItemXElement.Name = ItemXElement.Name.LocalName;
            }
        }

        /// <summary>
        /// Updates information for runtime.
        /// </summary>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to update.</param>
        public override void UpdateRuntimeInfo(IAppScope appScope = null,  ILog log = null)
        {
                _propertyDetail?.UpdateRuntimeInfo(appScope, log);

                ItemReference?.UpdateRuntimeInfo(appScope, log);

            if (ItemXElement != null)
            {
                Items = new List<object>();
                foreach (XElement subXElement in ItemXElement.Elements())
                {
                    subXElement.Add(
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema")
                        , new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"));
                    XNamespace aXNamespace = "http://meltingsoft.com/bindopen/xsd";
                    subXElement.Name = aXNamespace + subXElement.Name.LocalName;

                    string xElementString =
                        "<?xml version=\"1.0\" encoding=\"utf-8\"?>" + subXElement.ToString().Replace(" xmlns=\"\"", "");

                    AddItem(GetObjectFromString(xElementString, appScope, log));
                }
            }
            else if (Items != null)
            {
                for (int i = 0; i < Items.Count; i++)
                {
                    object item = Items[i];
                    DataValueType itemValueType = DataValueType.Any;
                    if (item != null && (itemValueType = item.GetValueType()) != ValueType)
                    {
                        if ((ValueType != DataValueType.Any)
                           && (itemValueType == DataValueType.Text || ValueType == DataValueType.Text))
                        {
                            if (itemValueType == DataValueType.Text)
                                item = GetObjectFromString(item as string, appScope, log);
                            else
                                item = item.ToString();
                            Items.RemoveAt(i);
                            Items.Insert(i, item);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the tex node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns>Returns the text node.</returns>
        public string GetTextNode(string nodeName, string indent)
        {
            string st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":name=\"" + Key() + "\"\n";
            st += "\t" + indent + nodeName + ":valueType=\"" + ValueType.ToString() + "\"\n";
            if (Items.Count>0)
            {
                st += "\t" + indent + nodeName + ":items\n";
                foreach (string aItemstring in Items)
                    st += "\t\t" + indent + nodeName + ":items:item=\"" + aItemstring + "\"\n";
            }

            return st;
        }

        public object Get()
        {
            if (this.ItemXElement != null)
            {
                XElement element = this.ItemXElement.Elements().FirstOrDefault();
                if (element != null)
                    return element.Value;
            }

            return null;
        }

        // Conversion ---------------------------

        /// <summary>
        /// Returns the string value from an object based on this instance's specification.
        /// </summary>
        /// <param name="object1">The object value to convert.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result string.</returns>
        public override string GetStringFromObject(
            Object object1,
            ILog log = null)
        {
            String stringValue = "";

            if (object1 != null)
                stringValue = object1.GetString(this.ValueType);

            return stringValue;
        }

        /// <summary>
        /// Returns the object value from a based on this instance's specification.
        /// </summary>
        /// <param name="stringValue">The string value to consider.</param>
        /// <param name="appScope">The application scope to consider.</param>
        /// <param name="log">The log to populate.</param>
        /// <returns>The result object.</returns>
        public override object GetObjectFromString(
            String stringValue,
            IAppScope appScope = null,
            ILog log = null)
        {
            Object object1 = null;

            String format = null;
            if (this.Specification != null && this.Specification.ConstraintStatement != null)
                format = this.Specification.ConstraintStatement.GetConstraintParameterValue("Format") as string;
            if (stringValue != null)
                object1 = stringValue.ToObject(this.ValueType, format);

            return object1;
        }
        }

    // Conversion -----------------------------------

    /// <summary>
    /// Returns the string value from an object based on this instance's specification.
    /// </summary>
    /// <param name="object1">The object value to convert.</param>
    /// <param name="log">The log to populate.</param>
    /// <returns>The result string.</returns>
    public override string GetStringFromObject(
        object object1,
        ILog log = null)
    {
        string stringValue = "";

        if (object1 is DataElement)
            stringValue = (object1 as DataElement).ToString();

        return stringValue;
    }

    /// <summary>
    /// Returns the object value from a based on this instance's specification.
    /// </summary>
    /// <param name="stringValue">The string value to consider.</param>
    /// <param name="appScope">The application scope to consider.</param>
    /// <param name="log">The log to populate.</param>
    /// <returns>The result object.</returns>
    public override object GetObjectFromString(
        string stringValue,
        IAppScope appScope = null,
        ILog log = null)
    {
        object object1 = null;

        if (stringValue != null)
            object1 = XmlHelper.LoadFromString<DataElement>(stringValue, log);

        return object1;
    }

    #endregion
}
}
