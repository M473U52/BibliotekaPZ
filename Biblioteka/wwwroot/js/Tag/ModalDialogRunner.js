const dialog_delete = document.querySelector('#delete_tag_dialog2');
var id_temp = 1;
function funDelete(id) {

    id_temp = id;
    dialog_delete.showModal();
    document.getElementById("inputter").value = id;
}


function Cancel() {

    dialog_delete.close();
}

function SetValue() {

    document.getElementById("inputter").value = id_temp;
    return it_temp;
}
