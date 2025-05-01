var searcher = document.querySelectorAll("#searcher");
searcher[0].addEventListener("keyup", () => AddItemToSearcherList(searcher[0].value));
searcher[1].addEventListener("keyup", () => AddItemToSearcherList(searcher[1].value));

async function AddItemToSearcherList(query) {
    const request = await fetch("/Catalog/AsyncFinder?query=" + query, {
        method: "GET",
    });

    var data = await request.json();
    console.log(data);

    var result_list = document.querySelector(".result-list");

    result_list.innerHTML = ""; 

    if (data["items"].length != 0) document.querySelector("#searcher-results").style.display = "block";
    else document.querySelector("#searcher-results").style.display = "none";

    data["items"].forEach((el) => {
        result_list.innerHTML += '<li class="result">' +
            '    <a href="/Catalog/ItemDetail/' + el["id"] + '">' +
            '        <div class="search-result">' +
            '            <div class="res-img-container">' +
            '                <img class="res-img" src="/imgs/' + el["image"] + '" width="100%" height="100%"/>' +
            '            </div>' +
            '            <div class="res-text-container">' +
            '                <div class="res-title-container">' +
            '                    <span class="res-title">' + el["title"] + '</span>' +
            '                </div>' +
            '                <div class="res-price-container">' +
            '                    <span class="res-price">' + el["price"] + " руб.</span>" +
            '                </div>' +
            '            </div>' +
            '        </div>' +
            '    </a>' +
            '</li>';
    });
}