//$(document).on("keypress", ".numonly", function (event) {
//	if (event.which < 46 || event.which >= 58 || event.which == 47 || e.which != 8) {
//		event.preventDefault();
//	}

//	if (event.which == 46 && $(this).val().indexOf('.') != -1) {
//		this.value = '';
//	}
//});

$(document).on("keypress", ".numonly", function (e) {
	//if the letter is not digit then display error and don't type anything
	if (e.which != 8 && e.which != 0 && event.which != 46 && (e.which < 48 || e.which > 57)) {
		return false;
	}
});


//Upper Case
$(document).on("keyup", ".upper", function () {
	this.value = this.value.toUpperCase();
});