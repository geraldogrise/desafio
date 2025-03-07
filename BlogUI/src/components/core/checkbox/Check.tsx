import React, { useState, useEffect } from 'react';
import "./check.css";

interface CheckboxProps {
   classe: string;
   initialChecked: boolean;
   value: number;
   classeInput?: string;
   onChange: (value: number, checked: boolean) => void;
}

const Check: React.FC<CheckboxProps> = ({ classe, initialChecked, value, classeInput, onChange }) => {

  const [checked, setChecked] = useState<boolean>(initialChecked);

  // Ensure that when `initialChecked` changes, the local `checked` state is updated
  useEffect(() => {
    setChecked(initialChecked);
  }, [initialChecked]);

  const handleCheckboxChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newChecked = !checked;
    setChecked(newChecked); // update local state
    onChange(Number(e.target.value), newChecked); // call onChange with the updated state
  };

  return (
    <div className="check-container">
        <div className={`form-check ${classe}`}>
            <input 
              className={`form-check-input ${classeInput}`}
              type="checkbox" 
              checked={checked} 
              onChange={handleCheckboxChange}
              value={value} />
        </div>
    </div>
  );
};

export default Check;
