$(function () {
    var productListVM = {
        dt: null,
        init: function () {
            dt = $('#data-table').DataTable({
                "serverSide": true,
                "ajax": "/Admin/Feedback/DataTableGet",
                "columns": [
                   { "title": "ID", "data": null, "sortable": false },
                   { "title": "Name", "data": "Name" },
                   { "title": "Phone", "data": "Phone" },
                   { "title": "Email", "data": "Email" },
                       { "title": "Content", "data": "Content" },
                     { "title": "UserID", "data": "UserID" },
                      { "title": "CreatedDate", "data": "CreatedDate" }
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
            function Delete(response) {
                if (response.Success == true) {
                    dt.ajax.reload();
                    return;
                }
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