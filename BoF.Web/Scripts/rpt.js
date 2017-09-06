
function initTable() {
	$('.Srchtable').DataTable({
		"destroy": true,
		paging: true,
		"autoWidth": true,
		//responsie : true,
		responsive: {
			details: {
				type: 'column',
				target: 'tr'
			}
		},
		columnDefs: [{
			className: 'control',
			orderable: false,
			targets: 0
		}],
		order: [1, 'desc']
		// ,scrollX: true
        , stateSave: true
        , dom: '<"pull-right"T><"clearfix"><"pull-right"f><"pull-left"l>rtip'
	});
	$('.whfSrchtable').DataTable({
		"destroy": true,
		paging: false,
		filter: false,
		info: false,
		sort: false,
		"autoWidth": true,
		responsive: {
			details: {
				type: 'column',
				target: 'tr'
			}
		},
		columnDefs: [{
			className: 'control',
			orderable: false,
			targets: 0
		}],
		order: [1, 'desc'],
		scrollX: true
        , stateSave: true

	});
	$('.scrolSrchtable').DataTable({
		"destroy": true,
		paging: true,
		filter: true,
		info: true,
		sort: true,
		"autoWidth": true,
		//responsive : true,
		//columnDefs: [{
		//    className: 'control',
		//    orderable: false,
		//   targets: 0,
		//}],
		order: [0, 'desc'],
		"scrollY": 400,
		"scrollX": true,
		"scrollCollapse": true
         , "lengthMenu": [[25, 50, 100, -1], [25, 50, 100, "All"]]
         , dom: '<"pull-right"T><"clearfix">R<"pull-right"f><"pull-left"l>rt<"pull-right"p><"pull-left"i><"clearfix">'
		//,stateSave: true
         , colVis: {
         	restore: "Restore",
         	showAll: "Show all",
         	showNone: "Show none"
         }
		//, dom: 'Rlfrtip'
		//,dom: 'RC<"clear">lfrtip',
		//    columnDefs: [
		//        {  targets: 1 }
		//    ]
		//,stateSave: true
	});
	//var tt = new $.fn.dataTable.TableTools(tbl);
	//$(tt.fnContainer()).insertBefore('div.dataTables_wrapper');

}

$(document).on("ready", function () {
	menuTable();
});

$(document).on('click', '.dlt', function () {
	var cnf = confirm("Are you Sure Delete this ?");
	if (cnf) {
		var url = $(this).attr('data-dlt-link');
		var $tr = $(this).parent().parent();
		$.post(url, { id: $(this).attr('data-id') }, function (d) {
			if (d[0] == "Success") {
				alert(d[1]);
				$tr.hide();
			} else {
				alert(d[1]);
			}
		});
	}
});

$(document).on('click', '.dltRole', function () {
	var cnf = confirm("Are you Sure ?");
	if (cnf) {
		var url = $(this).attr('data-dlt-link');
		var $tr = $(this).parent().parent();
		$.post(url, { id: $(this).attr('data-id') }, function (d) {
			if (d == "Success") {
				alert("Role Deleted...");
				$tr.hide();
			} else {
				alert(d);
			}
		});
	}
});

function resetpartial() {
	$('#edit').load($('#edit').attr('data-link'), function (d) {
		$('#edit').find('.inputfocus').focus();
	});
}
//Add
$(document).on('click', '#add', function () {
	resetpartial();
});

//Edit
$(document).on('click', '.edit', function () {
	var id = $(this).attr('data-id');
	$('#edit').load($('#edit').attr('data-link') + "?id=" + id, function (d) {
	});
});

$(document).on("keypress", ".numonly", function (event) {
	if (event.which < 47 || event.which >= 58) {
		event.preventDefault();
	}

});

function menuTable() {
	$('.MenusTable').DataTable({
		dom: 'Bfrtip',
		"paging": false,
		stateSave: true,
		//lengthMenu: [
		//  [25, 35, 50, -1],
		//  ['25 rows', '35 rows', '50 rows', 'Show all']
		//],
		buttons: [
            'colvis', 'copy', 'pdf', 'print'
		]
	});
}

