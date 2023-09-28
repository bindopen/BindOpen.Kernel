

import { BdoItemDto } from "../BdoItemDto";

export interface DictionaryDto extends BdoItemDto {
    id: string;
    values: any[];
}
