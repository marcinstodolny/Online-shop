// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function hideShippingDetails() {
    var checkbox = document.getElementById("same-as-billing");
    var shippingDetails = document.getElementById("shipping-details");

    if (checkbox.checked) {
        shippingDetails.style.display = "none";
    } else {
        shippingDetails.style.display = "block";
    }
}
