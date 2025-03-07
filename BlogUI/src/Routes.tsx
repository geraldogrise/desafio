import React from "react";
import {
  BrowserRouter as Router,
  Route,
  Routes,
} from 'react-router-dom';
/*--------------- componentes ---------------*/
import Home from './pages/home/Home';
import Login from './pages/login/Login';
import Usuario from './pages/usuarios/usuario';
import Usuarios from './pages/usuarios/Usuarios';
import Register from "./pages/register/Register";
import Posts from "./pages/posts/posts";
import Post from "./pages/posts/post";
import View from "./pages/view/Views";

const renderLoader = () => {
  return <div>Carregando...</div>;
};

const Main = () => (
  <React.Suspense fallback={renderLoader()}>
    <Router>
      <Routes>
        <Route path="/" element={<View />} />
        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
        <Route path="/home" element={<Home />} />
        <Route path="/usuarios" element={<Usuarios />} />
        <Route path="/usuario/:id?" element={<Usuario />} />
        <Route path="/posts" element={<Posts />} />
        <Route path="/post/:id?" element={<Post />} />
      </Routes>
    </Router>
  </React.Suspense>
);

export default Main;
