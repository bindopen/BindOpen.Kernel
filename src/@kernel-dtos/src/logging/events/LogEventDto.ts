/* Auto Generated */

import { EventDto } from "./EventDto";
import { LogDto } from "../logs/LogDto";
import { LogEventStackTraceDto } from "../logs/LogEventStackTraceDto";

/**
 * This class represents a BindOpen log event.
 */
export interface LogEventDto extends EventDto
{
    /// <summary>
    /// The result code of this instance.
    /// </summary>
    resultCode: string;

    /// <summary>
    /// The source of this instance.
    /// </summary>
    source: string;

    /// <summary>
    /// The stack traces of this instance.
    /// </summary>
    stackTraces: LogEventStackTraceDto[];

    /// <summary>
    /// The log of this instance.
    /// </summary>
    log: LogDto;
}
