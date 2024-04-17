async function showSlides() {
    var slides = [];
    slides.push(document.getElementById("slide1"));
    slides.push(document.getElementById("slide2"));

    slides[0].style.opacity = '0';
    slides[1].style.opacity = '0';
    for (var i = 0; i < slides.length; i++) {

        var item = slides[i];
        item.style.display = 'block';
        item.style.opacity = '0.25';
        await delay(i, 100);
        item.style.opacity = '0.50';
        await delay(i, 100);
        item.style.opacity = '0.75';
        await delay(i, 100);
        item.style.opacity = '1';

        await delay(i, 3500);

        item.style.opacity = '0.75';
        await delay(i, 100);
        item.style.opacity = '0.50';
        await delay(i, 100);
        item.style.opacity = '0.25';
        await delay(i, 100);
        item.style.opacity = '0';
        item.style.display = 'none';

        if (i === (slides.length - 1)) {
            i = -1;
        }
    }
};

function delay(i, time) {
    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve();
        }, time);
    });
};



showSlides();