import { PostModel } from "../models/PostModel";
import BaseService from "./core/BaseService";
import LocalStorageService from './/LocalStorageService';

class  PostService extends BaseService {
    listarPosts = async (page: number, size: number) => {
       return await this.Get("api/posts");
    };

    listarPostsPorUsuario = async (page: number, size: number) => {
        const id = LocalStorageService.getUsuarioLogado();
        return await this.Get("api/users/"+id+"/posts");
     };

    obterPost = async (id_post: number) => {
        return await this.Get("api/posts/"+ id_post);
    };

    inserirPost = async (post: PostModel) => {
        return await this.Post(post,"api/posts");
    };

    editarPost = async (id:number, post: PostModel) => {
        return await this.Put(id, post,"api/posts");
    };

    DeletarPost = async (id:number) => {
        return await this.Delete(id,"api/posts");
    };

    listarPostagens = async () => {
        return await this.Get("api/posts/all");
    };
}

export default PostService;