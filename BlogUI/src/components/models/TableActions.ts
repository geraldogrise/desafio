import { FuncionalidadeEnum } from "../../models/enums/FuncionalidadeEnum";
import { Tab } from "./Tab";

export class TableActions {
    icon: string;
    name: string;
    url: string;
    tab?: Tab | null
    tooltip: string;
    funcionalidade: FuncionalidadeEnum;

    constructor(icon: string, name: string, url: string, tab: Tab | null, tooltip: string, funcionalidade: FuncionalidadeEnum  = FuncionalidadeEnum.CONSULTAR_USUARIO) {
        this.icon = icon;
        this.name = name;
        this.url = url;
        this.tab = tab;
        this.tooltip = tooltip;
        this.funcionalidade = funcionalidade;
    }
}