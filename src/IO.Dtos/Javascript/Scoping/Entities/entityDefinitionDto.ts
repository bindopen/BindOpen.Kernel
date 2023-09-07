/* Auto Generated */

import { ExtensionDefinitionDto } from "./../_Core/extensionDefinitionDto";
import { ClassReferenceDto } from "./../../Data/Assemblies/classReferenceDto";

export interface EntityDefinitionDto extends ExtensionDefinitionDto {
    itemClass: ClassReferenceDto;
    viewerClass: string;
    outputSpecification: any[];
    outputSpecificationSpecified: boolean;
}
