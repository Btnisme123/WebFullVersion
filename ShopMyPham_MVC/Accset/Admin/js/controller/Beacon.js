$(function () {
    var productListVM = {
        dt: null,
        init: function () {
            dt = $('#data-table').DataTable({
                "serverSide": true,
                "ajax": "/Admin/Beacon/DataTableGet",
                "columns": [
             
                   { "title": "MACID", "data": "MACID" },
                   { "title": "LocationX", "data": "LocationX" },
                   { "title": "LocationY", "data": "LocationY" },
                   { "title": "ShopID", "data": "ShopID" },
                   { "title": "Information", "data": "Information" },               
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
               
            });
            function Delete(response) {
                if (response.Success == true) {
                    dt.ajax.reload();
                    return;
                }
            }
            $('#data-table').on('click', 'a.remove', function (e) {
                var data = dt.row($(this).closest('tr')).data();
                if (confirm('Are you sure you want to delete this?')) {
                    DeleteItem('/Admin/Beacon/Delete', "POST", data.Id, Delete);
                }

            });
            $('#data-table').on('click', 'a.detail', function (e) {
                var data = dt.row($(this).closest('tr')).data();
                window.location.replace("/Admin/Beacon/Edit/" + data.Id);
            });
        }
    }

    $('#refresh-button').on("click", productListVM.refresh);

    productListVM.init();

    dt.on('click', 'input.btnActive', function (e) {
        var data = dt.row($(this).closest('tr')).data();
        updateStatus(data.ID);
    });

});