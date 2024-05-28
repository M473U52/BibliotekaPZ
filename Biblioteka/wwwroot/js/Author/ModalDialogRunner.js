const confirmation_reservation_dialog = document.querySelector("#confirmation_reservation_dialog");
const confirmation_buttons_arr = document.querySelectorAll(".borrowing_confirmation_button");

const delete_buttons_arr = document.querySelectorAll(".action-delete");
const dialog_reservation_delete = document.querySelector("#delete_reservation_dialog");
const dialog_author_delete = document.querySelector("#delete_author_dialog");

const SetButtonsOnClickListeners = () => {
    delete_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowBorrowingDeleteDialog(button);
        });
    });
   
}


const ShowBorrowingDeleteDialog = (button) => {

    const dialog_content = dialog_author_delete.querySelector(".dialog_content");
    const dialog_header = dialog_author_delete.querySelector(".dialog_header");
    const cancel_button = dialog_author_delete.querySelector(".action-cancel");
    const confirm_button = dialog_author_delete.querySelector(".action-delete");

   
        dialog_content.textContent = `Czy napewno chcesz usunac z listy autorow?`;
        dialog_header.textContent = `Usuwanie Autora`;
        cancel_button.innerHTML = `<i class="fa-solid fa-xmark"></i>Nie, wroc`;
        confirm_button.innerHTML = `<i class="fa-solid fa-trash-can"></i>Tak, usun`;
    

    SetHiddenValueToFormInDialog(button, dialog_author_delete);

    SetCancelAndCloseDialogOnClickLisneters(dialog_author_delete);

    dialog_author_delete.showModal();
}


const SetHiddenValueToFormInDialog = (button, dialog) => {
    const hidden_value_author_id = button.querySelector(".hidden-authorId").textContent;
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_author_id;
}

const SetCancelAndCloseDialogOnClickLisneters = (dialog) => {
    const cancel_button = dialog.querySelector(".action-cancel");
    const close_dialog_button = dialog.querySelector(".dialog-exit");

    cancel_button.addEventListener('click', () => {
        dialog.close();
    });
    close_dialog_button.addEventListener('click', () => {
        dialog.close();
    });
}




SetButtonsOnClickListeners();