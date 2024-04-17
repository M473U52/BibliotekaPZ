const stars = document.querySelectorAll(".add_opinion_star");
const stars_div = document.querySelector(".stars_div");
let opinion_added = false;
const user_rating = document.querySelector(".your_rating");

const avg_opinions_stars = document.querySelectorAll(".avg_star");
const avg_opinion = document.querySelector(".book_rating_avg");

const other_comments_ratings = document.querySelectorAll(".user_rating");
const other_opinions_stars = document.querySelectorAll(".opinion_star");

const select = document.querySelector("#sortOpinions");

const deleteButton = document.querySelector(".delete_comment");

AddHoverStarsListeners();
FillOtherCommentsStars();
AddClickStarListener();
FillAvgStars(); 

AddSelectEventListener();

AddDeleteButtonEvent();
AddCloseDialogEvent();

function AddDeleteButtonEvent() {
    const dialog = document.querySelector(".delete_dialog");
    if (deleteButton !== null) {
        deleteButton.onclick = () => {
            dialog.showModal();
        }
    }
}
function AddCloseDialogEvent() {
    const dialog = document.querySelector(".delete_dialog");
    const cancelButton = document.querySelector(".cancel_button_dialog");
    console.log("here");
    cancelButton.onclick = () => {
        dialog.close();
    }
}

function AddSelectEventListener() {
    select.addEventListener("change", () => {
        sortOpinions(select.value);
    })
}
function sortOpinions(sortOption) {
    const container = document.querySelector("#opinions-container");
    const comments = Array.from(container.querySelectorAll(".comment"));

    switch (sortOption) {
        case "dateDesc": {
            comments.sort((a, b) => {
                let partsA = a.querySelector(".added-date").textContent.split(".");
                let formattedDateA = partsA[1] + "/" + partsA[0] + "/" + partsA[2];

                let partsB = b.querySelector(".added-date").textContent.split(".");
                let formattedDateB = partsB[1] + "/" + partsB[0] + "/" + partsB[2];

                let dateA = new Date(formattedDateA)
                let dateB = new Date(formattedDateB)

                return dateB - dateA;
            })
            break;
        }
        case "dateAsc": {
            comments.sort((a, b) => {
                let partsA = a.querySelector(".added-date").textContent.split(".");
                let formattedDateA = partsA[1] + "/" + partsA[0] + "/" + partsA[2];

                let partsB = b.querySelector(".added-date").textContent.split(".");
                let formattedDateB = partsB[1] + "/" + partsB[0] + "/" + partsB[2];

                let dateA = new Date(formattedDateA)
                let dateB = new Date(formattedDateB)

                return dateA - dateB;
            })
            break;
        }
        case "opinionsDesc": {
            comments.sort((a, b) => {
                let opinionA = +a.querySelector(".user_rating").textContent
                let opinionB = +b.querySelector(".user_rating").textContent

                return opinionB - opinionA;
            })
            break;
        }
        case "opinionsAsc": {
            comments.sort((a, b) => {
                let opinionA = +a.querySelector(".user_rating").textContent
                let opinionB = +b.querySelector(".user_rating").textContent

                return opinionA - opinionB;
            })
            break;
        }
    }
    comments.forEach(comment => {
        container.appendChild(comment)
    })
    
}
function FillAvgStars() {
    if (avg_opinions_stars != null && avg_opinion!=null) {
        const avg_value = +avg_opinion.textContent.split(",")[0];
        for (let i = 0; i < avg_opinions_stars.length; i++) {
            if (i < avg_value) {
                console.log(i);
                avg_opinions_stars[i].style.color = "orange";
            }
        }
    }
}

function FillOtherCommentsStars() {
    if (other_comments_ratings != null && other_opinions_stars != null) {
        let index = 0;
        other_comments_ratings.forEach(rating => {
            for (let i = index; i < 5 + index; i++) {
                if (i%5 < parseFloat(rating.textContent)) {
                    other_opinions_stars[i].style.color = "orange";
                    
                }
                else {
                    other_opinions_stars[i].style.color = "black";
                }
            }
            index += 5;
        })
    }
}
function AddClickStarListener() {
    if (stars.length!=0) {
        stars.forEach(star => {
            star.onclick = () => {
                opinion_added = !opinion_added;
                for (let i = 0; i < stars.length; i++) {
                    stars[i].style.color = "orange";
                    if (stars[i] == star) {
                        if (opinion_added) {
                            console.log(user_rating);
                            user_rating.value = i + 1;
                        }
                        else {
                            user_rating.value = "";
                        }
                        console.log("clicked_star: ", i + 1);
                        break;
                    }
                }
            }
        });
    }   
}
function AddHoverStarsListeners() {
    if (stars.length != 0) {
        stars.forEach(star => {
            star.onmouseover = () => {
                if (!opinion_added) {
                    for (let i = 0; i < stars.length; i++) {
                        stars[i].style.color = "orange";
                        if (stars[i] == star) {
                            break;
                        }
                    }
                }
            }
            star.onmouseleave = () => {
                if (!opinion_added) {
                    star.style.color = "black";
                }
            }
        })
        stars_div.onmouseleave = () => {
            if (!opinion_added) {
                stars.forEach(star => {
                    star.style.color = "black";
                })
            }
        }          
    }       
}