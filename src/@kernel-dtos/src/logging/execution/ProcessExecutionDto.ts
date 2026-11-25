/* Auto Generated */

import { ProcessExecutionState } from "../enums/ProcessExecutionState";
import { ProcessExecutionStatus } from "../enums/ProcessExecutionStatus";

export interface ProcessExecutionDto {
    customStatus: string;
    endDate: string;
    location: string;
    progressIndex: number;
    progressMax: number;
    restartDate: string;
    resultLevel: number;
    startDate: string;
    state: ProcessExecutionState;
    status: ProcessExecutionStatus;
}
