

import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { SpecDto } from "./definition/SpecDto";
import { IBdoTypedDto } from "../assemblies/IBdoTyped";
import { MetaDataKinds } from "../enums/MetaDataKinds";

export interface MetaDataDto extends IBdoTypedDto {
    id: string;
    parentId: string;
    metaKind: MetaDataKinds;
    name: string;
    index?: number;
    reference: ReferenceDto;
    spec: SpecDto;
}
