import { faker } from 'https://esm.sh/@faker-js/faker';

const inputs_divs = document.querySelectorAll(".form-floating");
let inputs = {
    titleInput: undefined,
    ISBNInput: undefined,
    descriptionInput: undefined,
    availableCopysInput: undefined,
    releaseInput: undefined,
    genreIDSelect: undefined,
    tagIDSelect: undefined,
    typeIDSelect: undefined,
    publisherIDSelect: undefined,
    authorIDSelect: undefined,
    floorInput: undefined,
    alleyInput: undefined,
    rowNumber: undefined
}
let genreIds = [];
let tagIds = [];
let typeIds = [];
let publisherIds = [];
let authorIds = [];

const GetInputs = () => {
    let i = 0;
    for (let key in inputs) {
        if (i === 10) {
            i = 13;
        }   
        inputs[key] = inputs_divs[i].firstElementChild;
        i++;
    }
}
const GetRealDataForRelations = () => {
    genreIds = [...inputs.genreIDSelect.querySelectorAll("option")].map(option => option.value);
    tagIds = [...inputs.tagIDSelect.querySelectorAll("option")].map(option => option.value);
    typeIds = [...inputs.typeIDSelect.querySelectorAll("option")].map(option => option.value);
    publisherIds = [...inputs.publisherIDSelect.querySelectorAll("option")].map(option => option.value);
    authorIds = [...inputs.authorIDSelect.querySelectorAll("option")].map(option => option.value);
}
const FillForm = () => {
    inputs['titleInput'].value = faker.lorem.words(3);
    inputs['ISBNInput'].value = faker.number.int({ min: 1000000000000, max: 9999999999999 });
    inputs['descriptionInput'].value = faker.lorem.words(6);
    inputs['availableCopysInput'].value = faker.number.int({ min: 1, max: 100 });
    const date = faker.date.birthdate({ min: 1900, max: 2023, mode: 'year' })//new Date(2020, 4, 17, 12, 30);
    const formattedDate = date.toISOString().slice(0, 16);
    inputs['releaseInput'].value = formattedDate;
    inputs['genreIDSelect'].value = genreIds[Math.floor(Math.random() * genreIds.length)];
    inputs['tagIDSelect'].value = tagIds[Math.floor(Math.random() * tagIds.length)];
    inputs['typeIDSelect'].value = typeIds[Math.floor(Math.random() * typeIds.length)];
    inputs['publisherIDSelect'].value = publisherIds[Math.floor(Math.random() * publisherIds.length)];
    inputs['authorIDSelect'].value = authorIds[Math.floor(Math.random() * authorIds.length)];
    inputs['floorInput'].value = faker.number.int({ min: 1, max: 3 });
    inputs['alleyInput'].value = faker.number.int({ min: 1, max: 3 });
    inputs['rowNumber'].value = faker.number.int({ min: 1, max: 3 });
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
