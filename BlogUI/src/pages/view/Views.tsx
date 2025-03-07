import { useCallback, useEffect, useState } from "react";
import Header from "../Header/Header";
import { useGlobalContext } from "../../providers/GlobalProvider";
import PostService from "../../services/PostService";
import { PostUserModel } from "../../models/PostUserModel";
import { MessageType } from "../../components/core/message/MessageType";
import Card from "../../components/core/card/Card.";


const Views: React.FC<any> = () => {
     const { OpenMessage } = useGlobalContext();
     const [tasks, setTasks] = useState<PostUserModel[]>(new Array<PostUserModel>());
     const carregarTarefas = useCallback(async () => {
        try {
            const taskService = new PostService();
            const response = await taskService.listarPostagens();
            const tarefas = (response as any).data.data as PostUserModel[];
            setTasks(tarefas);

        } catch (error) {
            OpenMessage(MessageType.Error, "Erro ao carregar a tarefas");
        }
    }, [OpenMessage]);

    useEffect(() => {
        const fetchData = async () => {
            await carregarTarefas();
        };
    
        fetchData();
    }, [carregarTarefas]);
      
    return (
        <div>
             <Header></Header>
             <div className="row">
                 {tasks.map((card, index) => (
                    <div key={index} className="col-sm-3">
                         <Card title={card.name} 
                               description={card.description} 
                               name={card.user}
                               email={card.email}
                          />
                    </div>
                 ))}
             </div>
             
        </div>

    )
};
export default Views;