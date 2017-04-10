$(function () {
    var productListVM = {
        dt: null,

        init: function () {
            dt = $('#data-table').DataTable({
                "serverSide": true,
                "ajax": "/Category/DataTableGet",
                "columns": [
                   { "title": "ID", "data": null, "sortable": false },
                   { "title": "Name", "data": "Name" },

                   { "title": "Author", "data": "UserName" },
                   { "title": "CreatedDate", "data": "CreatedDate" },
                   { "title": "Action", "data": "Action", "sortable": false }
                ],
                "lengthMenu": [[5, 10, 25], [5, 10, 25]],

                "columnDefs": [{
                    "searchable": false,
                    "orderable": false,
                    "targets": 0,
                    "className": "dt-center",
                    "targets": "_all"
                }],
                "order": [[1, 'asc']],
                "fnCreatedRow": function (row, data, index) {
                    $('td', row).eq(0).html(index + 1);
                }
            });
            dt.MakeCellsEditable({
                "onUpdate": myCallbackFunction,
                "inputCss": 'my-input-class',
                "columns": [1],
                "allowNulls": {
                    "columns": [0],
                    "errorClass": 'error'
                },
                "confirmationButton": {
                    "confirmCss": 'my-confirm-class',
                    "cancelCss": 'my-cancel-class'
                },
            });
            function Delete(response)
            {
                if(response.Success==true)
                {
                    dt.ajax.reload();
                    return;
                }
            }
            $('#data-table').on('click', 'a.remove', function (e) {
                var data = dt.row($(this).closest('tr')).data();
                if (confirm('Are you sure you want to delete this?')) {
                    DeleteItem('/Category/Delete', "POST", data.Id, Delete);
                }

            });
            function myCallbackFunction(updatedCell, updatedRow, oldValue, dataRow) {
                var obj = new Object({
                    ID: dataRow.Id,
                    Name: dataRow.Name
                });
                function Callback(response) {
                    if (response.Success == false) {
                        alert(" No Success !");
                        //MessageShowHide('alert-warning', mimasResource.getResourceByKey('NoUpdateSuccessfullNoParam'), 5000);
                    }
                    else if (response.Success == true) {
                        alert("Success !");
                        //MessageShowHide('alert-success', mimasResource.getResourceByKey('UpdateSuccessfullNoParam'), 5000);
                    }

                }
                SaveItem("/Category/Edit  ", "POST", dataRow.Id, obj, Callback);
                dt.ajax.reload();
            }
        }
    }

    $('#refresh-button').on("click", productListVM.refresh);

    productListVM.init();

    dt.on('click', 'input.btnActive', function (e) {
        var data = dt.row($(this).closest('tr')).data();
        updateStatus(data.ID);
    });

});
//$(function () {
//    var productListVM = {
//        dt: null,

//        init: function () {
//            dt = $('#data-table').DataTable({
//                "serverSide": true,
//                //"processing": true,
//                "sortable": true,
//                "ajax": "/Category/DataTableGet",
//                "columns": [
//                    { "title": "ID", "data": null, "sortable": false },
//                    { "title": "Name", "data": "Name" },
                
//                    { "title": "Author", "data": "UserName" },
//                    { "title": "CreatedDate", "data": "CreatedDate" },
//                    { "title": "Action", "data": "Action", "sortable": false }
//                ],
//                "lengthMenu": [[5, 10, 25], [5, 10, 25]],
//                "iDisplayLength": 10,
//                "columnDefs": [{
//                    "searchable": false,
//                    "orderable": false,
//                    "targets": 0,
//                    "className": "dt-center",
//                    "targets": "_all"

//                }],
//                "order": [[1, 'asc']],
//                "fnCreatedRow": function (row, data, index) {
//                    $('td', row).eq(0).html(index + 1);
//                }
//            });

//            function Delete(response) {
//                if (response.Success == true) {
//                    dt.ajax.reload();
//                }

//            }
//            $('#data-table').on('click', 'a.remove', function (e) {
//                var data = dt.row($(this).closest('tr')).data();
//                if (confirm('Are you sure you want to delete this?')) {
//                    DeleteItem('/Category/Delete', "POST", data.Id, Delete);
//                }
//            });
//            $('#data-table').on('click', 'a.detail', function (e) {
//                var data = dt.row($(this).closest('tr')).data();
//                window.location.replace("/Admin/Category/Edit/" + data.Id);
//            });
//        }
//    }

//    $('#refresh-button').on("click", productListVM.refresh);

//    productListVM.init();
//});
function AddCategory() {
    var getName = $('#titleCategory').val();
    var getshortname = $('#ShortNameCategory').val();
    //var e = document.getElementById("Status");
    //var strStatus = e.options[e.selectedIndex].value;
    var obj = new Object({
        Name: getName,
        ShortName: getshortname,
        //Status: strStatus,
    });
    function callBack(response) {
        if (response.Success == true) {
            alert("Success!");
           
        }
        else {
            alert("No Success!");
        }
        $('#myModal').modal('toggle');
        {
            dt.ajax.reload();
        }

    }
    AddNew("/Category/Create", "POST", obj, callBack)
};