
//AJAX
$(document).ajaxStart(function () {
    $(".loadingPanel").css("display", "block");
});
$(document).ajaxComplete(function () {
    $(".loadingPanel").css("display", "none");
    $.validator.unobtrusive.parse('form');
    ApplyReadOnly();
});
var sc = 0;

//Master Save Load and Edit and delete

//save
$(document).on("click", '.save', function () {
    var $f = $('#Form');
    var rqr = "";
    $('.required').each(function () {
        if(!$(this).val())
        {
            rqr += $(this).parent().find('label').html() + ", ";
        }
    });
    if (rqr) {
        swal(rqr + " is/are required");
    }
    else {
        if ($f.valid() && sc == 0) {
            sc++;
            $.post($f.attr('action'), $f.serialize(), function (d, t, j) {
                if (d == "Success") {
                    $('#list').load($('#list').attr('data-link'), function (a, b, c) {
                        $('#edit').load($('#edit').attr('data-link'), function (a, b, c) {
                            $('#Form').find('.inputfocus').focus();
                            sc = 0;
                        });
                    });
                }
                else {
                    swal(d);
                    sc = 0;
                }
            });
        }
    }
});
//Edit
function Edit(id)
{
    $('#edit').load($('#edit').attr('data-link')+"?id="+id, function (a, b, c) {
       $('#Form').find('.inputfocus').focus();

    });
}
//Delete
function Delete(Id) {
    swal({
        title: "Are you sure?",        
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: "#DD6B55",
        confirmButtonText: "Yes, delete it!",
        closeOnConfirm: false
    }, function () {
        $.post($('#list').attr('data-link-delete'), { Id: Id }, function (e) { swal(e); }).always(function () {
            $('#list').load($('#list').attr('data-link'), function (a, b, c) {
            });
        });

    });
}

function DefaultList()
{
    $.post($('#list').attr('data-link-default'), function (e) { swal(e); }).always(function () {
        $('#list').load($('#list').attr('data-link'), function (a, b, c) {
        });
    });
}
//Cancel
$(document).on("click", '.cancel', function () {
    $('#edit').load($('#edit').attr('data-link'), function (a, b, c) {
        $('#Form').find('.inputfocus').focus();
        sc = 0;
    });
});

//Call some functions on Ready
$(document).on("ready", function () {
    ApplyTypehead();
    ApplyReadOnly();
    var $maximize = '[data-widget="maximize"]';
    $($maximize).on('click', function (e) {
        var $ths = $(this);
        var $prnt = $ths.parents(".box").first().parent();
        if($prnt.attr('data-default-class') == $prnt.attr('class'))
            $prnt.removeClass($prnt.attr('data-default-class')).addClass('col-md-12');
        else
            $prnt.removeClass('col-md-12').addClass($prnt.attr('data-default-class'));
        $('html, body').animate({ scrollTop: $ths.position().top }, 'slow');
    });
});
//Change Other dropdown on current change event
$(document).on('change', '.chngevnt', function () {
    var $ths = $(this);
    $.getJSON($ths.attr('data-url') + $ths.val(), "", function (data) {
        var items = [];
        items.push("<option>--Select--</option>");
       
            $.each(data, function () {
                items.push("<option value=" + this.value + ">" + this.text + "</option>");
            });
        
        $($ths.attr('data-attachto')).html(items.join(' '));
    });
});

//Apply TypeHead
function ApplyTypehead()
{
    $('.typehead').each(function (i, e) {
        $(this).typeahead({
            ajax: {
                url: $(this).attr("data-typehead-url")
            }
        });
    });
}
//Apply ReadOnly
function ApplyReadOnly()
{
    $('.readonly').each(function (i, e) {
        if ($(this).attr('data-readonly') == 'True') {
            $(this).attr('readonly', 'readonly');
        }
    });
}

$('#Form').on('keyup keypress', function (e) {
    var code = e.keyCode || e.which;
    if (code == 13) {
        e.preventDefault();
        $('.save').trigger('click');
        return false;
    }
});


