import 'bootstrap/dist/css/bootstrap.min.css';
import './searchlist.css';

import { useState, useEffect } from 'react';
import Search from '../search/Search';
import { Item } from '../../models/Items';
import Button from '../button/Button';

interface SearchListProps {
    items: Item[];
    selected: Item | null;
    onClick: (item: Item) => void;
}

const SearchList: React.FC<SearchListProps> = ({ items, selected, onClick }) => {
    const [search, setSearch] = useState<string>('');
    const [filteredItems, setFilteredItems] = useState<Item[]>(items);

    // Update filteredItems when items or search term changes
    useEffect(() => {
        const filtered = items.filter(item =>
            item.text.toLowerCase().includes(search.toLowerCase())
        );
        setFilteredItems(filtered);
    }, [items, search]);

    const handleSearchChange = (value: string | number) => {
        setSearch(value.toString());
    };

    return (
        <>
            <div className="d-flex justify-content-between mb-3">
                <Search onChange={handleSearchChange} />
                <Button
                    text="Buscar"
                    disabled={false}
                    classe="btn-primary btn-searchlist"
                    onClick={() => {} /* The filter is handled automatically by useEffect */}
                />
            </div>
            <ul className="list-group">
                {filteredItems.length > 0 ? (
                    filteredItems.map((item, index) => (
                        <li
                            key={item.value} // Use item.id if available
                            className={`list-group-item ${selected?.value === item.value ? 'active' : ''}`}
                            aria-current={item.active ? 'true' : 'false'}
                        >
                            <button
                                className='btn-link'
                                onClick={() => {
                                    onClick(item);
                                }}
                            >
                                {item.text}
                            </button>
                        </li>
                    ))
                ) : (
                    <li className="list-group-item">Nenhum item encontrado</li>
                )}
            </ul>
        </>
    );
};

export default SearchList;
