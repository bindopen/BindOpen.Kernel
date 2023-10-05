

import { ExpressionDto } from "../objects/expression/ExpressionDto";
import { ConditionDto } from "./ConditionDto";

export interface ExpressionConditionDto extends ConditionDto {
    expression: ExpressionDto;
}
