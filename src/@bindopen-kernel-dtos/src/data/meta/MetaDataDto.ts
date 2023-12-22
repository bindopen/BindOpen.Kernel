

import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { SpecDto } from "./definition/SpecDto";
import { IBdoTypedDto } from "../assemblies/IBdoTyped";
import { MetaDataKinds } from "../enums/MetaDataKind";

export interface MetaDataDto extends IBdoTypedDto {
    id: string;
    parentId: string;
    metakind: MetaDataKinds;
    name: string;
    index?: number;
    reference: ReferenceDto;
    spec: SpecDto;
}
