﻿

import { SpecSetDto } from "./SpecSetDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";

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
