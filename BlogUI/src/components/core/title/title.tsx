import "./title.css";
const Title: React.FC<any> = ({ value}) => {
    return (
        <h2 className="title">{value}</h2>
    )
};
export default Title;