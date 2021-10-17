// Images preview in browser
$(function () {
    var imagesPreview = function (input, placeToInsertImagePreview) {

        if (input.files) {
            var filesAmount = input.files.length;
            for (i = 0; i < filesAmount; i++) {
                var reader = new FileReader();

                reader.onload = function (event) {
                    $($.parseHTML('<img class="rounded mx-auto d-block">')).attr('src', event.target.result).appendTo(placeToInsertImagePreview);
                }
                reader.readAsDataURL(input.files[i]);
            }
        }
    };

    $('#gallery-photo-add').on('change', function () {
        imagesPreview(this, 'div.gallery');
    });
    //Button reset input files
    var file = document.querySelector('#gallery-photo-add'),
        removeBtn = document.querySelector('#reset-button');

    removeBtn.addEventListener('click', function () {
        file.value = '';
    }, false);
    //Reset images form div
    (function ($) {
        $('#reset-button').on('click', function () {
            $('div.gallery').empty();
        });

    })(jQuery);
    //Reset images form div
    (function ($) {
        $('.label-input-image').on('click', function () {
            $('div.gallery').empty();
        });
    })(jQuery);

});