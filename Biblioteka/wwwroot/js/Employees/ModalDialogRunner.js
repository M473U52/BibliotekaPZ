const delete_buttons_arr = document.querySelectorAll(".action-delete");
const dialog_employee_delete = document.querySelector("#delete_employee_dialog");
console.log(dialog_employee_delete);

const SetButtonsOnClickListeners = () => {
    delete_buttons_arr.forEach(button => {
        button.addEventListener('click', () => {
            ShowEmployeeDeleteDialog(button);
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

const GetEmployeeInfo = (button) => {
    const parent_tr = button.parentNode.parentNode;
    const employee_name = parent_tr.querySelector(".td_employee_name").textContent;
    const employee_surname = parent_tr.querySelector(".td_employee_surname").textContent;
    const employment_date = parent_tr.querySelector(".td_employment_date").textContent;

    const employee = `${employee_name} ${employee_surname}`;
    return [employee, employment_date];
}
const SetHiddenValueToFormInDialog = (button, dialog) => {
    const hidden_value_employee_id = button.querySelector(".hidden-employeeId").textContent;
    const hidden_input_in_dialog = dialog.querySelector("input[type=hidden]");
    hidden_input_in_dialog.value = hidden_value_employee_id;
}

const ShowEmployeeDeleteDialog = (button) => {
    const [employee, employment_date] = GetEmployeeInfo(button);
    const dialog_content = dialog_employee_delete.querySelector(".dialog_content");
    dialog_content.textContent = `Czy napewno chcesz usunąć pracownika ${employee} zatrudnionego dnia ${employment_date} ?`;

    SetHiddenValueToFormInDialog(button, dialog_employee_delete);

    SetCancelAndCloseDialogOnClickLisneters(dialog_employee_delete);

    dialog_employee_delete.showModal();
}

SetButtonsOnClickListeners();