import { ScriptwordDto } from "../../../scoping/script/ScriptwordDto";
import { BdoExpressionKind } from "../../enums/BdoExpressionKind";
import { BdoItemDto } from "../BdoItemDto";

export interface ExpressionDto extends BdoItemDto {
    text: string;
    word: ScriptwordDto;
    kind: BdoExpressionKind;
}
