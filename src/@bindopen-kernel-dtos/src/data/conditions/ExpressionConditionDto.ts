

import { ExpressionDto } from "../objects/Expression/ExpressionDto";
import { ConditionDto } from "./ConditionDto";

export interface ExpressionConditionDto extends ConditionDto {
    expression: ExpressionDto;
}
