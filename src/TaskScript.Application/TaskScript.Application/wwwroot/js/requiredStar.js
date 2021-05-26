(() => {
    const requiredInputs = document.querySelectorAll('input[data-val-required]:not(input[type="hidden"])');

    requiredInputs.forEach(requiredInput => {
        const parent = requiredInput.parentNode;
        const label = parent.querySelector('label');

        if (label) {
            label.innerHTML += '<span class="text-danger">*</span>';
        }
    });
})();
