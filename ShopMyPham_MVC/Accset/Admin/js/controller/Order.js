var flag = false;
var Order = {
    init: function () {
        Order.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Order/ChangeStatus",
                data: { ID: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    console.log(response);
                    if (response.status == true) {
                        btn.text('Đã giao hàng')
                    }
                    else {
                        btn.text('Chưa giao hàng');
                    }
                }
            });
        });
        //$('.btn-action-order').off('click').on('click', function (e) {
        //    e.preventDefault();
        //    var btn = $(this);
        //    var id = btn.data('id');
        //    $('#dialogDetails').on('show.bs.modal', function () {
        //        $.ajax({
        //            url: "/Admin/Order/ViewOrderDetail",
        //            data: { id: id },
        //            type: "POST",
        //            success: function (response) {
        //                var dataOrder = response;
        //                var row_select = [];
        //                var table = $('#data-table').DataTable({
        //                    "aaData": dataOrder,
        //                    "aoColumns": [
        //                        { "mData": "ID" },
        //                        { "mData": "ProductName" },
        //                        { "mData": "Quantity" },
        //                        { "mData": "Price" },
        //                    ],
        //                    "lengthMenu": [[2, 5, 10, 25], [2, 5, 10, 25]],
        //                    'columnDefs': [{
        //                        'targets': 0,
        //                        'searchable': false,
        //                        'orderable': false,
        //                        'width': '1%',
        //                        //'className': 'dt-body-center',
        //                        //'render': function (data, type, full, meta) {
        //                        //    return '<input type="checkbox" class="cbUser" value="' + full.Id + '">';
        //                        //}
        //                    }],
        //                    'order': [[1, 'asc']],
        //                    "fnCreatedRow": function (row, dataOrder, index) {
        //                        $('td', row).eq(0).html(index + 1);
        //                    }
        //                });
        //                table.ajax.reload();
        //            },
        //            error: function (xhr, status, error) {
        //                var err = xhr.responseText;
        //                alert(err);
        //            }
        //        });
        //    });
        //});

    }
}

Order.init();
//$('#dialogDetails').on('hiden.bs.modal', function () {
//    table.ajax.reload();
//});
//$('.btn-action-order').off('click').on('click', function (e) {
//    var btn = $(this);
//    var id = btn.data('id');
//    var row_select = [];
//    var table = $('#data-table').DataTable({
//        "ajax": "/Admin/Order/ViewOrderDetail?id=" + id,
//        "aoColumns": [
//            { "mData": "ID" },
//            { "mData": "ProductName" },
//            { "mData": "Quantity" },
//            { "mData": "Price" },
//        ],
//        "lengthMenu": [[2, 5, 10, 25], [2, 5, 10, 25]],
//        'columnDefs': [{
//            'targets': 0,
//            'searchable': false,
//            'orderable': false,
//            'width': '1%',
//            //'className': 'dt-body-center',
//            //'render': function (data, type, full, meta) {
//            //    return '<input type="checkbox" class="cbUser" value="' + full.Id + '">';
//            //}
//        }],
//        'order': [[1, 'asc']],
//        "fnCreatedRow": function (row, data, index) {
//            $('td', row).eq(0).html(index + 1);
//        }
//    });
//    //$('#dialogDetails').on('hiden.bs.modal', function () {
//    //    table.ajax.reload();
//    //});
//})
function OrderDeatil(id)
{
    $('#dialogDetails').on('show.bs.modal', function () {
        $.ajax({
            url: "/Admin/Order/ViewOrderDetail",
            data: { id: id },
            type: "GET",
            success: function (response) {
                var dataOrder = response;
                var row_select = [];
                var table = $('#data-table').DataTable({
                    "aaData": dataOrder,
                    "aoColumns": [
                        { "mData": "ID" },
                        { "mData": "ProductName" },
                        { "mData": "Quantity" },
                        { "mData": "Price" },
                    ],
                    "lengthMenu": [[2, 5, 10, 25], [2, 5, 10, 25]],
                    'columnDefs': [{
                        'targets': 0,
                        'searchable': false,
                        'orderable': false,
                        'width': '1%',
                        //'className': 'dt-body-center',
                        //'render': function (data, type, full, meta) {
                        //    return '<input type="checkbox" class="cbUser" value="' + full.Id + '">';
                        //}
                    }],
                    'order': [[1, 'asc']],
                    "fnCreatedRow": function (row, dataOrder, index) {
                        $('td', row).eq(0).html(index + 1);
                    }
                });
           
            },
            error: function (xhr, status, error) {
                var err = xhr.responseText;
                alert(err);
            }
        });
    });
}