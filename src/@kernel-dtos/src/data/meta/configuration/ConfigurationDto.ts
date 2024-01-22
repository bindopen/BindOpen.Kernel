

import { MetaSetDto } from "../MetaSetDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";

export interface ConfigurationDto extends MetaSetDto {
    parentId: string;
    creationDate: string;
    lastModificationDate: string;
    description: DictionaryDto;
    title: DictionaryDto;
    children: any[];
    usedItemIds: any[];
    shouldUsedItemIds: boolean;
}
