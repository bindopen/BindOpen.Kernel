import { MetaDataDto } from "./MetaDataDto";

export interface MetaScalarDto extends MetaDataDto {
    item: string;
    items: any[];
    itemsSpecified: boolean;
}
