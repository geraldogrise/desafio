import React from 'react';
import "./select.css";
import { Combo } from '../../models/Combo';

interface SelectProps {
    options: Array<Combo>;
    value: number | string;
    label: string;
    required: boolean;
    onChange: (value: number | string) => void; 
}

const Select: React.FC<SelectProps> = ({options, value, label, required, onChange }) => {
    return (
        <div className="float-left select-group">
            <label className="form-label">{label} {required && <span className="required">*</span>}</label>
            <select 
                onChange={(e) => onChange(e.target.value)} 
                value={value} 
                className="form-select select" 
                aria-label="Default select example">
                <option value={0}>
                      Selecione um item
                </option>
                {options.map((option, index) => (
                    <option key={index} value={option.value}>
                        {option.text}
                    </option>
                ))}
        </select>
      </div>
    );
};

export default Select;