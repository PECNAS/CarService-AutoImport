function hide_reg() {
    document.querySelector("#reg-body").style.display = "none";
    document.querySelector("#login-body").style.display = "flex";
}

function hide_login() {
    document.querySelector("#reg-body").style.display = "flex";
    document.querySelector("#login-body").style.display = "none";
}