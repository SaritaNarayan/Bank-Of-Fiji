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