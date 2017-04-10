//Detele
function DeleteItem(url, type, id, func) {
    $.ajax({
        type: type,
        url: url + "?id=" + id,
        data: {
            ID: id
        },
        success: function (response) {
            func(response);
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;

        }
    });
};

//function DeleteItem(url, type, id, func, optional) {
//    $.ajax({
//        type: type,
//        url: url + "?id=" + id,
//        data: {
//            ID: id
//        },
//        success: function (response) {
//            func(response, optional);
//        },
//        error: function (xhr, status, error) {
//            var err = xhr.responseText;

//        }
//    });
//};

//var obj = new Object();
//obj.ID = 1;
//obj.Name = "phuc";
//function callAlert(response) {
//    confirm(response);
//}
//saveItem("/Categories/Edit", "POST", 1, obj, null);

//Save:
function SaveItem(url, type, id, jsonData, func) {
    $.ajax({
        type: type,
        url: url,
        data: jsonData,
        success: function (response) {
            func(response);
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
        }
    });
};
//AddNew:
function AddNew(url, type, jsonData, func) {
    $.ajax({
        type: type,
        url: url,
        data: jsonData,
        success: function (response) {
            func(response);
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;

        }

    });

};
//Detail:
function Detail(url, type, jsonData, func) {
    $.ajax({
        type: type,
        url: url,
        data: jsonData,
        success: function (response) {
            func(respond);
        },
        error: function (xhr, status, error) {
            var err = xhr.responseText;
        }

    });
};

function MessageShowHide(classname, message, milliseconds) {
    $('.' + classname + ' strong').text(message);
    $('.' + classname).show();
    setTimeout(function () { $('.' + classname).hide(); }, milliseconds);
}