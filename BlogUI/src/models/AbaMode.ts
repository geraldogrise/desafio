import { Entity } from "./Entity";

export class SecaoModel extends Entity
{
    id_secao: number;
    nome: string;
    status: boolean;
 

    constructor(id_secao: number, nome: string, status: boolean) {
        super();
        this.id_secao = id_secao;
        this.nome = nome;
        this.status = status;
    }

}