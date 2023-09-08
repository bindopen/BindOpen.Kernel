/* Auto Generated */

import { ExtensionDefinitionDto } from "./../_Core/extensionDefinitionDto";
import { DictionaryDto } from "./../../Data/Objects/Dictionary/dictionaryDto";

export interface FunctionDefinitionDto extends ExtensionDefinitionDto {
    callingClass: string;
    kind: any;
    maxParameterNumber: number;
    minParameterNumber: number;
    repeatedParameterName: string;
    repeatedParameterValueType: any;
    referenceUniqueName: string;
    returnValueType: any;
    runtimeFunctionName: string;
    children: any[];
    parameterSpecification: any[];
    parameterSpecificationSpecified: boolean;
    repeatedParameterDescription: DictionaryDto;
}
