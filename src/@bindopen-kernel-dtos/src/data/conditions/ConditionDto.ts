import { BdoConditionKind } from "../enums/BdoConditionKind";


export interface ConditionDto {
    id: string;
    kind: BdoConditionKind;
    trueValue: boolean;
}
