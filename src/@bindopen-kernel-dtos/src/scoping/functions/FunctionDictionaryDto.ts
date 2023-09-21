import { TBdoExtensionDictionaryDto } from "../TBdoExtensionDictionaryDto";
import { FunctionDefinitionDto } from "./FunctionDefinitionDto";

export interface FunctionDictionaryDto extends TBdoExtensionDictionaryDto<FunctionDefinitionDto> {
    definitionClass: string;
}
