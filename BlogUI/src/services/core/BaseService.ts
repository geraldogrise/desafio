import Api from './Api';
import { Entity } from "../../models/Entity";
import LocalStorageService from '../../services/LocalStorageService';
export default class BaseService {

    config = {
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        }
    };

    configData = {
        headers: {
          "Content-Type": "multipart/form-data",
        }
    };

   GetConfig() {
      const token = LocalStorageService.getToken()?.toString();
 
      return {
          headers: {
              ...this.config.headers,
              ...(token ? { "Authorization": `Bearer ${token}` } : {})
          }
      };
   }

    Post = async (model: Entity, action: string) => {
        
        const config = this.GetConfig(); 
        return new Promise((resolve, reject) => {
          Api.post(action, JSON.stringify(model), config)
            .then(res => {
              resolve(res);
            })
            .catch(error => {
               reject(error);
            });
        });
    };

    Put = async (id: number, model: Entity, action: string) => {
      const config = this.GetConfig(); 
      let url = action+"/"+id;
      if(id === 0)
      {
        url = action;
      }
      return new Promise((resolve, reject) => {
        Api.put(url, JSON.stringify(model), config)
          .then(res => {
            resolve(res);
          })
          .catch(error => {
            reject(error);
          });
      });
  };

  Patch = async (id: number, model: Entity, action: string) => {
      const config = this.GetConfig(); 
      return new Promise((resolve, reject) => {
        Api.patch(action+"/"+id, JSON.stringify(model), config)
          .then(res => {
            resolve(res);
          })
          .catch(error => {
            reject(error);
          });
      });
  };

  PatchModel = async (model: Entity, action: string) => {
    const config = this.GetConfig(); 
    return new Promise((resolve, reject) => {
      Api.patch(action, JSON.stringify(model), config)
        .then(res => {
          resolve(res);
        })
        .catch(error => {
          reject(error);
        });
    });
};

  Delete = async (id: number, action: string) => {
      const config = this.GetConfig(); 
      return new Promise((resolve, reject) => {
        Api.delete(action+"/"+id, config)
          .then(res => {
            resolve(res);
          })
          .catch(error => {
            reject(error);
          });
      });
      
  };

  Get = async (action: string) => {
      const config = this.GetConfig(); 
      return new Promise((resolve, reject) => {
        Api.get(action, config)
          .then(res => {
            resolve(res);
          })
          .catch(error => {
            reject(error);
          });
      });
  };

  GetById = async (id: number, action: string) => {
      const config = this.GetConfig(); 
      return new Promise((resolve, reject) => {
        Api.get(action+"/"+id, config)
          .then(res => {
            resolve(res);
          })
          .catch(error => {
            reject(error);
          });
      });
  };

}