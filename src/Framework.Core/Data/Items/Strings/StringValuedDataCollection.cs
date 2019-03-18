using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using BindOpen.Framework.Core.Data.Helpers.Objects;

namespace BindOpen.Framework.Core.Data.Items.Strings
{

    /// <summary>
    /// This class represents a collection of string valued items.
    /// </summary>
    [Serializable()]
    [XmlType("StringValuedDataCollection", Namespace = "http://meltingsoft.com/bindopen/xsd")]
    [XmlRoot(ElementName = "collection", Namespace = "http://meltingsoft.com/bindopen/xsd", IsNullable = false)]
    public class StringValuedDataCollection : DataItem
    {
        // ------------------------------------------
        // VARIABLES
        // ------------------------------------------

        #region Variables

        private List<StringValuedDataItem> _Items = new List<StringValuedDataItem>();

        #endregion


        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// Items of this instance.
        /// </summary>
        [XmlArray("items")]
        [XmlArrayItem("item")]
        public List<StringValuedDataItem> Items
        {
            get
            {
                if (this._Items == null) this._Items = new List<StringValuedDataItem>();
                return this._Items;
            }
            set { this._Items = value; }
        }

        #endregion


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the StringValuedDataCollection class.
        /// </summary>
        public StringValuedDataCollection() : base()
        {
        }
        
        /// <summary>
        /// Instantiates a new instance of the StringValuedDataCollection class.
        /// </summary>
        public StringValuedDataCollection(String[][] detailAttributeTable):this()
        {
            if (detailAttributeTable != null)
                foreach (String[] strings in detailAttributeTable)
                    this.AddItem(strings[0], strings[1]);
        }

        #endregion


        // ------------------------------------------
        // MUTATORS
        // ------------------------------------------

        #region Mutators

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="basicDataItem">The new item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public StringValuedDataItem AddItem(StringValuedDataItem basicDataItem)
        {
            if ((basicDataItem == null) || (basicDataItem.Name == null))
                return null;
            this.RemoveItem(basicDataItem.Name);

            this._Items.Add(basicDataItem);
            return basicDataItem;
        }

        /// <summary>
        /// Adds a new item with the specified name, value and value type.
        /// </summary>
        /// <param name="name">The name of the item to add.</param>
        /// <param name="value">The value of the item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public StringValuedDataItem AddItem(
            String name,
            String value)
        {
            StringValuedDataItem basicDataItem = new StringValuedDataItem(name, value);
            return this.AddItem(basicDataItem);
        }


        /// <summary>
        /// Sets the value of the item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item whose value is to be updated.</param>
        /// <param name="value">The new value of the item.</param>
        public void SeItemValue(String name, String value)
        {
            StringValuedDataItem basicDataItem = this.GetItemWithName(name);
            if (basicDataItem != null)
                basicDataItem.SetValue(value);
        }

        /// <summary>
        /// Sets the values of the item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item whose values are to be updated.</param>
        /// <param name="values">The new values of the item.</param>
        public void SetItemValue(String name, List<String> values)
        {
            StringValuedDataItem basicDataItem = this.GetItemWithName(name);
            if (basicDataItem != null)
                basicDataItem.SetValues(values);
        }

        // Remove an item
        /// <summary>
        /// Removes the item with the specified name.
        /// </summary>
        /// <param name="name">The unique name of the item to remove.</param>
        public void RemoveItem(String name)
        {
            if (name == null) return;
            this._Items.RemoveAll(p => p.KeyEquals(name));
        }

        #endregion


        // ------------------------------------------
        // ACCESSORS
        // ------------------------------------------

        #region Accessors
       
        /// <summary>
        /// Returns the item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to return.</param>
        /// <returns>Returns the item with the specified name.</returns>
        public StringValuedDataItem GetItemWithName(String name)
        {
            return this._Items.Find(p=> p.KeyEquals(name));
        }

        /// <summary>
        /// Returns the single object value of the item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item whose value must be returned.</param>
        /// <returns>Returns the single object value of the item with the specified name.
        /// Returns null if none was found.</returns>
        public String GetItemValue(String name)
        {
            StringValuedDataItem basicDataItem = this.GetItemWithName(name);
            return basicDataItem?.GetValue();
        }

        /// <summary>
        /// Checks if this instance has an item with the specified name.
        /// </summary>
        /// <param name="name">The name of the item to check.</param>
        /// <returns>Returns true if the instance has an item with the specified name.</returns>
        public Boolean HasItemWithName(String name)
        {
            return (this.GetItemWithName(name) != null);
        }

        /// <summary>
        /// Returns true if this instance has any item.
        /// </summary>
        /// <returns>Returns true if this instance has any item.</returns>
        public Boolean HasItems()
        {
            return this._Items.Count > 0;
        }

        #endregion

        // --------------------------------------------------
        // EXPORTING
        // --------------------------------------------------

        #region Exporting

        /// <summary>
        /// Returns a text node representing this instance.
        /// </summary>
        /// <param name="nodeName">Name of the text node.</param>
        /// <param name="indent">Tabulation indent to include in the text.</param>
        /// <returns></returns>
        public String GetTextNode(
            String nodeName,
            String indent)
        {
            String st = "";

            st += indent + nodeName + "\n";
            st += "\t" + indent + nodeName + ":items" + "\n";
            foreach (StringValuedDataItem attribute in this.Items)
                st += attribute.GetTextNode(nodeName + ":items:item", "\t\t" + indent);
            return st;
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
        public override Object Clone()
        {
            StringValuedDataCollection clonedBasicCollection = base.Clone() as StringValuedDataCollection;

            foreach (StringValuedDataItem currentBasicDataItem in this.Items)
                clonedBasicCollection.Items.Add(currentBasicDataItem.Clone());
            return clonedBasicCollection;
        }

        #endregion
    }
}
