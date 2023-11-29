import { BdoReferenceKind } from "../../enums/BdoReferenceKind";
import { MetaDataDto } from "../../meta/MetaDataDto";


export interface ReferenceDto {
    kind: any;
    identifier: string;
    text: string;
    expressionKind: BdoReferenceKind;
    metaData: MetaDataDto;
}
