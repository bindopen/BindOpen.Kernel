using BindOpen.Data.Elements;
using BindOpen.Extensions.Definition;
using BindOpen.System.Diagnostics;

namespace BindOpen.Extensions.Runtime
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBdoCarrier : ITBdoExtensionItem<IBdoCarrierDefinition>
    {
        /// <summary>
        /// 
        /// </summary>
        string RelativePath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string CreationDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string Flag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        bool IsReadonly { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LastAccessDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        string LastWriteDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="relativePath"></param>
        void SetPath(string path = null, string relativePath = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        ICarrierElement AsElement(string name = null, IBdoLog log = null);
    }
}