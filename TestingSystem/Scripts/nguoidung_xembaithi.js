// Check xem làm bài thi xong không được quay lại - nút back của trình duyệt
// Phải kiểm tra điều kiện - tránh xung đột với nút back, khi vào cá nhân xem bài thi.

if ($("#checkTestSuccess").length > 0) {
    var checkTest = $("#checkTestSuccess").val();
    if (parseInt(checkTest) == 1) {
        history.pushState(null, null);
        window.addEventListener('popstate', function (event) {
            history.pushState(null, null);
        });
    }
}


function clickViewDetailTest(userTestId) {
    $.ajax({
        type: "Post",
        url: $("#AjaxClickViewDetailTest").val(),
        data: { userTestId: userTestId },
        async: false,
        success: function (data) {
           
        },
        error: function ajaxError(response) {
            //console.log(response.status + ' ' + response.statusText);
        }
    });
}