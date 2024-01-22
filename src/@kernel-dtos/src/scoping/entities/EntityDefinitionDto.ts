

import { ExtensionDefinitionDto } from "../ExtensionDefinitionDto";
import { ClassReferenceDto } from "../../data/assemblies/ClassReferenceDto";

export interface EntityDefinitionDto extends ExtensionDefinitionDto {
    itemClass: ClassReferenceDto;
    viewerClass: string;
    outputSpecification: any[];
}
