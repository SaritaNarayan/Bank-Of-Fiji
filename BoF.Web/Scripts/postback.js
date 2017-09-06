/// <reference path="utility.js" />
var cnt = 0;

//DataTable
function dttable() {
	$('.Srchtable').DataTable({
		dom: 'Bfrtip',
		stateSave: true,
		lengthMenu: [
          [25, 35, 50, -1],
          ['25 rows', '35 rows', '50 rows', 'Show all']
		],
		buttons: [
            'colvis', 'pageLength', 'copy', 'pdf', 'print'
		]
	});
}

$(document).ready(function () {
	dttable();
});

$(document).on("click", '.save', function (e) {
	var listlink = $(this).attr('data-list-link');
	var $form = $(this).closest('form');
	if ($form.valid() && cnt == 0) {
		cnt++;
		$.post($form.attr('action'), $form.serialize(), function (d) {

			if (d[0] == "Success") {
				$('#showfailmsg').hide();
				$('#failmsg').hide();
				$('#showsuccessmsg').show();
				$('#sucessmsg').html(d[1]);
				$form.find("input,select,textarea").val('');
				$('#list').load(listlink, function () {
					dttable();
					cnt = 0;
				});

			}
			else {
				$('#showsuccessmsg').hide();
				$('#sucessmsg').hide();

				$('#showfailmsg').show();
				$('#failmsg').html(d[1]);
				cnt = 0;
			}
		});
	}


})



//Open Modal Popup

function reset() {
	$("#reset").trigger("click");
}

function resetpartial() {
	$('#edit').load($('#edit').attr('data-link'), function (d) {
	    $('#edit').find('.inputfocus').focus();
	    $(".rmv").hide();
	    $('.serdueration').hide();
	});
}

$(document).on('click', '#add', function () {
	resetpartial();
});


// edit
$(document).on('click', '.edit', function () {
	var id = $(this).attr('data-id');

	$('#edit').load($('#edit').attr('data-link') + "?id=" + id, function (d) {
	    $('.serdueration').hide();
	});
});


// view
$(document).on('click', '.view', function () {
	var id = $(this).attr('data-id');

	$('#viewdetail').load($('#viewdetail').attr('data-link') + "?id=" + id, function (d) {

	});
});


//Delete Attraction

$(document).on('click', '.dltData', function () {
	var id = $(this).attr('data-id');
	var listlink = $(this).attr('data-link');
	var $tr = $(this).parent().parent();
	var iscon = confirm("Are u Sure Want To Remove?")
	if (iscon) {
		$.post(listlink, { id: id }, function (d) {
			if (d[0] == "Success") {
				alert(d[1]);
				$tr.hide();
			}
			else {
				alert(d[1]);
			}
		});
	}
});



//Numonly

//$(document).on("keypress", ".numonly", function (e) {
//    //if the letter is not digit then display error and don't type anything
//    if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
//        return false;
//    }
//});

$(document).on("keypress", ".numonly", function (event) {
	if (event.which < 46 || event.which >= 58 || event.which == 47) {
		event.preventDefault();
	}

	if (event.which == 46 && $(this).val().indexOf('.') != -1) {
		this.value = '';
	}
});


//Upper Case
$(document).on("keyup", ".upper", function () {
	this.value = this.value.toUpperCase();
});

//Autocomplete

$(document).on("keydown.autocomplete", ".autocomplete", function (e) {
	var link = $(this).attr("data-link");
	var $this = $(this);
	$(".autocomplete").autocomplete({
		source: function (request, response) {
			$.getJSON(link, function (data) {
				$.each(data, function (_, item) {
					item.label = item.ItemTypeCode;
					item.Id = item.Id;
				});
				var results = $.ui.autocomplete.filter(data, request.term);
				response(results)
			});
		},
		autoFocus: true,
		minLength: 3,
		select: function (event, ui) {
			$this.text(ui.item.ItemTypeCode);
			$("#ItemTypeCode").val(ui.item.ItemTypeCode);
		}
	});
});

function removenewfiles() {
	$("#fileupload").val("");
	$("#Image").val("");
	$("#image-holder").empty();
	$('.rmv').hide();

}

$('.rmv').hide();
var filedata = [];
$(document).on('change', "#fileupload", function () {
	$("#image-holder").empty();
	$('.rmv').show();
	var countFiles = $(this)[0].files.length;
	var imgPath = $(this)[0].value;
	var extn = imgPath.substring(imgPath.lastIndexOf('.') + 1).toLowerCase();
	var image_holder = $("#image-holder");
	//image_holder.empty();
	if (extn == "gif" || extn == "png" || extn == "jpg" || extn == "jpeg") {
		if (typeof (FileReader) != "undefined") {
			for (var i = 0; i < countFiles; i++) {
				var reader = new FileReader();
				reader.onload = function (e) {
					$("<li class='col-md-12 imgli1' data-is='NewAdded'><div class='thumbnail'><img src='" + e.target.result + "' class='imgcls' style='height:100px;width:400px'/></div></li>").appendTo(image_holder);
					//image_holder.append(img);
				}
				image_holder.show();
				reader.readAsDataURL($(this)[0].files[i]);
				filedata.push($(this)[0].files[i]);
			}

		} else {
			alert("This browser does not support FileReader.");
		}
	} else {
		alert("Pls select only images");
	}
});



$('#fileupload').on('fileuploaddone', function (e, data) {
	var activeUploads = $fileInput.fileupload('active');
	if (activeUploads == 1) {
		console.info("All uploads done");
		// Your stuff here
		UpdateImgList();
	}
});


