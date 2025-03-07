import React, { forwardRef, useImperativeHandle } from "react";
import { MessageType } from "../MessageType";
import CoreUtil from "../../Util/CoreUtil";


const Message: React.FC<any> = forwardRef((props: any, ref) => {

    function OpenAlert(message: string, type: MessageType, time: number)
    {
        CreateContainer(message, type, time);
    }

    function CreateContainer(message: any, type: MessageType, time: number){
	
        let alertContainer = document.getElementById("AlertContainer");
        if(alertContainer === null){
            alertContainer = document.createElement("div"); 
            alertContainer.setAttribute("id", "AlertContainer");
            alertContainer.setAttribute("style", "position:fixed;top:20px;left:100px;z-index:9999900;width: 350px;");	
            document.body.appendChild(alertContainer); 
        }

        CreateMessageContent(alertContainer, message, type, time);
    }

    function CreateMessageContent(alertContainer: any, message: string, type: MessageType, time: number) {
        let messageTime = 5000;
        if (time !== undefined && time > 0) {
            messageTime = time;
        }
    
        if (message.indexOf("</li>") <= 0) {
            message = "<li>" + message + "</li>";
        }
    
        if (message.indexOf("</ul>") <= 0) {
            message = "<ul>" + message + "</ul>";
        }
    
        let id = CoreUtil.guidGenerator();
        var nomediv = 'msg_' + id;
        if (type.toLowerCase() !== "warning") {
            messageTime = 10000;
        }
    
        let content = `
            <div id='${nomediv}' class='alert alert-${type.toLowerCase()}' role='alert'>
                <button type='button' class='close close-alert' data-dismiss='alert' aria-label='Close' data-id='${nomediv}'>
                    <span aria-hidden='true'>&times;</span>
                </button>
                ${message}
            </div>
        `;
        alertContainer.innerHTML += content;
    
        fadeIn(nomediv, messageTime);
    
        // Attach event listener to the close button inside this alert
        const closeButton = document.querySelector(`#${nomediv} .close-alert`);
        if (closeButton) {
            closeButton.addEventListener("click", function () {
                const alertId = closeButton.getAttribute('data-id');
                if(alertId)
                    fadeIn(alertId, 0); 
            });
        }
    }
    

    function fadeIn(id: string, time: number){
        setTimeout(function(){ 
             let el = document.getElementById(id);
             console.log(id);
             if(el !== null){
                el.remove();
             }
         }, time);
     }

     useImperativeHandle(ref, () => {
        return {
          OpenAlert,
        };
    });
    

    return (
        <>
         
        </>
      );
});
export default Message;