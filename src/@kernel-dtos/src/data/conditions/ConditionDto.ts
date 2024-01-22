import { BdoConditionKind } from "../enums/BdoConditionKind";


export interface ConditionDto {
    id: string;
    parentId: string;
    kind: BdoConditionKind;
    trueValue: boolean;
}
