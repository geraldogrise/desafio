import { Link } from "react-router-dom";
import blog from "./blog.jpg";
import "./header.css";
import "bootstrap/dist/css/bootstrap.min.css";
import Settings from "../../components/core/settings/Settings";
import Search from "../../components/core/search/Search";
import { useState } from "react";
const Heeader: React.FC<any> = () => {
  const [search, setSearch] = useState<string | number>("");

  const handleSearchChange = (value: string | number) => {
    setSearch(value);
    console.log(search);
  };

  return (
    <div className="navibar">
      <div className="max-content">
        <div className="d-flex align-items-center h-100 mx-5">
          <nav className="my-3 d-flex w-50">
            <Link to="/" className="navbar-brand">
              <img src={blog} alt="Logo" height="41" width="111" />
            </Link>
            

            <div className="x">
              <ul className="navbar-nav d-flex flex-row">
                <li className="nav-item">
                  <Link to="/usuarios" className="nav-link px-3 manrope-500">
                    Usuário
                  </Link>
                </li>

                <li className="nav-item">
                  <Link to="/posts" className="nav-link px-3 manrope-500">
                    Post
                  </Link>
                </li>
               
               
              </ul>
            </div>
          </nav>
          <div className="nav-right">
            <div className="d-flex justify-content-between">
              <div className="float-left search-field">
                <Search onChange={handleSearchChange}></Search>
              </div>
              <div className="float-right">
                <Settings></Settings>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
export default Heeader;
