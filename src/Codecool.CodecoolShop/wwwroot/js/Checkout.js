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