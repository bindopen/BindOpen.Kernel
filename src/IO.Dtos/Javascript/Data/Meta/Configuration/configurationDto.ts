/* Auto Generated */

import { MetaSetDto } from "./../metaSetDto";
import { DictionaryDto } from "./../../Objects/Dictionary/dictionaryDto";

export interface ConfigurationDto extends MetaSetDto {
    creationDate: string;
    lastModificationDate: string;
    description: DictionaryDto;
    title: DictionaryDto;
    children: any[];
    childrenSpecficied: boolean;
    usedItemIds: any[];
    usedItemIdsSpecified: boolean;
    shouldUsedItemIds: boolean;
}
