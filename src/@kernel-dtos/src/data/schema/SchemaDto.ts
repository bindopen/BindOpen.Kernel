import { ConditionDto } from "../conditions/ConditionDto";
import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { DictionaryDto } from "../objects/dictionary/DictionaryDto";
import { MetaSetDto } from "../meta/MetaSetDto";
import { SchemaRuleDto } from "./SchemaRuleDto";
import { DataMode } from "../_core/enums/DataMode";
import { IBdoDataTyped } from "../assemblies/IBdoDataTyped";

export interface SchemaDto extends IBdoDataTyped {
    children: SchemaDto[];
    id: string;
    parentId: string;
    name: string;
    condition: ConditionDto;
    rules: SchemaRuleDto[];
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
