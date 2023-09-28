

import { MetaObjectDto } from "../../data/meta/MetaObjectDto";

export interface ScriptwordDto extends MetaObjectDto {
    kind: any;
    child: ScriptwordDto;
}
