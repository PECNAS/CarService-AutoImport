var minus = document.querySelector("#minus-btn");
var counter = document.querySelector("#counter");
var plus = document.querySelector("#plus-btn");

minus.addEventListener("click", (event) => {
    var counter_input = document.querySelector("#Counter");

    if (parseInt(counter.value) > 1) {
        counter.value = parseInt(counter.value) - 1
    }
    counter_input.value = counter.value;
    console.log(counter_input);
});

plus.addEventListener("click", (event) => {
    var counter_input = document.querySelector("#Counter");

    if (parseInt(counter.value) < window.max_count) {
        counter.value = parseInt(counter.value) + 1;
    }
    counter_input.value = counter.value;
    console.log(counter_input);
});

counter.addEventListener("focusout", (event) => {
    var counter_input = document.querySelector("#Counter");

    if (parseInt(counter.value) > window.max_count) {
        counter.value = window.max_count;
    }
    counter_input.value = counter.value;
    console.log(counter_input);
})