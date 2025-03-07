import BaseService from "./core/BaseService";


class GenericService extends BaseService {
    ativar = async (id: number, controller: string) => {
        
       return await this.PatchModel({},"v1/"+controller+"/"+id+"/ativar" );
    };

    inativar = async (id: number, controller: string) => {
        
        return await this.PatchModel({},"v1/"+controller+"/"+id+"/inativar" );
     };
 }


export default GenericService;