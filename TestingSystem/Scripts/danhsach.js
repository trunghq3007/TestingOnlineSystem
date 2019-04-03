//update trạng thái đã đọc 1 thông báo
function RedirectNotification(id) {
    var idlink = "link_" + id;
    var link = $("#" + idlink).val();
    $.ajax({
        type: "POST",
        url: $("#UpdateStatusViewNotification").val(),
        dataType: 'json',
        data: { id: id },
        success: function (data) {
            if (parseInt(data) == 1) {
                window.location = link;
            }
        }
    });
}
//update trạng thái đã đọc tất cả thông báo
function ReadAllStatus() {
    var read = $("#readAll").val();
    if (read != 0) {
        $.ajax({
            type: "POST",
            url: $("#UpdateAllStatusNotification").val(),
            dataType: 'json',
            success: function () {
                location.reload();
            }
        });
    }
}

function DeleteNotification(id) {
    var codeId = "#a_" + id;
    var userid = $("#sessionUserId").val();
    
    $.ajax({
        type: "POST",
        url: $("#DeleteNotification").val(),
        dataType: 'json',
        data: { id: id },
        success: function(data) {
            if (parseInt(data) == 1) {
                $.ajax({
                    type: "POST",
                    url: $("#Noticetion_CountStatus").val(),
                    dataType: 'json',
                    data: { userId: userid },
                    success: function (data) {
                        var ctrlTotalNoti = $("#totalNoti");
                        if(typeof (ctrlTotalNoti) != "undefined")
                        {
                            ctrlTotalNoti.html(data);
                        }
                    }
                });
                $(codeId).remove();
                $(".dropdown.dropdown-extended.dropdown-notification").addClass("open");
            }
        }
    });
      
}

$(document).ready(function () {
    $('#sample_1 input:first:checkbox').click(function () {
        if ($(this).is(':checked')) {
            $('#sample_1  input:checkbox').each(function () {
                $(this).prop("checked", true);

            });
        } else {
            $('#sample_1 input:checkbox').each(function () {
                $(this).prop("checked", false);
            });
        }
    });
});
function deleteMutil() {
    var listid = "-1";
    var listiid = $("#sample_1 input:checked");
    for (i = 0; i < listiid.length; i++) {
        listid += ";" + $(listiid[i]).attr("iid");
    }
    if (listid == "-1") {
        bootbox.alert($('#msgDeleteAlert').val());
        return false;
    } else {
        bootbox.confirm($('#msgDeleteConfirm').val(), function (result) {
            if (result) {
                $.ajax({
                    type: "POST",
                    url: $("#DKP_Notification_DeleteFromList").val(),
                    dataType: 'json',
                    data: { listId: listid },
                    success: function () {
                        // location.reload();
                        window.location.href = window.location.protocol + '//' + window.location.host + window.location.pathname;
                    }
                });
            }
        });
    }

}

//active phân trang - begin
function PagerClick(value) {
    $("#Page").val(value);
    $("#btn_submitpaging").click();
};
var page = $("#Page").val();
if (page == 0) {
    $(".pagination li").eq(0).addClass("active");
}
else if (page > 0) {
    $(".pagination li a").each(function (index) {
        if ($(this).text() == page) {
            $(this).parent(".pagination li").addClass("active");
        }
    });
}
// active phân trang - end