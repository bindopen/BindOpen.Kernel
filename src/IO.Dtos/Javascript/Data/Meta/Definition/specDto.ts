/* Auto Generated */

import { ConditionDto } from "./../../Conditions/conditionDto";
import { ReferenceDto } from "./../../Objects/Reference/referenceDto";
import { StringDictionaryDto } from "./../../Objects/Dictionary/stringDictionaryDto";
import { MetaSetDto } from "./../metaSetDto";
import { ClassReferenceDto } from "./../../Assemblies/classReferenceDto";
import { StringConditionalStatementDto } from "./stringConditionalStatementDto";
import { RequirementLevelConditionalStatementDto } from "./requirementLevelConditionalStatementDto";

export interface SpecDto {
    children: any[];
    childrenSpecficied: boolean;
    id: string;
    name: string;
    condition: ConditionDto;
    reference: ReferenceDto;
    description: StringDictionaryDto;
    title: StringDictionaryDto;
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
