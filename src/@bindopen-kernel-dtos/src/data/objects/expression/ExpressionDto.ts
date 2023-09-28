import { ScriptwordDto } from "../../../scoping/script/ScriptwordDto";
import { BdoItemDto } from "../BdoItemDto";

export interface ExpressionDto extends BdoItemDto {
    text: string;
    word: ScriptwordDto;
    kind: any;
}
