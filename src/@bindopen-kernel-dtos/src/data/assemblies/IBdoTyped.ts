

import { ClassReferenceDto } from "../assemblies/ClassReferenceDto";

export interface IBdoTypedDto {
    valueType: any;
    definitionUniqueName: string;
    classReference: ClassReferenceDto;
}
