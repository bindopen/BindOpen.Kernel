/* Auto Generated */

import { SpecSetDto } from "./specSetDto";
import { StringDictionaryDto } from "./../../Objects/Dictionary/stringDictionaryDto";

export interface DefinitionDto extends SpecSetDto {
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
