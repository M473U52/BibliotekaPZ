var months = ["STYCZEŃ", "LUTY", "MARZEC", "KWIECEŃ", "MAJ", "CZERWIEC",
    "LIPIEC", "SIERPIEŃ", "WRZESIEŃ", "PAŹDZIERNIK", "LISTOPAD", "GRUDZIEŃ"];

var week = ["PON", "WT", "ŚR", "CZW", "PT", "SOB", "NDZ"];

var selectedDays = [];

var reset = 0;
var lastNumberDays = 0;
var numberDays = 0;

var currentDate = document.getElementById("currentDate");
var month = new Date().getMonth();
var year = new Date().getFullYear();
currentDate.innerText = months[month] + " " + year;
GetDays(month, year);

function ChangeDateLeftArrow() {

    if (month > 0 && year > 2020) {
        month = month - 1;
    }
    else if (month <= 0 && year > 2020) {
        year = year - 1;
        month = 11;
    }

    currentDate.innerText = months[month] + " " + year;
    GetDays(month, year);

};

function ChangeDateRightArrow() {

    if (month < 11) {
        month = month + 1;
    }
    else if (month >= 11) {
        year = year + 1;
        month = 0;
    }

    currentDate.innerText = months[month] + " " + year;
    GetDays(month, year);
};

function GetDays(month, year) {
    var daysContainer = document.getElementById("days-container");

    lastNumberDays = numberDays;
    numberDays = new Date(year, month + 1, 0).getDate();

    while (daysContainer.firstChild) {
        daysContainer.removeChild(daysContainer.firstChild);
    }

    //NAZWY TYGODNIA
    for (var i = 0; i < 7; i++) {
        var dayDiv = document.createElement("div");
        dayDiv.className = "day-box-name-week";
        dayDiv.innerText = week[i];

        daysContainer.appendChild(dayDiv);
    }

    //przesuniecie wg nazwy tygodnia
    var dateToCheck = new Date(year, month, 1);
    var dayOfWeek = dateToCheck.getDay();
    if (dayOfWeek == 2) {
        var dayDiv = document.createElement("div");
        dayDiv.className = "day-box-empty";


        daysContainer.appendChild(dayDiv);
    }
    else if (dayOfWeek == 3) {
        for (var i = 0; i < 2; i++) {
            var dayDiv = document.createElement("div");
            dayDiv.className = "day-box-empty";

            daysContainer.appendChild(dayDiv);
        }
    }
    else if (dayOfWeek == 4) {
        for (var i = 0; i < 3; i++) {
            var dayDiv = document.createElement("div");
            dayDiv.className = "day-box-empty";

            daysContainer.appendChild(dayDiv);
        }
    }
    else if (dayOfWeek == 5) {
        for (var i = 0; i < 4; i++) {
            var dayDiv = document.createElement("div");
            dayDiv.className = "day-box-empty";


            daysContainer.appendChild(dayDiv);
        }
    }
    else if (dayOfWeek == 6) {
        for (var i = 0; i < 5; i++) {
            var dayDiv = document.createElement("div");
            dayDiv.className = "day-box-empty";


            daysContainer.appendChild(dayDiv);
        }
    }
    else if (dayOfWeek == 0) {
        for (var i = 0; i < 6; i++) {
            var dayDiv = document.createElement("div");
            dayDiv.className = "day-box-empty";


            daysContainer.appendChild(dayDiv);
        }
    }

    //DNI
    for (var i = 0; i < numberDays; i++) {
        var dayDiv = document.createElement("div");

        dateToCheck = new Date(year, month, (i + 1));
        dayOfWeek = dateToCheck.getDay();

        if (dayOfWeek == 0) {
            dayDiv.className = "box-day-weekend";
        }
        else {
            dayDiv.className = "day-box";
        }

        dayDiv.innerText = i + 1;

        dayDiv.id = 'day-box-' + (i + 1);

        daysContainer.appendChild(dayDiv);

    }

    if (reset >= 1) {
        RemoveListeners();
    }

    AddListeners(numberDays);

    reset = 1;
};



