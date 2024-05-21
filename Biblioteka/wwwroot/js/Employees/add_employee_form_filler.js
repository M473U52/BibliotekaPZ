import { faker } from 'https://esm.sh/@faker-js/faker';

const inputs_divs = document.querySelectorAll(".form-floating");
let inputs = {
    name: undefined,
    surname: undefined,
    birthDate: undefined,
    email: undefined,
    pesel: undefined,
    street: undefined,
    houseNumber: undefined,
    town: undefined,
    zipCode: undefined,
    phoneNumber: undefined,
    positionIDSelect: undefined,
    password: undefined,
    passwordConfirm: undefined,
}
let positionIds = [];

const GetInputs = () => {
    let i = 0;
    for (let key in inputs) {
        if (i === 11) i = 12;
        inputs[key] = inputs_divs[i].firstElementChild;
        i++;
    }
}
const GetRealDataForRelations = () => {
    positionIds = [...inputs.positionIDSelect.querySelectorAll("option")].map(option => option.value);
}
const FillForm = () => {
    const name = faker.person.firstName(); 
    const surname = faker.person.lastName();
    inputs['name'].value = name;
    inputs['surname'].value = surname;
    const date = faker.date.birthdate({ min: 1950, max: 2006, mode: 'year' })
    const formattedDate = date.toISOString().slice(0, 16);
    inputs['birthDate'].value = formattedDate;
    const email = faker.internet.email({ firstName: name, lastName: surname });
    inputs['email'].value = email;
    inputs['pesel'].value = faker.number.int({ min: 10000000000, max: 99999999999 });
    inputs['street'].value = faker.location.street();
    inputs['houseNumber'].value = faker.number.int({ min: 1, max: 100 });
    inputs['town'].value = faker.location.city();
    inputs['zipCode'].value = faker.location.zipCode('##-###'); 
    inputs['phoneNumber'].value = faker.string.numeric(9)//faker.phone.number('### ### ###')
    console.log(positionIds)
    console.log(Math.floor(Math.random() * (positionIds.length-1))+1);
    inputs['positionIDSelect'].value = positionIds[Math.floor(Math.random() * (positionIds.length - 1)) + 1];
    const passwd = faker.internet.password(
        {
            length: 15,
            pattern: /^[0-9a-zA-Z]$/,
            prefix: "a7C"
        });
    inputs['password'].value = passwd;
    inputs['passwordConfirm'].value = passwd;

    const info_block = document.querySelector(".password_text");
    info_block.innerHTML = `Dane do logowania to: <br/>
                            Login: ${email} <br/>
                            Hasło: ${passwd}`;
}
const AddFillFormButtonEventListener = () => {
    const fill_form_btn = document.querySelector(".fill-form-btn");
    fill_form_btn.addEventListener('click', () => {
        FillForm();
    });
}

GetInputs();
GetRealDataForRelations();
AddFillFormButtonEventListener();
