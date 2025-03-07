import "./settings.css";
import React from 'react';
import { jwtDecode } from "jwt-decode";
import { useNavigate } from 'react-router-dom';
import { MessageType } from "../message/MessageType";
import { useGlobalContext } from '../../../providers/GlobalProvider';
import LocalStorageService from '../../../services/LocalStorageService';

const Settings = () => {
  const { OpenMessage, GetNomeUsuario } = useGlobalContext();
  const navigate = useNavigate();

  const logout = async () => {
    try {
      LocalStorageService.clear();
      navigate("/login");
    } catch (error) {
      OpenMessage(MessageType.Error, (error as any).data.message);
    }
  };

  const obterNomeUsuario = () => { 
    const token = LocalStorageService.getToken();
    if (!token) return null; // Se não houver token, retorna null

    let name = GetNomeUsuario();
    if (!name) {
      try {
        const decoded = jwtDecode<{ name: string; unique_name: string }>(token);
        name = decoded.name;
      } catch {
        return null; // Caso haja erro na decodificação, evita falhas
      }
    }
    return name;
  };

  const nomeUsuario = obterNomeUsuario();

  return (
    <>
     {!nomeUsuario && (
       <a className="dropdown-item" href="login">Logar</a>
     )}
      {nomeUsuario && ( // Renderiza apenas se houver nome de usuário
        <div className="dropdown">
          <button
            className="btn btn-primary btn-drop dropdown-toggle custom-dropdown"
            type="button"
            id="dropdownMenuButton"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
            {nomeUsuario}
          </button>
          <ul className="dropdown-menu custom-dropdown-menu" aria-labelledby="dropdownMenuButton">
            <li><a className="dropdown-item" href="#settings">Configurações</a></li>
            <li>
              <a 
                onClick={(e) => {
                  e.preventDefault();
                  logout();
                }}
                className="dropdown-item"
                href="#logout"
              >
                Sair
              </a>
            </li>
          </ul>
        </div>
      )}
    </>
  );
};

export default Settings;
