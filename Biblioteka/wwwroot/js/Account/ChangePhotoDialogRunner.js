const dialog = document.querySelector("#change_profile_photo_dialog");
const photo_container = document.querySelector(".photo-container");

const ShowDialog = () => {
    photo_container.addEventListener('click', () => {
        dialog.showModal();
    });
}
const SetCancelAndCloseDialogOnClickLisneters = () => {
    const cancel_button = dialog.querySelector(".action-cancel");

    cancel_button.addEventListener('click', () => {
        dialog.close();
    });
    const close_dialog_button = dialog.querySelector(".dialog-exit");
    close_dialog_button.addEventListener('click', () => {
        dialog.close();
    });
}

ShowDialog();
SetCancelAndCloseDialogOnClickLisneters();