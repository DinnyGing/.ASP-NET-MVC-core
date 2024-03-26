// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



$(document).ready(function () {
    $('.time-option').click(function () {
        // Отримуємо поточний елемент "time-option"
        var selectedOption = $(this);

        // Видаляємо клас "active" з усіх елементів "time-option"
        $('.time-option').removeClass('active');

        // Додаємо клас "active" до поточної опції
        selectedOption.addClass('active');
    });
});   
    
const ratingElements = document.querySelectorAll('.rating');

ratingElements.forEach(ratingElement => {
    const ratingValueElement = ratingElement.nextElementSibling;
    const ratingValueModel = ratingValueElement.nextElementSibling;

    let ratingValue = parseFloat(ratingValueElement.innerText || '0');
    const maxRating = 5;

    function fillStars(rating) {
        const stars = ratingElement.querySelectorAll('.star');

        for (let i = 0; i < stars.length; i++) {
            if (i < rating) {
                stars[i].classList.add('filled');
            } else {
                stars[i].classList.remove('filled');
            }
        }

        ratingValueElement.textContent = rating;
        ratingValueModel.value = rating;
    }
    const roleSpan = document.getElementById('roleSpan');
    const role = roleSpan.dataset.role;
    if (role == "client") {
        ratingElement.addEventListener('click', (event) => {
            const starElement = event.target.closest('.star');

            if (starElement) {
                const rating = parseInt(starElement.dataset.rating);
                ratingValue = rating;
                fillStars(rating);
            }

            const button = ratingElement.previousElementSibling;
            button.click();
        });
    }

    fillStars(ratingValue);
});