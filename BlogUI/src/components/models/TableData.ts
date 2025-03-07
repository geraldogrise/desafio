import { Entity } from "../../models/Entity";
import { FuncionalidadeEnum } from "../../models/enums/FuncionalidadeEnum";
import { TableActions } from "./TableActions";

export class TableData
{
    pages: number | null = 0;
    id_column = "";
    currentPage = 1;
    data = Array<Entity>();
    actions = Array<TableActions>();
    controller: string = "";
    status_funcionalidade: FuncionalidadeEnum = FuncionalidadeEnum.CONSULTAR_USUARIO;
    
    constructor(pages: number | null, currentPage: number, data : Array<Entity>, actions: Array<TableActions>, id: string, controller: string,status_funcionalidade: FuncionalidadeEnum = FuncionalidadeEnum.EXCLUIR_USUARIO ){
        this.pages = pages;
        this.currentPage = currentPage;
        this.data = data;
        this.actions = actions;
        this.id_column = id;
        this.controller = controller;
        this.status_funcionalidade = status_funcionalidade;
    }
}