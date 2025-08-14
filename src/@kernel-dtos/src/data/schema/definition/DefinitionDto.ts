

import { SchemaSetDto } from "../SchemaSetDto";
import { DictionaryDto } from "../../objects/dictionary/DictionaryDto";

export interface DefinitionDto extends SchemaSetDto {
    parentId: string;
    creationDate: string;
    lastModificationDate: string;
    description: DictionaryDto;
    title: DictionaryDto;
    children: any[];
    usedItemIds: any[];
    shouldUsedItemIds: boolean;
}
