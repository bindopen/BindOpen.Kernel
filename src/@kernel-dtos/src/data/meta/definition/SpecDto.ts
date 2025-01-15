import { ConditionDto } from "../../conditions/ConditionDto";
import { ReferenceDto } from "../../objects/reference/ReferenceDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";
import { MetaSetDto } from "../MetaSetDto";
import { SpecRuleDto } from "./SpecRuleDto";
import { DataMode } from "../../enums/DataMode";
import { IBdoDataTyped } from "../../assemblies/IBdoTyped";

export interface SpecDto extends IBdoDataTyped {
    children: SpecDto[];
    id: string;
    parentId: string;
    name: string;
    condition: ConditionDto;
    rules: SpecRuleDto[];
    reference: ReferenceDto;
    description: DictionaryDto;
    title: DictionaryDto;
    detail: MetaSetDto;
    groupId: string;
    defaultItems: any[];
    aliases: string[];
    availableDataModes: DataMode[];
    minDataItemNumber?: number;
    maxDataItemNumber?: number;
    label: string;
    accessibilityLevel: any;
    inheritanceLevel: any;
}
