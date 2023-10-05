/* Auto Generated */

import { MetaSetDto } from "./../metaSetDto";
import { StringDictionaryDto } from "./../../Objects/Dictionary/stringDictionaryDto";

export interface ConfigurationDto extends MetaSetDto {
    creationDate: string;
    lastModificationDate: string;
    description: StringDictionaryDto;
    title: StringDictionaryDto;
    children: any[];
    childrenSpecficied: boolean;
    usedItemIds: any[];
    usedItemIdsSpecified: boolean;
    shouldUsedItemIds: boolean;
}
