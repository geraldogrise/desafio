function OpenMessage(title, message, type, time)
{
	CreateContainer(title, message, type, time);
}

function CreateContainer(title, message, type, time){
	
	var msgContainer = document.getElementById("MsgContainer");
	if(msgContainer == null){
		msgContainer = document.createElement("div"); 
		msgContainer.setAttribute("id", "MsgContainer");
		msgContainer.setAttribute("style", "position:fixed;top:20px;left:100px;z-index:9999900;");	
		document.body.appendChild(msgContainer); 
	}
	CreateMessageContent(msgContainer,title, message, type, time);
}

function CreateMessageContent(msgContainer,title, message, type, time){
    var messageTime = 5000;
    if (time != undefined) {
       if (time > 0)
          messageTime = time;
    }
	
	if (message.indexOf("</li>") <= 0) {
        
        message = "<li>" + message + "</li>";
    }


    if (message.indexOf("</ul>") <= 0) {
        message = "<ul>" + message + "</ul>";
    }
	var id = guidGenerator();
    var nomediv = 'msg_' + id;
    if ((type.toLowerCase() != "warning")) {
        messageTime = 10000;
    }
	
	content =  "<div class='messages " + type.toLowerCase() + "' id='" + nomediv + "'>" +
        "<span onclick='fadeIn(\""+nomediv+"\",0)' class='close-message'></span>" +
        "<div class='title-message'>" +
        "<span></span>" +
        "<h1>Atenção</h1>" +
        "</div><div class='message-body'>" +
         message +
        "</div></div>";
	msgContainer.innerHTML =msgContainer.innerHTML+  content;	
	fadeIn(nomediv,messageTime);
	

}

function fadeIn(id, time){
   setTimeout(function(){ 
	    var el = document.getElementById(id);
	    el.classList.add("fade");
        el.remove();
	}, time);
}

function guidGenerator() {
    var S4 = function () {
        return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
    };
    return (S4() + S4() + "" + S4() + "" + S4() + "" + S4() + "" + S4() + S4() + S4());
}


