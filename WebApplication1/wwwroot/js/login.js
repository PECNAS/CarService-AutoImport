var btn = document.querySelector("#send");
btn.setAttribute("disabled", "");

var email = document.querySelector("#Email");
var psswd = document.querySelector("#Password");

window.email_valid = false;
window.psswd_valid = false;

function button_unlock() {
	if (window.psswd_valid && window.email_valid) {
		btn.removeAttribute("disabled");
	} else {
		btn.setAttribute("disabled", "");
	}
}

function email_check() {
	if (email.value.includes("@") && (email.value.includes(".com") || email.value.includes(".ru"))) {
		window.email_valid = true;
	} else {
		window.email_valid = false;
	}
	button_unlock();
}

function password_check() {
	if (psswd.value != "" && psswd.value.length >= 8) {
		window.psswd_valid = true
	} else {
		window.psswd_valid = false;
	}
	button_unlock();
}

email.addEventListener("keyup", email_check);
psswd.addEventListener("keyup", password_check);