using BindOpen.Logging;

namespace BindOpen.Data.Meta
{
    /// <summary>
    /// This class represents a catalog el that is an el whose els are carriers.
    /// </summary>
    public partial class BdoMetaObject : BdoMetaData,
        IBdoMetaObject
    {
        public void Update(
            ITBdoSet<IBdoMetaData> refSet,
            string[] areas = null,
            UpdateModes[] updateModes = null,
            IBdoLog log = null)
        {
            TBdoSetExtensions.Update(this, refSet, updateModes, areas, log);
        }

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

            if (refItem is IBdoMetaObject metaObj)
            {
                if (updateModes.Has(UpdateModes.Incremental_UpdateCommon))
                {
                    //this.WithClassReference(metaObj.ClassReference);
                    this.WithDataMode(metaObj.DataMode);
                    WithData(metaObj.GetData());
                    this.WithDataReference(metaObj.Reference);
                    //this.WithDataValueType(metaObj.DataValueType);
                    //this.WithIndex(metaObj.Index);
                    //this.WithName(metaObj.Name);
                    //this.WithSpecs(metaObj.Specs?.ToArray());
                }
            }
        }
    }
}
