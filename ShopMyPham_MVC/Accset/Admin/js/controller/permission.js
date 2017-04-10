$(document).ready(function () {
   
    $(".ul-tree").each(function () {
        var countAll = $(this).find('input').length;

        var countChecked = $(this).find('input:checked').length;

        if (0 < countChecked && countChecked < countAll) {
            $(this).prev().prev().prop("indeterminate", "true");
        }
    });

    $(".ul-tree").css("display", "none");

    $('.tree').click(function (e) {
        var treenode = $(this).attr('id');
        if ($("#tree-" + treenode).css("display") == "none") {
            $(this).removeClass("glyphicon-plus").addClass("glyphicon-minus");
            $("#tree-" + treenode).css("display", "block");
        }
        else {
            $(this).removeClass("glyphicon-minus").addClass("glyphicon-plus");
            $("#tree-" + treenode).css("display", "none");
        }

    });

});

function clickandshow() {
    var s = "";
    $(".check-role").each(function () {
        if ($(this).is(":checked")) {
            s += $(this).prop("id") + "*";
        }
    });
    $("#lstRoleSelected").val(s);
}

$('input[type="checkbox"]').change(function (e) {

    var checked = $(this).prop("checked"),
        container = $(this).parent(),
        siblings = container.siblings();

    container.find('input[type="checkbox"]').prop({
        indeterminate: false,
        checked: checked

    });

    function checkSiblings(el) {

        var parent = el.parent().parent(),
            all = true;

        el.siblings().each(function () {
            return all = ($(this).children('input[type="checkbox"]').prop("checked") === checked);
        });

        if (all && checked) {

            parent.children('input[type="checkbox"]').prop({
                indeterminate: false,
                checked: checked
            });

            checkSiblings(parent);

        } else if (all && !checked) {

            parent.children('input[type="checkbox"]').prop("checked", checked);
            parent.children('input[type="checkbox"]').prop("indeterminate", (parent.find('input[type="checkbox"]:checked').length > 0));
            checkSiblings(parent);

        } else {

            el.parents("li").children('input[type="checkbox"]').prop({
                indeterminate: true,
                checked: false
            });
        }
    }

    checkSiblings(container);
    clickandshow();
});



function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown menu if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {

        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}