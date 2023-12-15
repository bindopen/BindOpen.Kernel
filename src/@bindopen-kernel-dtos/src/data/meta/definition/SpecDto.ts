import { ConditionDto } from "../../conditions/ConditionDto";
import { ReferenceDto } from "../../objects/reference/ReferenceDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";
import { MetaSetDto } from "../MetaSetDto";
import { ClassReferenceDto } from "../../assemblies/ClassReferenceDto";
import { DataValueTypes } from "../../enums/DataValueTypes";
import { SpecRuleDto } from "./SpecRuleDto";

export interface SpecDto {
    children: any[];
    id: string;
    parentId: string;
    name: string;
    condition: ConditionDto;
    rules: SpecRuleDto[];
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
    label: string;
    accessibilityLevel: any;
    inheritanceLevel: any;
}
