const confirmation_reservation_dialog = document.querySelector("#confirmation_reservation_dialog");
const confirmation_buttons_arr = document.querySelectorAll(".borrowing_confirmation_button");

const delete_buttons_arr = document.querySelectorAll(".action-delete");
const dialog_reservation_delete = document.querySelector("#delete_reservation_dialog");

const SetButtonsOnClickListeners = () => {
    delete_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowBorrowingDeleteDialog(button);
        });
    });

    confirmation_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowConfirmBorrowingDialog(button);
        });
    });
}

const GetConfirmAndDate = (button) => {
    const parent_tr = button.parentNode.parentNode;
    let confirm = parent_tr.querySelector(".td_confirm");
    const room_number = parent_tr.querySelector(".td_room_number").textContent;
    const reservation_date = parent_tr.querySelector(".td_reservation_date").textContent;
   
    if (confirm !== null) {
        confirm = confirm.textContent;
    }
    return [room_number, confirm, reservation_date];
}

const ShowBorrowingDeleteDialog = (button) => {
    const [room_number, confirm, reservation_date] = GetConfirmAndDate(button);

    let reservation_date_OD = reservation_date.trim().split(' ')[0];
    const dialog_content = dialog_reservation_delete.querySelector(".dialog_content");
    const dialog_header = dialog_reservation_delete.querySelector(".dialog_header");
    const cancel_button = dialog_reservation_delete.querySelector(".action-cancel");
    const confirm_button = dialog_reservation_delete.querySelector(".action-delete");

    if (confirm!==null && confirm.trim().localeCompare('Potwierdzone') == 0) {
        dialog_content.textContent = `Czy napewno chcesz usunac rezerwacje sali nr ${room_number} z dnia: ${reservation_date_OD}?`;
        dialog_header.textContent = `Usuwanie rezerwacji`;
    }
    else {   
        dialog_content.textContent = `Czy napewno chcesz anulowac rezerwacje sali nr ${room_number} z dnia ${reservation_date_OD}?`;
        dialog_header.textContent = `Anulowanie rezerwacji`;
        cancel_button.innerHTML = `<i class="fa-solid fa-xmark"></i>Nie, wroc`;
        confirm_button.innerHTML = `<i class="fa-solid fa-trash-can"></i>Tak, anuluj`;
    }

    SetHiddenValueToFormInDialog(button, dialog_reservation_delete);

    SetCancelAndCloseDialogOnClickLisneters(dialog_reservation_delete);

    dialog_reservation_delete.showModal();
}

const ShowConfirmBorrowingDialog = (button) => {

    const dialog_content = confirmation_reservation_dialog.querySelector(".dialog_content");
    dialog_content.textContent = `Czy na pewno chcesz potwierdzic wydanie kluczy?`

    SetHiddenValueToFormInDialog(button, confirmation_reservation_dialog);

    SetCancelAndCloseDialogOnClickLisneters(confirmation_reservation_dialog);

    confirmation_reservation_dialog.showModal();
}

const SetHiddenValueToFormInDialog = (button, dialog) => {
    const hidden_value_borrow_id = button.querySelector(".hidden-resId").textContent;
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_borrow_id;
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