import React, { forwardRef, useImperativeHandle } from "react";
import CoreUtil from "../../Util/CoreUtil";


const Confirm: React.FC<any> = forwardRef((props: any, ref) => {

    function OpenConfirm(title: string, message: string, okText: string, cancelText: string)
    {
        CreateContainer(title, message, okText, cancelText);
    }

    function CreateContainer(title: string, message: any, okText: string, cancelText: string){
	
        let confirmContainer = document.getElementById("ConfirmContainer");
        if(confirmContainer === null){
            confirmContainer = document.createElement("div"); 
            confirmContainer.setAttribute("id", "ConfirmContainer");
            confirmContainer.setAttribute("style", "position:fixed;top:20px;left:100px;z-index:9999900;width: 350px;");	
            document.body.appendChild(confirmContainer); 
        }

        CreateMessageContent(confirmContainer, title, message, okText, cancelText);
    }

    function CreateMessageContent(confirmContainer: any,
                                  title: string,
                                  message: string, 
                                  okText: string,
                                  cancelText: string){
       
        if (message.indexOf("</li>") <= 0) {
            message = "<li>" + message + "</li>";
        }
    
    
        if (message.indexOf("</ul>") <= 0) {
            message = "<ul>" + message + "</ul>";
        }
        let id = CoreUtil.guidGenerator();
        var nomediv = 'msg_' + id;
        let content = "<div id='" + nomediv + "' class='modal show' tabindex='-1' role='dialog'>"+
                           "<div class='modal-dialog' role='document'>"+
                                "<div class='modal-content'>"+
                                     "<div class='modal-header'>"+
                                          "<h5 class='modal-title'>"+title+"</h5>"+
                                          "<button type='button' class='close close-confirm' data-dismiss='modal' aria-label='Close'>"+
                                                   "<span aria-hidden='true'>&times;</span>"+
                                          "</button>"+
                                      "</div>"+
                                      "<div class='modal-body'>"+
                                           "<p>"+message +"</p>"+
                                       "</div>"+
                                       "<div class='modal-footer'>"+
                                            "<button type='button' class='btn btn-secondary' data-dismiss='modal'>"+cancelText+"</button>"+
                                            "<button type='button' class='btn btn-primary'>"+okText+"</button>"+
                                       "</div>"+
                                 "</div>"+
                            "</div>"+
                      "</div>";
        
        confirmContainer.innerHTML = confirmContainer.innerHTML +  content;	
        let el = document.getElementsByClassName("close-confirm")[0];
        el.addEventListener("click", function(){
            fadeIn(nomediv,0);
        });
    }

    function fadeIn(id: string, time: number){
        setTimeout(function(){ 
             let el = document.getElementById(id);
             if(el !== null){
                el.remove();
             }
         }, time);
     }

     useImperativeHandle(ref, () => {
        return {
            OpenConfirm,
        };
    });
    

    return (
        <>
         
        </>
      );
});
export default Confirm;