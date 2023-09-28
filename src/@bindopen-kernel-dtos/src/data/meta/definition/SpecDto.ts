

import { ConditionDto } from "../../conditions/ConditionDto";
import { ReferenceDto } from "../../objects/reference/ReferenceDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";
import { MetaSetDto } from "../MetaSetDto";
import { ClassReferenceDto } from "../../assemblies/ClassReferenceDto";
import { StringConditionalStatementDto } from "./StringConditionalStatementDto";
import { RequirementLevelConditionalStatementDto } from "./RequirementLevelConditionalStatementDto";

export interface SpecDto {
    children: any[];
    childrenSpecficied: boolean;
    id: string;
    name: string;
    condition: ConditionDto;
    reference: ReferenceDto;
    description: DictionaryDto;
    title: DictionaryDto;
    detail: MetaSetDto;
    valueType: any;
    definitionUniqueName: string;
    classReference: ClassReferenceDto;
    groupId: string;
    defaultItems: any[];
    aliases: any[];
    availableDataModes: any[];
    minDataItemNumber?: number;
    minDataItemNumberSpecified: boolean;
    maxDataItemNumber?: number;
    maxDataItemNumberSpecified: boolean;
    isAllocatable?: boolean;
    isAllocatableSpecified: boolean;
    isStatic?: boolean;
    isStaticSpecified: boolean;
    label: string;
    specLevels: any[];
    itemSpecLevels: any[];
    accessibilityLevel: any;
    inheritanceLevel: any;
    constraintStatement: StringConditionalStatementDto;
    requirementStatement: RequirementLevelConditionalStatementDto;
    itemRequirementStatement: RequirementLevelConditionalStatementDto;
}
