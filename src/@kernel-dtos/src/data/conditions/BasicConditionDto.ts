

import { DataOperators } from "../enums/DataOperators";
import { ConditionDto } from "./ConditionDto";

export interface BasicConditionDto extends ConditionDto {
    argument1: any;
    operator: DataOperators;
    argument2: any;
}
