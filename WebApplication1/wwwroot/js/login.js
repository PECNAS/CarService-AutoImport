var login_btn = document.querySelector("#login-body").querySelector("#send");
login_btn.setAttribute("disabled", "");

var login_email = document.querySelector("#login-body").querySelector("#Email");
var login_psswd = document.querySelector("#login-body").querySelector("#Password");

window.login_email_valid = false;
window.login_psswd_valid = false;

function login_button_unlock() {
	if (window.login_psswd_valid && window.login_email_valid) {
		login_btn.removeAttribute("disabled");
	} else {
		login_btn.setAttribute("disabled", "");
	}
}

function login_email_check() {
	if (login_email.value.includes("@") && (login_email.value.includes(".com") || login_email.value.includes(".ru"))) {
		window.login_email_valid = true;
	} else {
		window.login_email_valid = false;
	}
	login_button_unlock();
}

function login_password_check() {
	if (login_psswd.value != "" && login_psswd.value.length >= 8) {
		window.login_psswd_valid = true
	} else {
		window.login_psswd_valid = false;
	}
	login_button_unlock();
}

login_email.addEventListener("keyup", login_email_check);
login_psswd.addEventListener("keyup", login_password_check);