using BindOpen.System.Diagnostics.Logging;

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
                    var spec = meta.GetSpec();

                    this.WithDataType(spec.DataType);
                    this.WithDataMode(meta.DataMode);
                    if (meta.DataMode == DataMode.Any || meta.DataMode == DataMode.Reference)
                    {
                        this.WithDataReference(meta.Reference);
                    }
                    if (meta.DataMode == DataMode.Any || meta.DataMode == DataMode.Value)
                    {
                        this.WithData(meta.GetData());
                    }
                    //this.WithDataValueType(metaScalar.DataValueType);
                    //this.WithIndex(metaObj.Index);
                    //this.WithName(metaObj.Name);
                    //this.WithSpecs(metaObj.Specs?.ToArray());
                }
            }
        }
    }
}
