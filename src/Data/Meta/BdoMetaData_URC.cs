using BindOpen.System.Logging;

namespace BindOpen.System.Data.Meta
{
    /// <summary>
    /// This class represents a data element.
    /// </summary>
    public abstract partial class BdoMetaData : BdoObject, IBdoMetaData
    {
        public virtual void Update(
            IBdoMetaData refItem,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            areas ??= new[] { nameof(DataAreaKind.Any) };
            updateModes ??= new[]
            {
                UpdateModes.Incremental_UpdateCommon
            };

            if (refItem is IBdoMetaData meta)
            {
                if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                {
                    this.WithDataType(meta.DataType);
                    this.WithDataReference(meta.DataReference);
                    this.WithData(meta.GetData());
                }
            }
        }
    }
}
