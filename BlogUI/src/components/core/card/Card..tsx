import "./card.css";

import React from 'react';
import foto from "./foto.png";
interface CardProps {
   title: string;
   description: string;
   name: string;
   email: string;
}

const Card: React.FC<CardProps> = ({ title, description, name, email }) => {

  return (
    <div className="card" style={{ width: "18rem",  marginTop:"20px", marginLeft:"20px"}}>
        <img src={foto} className="card-img-top" alt="..."/>
        <div className="card-body">
            <h5 className="card-title">{title}</h5>
            <span className="card-user">{name}</span>
            <span className="card-email">{email}</span>
            <p className="card-text">{description}</p>
            <a href={"mailto:"+ email} className="btn btn-primary" >Enviar e-mail</a>
           </div>
    </div>
  );
};

export default Card;
