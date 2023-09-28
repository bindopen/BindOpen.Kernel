

import { ExpressionDto } from "../objects/Expression/expressionDto";
import { ConditionDto } from "./ConditionDto";

export interface ExpressionConditionDto extends ConditionDto {
    expression: ExpressionDto;
}
