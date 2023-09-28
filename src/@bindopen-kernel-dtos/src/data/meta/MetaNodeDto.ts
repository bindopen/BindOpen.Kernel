import { MetaDataDto } from "./MetaDataDto";


export interface MetaNodeDto extends MetaDataDto {
    metaItems: any[];
    metaItemsSpecified: boolean;
}
