// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// *************| Checkout site |***********

function hideShippingDetails() {
    let checkbox = document.getElementById("same-as-billing");
    let shippingDetails = document.getElementById("shipping-details");

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

function validate() {
    let inputs = document.querySelectorAll(".validate");
    for (let i = 0; i < inputs.length; i++) {
        if (inputs[i].value == "") {
            inputs[i].style.borderColor = "red";
        } else {
            inputs[i].style.borderColor = "green";
        }
    }  
}

// ********* | Payment site | ************
//function showCreditCardDetails() {
//    let payButton = document.getElementById("pay-button");
//    let creditCardDetails = document.getElementById("credit-card-details");
//    let checkbox = document.getElementById("credit-card-checkbox");
//    if (checkbox.checked) {
//        creditCardDetails.style.display = "block";
//        payButton.style.display = "block";
//        checkbox.innerHTML = "true";
//    } else {
//        creditCardDetails.style.display = "none";
//        payButton.style.display = "none";
//        checkbox.innerHTML = "false";
//    }
//}
