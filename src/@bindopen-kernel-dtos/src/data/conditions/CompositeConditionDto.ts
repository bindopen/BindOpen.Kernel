

import { ConditionDto } from "./ConditionDto";

export interface CompositeConditionDto extends ConditionDto {
    kind: any;
    conditions: any[];
}
