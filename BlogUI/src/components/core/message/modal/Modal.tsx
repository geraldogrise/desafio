import React, { forwardRef, useImperativeHandle, ReactNode } from 'react';
import ReactDOM from 'react-dom';

const Modal: React.FC<any> = forwardRef((props: any, ref) => {
    const [id, setId] = React.useState(props.id);
    const [classe, setClasse] = React.useState(props.id);
    const [childrens, setChildrens] = React.useState<ReactNode>(null);
 
    function OpenModal(id: string, classe: string, childrens: ReactNode) {
        setChildrens(childrens);
        setId(id);
        setClasse(classe);
    }

    useImperativeHandle(ref, () => ({
        OpenModal,
    }));

    if (!childrens) return null;

    return ReactDOM.createPortal(
        <div id={id} className={`${classe} modal show`} tabIndex={-1}>
            <div className="modal-dialog">
                {childrens}
            </div>
        </div>,
        document.body
    );
});

export default Modal;