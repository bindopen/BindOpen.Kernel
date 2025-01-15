

import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { SpecDto } from "./definition/SpecDto";
import { IBdoDataTyped } from "../assemblies/IBdoTyped";
import { BdoMetaDataKinds } from "./enums/BdoMetaDataKinds";

export interface MetaDataDto extends IBdoDataTyped {
    id: string;
    parentId: string;
    metaKind: BdoMetaDataKinds;
    name: string;
    index?: number;
    reference: ReferenceDto;
    spec: SpecDto;
}
