const delete_buttons_arr = document.querySelectorAll(".action-delete");
const dialog_borrowing_delete = document.querySelector("#delete_borrowing_dialog");
console.log(delete_buttons_arr);

const confirmation_borrowing_dialog = document.querySelector("#confirmation_borrowing_dialog");
const confirmation_buttons_arr = document.querySelectorAll(".borrowing_confirmation_button");

const return_book_dialog = document.querySelector("#confirmation_return_dialog");
const return_buttons_arr = document.querySelectorAll(".book_return_button");

const payment_confirmation_dialog = document.querySelector("#confirmation_payment");
const payment_confirmation_buttons_arr = document.querySelectorAll(".payment_confirm_button");

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

    return_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowReturnBookDialog(button);
        });
    });
    payment_confirmation_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowPaymentConfirmationDialog(button);
        });
    });

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

const GetReaderBookAndBorrowDate = (button) => {
    const parent_tr = button.parentNode.parentNode;
    let reader = parent_tr.querySelector(".td_reader_name")//.textContent;
    const book = parent_tr.querySelector(".td_book_title").textContent;
    const borrow_date = parent_tr.querySelector(".td_borrow_date").textContent;
    if (reader !== null) {
        reader = reader.textContent
    }
    return [reader, book, borrow_date];
}
const SetHiddenValueToFormInDialog = (button, dialog) => {
    const hidden_value_borrow_id = button.querySelector(".hidden-borrowId").textContent;
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_borrow_id;
}

const ShowPaymentConfirmationDialog = (button) => {
    const [ reader, book, borrow_date ] = GetReaderBookAndBorrowDate(button);

    const dialog_content = payment_confirmation_dialog.querySelector(".dialog_content");
    dialog_content.textContent = `Czy chcesz potwierdzić opłatę książki \"${book}\" wypożyczonej przez czytelnika ${reader} \
                                   dnia ${borrow_date} ?`

    SetHiddenValueToFormInDialog(button, payment_confirmation_dialog);

    SetCancelAndCloseDialogOnClickLisneters(payment_confirmation_dialog);

    payment_confirmation_dialog.showModal();
}

const ShowReturnBookDialog = (button) => {
    const [ reader, book, borrow_date ] = GetReaderBookAndBorrowDate(button);

    const dialog_content = return_book_dialog.querySelector(".dialog_content");
    dialog_content.textContent = `Czy chcesz potwierdzić zwrot książki \"${book}\" wypożyczonej przez czytelnika ${reader} \
                                   dnia ${borrow_date} ?`

    SetHiddenValueToFormInDialog(button, return_book_dialog);

    SetCancelAndCloseDialogOnClickLisneters(return_book_dialog);

    return_book_dialog.showModal();
}

const ShowConfirmBorrowingDialog = (button) => {
    const [ reader, book, borrow_date ] = GetReaderBookAndBorrowDate(button);

    const dialog_content = confirmation_borrowing_dialog.querySelector(".dialog_content");
    dialog_content.textContent = `Czy chcesz potwierdzić wypożyczenie książki \"${book}\" czytelnika ${reader} \
                                   z dnia ${borrow_date} ?`

    SetHiddenValueToFormInDialog(button, confirmation_borrowing_dialog);

    SetCancelAndCloseDialogOnClickLisneters(confirmation_borrowing_dialog);

    confirmation_borrowing_dialog.showModal();
}

const ShowBorrowingDeleteDialog = (button) => {
    const [ reader, book, borrow_date ] = GetReaderBookAndBorrowDate(button);

    if (reader !== null) {
        const dialog_content = dialog_borrowing_delete.querySelector(".dialog_content");
        dialog_content.textContent = `Czy napewno chcesz usunąć wypożyczenie książki \"${book}\" czytelnika ${reader} \
                                   z dnia ${borrow_date} ?`;
    }
    else {
        const dialog_content = dialog_borrowing_delete.querySelector(".dialog_content");
        dialog_content.textContent = `Czy napewno chcesz anulować wypożyczenie książki \"${book}\"  \
                                   z dnia ${borrow_date} ?`;
    }
    

    SetHiddenValueToFormInDialog(button, dialog_borrowing_delete);

    SetCancelAndCloseDialogOnClickLisneters(dialog_borrowing_delete);

    dialog_borrowing_delete.showModal();
}

SetButtonsOnClickListeners();