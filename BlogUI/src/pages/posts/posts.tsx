import "./posts.css";
import Header from "../Header/Header";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Combo } from "../../components/models/Combo";
import Title from "../../components/core/title/title";
import Table from "../../components/core/table/Table";
import { PostModel } from "../../models/PostModel";
import Button from "../../components/core/button/Button";
import Search from "../../components/core/search/Search";
import PostService from "../../services/PostService";
import { TableData } from "../../components/models/TableData";
import Dropdown from "../../components/core/dropdown/Dropdown";
import { TableColumns } from "../../components/models/TableColumns";
import { TableActions } from "../../components/models/TableActions";
import LocalStorageService from "../../services/LocalStorageService";
import { MessageType } from "../../components/core/message/MessageType";
import { useGlobalContext } from "../../providers/GlobalProvider";

const Posts: React.FC<any> = () => {
  const navigate = useNavigate();
  const size = 10;
  const { OpenMessage } = useGlobalContext();
  const [page, setPage] = useState<number>(0);
  const [totalPages, setTotalPages] = useState<number>(0);
  const [totalResults, setTotalResults] = useState<number>(0);
  const [search, setSearch] = useState<string | number>("");
  const [Posts, setPosts] = useState<PostModel[]>([]);


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
  ];

  const actions: TableActions[] = [
    new TableActions("edit", "Editar", "post", null, "Editar post"),
    new TableActions("delete", "Remover", "post", null, "Remover post"),
   ];

  const carregarPosts = async () => {
    try {
         let response: any = [];
         const postService = new PostService();
           response = await postService.listarPostsPorUsuario(page, size);
          const posts = (response as any).data.data as PostModel[];
          let pages = Math.round(posts.length/10);
          let totalPages = pages === 0 ? 1 : page;
          setPosts(posts);
          setTotalPages(totalPages);
          setTotalResults(posts.length);
       } catch (error) {
          console.log(error);
          OpenMessage(MessageType.Error, "Houve um erro ao carregar usuários");
       } finally {
       }
  };

 

  useEffect(() => {
    carregarPosts();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [page]);
 

  useEffect(() => {

  }, [totalPages, totalResults]);

  const data: TableData = new TableData(
    1,
    1,
    Posts,
    actions,
    "id",
    "Post",
  );

  const buscar = () => {
    carregarPosts();
  };

  const adicionar = () => {
    navigate("/post");
  };

  const voltar = () => {
    navigate("../home");
  };

  const onChangePage = (page: number) =>
  {
      setPage(page - 1);
  }

  const reload = async() =>
  {
    await carregarPosts();
  }

 

  return (
    <>
      <Header />
      <div className="container container-user">
        <div className="padding-container">
          <Title value="Lista de posts" />
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
             onReload = {reload}
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
export default Posts;
