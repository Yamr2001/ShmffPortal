function IsEmail(email) {
    debugger
    var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!regex.test(email)) {
        return false;
    } else {
        return true;
    }
}

$('.IsInteger,.IsDecimal').focus(function (e) {
    if (this.value == "0") {
        this.value = "";
    }
});
$('.IsInteger,.IsDecimal').blur(function (e) {
    if (this.value == "") {
        this.value = "0";
    }
});

$('.IsInteger').keypress(function (e) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (charCode > 31
    && (charCode < 48 || charCode > 57))
        return false;
});
$('.IsDecimal').keypress(function (e) {
    var charCode = (e.which) ? e.which : e.keyCode;
    if (this.value.indexOf(".") > 0) {
        if (charCode == 46) {
            return false;
        }
    }
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
});
$('.IsSpecialChar').keypress(function (e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
});
$('.IsMaxLength').keypress(function (e) {
    var length = $(this).attr("maxlength");
    return (this.value.length <= length);
});

function IsPhoneNumber(e) {
    debugger
    var numbers = this.value;
    this.value = '';
    for (var i = 0; i < numbers.length; i++) {
        this.value += (char[i] || '') + numbers[i];
    }
}
function isNumber(evt) {
    debugger
    evt = (evt) ? evt : window.event;
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
$('.IsEmail').blur(function (e) {
    var flag = false;
    var email = this.value;
    if (email.length > 0) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        flag = regex.test(email);
    }
    if (!flag)
        this.value = "";
});