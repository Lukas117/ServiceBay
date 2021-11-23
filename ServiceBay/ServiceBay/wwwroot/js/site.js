// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetAuc(id) {
    return id;
}

function UpdateNotifications() {
    var auc1 = GetAuc(24);
    //var prev = auc1.Price();
    var auc2 = setTimeout(GetAuc(24), 2000);
    //var newp = auc2.Price();
    if (auc1 != auc2) {
        console.log("you are smart");
    }
    else {
        console.log("you may be smart...");
    }
}

GetAuc();