function removetempFile(element) {
	$(element).parents("li").first().remove();
	var no = $(element).attr("data-id");
	filedata.splice(no, 1);
	UpdateImgList();
}


function UpdateImgList() {
	var listofimages = $("#image-holder").find("li");
	var j = 0;
	for (var i = 0; i <= listofimages.length; i++) {
		if ($(listofimages[i]).attr('data-is') == "NewAdded") {
			$(listofimages[i]).find('close').attr("data-id", j);
			j++;
		}
	}
}

//Save Promoter XHR

$(document).on("click", ".savepromoter", function () {
	var cnt = 0;
	var formdata = new FormData();
	var fileInput = document.getElementById('fileupload');
	for (i = 0; i < fileInput.files.length; i++) {
		formdata.append(fileInput.files[i].name, fileInput.files[i]);
	}
	var PromoterName = $("#PromoterName").val();
	var Id = $("#Id").val();
	if (Id != "" && Id != null) {
		var vid = $("#vid").val();
		var vpid = $("#vpid").val();
	}
	var Mobile = $("#Mobile").val();
	var CreatedbyId = $("#CreatedbyId").val();
	var CreatedDate = $("#CreatedDate").val();
	var PromoterCode = $("#PromoterCode").val();
	var IntroducerCode = $("#IntroducerCode").val();
	var IntroducerId = $("#IntroId").val();
	var JoiningDate = $("#JoiningDate").val();
	var DOB = $("#DOB").val();
	var PancardNumber = $("#PancardNumber").val();
	var Email = $("#Email").val();
	var City = $("#City").val();
	var Pincode = $("#Pincode").val();
	var Address = $("#Address").val();
	var Image = $("#Image").val();
	var RegistrationFees = $("#RegistrationFees").val();
	var url = "";


	//var IsIntro = $("#IsIntro").val();
	//var IsActive = $("#IsActive").val();
	//var IsLocal = $("#IsLocal").val();
	var $form = $(this).closest('form');
	if ($form.valid() && cnt == 0) {
		$(this).prop('disabled', true);
		cnt++;
		var xhr = new XMLHttpRequest();
		if (Id != "" && Id != null) {
			url = encodeURI("/promoter/AddPromoter?PromoterName=" + PromoterName + "&Id=" + Id + "&CreatedDate=" + CreatedDate + "&CreatedbyId=" + CreatedbyId + "&PromoterCode=" + PromoterCode + "&IntroducerCode=" + IntroducerCode + "&IntroducerId=" + IntroducerId + "&JoiningDate=" + JoiningDate + "&PancardNumber=" + PancardNumber + "&Email=" + Email + "&City=" + City + "&Image=" + Image + "&Pincode=" + Pincode + "&Address=" + Address + "&DOB=" + DOB + "&Mobile=" + Mobile + "&vJoiningFees[0].RegistrationFees=" + RegistrationFees + "&vJoiningFees[0].Id=" + vid + "&vJoiningFees[0].PromoterId=" + vpid);
		} else {
			url = encodeURI("/promoter/AddPromoter?PromoterName=" + PromoterName + "&Id=" + Id + "&CreatedDate=" + CreatedDate + "&CreatedbyId=" + CreatedbyId + "&PromoterCode=" + PromoterCode + "&IntroducerCode=" + IntroducerCode + "&IntroducerId=" + IntroducerId + "&JoiningDate=" + JoiningDate + "&PancardNumber=" + PancardNumber + "&Email=" + Email + "&City=" + City + "&Pincode=" + Pincode + "&Address=" + Address + "DOB=" + DOB + "&Mobile=" + Mobile + "&vJoiningFees[0].RegistrationFees=" + RegistrationFees);
		}
		xhr.open("POST", url);
		xhr.send(formdata);
		xhr.onreadystatechange = function (d) {
			if (xhr.readyState == 4) {
				if (xhr.statusText == "OK") {
					var id = xhr.responseText;
					alert("Promoter Saved Successfully", "Success");
					$('.savepromoter').prop('disabled', false);
					$('.rmv').trigger("click");
					$(".autocomplete").prop('readonly', false);
					cnt = 0;
					//$(".hd").hide();
					//$(".sh").show();
					$form.find("input,select,textarea,img").val('');
					var table = $("#data-table").DataTable();
					table.destroy();
					serverside();
					var link = $(".savepromoter").attr("data-link");
					$("#Print").load(link, { id: id }, function (d) {
						var printWindow = window.open('', '', 'height=500,width=900');
						printWindow.document.write(d);
						printWindow.document.close();
						printWindow.print();
					});
				}
				else if (xhr.responseText == 002) {
					$('.savepromoter').prop('disabled', false);
					alert("Your Audio is too large, Maximum allow size is:10mb");

				}
				else if (xhr.responseText == 003) {
					$('.savepromoter').prop('disabled', false);
					alert("Please upload Audio( .png,.jpg,.gif,.bmp,.JPG,.PNG,.GIF)");

				}
				else if (xhr.responseText == null) {
					$('.savepromoter').prop('disabled', false);
					alert("Opps ! Something Wrong");

				}
			}
		}
		cnt = 0;
	}
});



//reprint


$(document).on('click', '.reprint', function () {
	if (cnt == 0) {
		cnt++;
		var $this = $(this);
		var id = $(this).attr("data-id");
		var link = $(this).attr('data-link');
		jQuery.ajaxSettings.traditional = true;

		$("#Print").load(link, { id: id }, function (d) {
			var printWindow = window.open('', '', 'height=500,width=900');
			printWindow.document.write(d);
			printWindow.document.close();
			printWindow.print();
		});

		cnt = 0;

	}

});
