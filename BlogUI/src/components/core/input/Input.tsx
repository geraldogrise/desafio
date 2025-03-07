
import "./input.css";
interface InputhProps {
    label: string;
    type: string;
    value: string | number | null;
    disabled: boolean;
    required: boolean;
    placeholder: string;
    maxlength?: number;
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => void;
 }
const Input: React.FC<InputhProps> = ({label, type, value, placeholder, disabled, required, maxlength, onChange}) => {
    return (

            <div className="float-left">
                <label className="form-label">{label} {required && <span className="required">*</span>}</label>
                <input 
                    type={type}
                    className="form-control form-input" 
                    disabled={disabled}
                    placeholder={placeholder}
                    required ={required}
                    value={value != null ? value: ""}
                    onChange={onChange}
                    maxLength={maxlength}
                />
            </div>
    )
};
export default Input;