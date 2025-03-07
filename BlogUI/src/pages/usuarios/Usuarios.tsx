import "./usuarios.css";
import Header from "../Header/Header";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Tab } from "../../components/models/Tab";
import { Combo } from "../../components/models/Combo";
import Title from "../../components/core/title/title";
import Table from "../../components/core/table/Table";
import { UsuarioModel } from "../../models/UsuarioModel";
import Button from "../../components/core/button/Button";
import Search from "../../components/core/search/Search";
import UsuarioService from "../../services/UsuarioService";
import { TableData } from "../../components/models/TableData";
import Dropdown from "../../components/core/dropdown/Dropdown";
import { TableColumns } from "../../components/models/TableColumns";
import { TableActions } from "../../components/models/TableActions";
import LocalStorageService from "../../services/LocalStorageService";
import { MessageType } from "../../components/core/message/MessageType";
import { useGlobalContext } from "../../providers/GlobalProvider";
import { FuncionalidadeEnum } from "../../models/enums/FuncionalidadeEnum";


const Usuarios: React.FC<any> = () => {
  const navigate = useNavigate();
  const size = 10;
  const { OpenMessage } = useGlobalContext();
  const [page, setPage] = useState<number>(0);
  const [totalPages, setTotalPages] = useState<number>(0);
  const [totalResults, setTotalResults] = useState<number>(0);
  const [search, setSearch] = useState<string | number>("");
  const [usuarios, setUsuarios] = useState<UsuarioModel[]>([]);

  const tabs: Tab[] = [
    new Tab("unidades","Unidades",true),
    new Tab("perfis","Perfis",true),
    new Tab("usuario","Dados básicos",true),
 
 ];

  useEffect(() => {
    if (!LocalStorageService.getToken()) {
      navigate("/login");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const handleSearchChange = (value: string | number) => {
      setSearch(value);

  };

  const options: Combo[] = [new Combo("nome", "Nome")];

  const [selectedItem, setSelectedItem] = useState<Combo>(options[0]);

  const handleSelect = (item: Combo) => {
    setSelectedItem(item);
  };

  const columns: TableColumns[] = [
    new TableColumns("name", "Nome", "left"),
    new TableColumns("email", "Email", "left"),
    new TableColumns("login", "username", "left"),
  ];

  const actions: TableActions[] = [
    new TableActions("edit", "Editar", "usuario", tabs[2], "Editar usuário"),
    new TableActions("delete", "Remover", "usuario", tabs[2], "Remover usuário"),
   ];

  const carregarUsuarios = async () => {
    try {
         let response: any = [];
         const usuarioService = new UsuarioService();
          response = await usuarioService.listarUsuarios(page, size);
          const users = (response as any).data.data as UsuarioModel[];
          let pages = Math.round(users.length/10);
          let totalPages = pages === 0 ? 1 : page;
          setUsuarios(users);
          setTotalPages(totalPages);
          setTotalResults(users.length);
       } catch (error) {
          console.log(error);
          OpenMessage(MessageType.Error, "Houve um erro ao carregar usuários");
       } finally {
       }
  };

 

  useEffect(() => {
    carregarUsuarios();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page]);
 

  useEffect(() => {

  }, [totalPages, totalResults]);

  const data: TableData = new TableData(
    1,
    1,
    usuarios,
    actions,
    "id",
    "usuario",
    FuncionalidadeEnum.EXCLUIR_USUARIO,

  );

  const buscar = () => {
    carregarUsuarios();
  };

  const adicionar = () => {
    navigate("/usuario");
  };

  const voltar = () => {
    navigate("../home");
  };

  const onChangePage = (page: number) =>
  {
      setPage(page - 1);
  }

  const reload = async() =>
  {}
 

  return (
    <>
      <Header />
      <div className="container container-user">
        <div className="padding-container">
          <Title value="Lista de usuários" />
          <div className="row pt-3">
            <div className="filter">
              <Search text={search} onChange={handleSearchChange} />
              <div className="filter-dropdown">
                <Dropdown
                  selected={selectedItem}
                  onSelect={handleSelect}
                  data={options}
                ></Dropdown>
              </div>
              <div className="d-flex justify-content-between ps-4 align-items-center h-100">
                <Button
                  text="Buscar"
                  disabled={false}
                  classe="btn-primary button-container"
                  onClick={buscar}
                  
                />
                <Button
                  text="Novo"
                  disabled={false}
                  classe="btn-outline-primary button-container-outline"
                  onClick={adicionar}
                />
              </div>
            </div>
          </div>

          <Table 
             columns={columns} 
             data={data} 
             totalResults={totalResults}  
             pages={totalPages}
             onPage ={onChangePage}
             onReload={reload}
            />

          <hr />
          <div className="d-flex justify-content-end mb-3">
            <Button
              text="Fechar"
              disabled={false}
              classe="btn-outline-secondary btn-standard-size "
              onClick={voltar}
            />
          </div>
        </div>
      </div>
    </>
  );
};
export default Usuarios;
