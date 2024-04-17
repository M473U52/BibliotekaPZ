const faq_accordion = document.querySelector(".faq-accordion");

AddButtonsEventListeners();


function AddButtonsEventListeners() {
    const buttons = document.querySelectorAll(".show_faq_content_button");
    buttons.forEach(button => {
        button.onclick = () =>{
            ShowOrHideAccordion(button);
        }
    })
}
function ShowOrHideAccordion(button) {
    const container = button.parentNode.parentNode;
    const accordion = container.querySelector(".faq-accordion");
    accordion.classList.toggle("accordion_active");
    button.classList.toggle("active_button");
}