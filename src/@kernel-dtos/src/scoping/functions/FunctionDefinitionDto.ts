

import { DataValueTypes } from "../../data/_core/enums/DataValueTypes";
import { DictionaryDto } from "../../data/objects/dictionary/DictionaryDto";
import { ExtensionDefinitionDto } from "./../ExtensionDefinitionDto";

export interface FunctionDefinitionDto extends ExtensionDefinitionDto {
    callingClass: string;
    kind: any;
    maxParameterNumber: number;
    minParameterNumber: number;
    repeatedParameterName: string;
    repeatedParameterValueType: DataValueTypes;
    referenceUniqueName: string;
    returnValueType: any;
    runtimeFunctionName: string;
    children: any[];
    parameterSchemaification: any[];
    repeatedParameterDescription: DictionaryDto;
}
