var btn = document.querySelector("#send");
btn.setAttribute("disabled", "");

var email = document.querySelector("#Email");
var psswd = document.querySelector("#Password");
var phone_number = document.querySelector("#PhoneNumber");
var name_input = document.querySelector("#Name");

window.email_valid = false;
window.psswd_valid = false;
window.phone_valid = false;
window.name_valid = false;

function button_unlock() {
	if (window.psswd_valid && window.email_valid && window.phone_valid && window.name_valid) {
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

function number_check() {
	if (phone_number.value.startsWith("+7") && phone_number.value.length == 12) {
		window.phone_valid = true;
	} else {
		window.phone_valid = false;
	}

	button_unlock();
}

function name_check() {
	if (name_input.value != "") {
		window.name_valid = true;
	} else {
		window.name_valid = false;
	}

	button_unlock();

}

email.addEventListener("keyup", email_check);
psswd.addEventListener("keyup", password_check);
phone_number.addEventListener("keyup", number_check);
name_input.addEventListener("keyup", name_check);