async function minus_count(id) {
    var counter = document.querySelector("#counter-" + id)
    if (parseInt(counter.value) > 1) {
        //counter.innerHTML = parseInt(counter.innerHTML) - 1;
        counter.value = parseInt(counter.value) - 1
    }

    const request = await fetch("/Catalog/ChangeCart?val=-1&item_id=" + id, {
        method: "get"
    });
}

async function plus_count(id, max) {
    var counter = document.querySelector("#counter-" + id)
    if (parseInt(counter.value) < max) {
        counter.value = parseInt(counter.value) + 1;
    }

    const request = await fetch("/Catalog/ChangeCart?val=1&item_id=" + id, {
        method: "get"
    });

}


function focus_out(id, max) {
    var counter = document.querySelector("#counter-" + id)
    if (parseInt(counter.value) > max) {
        counter.value = max;
    }
}