'use strict';

document.getElementById("clearAll").addEventListener("click", function () {
    ClearItems();
    ClearLogs();
});

document.getElementById("loadDbItems").addEventListener("click", function () {

    ClearItems();

    var request = new XMLHttpRequest();

    request.open('GET', '/api/weapondata', true);

    request.onload = function () {
        var data = JSON.parse(this.response);

        if (request.status >= 200 && request.status < 400) {
            data.forEach(item => {
                AppendItem(item);
            });


        } else {
            console.log('error');
        }
    };

    request.send();
});