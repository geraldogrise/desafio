import './login.css';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { LoginModel } from '../../models/LoginModel';
import LoginService from '../../services/LoginService';
import { useGlobalContext } from '../../providers/GlobalProvider';
import { MessageType } from '../../components/core/message/MessageType';
import LocalStorageService from '../../services/LocalStorageService';
import { jwtDecode } from "jwt-decode";


const Login: React.FC<any> = () => {
    const { OpenMessage, SetIdUsuario, SetNomeUsuario } = useGlobalContext();
    const navigate = useNavigate();
    const [login, setLogin] = useState(new LoginModel());
    const handleUsername = (event: React.ChangeEvent<HTMLInputElement>) => {
          setLogin({
            ...login, 
            username: event.target.value
        });
    };

    const handleSenha = (event: React.ChangeEvent<HTMLInputElement>) => {
        setLogin({
            ...login, 
            password: event.target.value 
        });;
    };

    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        try {
            const loginAdapter: LoginModel = {
                username: login.username,
                password: login.password
            } ;
          const loginSrrvice = new LoginService();
          const response = await loginSrrvice.logar(loginAdapter);
          const token =  (response as any).data.token;
          LocalStorageService.setToken(token);
          const decoded = jwtDecode<{ name: string; unique_name: string}>(token);
          const id = Number(decoded.unique_name);
          const nome = decoded.name
          SetIdUsuario(id);
          SetNomeUsuario(nome);
          navigate("/home");
        } catch (error) {
            setLogin({
                ...login, 
                password: "" 
            });;
            OpenMessage(MessageType.Error, (error as any).data.message);
        }
      };
     
      const registrar =  () => {
          navigate("/register");
      }
      


    return (
     <div className="login-container">
         <div className="login-content">
            <h2 className="login-title">Identifique-se no SIGA</h2>
            <form onSubmit={handleSubmit}
            >
               <div className="form-group">
                    <input 
                        value={login.username}
                        type="text" 
                        className="form-control" 
                        id="username"  
                        placeholder="USERNAME*" 
                        onChange={handleUsername}
                        required
                    />
                          
                </div>
                <div className="form-group">
                    <input type="password" 
                           className="form-control" 
                           id="senha"  
                           placeholder="Senha *" 
                           onChange={handleSenha}
                           required/>
                </div>
                <button type="submit" className="login-button">Fazer Login</button>
                <a onClick={registrar}  href="login" className="forgot-password">Registrar-se</a>
            </form>
         </div>
     </div>
    )
};

export default Login;
