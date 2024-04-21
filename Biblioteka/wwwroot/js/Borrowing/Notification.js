const notification_div = document.querySelector(".notification_div");
const hide_notification_button = document.querySelector(".hide-notification");
console.log(hide_notification_button)
const CloseOrHideNotification = () => {
    if (notification_div !== null && hide_notification_button!==null) {

        hide_notification_button.addEventListener('click', () => {
            notification_div.style.animation = 'fadeOut 1s ease-in-out forwards';
        });

        setTimeout(() => {
            notification_div.style.animation = 'fadeOut 1s ease-in-out forwards';
            hide_notification_button.removeEventListener('click');
        }, 5000);
    }
}

CloseOrHideNotification();
