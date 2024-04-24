const confirmation_borrowing_dialog = document.querySelector("#confirmation_borrowing_dialog");
const confirmation_buttons_arr = document.querySelectorAll(".borrowing_confirmation_button");

const delete_buttons_arr = document.querySelectorAll(".action-delete");
const dialog_borrowing_delete = document.querySelector("#delete_borrowing_dialog");


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

    const ShowBorrowingDeleteDialog = (button) => {

        const [confirm, reservation_date] = GetConfirmAndDate(button);
        console.log(confirm.localeCompare('Potwierdzone'));

        let reservation_date_OD = reservation_date.trim().split(' ')[0];
        if (confirm.trim().localeCompare('Potwierdzone') == 0) {
            const dialog_content = dialog_borrowing_delete.querySelector(".dialog_content");
            dialog_content.textContent = `Czy napewno chcesz usunac rezerwacje tej sali z dnia: ${reservation_date_OD}?`;
        }
        else {
            const dialog_content = dialog_borrowing_delete.querySelector(".dialog_content");
            dialog_content.textContent = `Czy napewno chcesz anulowac rezerwacje tej sali z dnia ${reservation_date_OD}?`;
        }

        

        SetHiddenValueToFormInDialog(button, dialog_borrowing_delete);

        SetCancelAndCloseDialogOnClickLisneters(dialog_borrowing_delete);

        dialog_borrowing_delete.showModal();
    }

const ShowConfirmBorrowingDialog = (button) => {

    const dialog_content = confirmation_borrowing_dialog.querySelector(".dialog_content");
    dialog_content.textContent = `Czy na pewno chcesz potwierdzic wydanie kluczy?`

    SetHiddenValueToFormInDialog(button, confirmation_borrowing_dialog);

    SetCancelAndCloseDialogOnClickLisneters(confirmation_borrowing_dialog);

    confirmation_borrowing_dialog.showModal();
}





const SetHiddenValueToFormInDialog = (button, dialog) => {
    const hidden_value_borrow_id = button.querySelector(".hidden-borrowId").textContent;
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_borrow_id;
}


const SetCancelAndCloseDialogOnClickLisneters = (dialog) => {
    const cancel_button = dialog.querySelector(".action-cancel");
    cancel_button.addEventListener('click', () => {
        dialog.close();
    });
    const close_dialog_button = dialog.querySelector(".dialog-exit");
    close_dialog_button.addEventListener('click', () => {
        dialog.close();
    });
    }


const GetConfirmAndDate = (button) => {
    const parent_tr = button.parentNode.parentNode;
    let confirm = parent_tr.querySelector(".td_confirm");
    const reservation_date = parent_tr.querySelector(".td_reservation_date").textContent;
    if (confirm !== null) {
        confirm = confirm.textContent;
    }
    return [confirm, reservation_date];
}

    SetButtonsOnClickListeners();