function RemoveListeners(numberDays) {
    selectedDays = [];
};

function AddListeners(numberDays) {

    const day_boxes = document.querySelectorAll(".day-box");

    day_boxes.forEach(box => {
        box.addEventListener('click', (event) => {
            handleClick(event);
        })
    });
    /*document.getElementById('day-box-15').addEventListener('click', handleClick15);
    document.getElementById('day-box-1').addEventListener('click', handleClick1);
    document.getElementById('day-box-2').addEventListener('click', handleClick2);
    document.getElementById('day-box-3').addEventListener('click', handleClick3);
    document.getElementById('day-box-4').addEventListener('click', handleClick4);
    document.getElementById('day-box-5').addEventListener('click', handleClick5);
    document.getElementById('day-box-6').addEventListener('click', handleClick6);
    document.getElementById('day-box-7').addEventListener('click', handleClick7);
    document.getElementById('day-box-8').addEventListener('click', handleClick8);
    document.getElementById('day-box-9').addEventListener('click', handleClick9);
    document.getElementById('day-box-10').addEventListener('click', handleClick10);
    document.getElementById('day-box-11').addEventListener('click', handleClick11);
    document.getElementById('day-box-12').addEventListener('click', handleClick12);
    document.getElementById('day-box-13').addEventListener('click', handleClick13);
    document.getElementById('day-box-14').addEventListener('click', handleClick14);
    document.getElementById('day-box-16').addEventListener('click', handleClick16);
    document.getElementById('day-box-17').addEventListener('click', handleClick17);
    document.getElementById('day-box-18').addEventListener('click', handleClick18);
    document.getElementById('day-box-19').addEventListener('click', handleClick19);
    document.getElementById('day-box-20').addEventListener('click', handleClick20);
    document.getElementById('day-box-21').addEventListener('click', handleClick21);
    document.getElementById('day-box-22').addEventListener('click', handleClick22);
    document.getElementById('day-box-23').addEventListener('click', handleClick23);
    document.getElementById('day-box-24').addEventListener('click', handleClick24);
    document.getElementById('day-box-25').addEventListener('click', handleClick25);
    document.getElementById('day-box-26').addEventListener('click', handleClick26);
    document.getElementById('day-box-27').addEventListener('click', handleClick27);
    document.getElementById('day-box-28').addEventListener('click', handleClick28);


    if (numberDays >= 29) {
        document.getElementById('day-box-29').addEventListener('click', handleClick29);
    }
    if (numberDays >= 30) {
        document.getElementById('day-box-30').addEventListener('click', handleClick30);
    }
    if (numberDays >= 31) {
        document.getElementById('day-box-31').addEventListener('click', handleClick31);
    }*/

};

