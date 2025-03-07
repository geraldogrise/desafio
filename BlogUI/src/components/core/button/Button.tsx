import React from 'react';
import "./button.css";

interface ButtonProps {
    text: string;
    disabled: boolean;
    classe: string;
    onClick: () => void; 
    minWidth?: string;
    iconLeft?: string;
    iconRight?: string;
}

const Button: React.FC<ButtonProps> = ({ text, disabled, classe, onClick, minWidth, iconLeft, iconRight}) => {
    return (
        <button 
            type="button" 
            disabled={disabled} 
            className={`btn ${classe} ${disabled ? 'btn-disabled' : ''}`}  
            onClick={onClick}   
            style={{ 
                minWidth: minWidth,
             }}          
        >
           {iconLeft && (
                <span className="material-icons-outlined">{iconLeft}</span>
            )}  
            <span>{text}</span>
            {iconRight && (
                <span className="material-icons-outlined">{iconRight}</span>
            )}  
        </button>
    );
};

export default Button;