const imageInput = document.querySelector("#image_input");
const previewImage_1 = document.querySelector(".photo_div_1>img");
const previewImage_2 = document.querySelector(".photo_div_2>img");
const previewImage_3 = document.querySelector(".photo_div_3>img");


const SetNewImageListener = () => {
    imageInput.addEventListener('change', () => {
        const file = imageInput.files[0];
        if (file) {
            const reader = new FileReader();
            reader.onload = (event) => {
                previewImage_1.src = event.target.result;
                previewImage_1.style.display = 'block';
                previewImage_2.src = event.target.result;
                previewImage_2.style.display = 'block';
                previewImage_3.src = event.target.result;
                previewImage_3.style.display = 'block';
            };
            reader.readAsDataURL(file);
        }
    });
} 

SetNewImageListener();
