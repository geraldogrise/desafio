import { useEffect } from "react";
import Header from "../Header/Header";
import LocalStorageService from "../../services/LocalStorageService";
import { useNavigate } from "react-router-dom";


const Home: React.FC<any> = () => {
    const navigate = useNavigate();
    
    useEffect(() => {
        if(!LocalStorageService.getToken()) {
            navigate('/login')
        }    
      }, [navigate]);
      
    return (
        <div>
             <Header></Header>
        </div>

    )
};
export default Home;