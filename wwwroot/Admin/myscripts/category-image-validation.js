$(document).ready(function () {
    $('div#image_form form').submit(function () {
        return ImageValidation();
    });
});
function ImageValidation() {
    var fileSelected = document.getElementById('gallery-photo-add').files[0];
    var $error = $("#error-message");
    var fileExtantion = ['jpg', 'jpeg', 'png'];
    if (fileSelected != undefined && !CheckImageFileFormat(fileSelected.name)) {
        $error.html("Please select image format JPEG(.jpeg, .jpg) or PNG(.png).");
        return false;
    }
    if (fileSelected == undefined) {
        $error.html("The field is required.");
        return false;
    }
    else {
        $error.html("");
        return true;
    }
    function CheckImageFileFormat(fileName) {
        var array = fileName.split('.');
        var selectedFileExtantion = array[array.length - 1];
        if (fileExtantion.indexOf(selectedFileExtantion) == -1) {
            return false;
        }
        else {
            return true;
        }
    };
};
