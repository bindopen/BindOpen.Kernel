import { ConditionDto } from "../../conditions/ConditionDto";
import { ReferenceDto } from "../../objects/reference/ReferenceDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";
import { MetaSetDto } from "../MetaSetDto";
import { ClassReferenceDto } from "../../assemblies/ClassReferenceDto";
import { DataValueTypes } from "../../enums/DataValueTypes";
import { ConstraintDto } from "./ConstraintDto";

export interface SpecDto {
    children: any[];
    id: string;
    name: string;
    condition: ConditionDto;
    constraints: ConstraintDto[];
    reference: ReferenceDto;
    description: DictionaryDto;
    title: DictionaryDto;
    detail: MetaSetDto;
    valueType: DataValueTypes;
    definitionUniqueName: string;
    classReference: ClassReferenceDto;
    groupId: string;
    defaultItems: any[];
    aliases: any[];
    availableDataModes: any[];
    minDataItemNumber?: number;
    maxDataItemNumber?: number;
    isAllocatable?: boolean;
    isStatic?: boolean;
    label: string;
    specLevels: any[];
    itemSpecLevels: any[];
    accessibilityLevel: any;
    inheritanceLevel: any;
}
