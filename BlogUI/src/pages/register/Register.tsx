import './register.css';
import React, { useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { UsuarioModel } from '../../models/UsuarioModel';
import { useGlobalContext } from '../../providers/GlobalProvider';
import { MessageType } from '../../components/core/message/MessageType';
import UsuarioService from '../../services/UsuarioService';



const Register: React.FC<any> = () => {
    const navigate = useNavigate();
    const { OpenMessage } = useGlobalContext();
    const [user, setUser] = useState(new UsuarioModel());
    const handleName = (event: React.ChangeEvent<HTMLInputElement>) => {
          setUser({
            ...user, 
            name: event.target.value
        });
    };

    const handleEmail = (event: React.ChangeEvent<HTMLInputElement>) => {
            setUser({
            ...user, 
            email: event.target.value
        });
    };

    const handleLogin = (event: React.ChangeEvent<HTMLInputElement>) => {
            setUser({
            ...user, 
            login: event.target.value
        });
   };

   const handleLSenha = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUser({
        ...user, 
        password: event.target.value
    });
};


    const handleSubmit = async (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
        try {
            const usuario: UsuarioModel = {
                id: 0,
                name: user.name,
                email: user.email,
                login: user.login,
                password: user.password
            } ;
          const usuarioService = new UsuarioService();
          const response = await usuarioService.Post(usuario,"api/users");
          setUser(new UsuarioModel());
          console.log(response);
          OpenMessage(MessageType.Success, "Usuáerio cadastrado com sucesso)");
        } catch (error) {

            OpenMessage(MessageType.Error, (error as any).data.message);
        }
      };

      const cancelar =  () => {
            navigate("/login");
     }
    

    return (
     <div className="login-container">
         <div className="login-content">
            <h2 className="login-title">Identifique-se no SIGA</h2>
            <form onSubmit={handleSubmit}
            >
               <div className="form-group">
                    <input 
                        value={user.name}
                        type="text" 
                        className="form-control" 
                        id="username"  
                        placeholder="USERNAME*" 
                        onChange={handleName}
                        required
                    />
                          
                </div>
                <div className="form-group">
                    <input 
                        value={user.email}
                        type="email" 
                        className="form-control" 
                        id="email"  
                        placeholder="EMAIL*" 
                        onChange={handleEmail}
                        required
                    />
                          
                </div>
                <div className="form-group">
                    <input 
                        value={user.login}
                        type="text" 
                        className="form-control" 
                        id="login"  
                        placeholder="LOGIN*" 
                        onChange={handleLogin}
                        required
                    />
                          
                </div>
                <div className="form-group">
                    <input 
                        value={user.password}
                        type="password" 
                        className="form-control" 
                        id="password"  
                        placeholder="PASSWORD*" 
                        onChange={handleLSenha}
                        required
                    />
                          
                </div>
                <button type="submit" className="login-button">Registrar</button>
                <a onClick={cancelar}  href="#register" className="forgot-password">Cancelar</a>
            </form>
         </div>
     </div>
    )
};

export default Register;
