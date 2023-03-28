using BindOpen.Logging;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class BdoMetaScalar : BdoMetaData,
        IBdoMetaScalar
    {
        public override void Update(
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

            if (refItem is IBdoMetaScalar metaScalar)
            {
                if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                {
                    this.WithDataMode(metaScalar.DataMode);
                    WithData(metaScalar.GetData());
                    this.WithDataReference(metaScalar.Reference);
                    this.WithDataValueType(metaScalar.DataValueType);
                    //this.WithIndex(metaObj.Index);
                    //this.WithName(metaObj.Name);
                    //this.WithSpecs(metaObj.Specs?.ToArray());
                }
            }
        }
    }
}
