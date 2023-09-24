document.addEventListener("DOMContentLoaded", function () {
    // Get the elements by their IDs
    let pEl = document.getElementById("pa");
    let inputEl = document.getElementById("inputs");

    function deistirici() {
        // Update the text content of the <p> element with the value of the input
        pEl.textContent = inputEl.value;
    }

    // Attach the click event handler to the button
    document.querySelector("button.btn-primary").addEventListener("click", deistirici);
});