/* Auto Generated */

import { ExtensionDefinitionDto } from "./../_Core/extensionDefinitionDto";
import { StringDictionaryDto } from "./../../Data/Objects/Dictionary/stringDictionaryDto";

export interface FunctionDefinitionDto extends ExtensionDefinitionDto {
    callingClass: string;
    kind: any;
    maxParameterNumber: number;
    minParameterNumber: number;
    repeatedParameterName: string;
    repeatedParameterValueType: any;
    repeatedParameterDescription: StringDictionaryDto;
    referenceUniqueName: string;
    returnValueType: any;
    runtimeFunctionName: string;
    children: any[];
    parameterSpecification: any[];
    parameterSpecificationSpecified: boolean;
}
