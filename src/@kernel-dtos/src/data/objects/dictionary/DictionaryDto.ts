import { BdoItemDto } from "../BdoItemDto";
import { KeyValuePairDto } from "./KeyValuePairDto";

export interface DictionaryDto extends BdoItemDto {
    id: string;
    values: KeyValuePairDto[];
}
