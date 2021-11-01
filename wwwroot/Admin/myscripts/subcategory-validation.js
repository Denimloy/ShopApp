$(document).ready(function () {
    $('div#form form').submit(function () {
        return SubcategoryValidation();
    });
});
function SubcategoryValidation() {
    var selectedElement = document.getElementById("main-category");
    var mainCategoryId = selectedElement.value;
    var $error = $("#error-message");
    if (mainCategoryId == 0) {
        $error.html("The Main Category field is required.");
        return false;
    }
    else {
        $error.html("");
        return true;
    }
};
