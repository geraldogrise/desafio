import React, { useState } from 'react';

interface PaginationProps {
    page: number;
    total: number; 
    onClick: (page: number) => void; 
}

const Pagination: React.FC<PaginationProps> = ({ page, total, onClick }) => {
    const pages = Array.from({ length: total}, (_, index) => index + 1);
    const [activePage, setActivePage] = useState(1); 
    return (
        <nav aria-label="Page navigation example">
        <ul className="pagination">
        <li className={`page-item ${activePage === 1 ? 'disabled' : ''}`}>
            <a  onClick={(e) => {
                    e.preventDefault(); 
                    const previous = activePage - 1
                    setActivePage(previous);
                    onClick(previous);
                }}   
                className="page-link" 
                href="#page"
                aria-label="Previous">
              <span aria-hidden="true">&laquo;</span>
            </a>
          </li>
          <li className={`page-item ${activePage === 1 ? 'disabled' : ''}`}>
            <a  onClick={(e) => {
                    e.preventDefault(); 
                    onClick(1);
                    setActivePage(1);
                }}   
                className="page-link" 
                href="#page"
                aria-label="Previous">
              <span aria-hidden="true">&laquo;&laquo;</span>
            </a>
          </li>
          {pages.map((page) => (
             <li key={page} 
               
                 className={`page-item ${page === activePage ? 'active' : ''}`}>
                <a 
                    onClick={(e) => {
                        e.preventDefault(); 
                        setActivePage(page);
                        onClick(page);
                    }}     
                    className="page-link" 
                    href="#page"
                >
                   {page}
                </a>
              </li>
           ))}
          <li className={`page-item ${activePage === total ? 'disabled' : ''}`}>
            <a  onClick={(e) => {
                     e.preventDefault(); 
                     onClick(total);
                     setActivePage(total);
                }}   
                className="page-link" 
                href="#page"
                aria-label="Next">
              <span aria-hidden="true">&raquo;&raquo;</span>
            </a>
          </li>
          <li className={`page-item ${activePage === total ? 'disabled' : ''}`}>
            <a  onClick={(e) => {
                    e.preventDefault(); 
                    const next = activePage + 1;
                    setActivePage(next);
                    onClick(next);
                }}   
                className="page-link" 
                href="#page"
                aria-label="Next">
              <span aria-hidden="true">&raquo;</span>
            </a>
          </li>
        </ul>
      </nav>
    );
};

export default Pagination;