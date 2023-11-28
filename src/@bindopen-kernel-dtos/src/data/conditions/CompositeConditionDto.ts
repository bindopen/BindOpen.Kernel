

import { CompositeConditionKind } from "../enums/AdvancedConditionKind";
import { ConditionDto } from "./ConditionDto";

export interface CompositeConditionDto extends ConditionDto {
    kind: CompositeConditionKind;
    conditions: any[];
}
