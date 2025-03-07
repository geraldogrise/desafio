import React, { useState } from 'react';
import 'bootstrap/dist/css/bootstrap.min.css';
import "./dropdown.css";
import { Combo } from '../../models/Combo';
interface DropdownProps {
   selected: Combo;
   data: Array<Combo>;
   onSelect: (item: Combo) => void; 
}
const Dropdown: React.FC<DropdownProps> = ({ selected, data, onSelect }) => {
  const [selectedItem, setSelectedItem] = useState<Combo>(selected);

  return (
    <div className="dropdown">
        <button
            className="btn btn-primary btn-drop dropdown-toggle custom-dropdown"
            type="button"
            id="dropdownMenuButton"
            data-bs-toggle="dropdown"
            aria-expanded="false"
          >
           {selectedItem.text}
        </button>
        <ul className="dropdown-menu custom-dropdown-menu" aria-labelledby="dropdownMenuButton">
            {data.map((item, index) => {
                const text = item.text

                return (
                  <li key={index}>
                    <a className="dropdown-item"  
                            onClick={(e) => {
                                e.preventDefault(); 
                                setSelectedItem(item);
                                onSelect(item); 
                            }} 
                            href="#drop">{text}                     
                    </a>
                  </li>
                );
             })}
        </ul>
    </div>
  );
};

export default Dropdown;
