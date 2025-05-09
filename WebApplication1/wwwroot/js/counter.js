var minus = document.querySelector("#minus-btn");
var counter = document.querySelector("#counter");
var counter_input = document.querySelector("#Counter");
var plus = document.querySelector("#plus-btn");

console.log(counter);

minus.addEventListener("click", (event) => {
    if (parseInt(counter.value) > 1) {
        //counter.innerHTML = parseInt(counter.innerHTML) - 1;
        counter.value = parseInt(counter.value) - 1
        counter_input.value = counter.value;
    }
});

plus.addEventListener("click", (event) => {
    //counter.innerHTML = parseInt(counter.innerHTML) + 1;
    if (parseInt(counter.value) < window.max_count) {
        counter.value = parseInt(counter.value) + 1;
        counter_input.value = counter.value;
    }
});

counter.addEventListener("focusout", (event) => {
    if (parseInt(counter.value) > window.max_count) {
        counter.value = window.max_count;
    }
})