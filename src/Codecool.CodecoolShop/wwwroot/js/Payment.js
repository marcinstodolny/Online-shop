function showCreditCardDetails() {
    let payButton = document.getElementById("pay-button");
    let creditCardDetails = document.getElementById("credit-card-details");
    let checkbox = document.getElementById("credit-card-checkbox");
    if (checkbox.checked) {
        creditCardDetails.style.display = "block";
        payButton.style.display = "block";
        checkbox.innerHTML = "true";
    } else {
        creditCardDetails.style.display = "none";
        payButton.style.display = "none";
        checkbox.innerHTML = "false";
    }
}
