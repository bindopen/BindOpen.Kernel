

import { ConditionDto } from "./ConditionDto";

export interface BasicConditionDto extends ConditionDto {
    argument1: any;
    operator: any;
    argument2: any;
}
