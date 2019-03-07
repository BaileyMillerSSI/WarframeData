"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/logHub").build();

connection.on("LogEvent", function (message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);

    li.scrollIntoView();

    var btn = document.getElementById("clearAll");
    btn.disabled = false;
});

connection.on("ItemLoaded", function (item) {
    AppendItem(item);
});

connection.start().then(function () {
    
}).catch(function (err) {
    return console.error(err.toString());
    });


function AppendItem (item) {
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

    var btn = document.getElementById("clearAll");
    btn.disabled = false;
}

function ClearItems() {
    var container = document.getElementById("itemList");

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }

    var btn = document.getElementById("clearAll");
    btn.disabled = true;
}

function ClearLogs() {
    var container = document.getElementById("messagesList");

    while (container.firstChild) {
        container.removeChild(container.firstChild);
    }

    var btn = document.getElementById("clearAll");
    btn.disabled = true;
}