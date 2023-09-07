/* Auto Generated */

import { ReferenceDto } from "./../Objects/Reference/referenceDto";
import { SpecDto } from "./Definition/specDto";
import { ClassReferenceDto } from "./../Assemblies/classReferenceDto";

export interface MetaDataDto {
    id: string;
    name: string;
    index?: number;
    indexSpecified: boolean;
    reference: ReferenceDto;
    spec: SpecDto;
    valueType: any;
    definitionUniqueName: string;
    classReference: ClassReferenceDto;
}
