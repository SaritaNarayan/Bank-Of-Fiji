$('.main-multi-field-wrapper').each(function () {
    //$('.select2').each(function(){$(this).select2("destroy")});
    var $wrapper = $('.sub-multi-fields', this);
    var $this = $(this);
    $(".add-field", $this).click(function (e) {
        //select2.select2('destroy');
        var $len = $('.multi-field', $wrapper).length;
        var $clone = $('.multi-field:first-child', $wrapper);

        $clone.clone().appendTo($wrapper).find('input,select').each(function () {
            if ($(this).hasClass("clr")) {
                $(this).val('');
            }
            //
            $(this).attr('name', $(this).attr('name').replace(/\d+/g, $len));
        });
        //  select2 = $('.select2').select2();
        $('.multi-field', $wrapper).each(function (idx) {
            $(this).find('td:first-child').html(idx + 1);
        });
    });
    $($wrapper).on("click", ".multi-field .remove-field", function (e) {

        var dltid = $(this).attr('data-dlt-id');
        var $cthis = $(this);
        if (dltid) {
            var cnfirm = confirm("Deleted data cant't be Recoverd");
            if (cnfirm) {
                $.post('/invoice/deletetransactionitem', { id: dltid }, function (d) {
                    if (d == "Success") {
                        RemoveColumn($wrapper, $this, $cthis)
                    }
                });
            }
        }
        else {
            RemoveColumn($wrapper, $this, $cthis);
        }
    });
    $($wrapper).on("keyup", '.amt', function () {
        TotlaAmt($wrapper, $this);
    });
    $($wrapper).on("keyup", '.prc', function () {
        var $tr = $(this).closest('tr');
        var pprc = parseInt($('.pprc', $tr).val()) || 0;
        var rprc = parseInt($('.rprc', $tr).val()) || 0;
        var ttl = pprc + rprc;
        $('.tprc', $tr).val(ttl);
    });
    $($wrapper).on("keyup", '.rate', function () {
        //var get qty
        var $prnt = $(this).closest('tr');
        Rate($prnt);
        TotlaAmt($wrapper, $this);
    });
    $($wrapper).on("keyup", '.qty', function () {
        //var get qty
        var $prnt = $(this).closest('tr');
        Rate($prnt);
        TotlaAmt($wrapper, $this);
    });
    $($wrapper).on('change', '.prd', function () {
        var $pthis = $(this);
        var $typ = $pthis.attr('data-for');
        var id = parseInt($pthis.val());
        //find the price
        var prd = _.findWhere(Products, { Id: id });
        if (!prd) { return false; }
        var $prnt = $(this).closest('tr');
        var rat = $prnt.find('.rate').val(prd[$typ + 'Price']);
        Rate($prnt);
        TotlaAmt($wrapper, $this);
    });
    TotlaAmt($wrapper, $this);
    //  GrnadTotal();
});
function RemoveColumn($wrapper, $this, $cthis) {
    if ($('.multi-field', $wrapper).length > 0) {
        // select2.select2('destroy');
        $cthis.closest('tr').remove();
        $('.multi-field', $wrapper).each(function (indx) {
            $(this).find('input,select').each(function () {
                var $ths = $(this);
                $ths.attr('name', $ths.attr('name').replace(/\d+/g, indx));
            });
        });
        // select2 = $('.select2').select2();
    }
    TotlaAmt($wrapper, $this);
    $('.multi-field', $wrapper).each(function (idx) {
        $(this).find('td:first-child').html(idx + 1);
    });
}
function TotlaAmt($wrapper, $this) {
    var totalSum = 0;
    var totalSum1 = 0;
    $wrapper.find('.amt').each(function () {

        if ($(this).hasClass('neg')) {
            totalSum -= parseFloat($(this).val() || 0);
        }
        else {
            totalSum += parseFloat($(this).val() || 0);
        }
    })
    $wrapper.find('.tamt').each(function () {

        if ($(this).hasClass('neg')) {
            totalSum1 -= parseFloat($(this).val() || 0);
        }
        else {
            totalSum1 += parseFloat($(this).val() || 0);
        }
    })
    $this.find('.totalSum').each(function () {
        $(this).val(totalSum);
        $(this).html(totalSum + totalSum1);
        var p = $('#tax').val(totalSum1);
       $('#gtotal').val(totalSum + totalSum1);
       $('#subtotal').val(totalSum + totalSum1);
       $('#nettotal').val(totalSum + totalSum1);
    });
    //GrnadTotal();
    try {
        function isnumber(evt) {
            var charcode = (evt.which) ? evt.which : event.keycode
            if (charcode != 45 && (charcode != 46 || $(this).val().indexof('.') != -1) &&
                         (charcode < 48 || charcode > 57))
                return false;
            return true;
        }
        $('.numonly').keypress(function (e) {
            return isnumber(event);
        });

    } catch (e) {

    }
}
