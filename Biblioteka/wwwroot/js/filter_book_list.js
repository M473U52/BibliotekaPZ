document.addEventListener('DOMContentLoaded', function () {
    const selectElementGenre = document.querySelector('#genreDropdown>select');
    const selectElementTag = document.querySelector('#tagDropdown>select');
    const selectElementAuthor = document.querySelector('#authorDropdown>select');

    console.log(selectElementGenre)
    // Genre
    selectElementGenre.addEventListener('change', () => {
        let selectedOptions = Array.from(selectElementGenre.options)
            .filter(option => option.selected)
            .map(option => option.value);
        console.log(selectedOptions)

        filterBooksByGenre(selectedOptions);
    });

    // Tag
    selectElementTag.addEventListener('change', () => {
        let selectedOptions = Array.from(selectElementTag.options)
            .filter(option => option.selected)
            .map(option => option.value);

        filterBooksByTag(selectedOptions);
    });

    // Author
    selectElementAuthor.addEventListener('change', () => {
        let selectedOptions = Array.from(selectElementAuthor.options)
            .filter(option => option.selected)
            .map(option => option.value);

        filterBooksByAuthor(selectedOptions);
    });

});

function filterBooksByGenre(selectedGenres) {
    const rows = document.querySelectorAll('#booksTable tbody tr');
    rows.forEach(row => {
        const bookGenre = row.getAttribute('data-genre');
        if (selectedGenres.length === 0 || selectedGenres.includes(bookGenre)) {
            row.style.display = '';
        } else {
            row.style.display = 'none';
        }
    });
}
function filterBooksByTag(selectedTags) {
    const rows = document.querySelectorAll('#booksTable tbody tr');
    rows.forEach(row => {
        const dataTags = row.getAttribute('data-tags');
        const bookTags = dataTags ? dataTags.split(',') : [];

        const hasTag = selectedTags.some(tag => bookTags.includes(tag));
        if (selectedTags.length === 0 || hasTag) {
            row.style.display = ''; 
        } else {
            row.style.display = 'none';
        }
    });
}

function filterBooksByAuthor(selectedAuthors) {
    var rows = document.querySelectorAll('#booksTable tbody tr');
    rows.forEach(row => {
        var dataAuthors = row.getAttribute('data-authors');
        var bookAuthors = dataAuthors ? dataAuthors.split(',') : [];

        var hasAuthor = selectedAuthors.some(author => bookAuthors.includes(author));
        if (selectedAuthors.length === 0 || hasAuthor) {
            row.style.display = '';
        } else {
            row.style.display = 'none'; 
        }
    });
}

function toggleDropdownByGenre() {
    var dropdownContent = document.getElementById('genreDropdown');
    dropdownContent.style.display = dropdownContent.style.display === 'block' ? 'none' : 'block';
}

function toggleDropdown() {
    var dropdownContent = document.getElementById('genreDropdown');
    dropdownContent.style.display = dropdownContent.style.display === 'block' ? 'none' : 'block';
}