import { BdoReferenceKind } from "../../enums/BdoReferenceKind";
import { MetaDataDto } from "../../meta/MetaDataDto";


export interface ReferenceDto {
    kind: BdoReferenceKind;
    identifier: string;
    text: string;
    metaData: MetaDataDto;
}
