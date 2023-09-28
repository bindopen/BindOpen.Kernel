import { DictionaryDto } from "../data/objects/dictionary/DictionaryDto";


export interface PackageDefinitionDto {
    description: DictionaryDto;
    groupName: string;
    assemblyName: string;
    rootNamespace: string;
    fileName: string;
    usingAssemblyFileNames: any[];
    itemIndexFullNameDictionary: DictionaryDto;
}
