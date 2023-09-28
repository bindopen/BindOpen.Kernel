

import { BdoItemDto } from "../objects/BdoItemDto";
import { MetaNodeDto } from "./MetaNodeDto";

export interface MetaObjectDto extends MetaNodeDto {
    item: BdoItemDto;
}
