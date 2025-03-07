import "./usuario.css";
import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Header from "../Header/Header";
import Input from "../../components/core/input/Input";
import Title from "../../components/core/title/title";
import { UsuarioModel } from "../../models/UsuarioModel";
import Button from "../../components/core/button/Button";
import UsuarioService from "../../services/UsuarioService";
import { useGlobalContext } from "../../providers/GlobalProvider";
import { MessageType } from "../../components/core/message/MessageType";
import LocalStorageService from "../../services/LocalStorageService";

const Usuario: React.FC<any> = () => {
    const [usuario, setUsuario] = useState<UsuarioModel>(new UsuarioModel());
    const { id } = useParams<{ id?: string }>();
    const { OpenMessage } = useGlobalContext();
    const navigate = useNavigate();



    useEffect(() => {
        if (!LocalStorageService.getToken()) {
            navigate('/login');
        }
    }, [navigate]);


    const loadUser = useCallback((response: any) => {
        const user = {
            name: response.name,
            email: response.email,
            login: response.login,
            password: response.password,
            
        };
        setUsuario(user);
    }, []);

    const carregarUsuario = useCallback(async (id: number) => {
        const usuarioService = new UsuarioService();
        const response = await usuarioService.obterUsuario(id);
        console.log((response as any).data.data);
        loadUser((response as any).data.data);
    }, [loadUser]);

    useEffect(() => {
        if (id) {
            carregarUsuario(Number(id));
        }
    }, [id, carregarUsuario]);



    const handleNome = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUsuario({
            ...usuario,
            name: event.target.value
        });
    };
    

    const handleEmail = (event: React.ChangeEvent<HTMLInputElement>) => {
        setUsuario({
            ...usuario,
            email: event.target.value
        });
    };

    const cancelar = () => {
        navigate("/usuarios");
    };


    const salvar = async () => {
        const usuarioService = new UsuarioService();
        try {
         
            if (id !== undefined) {
                await usuarioService.editarUsuario(Number(id), usuario);
            } else {
                await usuarioService.inserirUsuario(usuario);
            }
            OpenMessage(MessageType.Success, "Registro inserido com sucesso");
            localStorage.removeItem("perfisIds");
            localStorage.removeItem("unidadesIds");
            navigate("/usuarios");
        } catch (error) {
            OpenMessage(MessageType.Error, (error as any).data.message);
        }
    };

   

   return (
        <>
             <Header />
             <div  className="container container-user">
                <div className="padding-container">
                    <Title value="Cadastrar usuário" />
                        <div className="row">
                        
                               <div className="col-md-12">
                                <Input
                                    label="Nome completo"
                                    type="text"
                                    value={usuario.name}
                                    placeholder="Nome Sobrenome"
                                    required={true}
                                    disabled={false}
                                    maxlength={150}
                                    onChange={handleNome}
                                    

                                />
                            </div>
     
                        <div className="row">
                            <div className="col-md-12">
                                <Input
                                    label="Email"
                                    type="text"
                                    value={usuario.email}
                                    placeholder="teste@email.com"
                                    required={true}
                                    disabled={false}
                                    onChange={handleEmail}
                                    maxlength={15}

                                />
                            </div>
                           </div>
                </div>
              
                <hr />
                <div className="mb-3 pt-3 d-flex justify-content-between">
                        <div className="justify-content-end">
                            <Button 
                                text="Voltar" 
                                disabled={false} 
                                classe="btn-outline-primary button-user button-user-link"
                                onClick={cancelar} 
                            />
                        </div>
                        <div className='justify-content-end'>
                            <Button 
                                text="Cancelar" 
                                disabled={false} 
                                classe="btn-outline-primary button-user button-user-link"
                                onClick={cancelar} 
                            />
                            <Button 
                                text="Salvar" 
                                disabled={false}
                                classe="btn-primary button-user"  
                                onClick={salvar} 
                            />
                        </div>
                    </div>
                </div>
            </div>
          
        </>
    );
}
export default Usuario;
