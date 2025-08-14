import { BdoConditionKind } from "./_enums/BdoConditionKind";


export interface ConditionDto {
    id: string;
    parentId: string;
    kind: BdoConditionKind;
    trueValue: boolean;
}
