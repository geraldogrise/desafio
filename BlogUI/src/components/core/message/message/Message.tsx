
import "./message.css";
import CoreUtil from "../../Util/CoreUtil";
import { MessageType } from "../MessageType";
import React, { forwardRef, useImperativeHandle } from "react";


const Message: React.FC<any> = forwardRef((props: any, ref) => {

    function OpenMessage(title: string, message: string, type: MessageType, time: number)
    {
        CreateContainer(title, message, type, time);
    }

    function CreateContainer(title: any, message: any, type: MessageType, time: number){
	
        let msgContainer = document.getElementById("MsgContainer");
        if(msgContainer === null){
            msgContainer = document.createElement("div"); 
            msgContainer.setAttribute("id", "MsgContainer");
            msgContainer.setAttribute("style", "position:fixed;top:20px;left:100px;z-index:9999900;");	
            document.body.appendChild(msgContainer); 
        }

        CreateMessageContent(msgContainer,title, message, type, time);
    }

    function CreateMessageContent(msgContainer: any,
                                  title: string,
                                  message: string, 
                                  type: MessageType, 
                                  time: number){
        let  messageTime = 5000;
        if (time !== undefined) {
           if (time > 0)
              messageTime = time;
        }
        
        message = "<li>" + message + "</li>";
    
    
        if (message.indexOf("</ul>") <= 0) {
            message = "<ul>" + message + "</ul>";
        }
        let id = CoreUtil.guidGenerator();
        var nomediv = 'msg_' + id;
        if ((type.toLowerCase() === "warning")) {

            messageTime = 10000;
        }
        if (type.toLowerCase() === "error") {

            messageTime = 5000;
        }
        
        let content =  "<div class='messages " + type.toLowerCase() + "' id='" + nomediv + "'>" +
            "<span class='close-message'></span>" +
            "<div class='title-message'>" +
            "<span></span>" +
            "<h1>Atenção</h1>" +
            "</div><div class='message-body'>" +
             message +
            "</div></div>";
        msgContainer.innerHTML = msgContainer.innerHTML +  content;	
        fadeIn(nomediv,messageTime);
        let el = document.getElementsByClassName("close-message")[0];
        el.addEventListener("click", function(){
            fadeIn(nomediv,0);
        });
    }

    function fadeIn(id: string, time: number){
        setTimeout(function(){ 
             let el = document.getElementById(id);
             if(el !== null){
                el.classList.add("fade");
                el.remove();
             }
         }, time);
     }

     useImperativeHandle(ref, () => {
        return {
          OpenMessage,
        };
    });
    

    return (
        <>
         
        </>
      );
});
export default Message;