import { MetaDataDto } from "./MetaDataDto";


export interface MetaNodeDto extends MetaDataDto {
    items: MetaDataDto[];
}
