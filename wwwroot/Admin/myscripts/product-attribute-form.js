//add partial view _GetAttributesTemplatesPartial to the form after the selected category
$(function () {

    $('#selected-category').change(function () {
        // get selected categoryId
        var categoryId = $(this).val();
        $.ajax({
            type: 'GET',
            url: window.location.origin + '/admin/ProductAttribute/_GetAttributesTemplatesPartial/' + categoryId,
            success: function (data) {

                // replace the content with a partial view
                $('#attributes-templates').replaceWith(data);
                var form = $("div#form form")
                    .removeData("validator") /* added by the raw jquery.validate plugin */
                    .removeData("unobtrusiveValidation");  /* added by the jquery unobtrusive plugin*/

                $.validator.unobtrusive.parse(form);
            }
        });
    });
})

