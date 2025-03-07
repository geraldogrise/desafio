import { useEffect, useState } from "react";
import "./toggle.css";

interface ToggleProps {
    id: number | string | null;
    value: boolean;
    onChange: (id: number | string | null, isChecked: boolean) => void; 
    disabled?: boolean
}
const Toggle: React.FC<ToggleProps> = ({id, value, onChange, disabled = false}) => {
    const [isChecked, setIsChecked] = useState<boolean>(value);
    useEffect(() => {
        setIsChecked(value);
    }, [value]);
    const handleCheckboxChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setIsChecked(event.target.checked);
        onChange(id, event.target.checked);
    };
    return (
    <div className="form-check form-switch toggle-container">
        <input 
            className="form-check-input" 
            type="checkbox" 
            checked={isChecked} 
            onChange={handleCheckboxChange}
            disabled={disabled}
        />
        <label 
            style={{
                color: isChecked ? '#fff' : '#333', 
            }}
            className="form-check-label">
            {isChecked ? 'Ativo' : 'Inativo'}
        </label>
    </div>
    )
};
export default Toggle;