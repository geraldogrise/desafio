import { jwtDecode } from "jwt-decode";
class LocalStorageService {
  
    setToken = (token: string) => {
        sessionStorage.setItem("token", token);
    };

    getToken = () => {
        return sessionStorage.getItem("token")?.toString();
    };

    getUsuarioLogado = () => {
        const token  =  sessionStorage.getItem("token")?.toString();
        let id = 0;
        if(token)
        {
            const decoded = jwtDecode<{ name: string; unique_name: string}>(token);
            id = Number(decoded.unique_name);
        }
        return id;
    };

    removeToken = () => {
        sessionStorage.removeItem("token");
    };  

    clear = () => {
        sessionStorage.clear();
    };  
}

const storageSrrvice  = new LocalStorageService();
export default storageSrrvice;