/* Auto Generated */

import { SpecSetDto } from "./specSetDto";
import { DictionaryDto } from "./../../Objects/Dictionary/dictionaryDto";

export interface DefinitionDto extends SpecSetDto {
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
