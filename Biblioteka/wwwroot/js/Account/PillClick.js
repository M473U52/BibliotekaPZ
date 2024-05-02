const currentUrl = window.location.href;
const URL = currentUrl.split("/").reverse()[0];
const pills = document.querySelectorAll(".panel_navigation_item");

const RemoveAllColorsFromPills = () => {
    pills.forEach(pill => {
        pill.classList.remove("active_pill");
    });
}

const MapURLToPill = () => {
    RemoveAllColorsFromPills();
    console.log(URL);
    switch (URL) {
        case 'ChangePassword': {
            pills[1].classList.add("active_pill")
            console.log(pills[1]);
            break;
        }
        case 'TwoFactorAuthentication': {
            pills[2].classList.add("active_pill")
            break;
        }
        case 'PersonalData': {
            pills[3].classList.add("active_pill")
            break;
        }
        case 'NotifyFreq': {
            pills[4].classList.add("active_pill")
            break;
        }  
        default: {
            pills[0].classList.add("active_pill")
            break;
        }
    }
}

MapURLToPill();

