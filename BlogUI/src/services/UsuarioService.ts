import { UsuarioModel } from "../models/UsuarioModel";
import BaseService from "./core/BaseService";

class UsuarioService extends BaseService {
    listarUsuarios = async (page: number, size: number) => {
       return await this.Get("api/users");
    };

    obterUsuario = async (id_usuario: number) => {
        return await this.Get("api/users/"+ id_usuario);
    };

    inserirUsuario = async (usuario: UsuarioModel) => {
        return await this.Post(usuario,"api/users");
    };

    editarUsuario = async (id:number, usuario: UsuarioModel) => {
        return await this.Put(id, usuario,"api/users");
    };
}

export default UsuarioService;