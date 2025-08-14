

import { ExtensionDefinitionDto } from "./../ExtensionDefinitionDto";

export interface TaskDefinitionDto extends ExtensionDefinitionDto {
    groupName: string;
    isExecutable: boolean;
    itemClass: string;
    maximumIndex: number;
    inputSchemaification: any[];
    outputSchemaification: any[];
}
