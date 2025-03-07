import "./post.css";
import { useCallback, useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Header from "../Header/Header";
import Input from "../../components/core/input/Input";
import Title from "../../components/core/title/title";
import { PostModel } from "../../models/PostModel";
import Button from "../../components/core/button/Button";
import PostService from "../../services/PostService";
import { useGlobalContext } from "../../providers/GlobalProvider";
import { MessageType } from "../../components/core/message/MessageType";
import LocalStorageService from "../../services/LocalStorageService";
import { jwtDecode } from "jwt-decode";

const Post: React.FC<any> = () => {
    const [post, setPost] = useState<PostModel>(new PostModel());
    const { id } = useParams<{ id?: string }>();
    const { OpenMessage } = useGlobalContext();
    const navigate = useNavigate();



    useEffect(() => {
        if (!LocalStorageService.getToken()) {
            navigate('/login');
        }
    }, [navigate]);


    const loadPost = useCallback((response: any) => {
        const postagem = {
            id_user: response.id_user,
            name: response.name,
            description: response.description,
        };
        setPost(postagem);
    }, []);

    const carregarPost = useCallback(async (id: number) => {
        const postService = new PostService();
        const response = await postService.obterPost(id);
        console.log((response as any).data.data);
        loadPost((response as any).data.data);
    }, [loadPost]);

    const carregarUsuario = useCallback(() => {
        const token = LocalStorageService.getToken();
        if (token) {
            const decoded = jwtDecode<{ name: string; unique_name: string }>(token);
            setPost(prevPost => ({
                ...prevPost,
                id_user: Number(decoded.unique_name)
            }));
        }
    }, []);

    useEffect(() => {
        if (id) {
            carregarPost(Number(id));
        }
        carregarUsuario();
    }, [id, carregarPost, carregarUsuario]);


    const handleNome = (event: React.ChangeEvent<HTMLInputElement>) => {
        setPost({
            ...post,
            name: event.target.value
        });
    };
    

    const handleDescription = (event: React.ChangeEvent<HTMLTextAreaElement>) => {
        setPost({
            ...post,
            description: event.target.value
        });
    };

    const cancelar = () => {
        navigate("/posts");
    };


    const salvar = async () => {
        const postService = new PostService();
        try {
            
            if (id !== undefined) {
                await postService.editarPost(Number(id), post);
            } else {
                await postService.inserirPost(post);
            }
            OpenMessage(MessageType.Success, "Registro inserido com sucesso");
            navigate("/posts");
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
                                    label="Titutlo"
                                    type="text"
                                    value={post.name}
                                    placeholder="Titutlo"
                                    required={true}
                                    disabled={false}
                                    maxlength={50}
                                    onChange={handleNome}
                                    

                                />
                            </div>
                            <br/>        <br/>
                        <div className="row">
                    
                            <div className="col-md-12 mb-1 mt-1">
                              <label className="form-label">Texto</label>
                            </div>
                            <div className="col-md-12">
                                <textarea
                                    rows={5}
                                    maxLength={255}
                                    className="form-control"
                                    value={post.description} 
                                    onChange={handleDescription}
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
export default Post;
