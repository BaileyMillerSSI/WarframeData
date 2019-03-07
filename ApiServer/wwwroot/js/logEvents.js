"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/logHub").build();

connection.on("LogEvent", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);

    li.scrollIntoView();
});

connection.on("ItemLoaded", function (item) {

    var link = document.createElement("a");
    link.href = item.wikiLink;


    var div = document.createElement("div");
    var title = document.createElement("p");
    title.innerText = item.title;
    var img = document.createElement("img");
    img.src = item.thumbnailImage;

    div.appendChild(img);
    div.appendChild(title);
    var li = document.createElement("li");
    li.appendChild(div);
    link.appendChild(li);
    document.getElementById("itemList").appendChild(link);

    div.scrollIntoView();
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
});