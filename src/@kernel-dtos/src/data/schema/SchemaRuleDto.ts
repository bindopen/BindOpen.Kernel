import { EventKinds } from "../../logging/enums/EventKinds";
import { ConditionDto } from "../conditions/ConditionDto";
import { SchemaRuleKinds } from "./enums/SchemaRuleKinds";
import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { MetaScalarDto } from "../meta/MetaScalarDto";

/// </summary>
export interface SchemaRuleDto
{
    /// <summary>
    /// Identifier of this instance.
    /// </summary>
    id: string;

    //
    kind: SchemaRuleKinds;

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
