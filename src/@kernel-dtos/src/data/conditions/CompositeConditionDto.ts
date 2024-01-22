
import { BdoCompositeConditionKind } from "../enums/BdoCompositeConditionKind";
import { ConditionDto } from "./ConditionDto";

export interface CompositeConditionDto extends ConditionDto {
    compositionKind: BdoCompositeConditionKind;
    conditions: any[];
}
