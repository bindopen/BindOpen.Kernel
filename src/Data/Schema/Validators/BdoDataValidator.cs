using BindOpen.Data.Helpers;
using BindOpen.Data.Meta;
using BindOpen.Logging;
using BindOpen.Scoping;
using System.Data;
using System.Linq;

namespace BindOpen.Data.Schema;

/// <summary>
/// This class represents a data validator.
/// </summary>
public class BdoDataValidator : ITBdoDataValidator<IBdoMetaData, IBdoSchema>
{
    /// <summary>
    /// The scope of this instance.
    /// </summary>
    public IBdoScope Scope { get; set; }

    /// <summary>
    /// Checks the specified meta data.
    /// </summary>
    /// <param name="meta">The meta data to check.</param>
    /// <returns>Returns the check log./returns>
    public bool Check(
        IBdoMetaData meta,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
        => Check(meta, meta?.Schema, varSet, log);

    /// <summary>
    /// Checks the specified meta data corresponding to the meta schema.
    /// </summary>
    /// <param name="meta">The meta data to check.</param>
    /// <param name="defaultSpec">The meta schema to consider.</param>
    /// <returns>Returns the check log./returns>
    public virtual bool Check(
        IBdoMetaData meta,
        IBdoSchema defaultSpec,
        IBdoMetaSet varSet = null,
        IBdoLog log = null)
    {
        bool valid = true;

        // Si meta est null, on vérifie si le schéma par défaut exige sa présence
        if (meta == null)
        {
            if (defaultSpec != null)
            {
                var requirementLevel = defaultSpec.GetRuleValue<RequirementLevels>(
                    BdoMetaDataProperties.RequirementLevel, BdoSchemaRuleKinds.Requirement, Scope, varSet, log);

                if (requirementLevel == RequirementLevels.Required)
                {
                    log?.AddEvent(BdoEventKinds.Error, "Element missing",
                        $"The required element '{defaultSpec.Name}' is missing",
                        resultCode: BdoSchemaRuleResultCodes.ElementMissing);
                    return false;
                }
            }
            return true;
        }

        // Initialisation du contexte d'exécution local
        var scope = meta.Scope ?? Scope;
        var localVarSet = BdoData.NewNode(varSet?.ToArray());
        localVarSet.With(BdoData.__VarName_This, meta);

        var schema = meta.Schema ?? defaultSpec;

        // 1. Vérification de la compatibilité des types
        var data = meta.GetData(scope, localVarSet, log);

        if (schema != null)
        {
            bool typeCompatible = meta.DataType?.IsCompatibleWithType(schema.DataType) != false
                                && schema.IsCompatibleWithData(data);

            if (!typeCompatible)
            {
                valid = false;
                log?.AddEvent(BdoEventKinds.Error, "Bad value type",
                    $"The type of data of element '{meta.Name}' is not compatible with '{schema.DataType}' type",
                    resultCode: BdoSchemaRuleResultCodes.InvalidData);
            }

            // 2. Vérification du niveau d'exigence (Requirement)
            var requirementLevel = schema.GetRuleValue<RequirementLevels>(
                BdoMetaDataProperties.RequirementLevel, BdoSchemaRuleKinds.Requirement, scope, localVarSet, log);

            if (requirementLevel == RequirementLevels.Forbidden)
            {
                log?.AddEvent(BdoEventKinds.Error, "Element forbidden",
                    $"The element '{meta.Name}' is forbidden",
                    resultCode: BdoSchemaRuleResultCodes.ElementForbidden);
                return false;
            }

            // 3. Vérification de l'exigence des items (ItemRequirement)
            var itemRequirementLevel = schema.GetRuleValue<RequirementLevels>(
                BdoMetaDataProperties.ItemRequirementLevel, BdoSchemaRuleKinds.Requirement, scope, localVarSet, log);

            var itemNumber = data.ToObjectList()?.Count ?? 0;

            if (itemRequirementLevel == RequirementLevels.Required && itemNumber == 0)
            {
                log?.AddEvent(BdoEventKinds.Error, "Value missing",
                    $"The value of the element '{meta.Name}' is missing",
                    resultCode: BdoSchemaRuleResultCodes.ElementMissing);
                valid = false;
            }
            else if (itemRequirementLevel == RequirementLevels.Forbidden && itemNumber > 0)
            {
                log?.AddEvent(BdoEventKinds.Error, "Value forbidden",
                    $"Any value of element '{meta.Name}' is forbidden",
                    resultCode: BdoSchemaRuleResultCodes.ElementForbidden);
                valid = false;
            }

            // 4. Vérification du nombre d'items
            var maxNumber = schema.MaxDataItemNumber ?? int.MaxValue;
            if (schema.DataType?.ValueType != DataValueTypes.Null
                && (itemNumber > maxNumber || itemNumber < schema.MinDataItemNumber))
            {
                valid = false;
                log?.AddEvent(BdoEventKinds.Error, "Invalid data item number",
                    $"The element '{meta.Name}' must have between {schema.MinDataItemNumber} and {maxNumber} data items ({itemNumber} found)",
                    resultCode: BdoSchemaRuleResultCodes.BadItemNumber);
            }

            // 5. Vérification des règles (Requirements & Constraints)
            if (schema.RuleSet != null)
            {
                // Groupes de conditions (Requirements)
                var groupIds = schema.RuleSet.Where(q => q.Kind == BdoSchemaRuleKinds.Requirement)
                    .OrderBy(q => q.GetIndexValue())
                    .Select(q => q.GroupId).Distinct();

                foreach (var groupId in groupIds)
                {
                    var rule = meta.GetSchemaRule(scope, groupId, BdoSchemaRuleKinds.Requirement, localVarSet, log);
                    if (rule != null)
                    {
                        var exp = BdoData.NewExp(rule.GroupId);
                        var currentValue = scope?.Interpreter.Evaluate(exp, localVarSet, log);

                        if (!Equals(currentValue, rule.Value))
                        {
                            valid = false;
                            log?.AddEvent(rule.ResultEventLevel, rule.ResultTitle, rule.ResultDescription, resultCode: rule.ResultCode);
                        }
                    }
                }

                // Contraintes (Constraints)
                var constraints = schema.RuleSet.Where(q => q.Kind == BdoSchemaRuleKinds.Constraint);
                foreach (var constraint in constraints)
                {
                    var refObj = constraint.Reference != null
                        ? scope?.Interpreter.Evaluate(constraint.Reference, localVarSet, log)
                        : meta;

                    var constraintVarSet = BdoData.NewNode(localVarSet?.ToArray());
                    constraintVarSet.With(BdoData.__VarName_This, refObj);

                    if (scope?.Interpreter.Evaluate(constraint.Condition, constraintVarSet, log) != true)
                    {
                        valid = false;
                        log?.AddEvent(constraint.ResultEventLevel, constraint.ResultTitle, constraint.ResultDescription, resultCode: constraint.ResultCode);
                    }
                }
            }
        }
        else if (!meta.IsCompatibleWithData(data)) // Cas sans schéma
        {
            valid = false;
            log?.AddEvent(BdoEventKinds.Error, "Incompatible data", $"Data of element '{meta.Name}' is incompatible",
                resultCode: BdoSchemaRuleResultCodes.InvalidData);
        }

        // 6. Validation récursive des enfants
        if (schema?._Children != null)
        {
            var metaSet = meta as ITBdoSet<IBdoMetaData>;
            foreach (var childSpec in schema._Children)
            {
                var childRequirement = childSpec.GetRuleValue<RequirementLevels>(
                    BdoMetaDataProperties.RequirementLevel, BdoSchemaRuleKinds.Requirement, scope, localVarSet, log);

                bool hasChild = metaSet?.Has(childSpec.Name) == true;

                if (childRequirement == RequirementLevels.Required && !hasChild)
                {
                    valid = false;
                    log?.AddEvent(BdoEventKinds.Error, "Child element missing", $"Option '{childSpec.Name}' missing");
                }
                else if (childRequirement == RequirementLevels.Forbidden && hasChild)
                {
                    valid = false;
                    log?.AddEvent(BdoEventKinds.Error, "Child element forbidden", $"Option '{childSpec.Name}' is forbidden here");
                }
            }

            // Appel récursif pour valider le contenu des sous-éléments
            if (metaSet != null)
            {
                foreach (var subMeta in metaSet)
                {
                    var subSpec = schema.Child(subMeta?.Name);
                    valid &= Check(subMeta, subSpec, varSet, log);
                }
            }
        }

        return valid;
    }
}