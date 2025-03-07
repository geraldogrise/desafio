import { createContext, ReactNode } from "react";
import { Tab } from "../components/models/Tab";
import { MessageType } from "../components/core/message/MessageType";



interface GlobalContextType {
    OpenAlert: (type: MessageType, message: string, redirect: string) => void;
    OpenConfirm: (title: string, message: string, okText: string, cancelText: string) => void;
    OpenMessage: (type: MessageType, message: string, redirect?: string) => void;
    OpenModal: (id: string, classe: string, children: ReactNode, data?: any) => void;
    OpenMsg: (type: MessageType, title: string, message: string, redirect: string) => void;
    OpenToast: (title: string, type: MessageType, message: string, redirect: string, icon: string) => void;
    SetTab: (tab: Tab) => void;
    GetTab: () => Tab;
  
    GetNomeUsuario: () => string;
    SetNomeUsuario: (nome: string) => void;
    SetIdUsuario: (id: number) => void;
    GetIdUsuario: () => number;
}

const context = createContext<GlobalContextType>({
    OpenAlert: (type: MessageType, message: string, redirect: string = "") => {},
    OpenConfirm: (title: string, message: string, okText: string ="", cancelText: string = "") => {},
    OpenMessage: (type: MessageType, message: string, redirect: string = "") => {},
    OpenModal: (id: string, classe: string, childrens: ReactNode) => {},
    OpenMsg: (type: MessageType, title: string, message: string, redirect: string = "") => {},
    OpenToast: (title: string, type: MessageType, message: string, redirect: string, icon: string) => {},
    SetTab: () => {},
    GetTab: () => new Tab("", "", false), 
    SetNomeUsuario: (nome: string) => {},
    GetNomeUsuario: () => "",
    SetIdUsuario: (id: number) => {},
    GetIdUsuario: () => 0
});

export default context;