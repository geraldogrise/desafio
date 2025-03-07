import "bootstrap/dist/css/bootstrap.min.css";
import "./search.css";
interface InputSearchProps {
  text?: string | number;
  onChange: (item: string | number) => void;
}
const Search: React.FC<InputSearchProps> = ({ text, onChange }) => {
  return (
    <div className="container-inline">
      <div className="input-group mb-3">
        <div className="d-flex justify-content-between">
          <span className="input-group-text p-0" id="basic-addon1">
            <span className="material-icons-outlined m-0">search</span>
          </span>
          <input
            value={text}
            type="text"
            className="form-control input-search"
            placeholder="Buscar..."
            aria-label="Buscar"
            onChange={(e) => onChange(e.target.value)}
            aria-describedby="basic-addon1"
          />
        </div>
      </div>
    </div>
  );
};
export default Search;
