import React, { useEffect, useState } from 'react';
import "./picklist.css";
import Button from '../button/Button';
import { Item } from '../../models/Items';
import Datalist from '../datalist/Datalist';

interface PicklistProps {
    titleSelected: string;
    titleUnselected: string;
    items: Array<Item>;
    selectedItems: Array<Item>;
    associarText: string;
    desassociarText: string;
    onClick: (selectedItems: Array<Item>, unselectedItems: Array<Item>) => void;
    disabled: boolean;
}

const PickList: React.FC<PicklistProps> = ({ titleSelected, titleUnselected, associarText, desassociarText, items, selectedItems, onClick, disabled }) => {
    const [selectedItem, setSelectedItem] = useState<Item | null>(null);
    const [unselectedItem, setUnselectedItem] = useState<Item | null>(null);
    const [selecteds, setSelecteds] = useState<Array<Item>>(selectedItems);
    const [unselecteds, setUnselecteds] = useState<Array<Item>>(items);

    // Synchronize selected and unselected items when props change
    useEffect(() => {
        setUnselecteds(items);
        setSelecteds(selectedItems);
    }, [items, selectedItems]);

    const onPick = (item: Item) => {
        setSelectedItem(item);
    };

    const onRemove = (item: Item) => {
        setUnselectedItem(item);
    };

    const add = () => {
        if (selectedItem !== null) {
            const newSelecteds = [...selecteds, selectedItem]; // Add to selecteds
            const newUnselecteds = unselecteds.filter(unselected => unselected.value !== selectedItem.value); // Remove from unselecteds

            setSelecteds(newSelecteds);
            setUnselecteds(newUnselecteds);

            // Ensure the updated state is passed to the parent after state update
            onClick(newSelecteds, newUnselecteds);
            setSelectedItem(null);
        }
    };

    const remove = () => {
        if (unselectedItem !== null) {
            const newUnselecteds = [...unselecteds, unselectedItem]; // Add to unselecteds
            const newSelecteds = selecteds.filter(selected => selected.value !== unselectedItem.value); // Remove from selecteds

            setSelecteds(newSelecteds);
            setUnselecteds(newUnselecteds);

            // Ensure the updated state is passed to the parent after state update
            onClick(newSelecteds, newUnselecteds);
            setUnselectedItem(null);
        }
    };

    return (
        <div className='row'>
            <div className='col-md-4'>
                <Datalist title={titleUnselected} items={unselecteds} selected={selectedItem} onClick={onPick} />
            </div>
            <div className='col-md-4'>
                <div className="d-flex justify-content-center align-items-center buttons" style={{ height: "50vh" }}>
                    <div className="d-flex flex-column align-items-center">
                        <div className="button mb-5">
                            <Button
                                text={associarText}
                                disabled={selectedItem === null || disabled}
                                classe="btn-outline-primary d-flex align-items-center justify-content-between"
                                minWidth='200px'
                                iconRight='chevron_right'
                                onClick={add}
                            />
                        </div>
                        <div className="button">
                            <Button
                                text={desassociarText}
                                disabled={unselectedItem === null || disabled}
                                classe="btn-outline-primary d-flex align-items-center justify-content-between"
                                minWidth='200px'
                                iconRight='chevron_left'
                                onClick={remove}
                            />
                        </div>
                    </div>
                </div>
            </div>
            <div className='col-md-4'>
                <Datalist title={titleSelected} items={selecteds} selected={unselectedItem} onClick={onRemove} />
            </div>
        </div>
    );
};

export default PickList;
