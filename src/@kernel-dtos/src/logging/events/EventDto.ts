/* Auto Generated */

import { EventKinds } from "../enums/EventKinds";
import { MetaSetDto } from "../../data/meta/MetaSetDto";
import { Criticalities } from "../../data/_core/enums/Criticalities";

export interface EventDto {
    title: string;
    description: string;
    kind: EventKinds;
    date: string;
    longDescription: string;
    detail: MetaSetDto;
    criticality: Criticalities;
}
