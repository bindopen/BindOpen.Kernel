

import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { ClassReferenceDto } from "../assemblies/ClassReferenceDto";
import { SpecDto } from "./definition/SpecDto";

export interface MetaDataDto {
    id: string;
    parentId: string;
    name: string;
    index?: number;
    reference: ReferenceDto;
    spec: SpecDto;
    valueType: any;
    definitionUniqueName: string;
    classReference: ClassReferenceDto;
}
