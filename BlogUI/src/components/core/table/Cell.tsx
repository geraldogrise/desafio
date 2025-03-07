import React from 'react';

interface TableCellProps {
  content: any;
  cellAlign: 'left' | 'center' | 'right' | 'justify'; // Ensure the correct possible values
}

const Cell: React.FC<TableCellProps> = ({ content, cellAlign }) => {
  return (
    <td
      style={{
        textAlign: cellAlign 
      }}
    >
      {content}
    </td>
  );
};

export default Cell;
