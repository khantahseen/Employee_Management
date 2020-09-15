// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


const connection = new signalR.HubConnectionBuilder()
    .withUrl("/NotificationHub")
    .build();
connection.on("AddEmployeeMessage", (employeeName, employeeSurname) => {
   // alert(employeeName + " " + employeeSurname + " was added.");
    var message = employeeName + " " + employeeSurname + " was added ."
    var li = document.createElement("li");
    li.textContent = message;
    var notifMenu = document.getElementById("Counter");
    notifMenu.appendChild(li);
});

connection.on("AddDepartmentMessage", (deptName) => {
    //alert(deptName + " was added by the Admin.");
    var message = deptName + " was added by the Admin."
    var li = document.createElement("li");
    li.textContent = message;
    var notifMenu = document.getElementById("Counter");
    notifMenu.appendChild(li);
});

connection.on("EditProfileMessage", (name) => {
    //alert(name + " edited their profile.");
    var message = name + " edited their profile."
    var li = document.createElement("li");
    li.textContent = message;
    var notifMenu = document.getElementById("Counter");
    notifMenu.appendChild(li);
});

try {
    connection.start();
    console.log("connected");
} catch (err) {
    console.log(err);
}


if (document.getElementById("createEmployeeButton")) {
    document.getElementById("createEmployeeButton").addEventListener("click", function (event) {
        var name = document.getElementById("nameInput").value;
        var surname = document.getElementById("surnameInput").value;
        connection.invoke("SendAddEmployeeMessage", name, surname).catch(function (err) {
            return console.error(err.toString());
        });
    });
}

if (document.getElementById("createDepartmentButton")) {
    document.getElementById("createDepartmentButton").addEventListener("click", function (event) {
        var name = document.getElementById("nameInput").value;
        connection.invoke("SendAddDepartmentMessage", name).catch(function (err) {
            return console.error(err.toString());
        });
    });
}

if (document.getElementById("editEmployeeButton")) {
    document.getElementById("editEmployeeButton").addEventListener("click", function (event) {
        var name = document.getElementById("nameInput").value;
        connection.invoke("SendEditProfileMessage", name).catch(function (err) {
            return console.error(err.toString());
        });
    });
}


connection.onclose(async () => {
    await start();
});




