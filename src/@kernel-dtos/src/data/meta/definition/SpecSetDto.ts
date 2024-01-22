import { SpecDto } from "./SpecDto";


export interface SpecSetDto {
    id: string;
    name: string;
    items: SpecDto[];
}