const handleClick = (event) => {
    const day_box = event.target;

    day_box.classList.toggle("day-box-checked");
    if (day_box.classList.contains("day-box-checked")) {
        day_box.classList.remove("day-box");
        selectedDays.push(+event.target.textContent);
    }
    else {
        day_box.classList.add("day-box");
        let index = selectedDays.indexOf(+event.target.textContent);
        if (index !== -1) {
            selectedDays.splice(index, +event.target.textContent);
        }
    }
}
/*
function handleClick1(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(1);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(1);

            if (index !== -1) {
                selectedDays.splice(index, 1);
            }
        }
    }
};

function handleClick2(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(2);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(2);

            if (index !== -1) {
                selectedDays.splice(index, 2);
            }
        }
    }
};
function handleClick3(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(3);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(3);

            if (index !== -1) {
                selectedDays.splice(index, 3);
            }
        }
    }
};

function handleClick4(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(4);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(4);

            if (index !== -1) {
                selectedDays.splice(index, 4);
            }
        }
    }
};
function handleClick5(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(5);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(5);

            if (index !== -1) {
                selectedDays.splice(index, 5);
            }
        }
    }
};
function handleClick6(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(6);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(6);

            if (index !== -1) {
                selectedDays.splice(index, 6);
            }
        }
    }
};
function handleClick7(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(7);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(7);

            if (index !== -1) {
                selectedDays.splice(index, 7);
            }
        }
    }
};

function handleClick8(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === "day-box") {
            event.target.className = "day-box-checked";
            selectedDays.push(8);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(8);

            if (index !== -1) {
                selectedDays.splice(index, 8);
            }
        }
    }
};
function handleClick9(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(9);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(9);

            if (index !== -1) {
                selectedDays.splice(index, 9);
            }
        }
    }
};
function handleClick10(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(10);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(10);

            if (index !== -1) {
                selectedDays.splice(index, 10);
            }
        }
    }
};
function handleClick11(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(11);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(11);

            if (index !== -1) {
                selectedDays.splice(index, 11);
            }
        }
    }
};
function handleClick12(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(12);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(12);

            if (index !== -1) {
                selectedDays.splice(index, 12);
            }
        }
    }
};
function handleClick13(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(13);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(13);

            if (index !== -1) {
                selectedDays.splice(index, 13);
            }
        }
    }
};
function handleClick14(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(14);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(14);

            if (index !== -1) {
                selectedDays.splice(index, 14);
            }
        }
    }
};
function handleClick15(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(15);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(15);

            if (index !== -1) {
                selectedDays.splice(index, 15);
            }
        }
    }
};




function handleClick16(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(16);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(16);

            if (index !== -1) {
                selectedDays.splice(index, 16);
            }
        }
    }
};
function handleClick17(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(17);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(17);

            if (index !== -1) {
                selectedDays.splice(index, 17);
            }
        }
    }
};

function handleClick18(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(18);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(18);

            if (index !== -1) {
                selectedDays.splice(index, 18);
            }
        }
    }
};
function handleClick19(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(19);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(19);

            if (index !== -1) {
                selectedDays.splice(index, 19);
            }
        }
    }
};
function handleClick20(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(20);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(20);

            if (index !== -1) {
                selectedDays.splice(index, 20);
            }
        }
    }
};
function handleClick21(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(21);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(21);

            if (index !== -1) {
                selectedDays.splice(index, 21);
            }
        }
    }
};
function handleClick22(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(22);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(22);

            if (index !== -1) {
                selectedDays.splice(index, 22);
            }
        }
    }
};
function handleClick23(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(23);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(23);

            if (index !== -1) {
                selectedDays.splice(index, 23);
            }
        }
    }
};
function handleClick24(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(24);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(24);

            if (index !== -1) {
                selectedDays.splice(index, 24);
            }
        }
    }
};
function handleClick25(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(25);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(25);

            if (index !== -1) {
                selectedDays.splice(index, 25);
            }
        }
    }
};
function handleClick26(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(26);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(26);

            if (index !== -1) {
                selectedDays.splice(index, 26);
            }
        }
    }
};
function handleClick27(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(27);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(27);

            if (index !== -1) {
                selectedDays.splice(index, 27);
            }
        }
    }
};
function handleClick28(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(28);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(28);

            if (index !== -1) {
                selectedDays.splice(index, 28);
            }
        }
    }
};
function handleClick29(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(20);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(29);

            if (index !== -1) {
                selectedDays.splice(index, 29);
            }
        }
    }
};
function handleClick30(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(30);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(30);

            if (index !== -1) {
                selectedDays.splice(index, 30);
            }
        }
    }
};
function handleClick31(event) {
    if (event.target.tagName === 'DIV') {
        if (event.target.className === 'day-box') {
            event.target.className = "day-box-checked";
            selectedDays.push(31);
        }
        else if (event.target.className === "day-box-checked") {
            event.target.className = "day-box";
            var index = selectedDays.indexOf(31);

            if (index !== -1) {
                selectedDays.splice(index, 31);
            }
        }
    }
};*/