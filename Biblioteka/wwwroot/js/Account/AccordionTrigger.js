const arrow = document.querySelector(".roll_out_accordion");
const accordion = document.querySelector(".email_accordion_content");

const SetOnClickListeners = () => {
    arrow.addEventListener('click', () => {
        accordion.classList.toggle("accordion-active");
        arrow.classList.toggle("rotate");
    });
}

SetOnClickListeners();