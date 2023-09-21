

import { ExtensionDefinitionDto } from "../ExtensionDefinitionDto";

export interface ConnectorDefinitionDto extends ExtensionDefinitionDto {
    datasourceKind: any;
    itemClass: string;
    specs: any[];
    specsSpecified: boolean;
}
