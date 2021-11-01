$(document).ready(function () {
    $('div#form form').submit(function () {
        return CategoryValidation();
    });
});
function CategoryValidation() {
    var selectedElement = document.getElementById("categories-title");
    var categoriesTitleId = selectedElement.value;
    var $error = $("#error-message");
    if (categoriesTitleId == 0) {
        $error.html("The Category Title field is required.");
        return false;
    }
    else {
        $error.html("");
        return true;
    }
};
