$(document).ready(function () {

    // init background slide images
    $.backstretch([
   "../root/GiaodienLogin/bg/1.jpg",
   "../root/GiaodienLogin/bg/2.jpg",
   "../root/GiaodienLogin/bg/3.jpg",
   "../root/GiaodienLogin/bg/4.jpg"
    ], {
        fade: 1000,
        duration: 3000
    }
  );
    ///
    var numtype = localStorage.getItem("numtype"); //get session numtype
    if (numtype != null) {
        $(".container").css("display", "none")
        $(".content").css("display", "block")
        TypeLogin(numtype); //call fuction return text type login
    };

    $(".checklogin").click(function () {
        var type = $(this).attr('data-type');
        $(".container").css("display", "none")
        $(".content").css("display", "block")
        localStorage.setItem("numtype", type); // set session numtype
        TypeLogin(type); //call fuction return text type login
    });
    $("#btn-back").click(function () {
        $(".container").css("display", "block")
        $(".content").css("display", "none")
    });

});

function TypeLogin(type) {
    if (type == 1) {
        return $(".login-form h3.form-title").text("Đăng nhập tài khoản CMC Corp.")
    }
    if (type == 2) {
        return $(".login-form h3.form-title").text("Đăng nhập tài khoản CMC SISG.")
    }
}

function ssoTelecom() {
    localStorage.removeItem('numtype')
    var clientid = "82DKSLDJDS72"
    var redirecturi = $("#hiddenUrlCommon").val() + "/Login";
    var link = 'https://sso.cmctelecom.vn/oauth2.0/authorize?response_type=code&client_id=' + clientid + '&redirect_uri=' + redirecturi;
    window.location.href = link;
}

