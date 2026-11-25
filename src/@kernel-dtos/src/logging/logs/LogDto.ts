/* Auto Generated */

import { ProcessExecutionDto } from "../execution/ProcessExecutionDto";
import { ConfigurationDto } from "../../data/meta/configuration/ConfigurationDto";
import { MetaSetDto } from "../../data/meta/MetaSetDto";
import { LogEventDto } from "../events/LogEventDto";

export interface LogDto {
    title: string;
    description: string;
    execution: ProcessExecutionDto;
    task: ConfigurationDto;
    detail: MetaSetDto;
    events: LogEventDto[];
}
