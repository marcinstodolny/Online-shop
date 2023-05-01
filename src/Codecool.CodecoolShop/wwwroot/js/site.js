// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function hideShippingDetails() {
    var checkbox = document.getElementById("same-as-billing");
    var shippingDetails = document.getElementById("shipping-details");

    if (checkbox.checked) {
        shippingDetails.style.display = "none";

        document.getElementById("shipping-address").innerHTML = "default";
        document.getElementById("shipping-city").value = "default";
        document.getElementById("shipping-country").value = "default";
        document.getElementById("shipping-zipcode").value = "default";

        checkbox.innerHTML = "true";
    } else {

        document.getElementById("shipping-address").innerHTML = "";
        document.getElementById("shipping-city").value = "";
        document.getElementById("shipping-country").value = "";
        document.getElementById("shipping-zipcode").value = "";

        shippingDetails.style.display = "block";
        checkbox.innerHTML = "false";
    }
}
