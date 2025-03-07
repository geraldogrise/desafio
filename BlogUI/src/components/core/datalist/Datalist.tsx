import React, { useState } from 'react';
import "./datalist.css";
import { Item } from '../../models/Items';


interface DatalistProps {
    title: string;
    selected: Item | null;
    items: Array<Item>;
    onClick: (item: Item) => void; 
}


const Datalist: React.FC<DatalistProps> = ({title, selected, items, onClick}) => {
    const [selectedItem, setSelectedItem] = useState<Item | null>(selected);
    return (
        <ul className="list-group datalist">
             <li className='list-group-item'>
                <h2 className='datalist-title'>{title}</h2>
            </li>
            {items.map((item, index) => (
               
                <li
                    key={index}
                    className={`list-group-item ${selectedItem  !== null && item.value === selectedItem.value  ? 'active' : ''}`}
                    aria-current={item.active ? 'true' : 'false'}
                >
                    <button
                        className='btn-link'
                        onClick={(e) => {
                            e.preventDefault(); 
                            setSelectedItem(item);
                            onClick(item);
                        }} 
                    >{item.text}
                    </button>
                </li>
            ))}
        </ul>
    );
};

export default Datalist;