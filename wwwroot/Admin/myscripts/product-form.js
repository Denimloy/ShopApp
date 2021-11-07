//add partial view _GetProductAttributesPartial to the form after the selected category
$(function () {

    $('#selected-category').change(function () {
        // get selected categoryId
        var categoryId = $(this).val();
        $.ajax({
            type: 'GET',
            url: window.location.origin + '/admin/Product/_GetProductAttributesPartial/' + categoryId,
            success: function (data) {

                // replace the content with a partial view
                $('#product-attributes').replaceWith(data);
                //var form = $("div#form form")
                //    .removeData("validator") /* added by the raw jquery.validate plugin */
                //    .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/

            //    $.validator.unobtrusive.parse(form);
            }
        });
    });
})

