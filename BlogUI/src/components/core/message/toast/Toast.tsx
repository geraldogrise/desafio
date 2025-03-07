import React, { forwardRef, useImperativeHandle } from "react";
import { MessageType } from "../MessageType";
import CoreUtil from "../../Util/CoreUtil";

const Toast: React.FC<any> = forwardRef((props: any, ref) => {

    function OpenToast(title: string, message: string, type: MessageType, time: number, icon: string)
    {
        CreateContainer(title, message, type, time, icon);
    }

    function CreateContainer(title: string, message: any, type: MessageType, time: number, icon: string){
	
        let toastContainer = document.getElementById("ToastContainer");
        if(toastContainer === null){
            toastContainer = document.createElement("div"); 
            toastContainer.setAttribute("id", "AlertContainer");
            toastContainer.setAttribute("style", "position:fixed;top:20px;left:100px;z-index:9999900;width: 350px;");	
            document.body.appendChild(toastContainer); 
        }

        CreateMessageContent(toastContainer, title, message, type, time, icon);
    }

    function CreateMessageContent(toastContainer: any,
                                  title: string,
                                  message: string, 
                                  type: MessageType, 
                                  time: number,
                                  icon: string){
        let  messageTime = 5000;
        if (time !== undefined) {
           if (time > 0)
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
        if ((type.toLowerCase() !== "warning")) {
            messageTime = 10000;
        }
        let date = new Date();
        let hour =  date.getHours()+":" +date.getMinutes();
        let content = "<div id='" + nomediv + "' class='toast toast-"+type+"' role='alert' aria-live='assertive' aria-atomic='true'>"+
                             "<div class='toast-header'>"+
                                  "<strong class='mr-auto'><i class='fa fa-"+icon+"'></i>"+title+"</strong>"+
                                   "<small>"+hour+"</small>"+
                                  "<button  type='button' class='ml-2 mb-1 close close-toast' data-dismiss='toast' aria-label='Close'>"+
                                          "<span aria-hidden='true'>&times;</span>"+
                                   "</button>"+
                             "</div>"+
                             "<div class='toast-body'>"+
                                   message+
                             "</div>"+
                        "</div>";

        toastContainer.innerHTML = toastContainer.innerHTML +  content;	
        fadeIn(nomediv,messageTime);
        let el = document.getElementsByClassName("close-toast")[0];
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
            OpenToast,
        };
    });
    

    return (
        <>
         
        </>
      );
});
export default Toast;