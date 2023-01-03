using BindOpen.Runtime.Definition;
using System;

namespace BindOpen.Extensions.Modeling
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoCarrier : ITBdoExtensionItem<IBdoCarrierDefinition, IBdoCarrierConfiguration, IBdoCarrier>
    {
        /// <summary>
        /// 
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string RelativePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="relativePath"></param>
        IBdoCarrier WithPath(string path = null, string relativePath = null);

        /// <summary>
        /// 
        /// </summary>
        string ParentPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        IBdoCarrier WithParentPath(string path);

        /// <summary>
        /// 
        /// </summary>
        DateTime? CreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IBdoCarrier WithCreationDate(DateTime? date);

        /// <summary>
        /// 
        /// </summary>
        string Flag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        IBdoCarrier WithFlag(string flag);

        /// <summary>
        /// 
        /// </summary>
        bool IsReadonly { get; set; }

        IBdoCarrier AsReadonly(bool readOnly = false);

        /// <summary>
        /// 
        /// </summary>
        DateTime? LastAccessDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        IBdoCarrier WithLastAccessDate(DateTime? date);

        /// <summary>
        /// 
        /// </summary>
        DateTime? LastWriteDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IBdoCarrier WithLastWriteDate(DateTime? date);
    }
}