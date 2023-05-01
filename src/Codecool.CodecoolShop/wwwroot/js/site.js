// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(document).ready(function () {
    var $shippingDetails = $('#shipping-details');
    var $shippingCheckbox = $('input[name="ShippingSameAsBilling"]');

    $shippingCheckbox.on('change', function () {
        if ($shippingCheckbox.prop('checked')) {
            $shippingDetails.addClass('hidden');
        } else {
            $shippingDetails.removeClass('hidden');
        }
    });
});
