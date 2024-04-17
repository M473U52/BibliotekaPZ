const available_books_filters = document.querySelectorAll(".available_books_filter");
const genre_books_filters = document.querySelectorAll(".genre_book_filter");
const type_books_filters = document.querySelectorAll(".type_book_filter");
const clear_filters_div = document.querySelector(".remove_all_filters_container");

let defaultBooks = document.querySelectorAll(".book_div");
let actualBooks = document.querySelectorAll(".book_div");
let filter_list = [];

const SetFilterOnClickListeners = () => {

    available_books_filters.forEach(el => {
        el.addEventListener('click', () => TryFilterBookList(el.textContent, "available_books"));
    });
    genre_books_filters.forEach(el => {
        el.addEventListener('click', () => TryFilterBookList(el.textContent, "genre_books"));
    });
    type_books_filters.forEach(el => {
        el.addEventListener('click', () => TryFilterBookList(el.textContent, "type_books"));
    });
    clear_filters_div.addEventListener('click', () => ClearFilters());
}

const ClearFilters = () => {
    filter_list = [];
    SetFiltersToDefault();
    RemoveAllFilterNodes();
}
const RemoveAllFilterNodes = () => {
    const nodes_container = document.querySelector(".used_filters");
    const nodes_divs = document.querySelectorAll(".used_filter");

    nodes_divs.forEach(node => {
        nodes_container.removeChild(node);
    })
}
const TryFilterBookList = (filter_value, type) => {
    filter_value = filter_value.split("(")[0].trim();
    if (filter_list.includes(filter_value)) {
        return;
    }

    else {
        let found_element = undefined;
        switch (type) {
            case "available_books": {
                for (el of available_books_filters){
                    let available_books_value = el.textContent.split("(")[0].trim();
                    found_element = filter_list.find(item => item === available_books_value);
                    if (found_element !== filter_value && found_element !== undefined) {
                        break;
                    }
                }
                break;
            }
            case "genre_books": {
                for (el of genre_books_filters) { 
                    let genre_value = el.textContent.split("(")[0].trim();
                    found_element = filter_list.find(item => item === genre_value);
                    if (found_element !== filter_value && found_element !== undefined) {
                        break;
                    }
                }
                break;
            }
            case "type_books": {
                for (el of type_books_filters){
                    let type_value = el.textContent.split("(")[0].trim();
                    found_element = filter_list.find(item => item === type_value);
                    if (found_element !== filter_value && found_element !== undefined) {
                        break;
                    }
                }
                break;
            }
        }
        if (found_element !== undefined) {

            const index = filter_list.indexOf(found_element);
            if (index !== -1) { 
                filter_list[index] = filter_value;
            }
            SetFiltersToDefault();

            filter_list.forEach(val => {
                FilterBookList(val, false);
            });

            return;
        }
        else {
            filter_list.push(filter_value);

            FilterBookList(filter_value, true);
        }
    }
}
const FilterBookList = (filter_value, isNewNode) => {
   
    for (el of available_books_filters) {
        if (el.textContent.split("(")[0].trim() === filter_value) {
            FilterAvailableBook(filter_value);
            if (isNewNode) {
                AddNewFilterNode("Dostępność: ", filter_value);
            }
            else{
                UpdateFilterNode("Dostępność: ", filter_value);
            }
            break;
        }
    }
    for (el of genre_books_filters) {
        if (el.textContent.split("(")[0].trim() === filter_value) {
            FilterGenreBook(filter_value);
            if (isNewNode) {
                AddNewFilterNode("Gatunek: ", filter_value);
            }
            else {
                UpdateFilterNode("Gatunek: ", filter_value);
            }
            break;
        }
    }
    for (el of type_books_filters) {
        if (el.textContent.split("(")[0].trim() === filter_value) {
            FilterTypeBook(filter_value);
            if (isNewNode) {
                AddNewFilterNode("Rodzaj: ", filter_value);
            }
            else {
                UpdateFilterNode("Rodzaj: ", filter_value);
            }
            break;
        }
    }
}
const UpdateFilterNode = (type, value) => {
    const used_filter_div = document.querySelectorAll(".used_filter");

    for (el of used_filter_div) {
        const element_p = el.querySelector("p")
        if ( element_p.textContent.split(":")[0] === type.split(":")[0]) {
            element_p.textContent = `${type}${value}`;
            break;
        } 
    }
}
const AddNewFilterNode = (type, value) => {
    const used_filter_div = document.querySelector(".used_filters");

    const newNode = document.createElement('div');
    newNode.classList.add('used_filter');

    const p_filter_name = document.createElement('p');
    p_filter_name.classList.add('filter_name');
    p_filter_name.textContent = `${type}${value}`;

    const buttonRemove = document.createElement('button');
    buttonRemove.classList.add('remove_filter');
    buttonRemove.innerHTML = '<i class="fa-solid fa-xmark"></i>';

    newNode.appendChild(p_filter_name);
    newNode.appendChild(buttonRemove);

    used_filter_div.appendChild(newNode);

    buttonRemove.addEventListener('click', () => {
        RemoveFilter(used_filter_div, newNode, p_filter_name.textContent.split(": ")[1]);
    });
}

const RemoveFilter = (used_filter_div, newNode, value) => {
    used_filter_div.removeChild(newNode);
    filter_list = filter_list.filter(item => item !== value);

    SetFiltersToDefault();

    filter_list.forEach(val => {
        FilterBookList(val, false);
    });
}

const FilterGenreBook = (filter_value) => {
    filter_value = filter_value.split("(")[0].trim();

    actualBooks = Array.from(actualBooks).filter(book => {
        if (book.querySelector(".book_genre").textContent === filter_value) {
            return true;
        }
        else {
            book.style.display = 'none';
            return false;
        }
    });
}

const FilterTypeBook = (filter_value) => {
    filter_value = filter_value.split("(")[0].trim();

    actualBooks = Array.from(actualBooks).filter(book => {
        if (book.querySelector(".book_type").textContent == filter_value) {
            return true;
        }
        else {
            book.style.display = 'none';
            return false;
        }
    });
}
const FilterAvailableBook = (filter_value) => {
    filter_value = filter_value.split("(")[0].trim();

    switch (filter_value) {
        case "Dostępne": {
            actualBooks = Array.from(actualBooks).filter(book => {
                if (parseInt(book.querySelector(".book_quantity").textContent) > 0) {
                    return true;
                }
                else {
                    book.style.display = 'none';
                    return false;
                }
            });
            break;
        }
        case "Mała ilość": {
            actualBooks = Array.from(actualBooks).filter(book => {
                const low_value = parseInt(book.querySelector(".book_quantity").textContent) > 0;
                const high_value = parseInt(book.querySelector(".book_quantity").textContent) < 10;
                if (low_value && high_value) return true;
                else {
                    book.style.display = 'none';
                    return false;
                }        
            });
            break;
        }
        case "Niedostępne": {
            actualBooks = Array.from(actualBooks).filter(book => {
                if (parseInt(book.querySelector(".book_quantity").textContent) === 0) {
                    return true;
                }
                else {
                    book.style.display = 'none';
                    return false;
                }
            });
            break;
        }      
    }
}

const SetFiltersToDefault = () => {

    defaultBooks.forEach(book => {
        book.style.display = "flex";
    })
    actualBooks = document.querySelectorAll(".book_div");
}

SetFilterOnClickListeners();