import { faker } from 'https://esm.sh/@faker-js/faker';

const inputs_divs = document.querySelectorAll(".form-floating");
let inputs = {
    name: undefined,
    surname: undefined,
    birthDate: undefined,
    country: undefined,
    nickname: undefined,
    description: undefined,
}

const GetInputs = () => {
    let i = 0;
    for (let key in inputs) {
        inputs[key] = inputs_divs[i].firstElementChild;
        i++;
    }
}

const FillForm = () => {
    const name = faker.person.firstName();
    const surname = faker.person.lastName();
    inputs['name'].value = name;
    inputs['surname'].value = surname;
    const date = faker.date.birthdate({ min: 1700, max: 2006, mode: 'year' })
    const formattedDate = date.toISOString().slice(0, 16);
    inputs['birthDate'].value = formattedDate;
    inputs['country'].value = faker.location.country();
    inputs['nickname'].value = faker.lorem.word();
    inputs['description'].value = faker.lorem.words(6);
}

const AddFillFormButtonEventListener = () => {
    const fill_form_btn = document.querySelector(".fill-form-btn");
    fill_form_btn.addEventListener('click', () => {
        FillForm();
    });
}

GetInputs();
AddFillFormButtonEventListener();