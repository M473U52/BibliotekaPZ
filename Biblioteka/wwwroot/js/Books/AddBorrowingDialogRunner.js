const SetAddBorrowingButtonOnClickListener = () => {
    const borrow_btn = document.querySelector(".book_borrow_btn");
    borrow_btn.addEventListener('click', () => {
        ShowBorrowingAddDialog();
    });
}

const SetCancelAndCloseDialogOnClickListeners = (dialog) => {
    const cancel_button = dialog.querySelector(".action-cancel");
    const close_dialog_button = dialog.querySelector(".dialog-exit");

    cancel_button.addEventListener('click', () => {
        dialog.close();
    });
    close_dialog_button.addEventListener('click', () => {
        dialog.close();
    });
}

const ShowBorrowingAddDialog = () => {
    const dialog = document.querySelector("#borrowing_dialog");

    const book_title = document.querySelector(".book_title").textContent;
    const dialog_header = dialog.querySelector(".dialog_header");
    dialog_header.textContent = `Wypożyczenie książki \"${book_title}\"`;

    SetHiddenValueToFormInDialog(dialog);
    SetCancelAndCloseDialogOnClickListeners(dialog);

    dialog.showModal();
}
const SetHiddenValueToFormInDialog = (dialog) => {
    const currentURL = window.location.href;
    const hidden_value_borrow_id = currentURL.split('=')[1];
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_borrow_id;
}
SetAddBorrowingButtonOnClickListener();
