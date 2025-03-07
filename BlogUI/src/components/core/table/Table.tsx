
import "./table.css";
import React from 'react';
import Toggle from '../toggle/Toggle';
import { useNavigate } from 'react-router-dom';
import Pagination from '../pagination/Pagination';
import { TableData } from '../../models/TableData';
import { MessageType } from "../message/MessageType";
import { TableColumns } from '../../models/TableColumns';
import GenericService from "../../../services/GenericService";
import { useGlobalContext } from "../../../providers/GlobalProvider";
import { cpfMask } from "../Util/Masks";



interface TableProps {
    columns: TableColumns[];
    data: TableData;
    totalResults?: Number;
    pages?: number
    onPage: (page: number) => void; 
    onReload: () => Promise<void>; 
}
const Table: React.FC<TableProps> = ({ columns, data, totalResults, pages, onPage, onReload }) => {
   const { OpenMessage, SetTab } = useGlobalContext();
   const navigate = useNavigate();
   const onChangeStatus = async (id: number | string | null, isChecked : boolean) => {
   const service = new GenericService();

    try {
        if (isChecked) {
            const response = await service.ativar(Number(id), data.controller);
            const message  = (response as any).data.message;
            OpenMessage(MessageType.Success, message);
        } else {
            const response = await service.inativar(Number(id), data.controller);
            const message  = (response as any).data.message;
            OpenMessage(MessageType.Success, message);
        }
    } catch (error) {
        OpenMessage(MessageType.Error, "Error ao processar a ação");
    }
  }

  const getIdValue = (row: any, idColumn: string): string | number | null => {
    return row[idColumn] ?? null; 
 };

 const deleteInline = async(id: number) => {
     const service = new GenericService();
     service.Delete(id,"api/posts");
 }


  return (
    <div className="row table-responsive">
        <table className="table gridviewsei borderless">
            <thead>
                <tr>
                    {columns.map((column, index) => (
                        <th 
                            style={{
                               textAlign: column.align
                            }}
                            key={index}
                            scope="col"
                        >
                            {column.description}
                        </th>
                    ))}
                   {data.actions.length > 0 && (<th className="actions">Ações</th>)}

                </tr>
            </thead>
            <tbody>
                {data.data.map((row, rowIndex) => (
                    <tr key={rowIndex}>
                        {columns.map((column, colIndex) => {
                            const cellValue = (row as any)[column.name];
                            const cellAlign = column.align;
                            let cellValueMask = ''
                            
                            if(column.name === 'cpf') {
                                cellValueMask = cpfMask(cellValue);
                            }

                            return (
                                <td
                                    className="table-container"
                                    style={{
                                        textAlign: cellAlign
                                    }}
                                    key={colIndex}
                                >
                                    {typeof cellValue === 'boolean' 
                                        ? <Toggle disabled={false} id={getIdValue(row, data.id_column)}  onChange={onChangeStatus} value={cellValue} />
                                        : column.name === 'cpf' ? cellValueMask : cellValue
                                    }
                                </td>
                            );
                        })}
                        <td className="actions">
                         {data.actions.map((action, index) => {
                            const icon = action.icon
                            const id = getIdValue(row, data.id_column);
                            const existe = false;
                            return (
     
                                <a 
                                   href="action" 
                                   className={`btn-actions ${existe ? "disabled-link" : ""}`} 
                                   onClick={async (e) => {
                                      e.preventDefault(); 
                                      if(!existe)
                                      {
                                         if(action.tab)
                                         {
                                            SetTab(action.tab);
                                            navigate("../"+action.url+"/"+id)
                                         }
                                         if (action.icon === "delete") {
                                            await deleteInline(Number(id));
                                            OpenMessage(MessageType.Success, "Registro removido com sucesso");
                                             setTimeout(async () => {
                                                await onReload();
                                            }, 50);

                                         }
                                         if (action.icon === "edit") {
                                            navigate("../"+action.url+"/"+id)
                                         }
                                       }
                                   }} 
                                >
                                
                                   <span title={action.tooltip} key={rowIndex+"#"+index} className="material-icons-outlined">{icon}</span>
                                </a>   
                
                            );
                        })}
                        </td>
                       
                    </tr>
                ))}
            </tbody>
        </table>
        <div className="footer-table">
            <Pagination page={1} total={(pages !== undefined ? Number(pages): 0)} onClick={onPage}></Pagination>
            <div >
                <span className="footer-text">{`Exibindo de ${1} a ${10} de ${totalResults} resultados`}</span>
            </div>
        </div>
    </div>
  );
};

export default Table;
