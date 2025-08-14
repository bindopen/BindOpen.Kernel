import { SchemaDto } from "./SchemaDto";


export interface SchemaSetDto {
    id: string;
    name: string;
    items: SchemaDto[];
}
