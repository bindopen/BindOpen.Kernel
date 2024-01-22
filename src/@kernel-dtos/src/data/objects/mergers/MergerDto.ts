import { BdoItemDto } from "../BdoItemDto";


export interface MergerDto extends BdoItemDto {
    addedValues: any[];
    removedValues: any[];
}
