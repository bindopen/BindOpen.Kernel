import { MetaDataDto } from "../../meta/MetaDataDto";


export interface ReferenceDto {
    kind: any;
    identifier: string;
    text: string;
    expressionKind: any;
    metaData: MetaDataDto;
}
