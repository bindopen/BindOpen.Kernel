

import { SpecSetDto } from "./SpecSetDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";

export interface DefinitionDto extends SpecSetDto {
    parentId: string;
    creationDate: string;
    lastModificationDate: string;
    description: DictionaryDto;
    title: DictionaryDto;
    children: any[];
    usedItemIds: any[];
    shouldUsedItemIds: boolean;
}
