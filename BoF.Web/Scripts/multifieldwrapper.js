// For Add Add Functionality
function InitCloning() {

    try {
        $('select').select2('destroy');
        var select2 = $('.select2').select2();
        $('.main-multi-field-wrapper').each(function () {
            //$('.select2').each(function(){$(this).select2("destroy")});
            var $wrapper = $('.sub-multi-fields', this);
            var $this = $(this);
            $(".add-field", $this).click(function (e) {
                select2.select2('destroy');
                var $len = $('.multi-field', $wrapper).length;
                var $clone = $('.multi-field:first-child', $wrapper);

                $clone.clone().appendTo($wrapper).find('input,select').each(function () {
                    if ($(this).hasClass("clr")) {
                        $(this).val('');
                    }
                    //
                    $(this).attr('name', $(this).attr('name').replace(/\d+/g, $len));
                });
                $('.multi-field', $wrapper).each(function (idx) {
                    $(this).find('td:first-child').html(idx + 1);
                });
                select2 = $('.select2').select2();
            });
            $($wrapper).on("click", ".multi-field .remove-field", function (e) {

                var dltid = $(this).attr('data-dlt-id');
                var $cthis = $(this);
                if (dltid) {
                    var cnfirm = confirm("Deleted data cant't be Recoverd");
                    if (cnfirm) {
                        $.post('/PartyMaster/deletePartyMasterAttachment', { id: dltid }, function (d) {
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
        });
        function RemoveColumn($wrapper, $this, $cthis) {
            if ($('.multi-field', $wrapper).length > 1) {
                select2.select2('destroy');
                $cthis.closest('tr').remove();
                $('.multi-field', $wrapper).each(function (indx) {
                    $(this).find('input,select').each(function () {
                        var $ths = $(this);
                        $ths.attr('name', $ths.attr('name').replace(/\d+/g, indx));
                    });
                });
                select2 = $('.select2').select2();
            }
            //TotlaAmt($wrapper, $this);
            $('.multi-field', $wrapper).each(function (idx) {
                $(this).find('td:first-child').html(idx + 1);
            });
        }
    } catch (e) {

    }

};