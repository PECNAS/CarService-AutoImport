document.addEventListener('DOMContentLoaded', function () {
	const allDetails = document.querySelectorAll('details');
	allDetails.forEach(details => {
		details.addEventListener('toggle', function () {
			if (this.open) {
				allDetails.forEach(otherDetails => {
					if (otherDetails !== this) {
						otherDetails.open = false;
					}
				});
			}
		});
	});
});