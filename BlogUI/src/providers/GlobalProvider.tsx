import React, { useRef, ReactNode, useContext } from 'react';
import { Tab } from '../components/models/Tab';
import GlobalContext from './GlobalContextType';
import Modal from '../components/core/message/modal/Modal';
import Alert from '../components/core/message/alert/Alert';
import Toast from '../components/core/message/toast/Toast';
import Message from '../components/core/message/message/Message';
import Confirm from '../components/core/message/confirm/confirm';
import { MessageType } from '../components/core/message/MessageType';


interface PropTypes {
    children?: ReactNode;
}

const GlobalContainer: React.FC<PropTypes> = ({ children }) => {
    const refAlert = useRef(null);
    const refConfirm = useRef(null);
    const refMenssage = useRef(null);
    const refToast = useRef(null);
    const refModal = useRef(null);
    const [id, setId] = React.useState("");
    const [classe, setClasse] = React.useState("");
    const [tab, setTab] = React.useState(new Tab("","", false));
    const [idUsuario, setIdUsuario] = React.useState(0);
    const [nomeUsuario, setNomeUsuario] = React.useState("");



    function OpenAlert(type: MessageType, message: string, redirect: string) {
        if (refAlert.current) {
            const refer = refAlert.current as any;
            refer.OpenAlert(message, type, redirect);
        }
    }

    function OpenConfirm(title: string, message: string, okText: string, cancelText: string) {
        if (refConfirm.current) {
            const refer = refConfirm.current as any;
            refer.OpenConfirm(title, message, okText, cancelText);
        }
    }

    function OpenMessage(type: MessageType, message: string, redirect?: string) {
        if (refMenssage.current) {
            const refer = refMenssage.current as any;
            refer.OpenMessage("Atenção", message, type, redirect);
        }
    }

    function OpenModal(id: string, classe: string, childrens: ReactNode) {
        if(refModal.current)
        {
            const refer = refModal.current as any;
            setId(id);
            setClasse(classe);
            const element = document.querySelector('.'+classe);
            if (element) {
                element.classList.remove('d-none');
            }
            refer.OpenModal(id, classe, childrens)
        }
        
    }

    function OpenToast(title: string, type: MessageType, message: string, redirect: string, icon: string) {
        if (refToast.current) {
            const refer = refToast.current as any;
            refer.OpenToast(title, message, type, redirect, icon);
        }
    }

    function OpenMsg(type: MessageType, title: string, message: string, redirect: string) {
        if (refMenssage.current) {
            const refer = refMenssage.current as any;
            refer.OpenMessage(title, message, type, redirect);
        }
    }

    function SetTab(tab: Tab)
    {
        setTab(tab);
    }

    function GetTab()
    {
       return tab;
    }

    function GetIdUsuario()
    {
        return idUsuario;
    }

    function SetIdUsuario(id: number)
    {
        setIdUsuario(id);
    }

    function GetNomeUsuario()
    {
        return nomeUsuario;
    }

    function SetNomeUsuario(nome: string)
    {
        setNomeUsuario(nome);
    }

    return (
        <GlobalContext.Provider
            value={{
                OpenAlert,
                OpenConfirm,
                OpenMessage,
                OpenMsg,
                OpenModal,
                OpenToast,
                SetTab,
                GetTab,
                GetIdUsuario,
                SetIdUsuario,
                GetNomeUsuario,
                SetNomeUsuario

            }}
        >
            {children}
            <Alert ref={refAlert} />
            <Confirm ref={refConfirm} />
            <Message ref={refMenssage} />
            <Modal id={id} classe={classe} ref={refModal} />
            <Toast ref={refToast} />
        </GlobalContext.Provider>
    );
};

export const useGlobalContext = () => useContext(GlobalContext);

export default GlobalContainer;
