const delete_buttons_arr = document.querySelectorAll(".action-delete");
const delete_genre_dialog = document.querySelector("#delete_genre_dialog");


const SetButtonsOnClickListeners = () => {
    delete_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowGenreDeleteDialog(button);
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

const SetHiddenValueToFormInDialog = (button, dialog) => {
    const hidden_value_genre_id = button.querySelector(".hidden-genreId").textContent;
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_genre_id;
}

const ShowGenreDeleteDialog = (button) => {
    const genreName = button.parentNode.parentNode.querySelector(".genre_name").textContent;
    const dialog_content = delete_genre_dialog.querySelector(".dialog_content");
    dialog_content.textContent = `Czy napewno chcesz usunąć gatunek: \"${genreName}\" ?`;

    SetHiddenValueToFormInDialog(button, delete_genre_dialog);

    SetCancelAndCloseDialogOnClickLisneters(delete_genre_dialog);

    delete_genre_dialog.showModal();
}

SetButtonsOnClickListeners();