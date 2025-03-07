import React, { useState } from 'react';
import { Tab } from '../../models/Tab';

import "./tabs.css"

interface  TabsProps {
   data: Array<Tab>;
   onClick: (tab: Tab) => void; 
}

const Tabs: React.FC<TabsProps> = ({data, onClick}) => {
    const [active, setActive] = useState<Tab | undefined>(data.find(x=> x.active));
    return (
    <ul className="nav nav-tabs mb-3">
         {data.map((tab, index) => (
            <li className="nav-item" key={index}>
                <a 
                    onClick={(e) => {
                        e.preventDefault(); 
                        setActive(tab)
                        onClick(tab);
                    }} 
                     className={`nav-link  
                         ${tab.disabled  ? 'disabled' : ''}
                         ${active?.action === tab.action  ? 'active' : ''}`
                    } 
                    aria-disabled={tab.disabled} 
                    href={tab.action}>
                        {tab.text}
                </a>
            </li>
          ))}
      </ul>
    );
};

export default Tabs;