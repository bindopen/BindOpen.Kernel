import { EventKinds } from "../../../logging/enums/EventKinds";
import { ConditionDto } from "../../conditions/ConditionDto";
import { SpecRuleKinds } from "../../enums/SpecRuleKinds";
import { ReferenceDto } from "../../objects/reference/ReferenceDto";
import { MetaScalarDto } from "../MetaScalarDto";

/// </summary>
export interface SpecRuleDto
{
    /// <summary>
    /// Identifier of this instance.
    /// </summary>
    id: string;

    //
    kind: SpecRuleKinds;

    /// <summary>
    /// The group identifier of this instance.
    /// </summary>
    groupId: string;

    /// <summary>
    /// Values of this instance.
    /// </summary>
    value: MetaScalarDto;

    /// <summary>
    /// The reference of this instance.
    /// </summary>
    reference: ReferenceDto;

    /// <summary>
    /// Default items of this instance.
    /// </summary>
    condition: ConditionDto;

    /// <summary>
    /// The result code of this instance.
    /// </summary>
    resultCode: string;

    resultEventKind: EventKinds; 

    resultTitle: string;

    resultDescription: string;
}
