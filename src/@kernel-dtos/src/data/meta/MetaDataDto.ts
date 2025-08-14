

import { ReferenceDto } from "../objects/reference/ReferenceDto";
import { SchemaDto } from "../schema/SchemaDto";
import { IBdoDataTyped } from "../assemblies/IBdoDataTyped";
import { BdoMetaDataKinds } from "./enums/BdoMetaDataKinds";

export interface MetaDataDto extends IBdoDataTyped {
    id: string;
    parentId: string;
    metaKind: BdoMetaDataKinds;
    name: string;
    index?: number;
    reference: ReferenceDto;
    schema: SchemaDto;
}
